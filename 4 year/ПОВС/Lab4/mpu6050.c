#include "mpu6050.h"

#define REG_WHO_AM_I_VALUE 0x68

static MPU6050_Error glob_error = ERROR_OK;
static HAL_StatusTypeDef glob_status = HAL_OK;

static MPU6050_Sample raw0 = {0};

static uint32_t dt0 = 0;

static MPU6050_Error MPU6050_Write8_Reg(uint8_t data, MPU6050_Reg reg) {
	glob_status = HAL_I2C_Mem_Write(
		&hi2c1, 
		MPU6050_I2C_ADDR, 
		(uint16_t)reg, 
		sizeof(uint8_t),
		&data, 
		sizeof(uint8_t),
		MPU6050_I2C_TOUT
	);
	glob_error = glob_status != HAL_OK ?
		ERROR_REG_WRITE : ERROR_OK;
	return glob_error;
}

static MPU6050_Error MPU6050_Read8_Reg(uint8_t* value, MPU6050_Reg reg) {
	glob_status = HAL_I2C_Mem_Read(
		&hi2c1,
		MPU6050_I2C_ADDR,
		(uint16_t)reg,
		sizeof(uint8_t),
		value,
		sizeof(uint8_t),
		MPU6050_I2C_TOUT
	);
	glob_error = glob_status != HAL_OK ?
		ERROR_REG_WRITE : ERROR_OK;
	return glob_error;
}

static MPU6050_Error MPU6050_Yield_Raw_Gyro(MPU6050_Sample* raw) {
	uint8_t yield[6] = {0};
	HAL_StatusTypeDef error = HAL_I2C_Mem_Read(
		&hi2c1,
		MPU6050_I2C_ADDR,
		REG_GYRO_XOUT_H,
	  I2C_MEMADD_SIZE_8BIT,
		yield,
		sizeof(yield),
		MPU6050_I2C_TOUT
	);
	raw->x = (int16_t)(yield[0] << 8 | yield[1]);
	raw->y = (int16_t)(yield[2] << 8 | yield[3]);
	raw->z = (int16_t)(yield[4] << 8 | yield[5]);
	return error == HAL_OK ? ERROR_OK : ERROR_REG_READ;
}

static MPU6050_Error MPU6050_Yield_Raw_Accel(MPU6050_Sample* raw) {
	uint8_t yield[6] = {0};
	HAL_StatusTypeDef error = HAL_I2C_Mem_Read(
		&hi2c1,
		MPU6050_I2C_ADDR,
		REG_ACCEL_XOUT_H,
	  I2C_MEMADD_SIZE_8BIT,
		yield,
		sizeof(yield),
		MPU6050_I2C_TOUT
	);
	raw->x = (int16_t)(yield[0] << 8 | yield[1]);
	raw->y = (int16_t)(yield[2] << 8 | yield[3]);
	raw->z = (int16_t)(yield[4] << 8 | yield[5]);
	return error == HAL_OK ? ERROR_OK : ERROR_REG_READ;
}

static void MPU6050_Calibrate(int32_t rate) {
	MPU6050_Sample raw = {0};
	int32_t raw0_large[3] = {0};
	for (int32_t i = 0; i < rate; i++) {
		MPU6050_Yield_Raw_Gyro(&raw);
		raw0_large[0] += raw.x;
		raw0_large[1] += raw.y;
		raw0_large[2] += raw.z;
		HAL_Delay(2);
	}
	raw0.x = (int16_t)(raw0_large[0] / rate);
	raw0.y = (int16_t)(raw0_large[1] / rate);
	raw0.z = (int16_t)(raw0_large[2] / rate);
	dt0 = HAL_GetTick();
}

