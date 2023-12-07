#ifndef _MPU_6050_H
#define _MPU_6050_H

#include "i2c.h"
#include <stdint.h>
#include <math.h>

#define MPU6050_I2C_ADDR ((uint16_t)(0x68 << 1))
#define MPU6050_I2C_TOUT (10)

extern I2C_HandleTypeDef hi2c1;

typedef enum _MPU6050_Reg {
	REG_SMPRT_DIV     = 0x19,
	REG_CONFIG        = 0x1A,
	REG_GYRO_CONFIG   = 0x1B,
	REG_ACCEL_CONFIG  = 0x1C,
	REG_ACCEL_XOUT_H  = 0x3B,
	REG_ACCEL_XOUT_L  = 0x3C,
	REG_ACCEL_YOUT_H  = 0x3D,
	REG_ACCEL_YOUT_L  = 0x3E,
	REG_ACCEL_ZOUT_H  = 0x3F,
	REG_ACCEL_ZOUT_L  = 0x40,
	REG_GYRO_XOUT_H   = 0x43,
	REG_GYRO_XOUT_L   = 0x44,
	REG_GYRO_YOUT_H   = 0x45,
	REG_GYRO_YOUT_L   = 0x46,
	REG_GYRO_ZOUT_H   = 0x47,
	REG_GYRO_ZOUT_L   = 0x48,
	REG_PWR_MGMT_1    = 0x6B,
	REG_PWR_MGMT_2    = 0x6C,	
	REG_WHO_AM_I      = 0x75,
} MPU6050_Reg;

typedef enum _MPU6050_Error {
	ERROR_OK,
	ERROR_INIT,
	ERROR_REG_READ,
	ERROR_REG_WRITE
} MPU6050_Error;

typedef struct _MPU6050_Sample {
	int16_t x, y, z;
} MPU6050_Sample;

typedef struct _MPU6050_SampleF {
	float x, y, z;
} MPU6050_SampleF;

typedef struct _MPU6050_Angle {
	float yaw, pitch, roll;
} MPU6050_Angle;

MPU6050_Error MPU6050_Init();
MPU6050_Error MPU6050_Yield_Gyro(MPU6050_SampleF* s);
MPU6050_Error MPU6050_Yield_Accel(MPU6050_SampleF* s);

MPU6050_Error MPU6050_Angle_Gyro(MPU6050_Angle* a);
MPU6050_Error MPU6050_Angle_Accel(MPU6050_Angle* a);
MPU6050_Error MPU6050_Angle_Mixed(MPU6050_Angle* a);

#endif