MPU6050_Error MPU6050_Init() {
	uint8_t value8 = 0;
	dt0 = HAL_GetTick();
	MPU6050_Error error = ERROR_OK;
	// This register is used to verify the identity of the device.
	MPU6050_Read8_Reg(&value8, REG_WHO_AM_I);
	HAL_Delay(50);
	if (glob_error != ERROR_OK) {
		return glob_error;
	}
	if (value8 != REG_WHO_AM_I_VALUE) {
		return ERROR_INIT;
	}
	// This register allows the user to configure the power mode and clock source. It also provides a bit for 
  // resetting the entire device, and a bit for disabling the temperature sensor.
	// DEVICE_RESET When set to 1, this bit resets all internal registers to their default values. 
  // The bit automatically clears to 0 once the reset is done.
	// TEMP_DIS When set to 1, this bit disables the temperature sensor
	//value8 = 0b10000000;
	//error |= MPU6050_Write8_Reg(value8, REG_PWR_MGMT_1);
	value8 = 0b00001000;
	error |= MPU6050_Write8_Reg(value8, REG_PWR_MGMT_1);
	// Sample Rate = Gyroscope Output Rate / (1 + SMPLRT_DIV)
	// The accelerometer output rate is 1kHz. This means that for a Sample Rate greater than 1kHz, 
  // the same accelerometer sample may be output to the FIFO, DMP, and sensor registers more than once.
	value8 = 0x7;
	error |= MPU6050_Write8_Reg(value8, REG_SMPRT_DIV);
	// This register is used to trigger gyroscope self-test and configure the gyroscopes’ full scale range.
	value8 = 0b00000000;
	error |= MPU6050_Write8_Reg(value8, REG_GYRO_CONFIG);
	// This register is used to trigger accelerometer self test and configure the accelerometer full scale range
	value8 = 0b00000000;
	error |= MPU6050_Write8_Reg(value8, REG_ACCEL_CONFIG);
	MPU6050_Calibrate(1000);
	if (error != ERROR_OK) {
		return ERROR_INIT;
	}
	return ERROR_OK;
}

MPU6050_Error MPU6050_Yield_Gyro(MPU6050_SampleF* s) {
	MPU6050_Sample raw = {0};
	MPU6050_Error error = MPU6050_Yield_Raw_Gyro(&raw);
	s->x = ((float)raw.x - raw0.x) / 131.0f;
	s->y = ((float)raw.y - raw0.y) / 131.0f;
	s->z = ((float)raw.z - raw0.z) / 131.0f;
	return error;
}

MPU6050_Error MPU6050_Yield_Accel(MPU6050_SampleF* s) {
	MPU6050_Sample raw = {0};
	MPU6050_Error error = MPU6050_Yield_Raw_Accel(&raw);
	s->x = raw.x / 16384.0f;
	s->y = raw.y / 16384.0f;
	s->z = raw.z / 16384.0f;
	return error;
}

MPU6050_Error MPU6050_Angle_Gyro(MPU6050_Angle* a) {
	MPU6050_SampleF s = {0};
	uint32_t ct = HAL_GetTick();
	MPU6050_Yield_Gyro(&s);
	uint32_t dt = ct - dt0;
	a->roll += s.x * (dt / 1000.0);
	a->yaw += s.y * (dt / 1000.0);
	a->pitch += s.z * (dt / 1000.0);
	dt0 = ct;
	return ERROR_OK;
}

MPU6050_Error MPU6050_Angle_Accel(MPU6050_Angle* a) {
	MPU6050_SampleF s = {0};
	MPU6050_Yield_Accel(&s);
	a->roll  = 57.0f * atan2f(s.y, s.z);
	a->yaw   = 57.0f * atan2f(s.x, s.z);
	a->pitch = 57.0f * atan2f(s.y, s.x);
	return ERROR_OK;
}

MPU6050_Error MPU6050_Angle_Mixed(MPU6050_Angle* a) {
	const float fk = 0.90;
	static MPU6050_Angle aa = {0};
	static MPU6050_Angle ga = {0};
	MPU6050_Angle_Gyro(&ga);
	MPU6050_Angle_Accel(&aa);
	a->yaw   = fk * ga.yaw +   (1.0f - fk) * aa.yaw;
	a->roll  = fk * ga.roll +  (1.0f - fk) * aa.roll;
	a->pitch = fk * ga.pitch + (1.0f - fk) * aa.pitch;
	return ERROR_OK;
}