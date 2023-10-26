#include "74hc595_utils.h"
#include <string.h>

/*
	@brief Enables serial data transmitting. (SH_CP -> PA8 pin)
*/
static void HAL_Reg_StartTransmit() {
	HAL_GPIO_WritePin(SH_CP_Port, SH_CP, GPIO_PIN_SET);
}

/*
	@brief Disables serial data transmitting. (SH_CP -> PA8 pin)
*/
static void HAL_Reg_StopTransmit() {
	HAL_GPIO_WritePin(SH_CP_Port, SH_CP, GPIO_PIN_RESET);
}

/*
	@brief Sends single bit to serial port. (DS -> PB5 pin)
*/
static void HAL_Reg_StoreBit(uint8_t bit) {
	HAL_GPIO_WritePin(DS_Port, DS, bit ? GPIO_PIN_SET : GPIO_PIN_RESET);
}

/*
	@brief Performs transmission of data register. (ST_CP -> PA9 pin)
*/
static void HAL_Reg_Sync() {
	HAL_GPIO_WritePin(ST_CP_Port, ST_CP, GPIO_PIN_SET);
	HAL_GPIO_WritePin(ST_CP_Port, ST_CP, GPIO_PIN_RESET);	
}

/*
	@brief Sends single bit in transaction.
*/
static void HAL_Reg_TransmitBit(uint8_t bit) {
	HAL_Reg_StartTransmit();
	HAL_Reg_StoreBit(bit & 0x1);
	HAL_Reg_StopTransmit();
}

/*
	@brief Sends byte (8 bits) in transaction.
*/
extern UART_HandleTypeDef huart2;

static void HAL_Reg_TransmitByte(uint8_t byte) {
	//char cbuf[128];
	//sprintf(cbuf, "\n\r%s\n\r", "----------------");
	//HAL_UART_Transmit(&huart2, (uint8_t*)cbuf, strlen(cbuf), 10);
	//memset(cbuf, 0, sizeof cbuf);
	for (int8_t i = 7; i >= 0; i--) {
		uint8_t bit = (byte >> i) & 0x1;
		//sprintf(cbuf, "%d", bit);
		//HAL_UART_Transmit(&huart2, (uint8_t*)cbuf, strlen(cbuf), 10);
		HAL_Reg_TransmitBit(bit);
	}
}

/*
	@brief Initializes both slave and master registers & pins which will be used.
*/
void HAL_Reg_Init() {
	HAL_GPIO_WritePin(DS_Port, DS, GPIO_PIN_RESET);
	HAL_GPIO_WritePin(SH_CP_Port, SH_CP, GPIO_PIN_RESET);
	HAL_GPIO_WritePin(ST_CP_Port, ST_CP, GPIO_PIN_RESET);
	// For slave initialization
	HAL_Reg_TransmitByte(~0b00000000);
	// For master initialization
	HAL_Reg_TransmitByte(0b00000000);
	HAL_Reg_Sync();
}

void HAL_Reg_SetDigit(uint8_t order, uint8_t digit) {
	static const uint8_t map[] = {
		[0] = 0b01111110, [1] = 0b00001100,
		[2] = 0b10110110, [3] = 0b10011110, 
		[4] = 0b11001100, [5] = 0b11011010,
		[6] = 0b11111010, [7] = 0b00001110,
		[8] = 0b11111110, [9] = 0b11011110
	};
	HAL_Reg_TransmitByte(~map[digit]);
	HAL_Reg_TransmitByte(0x0 | (1 << order));
	HAL_Reg_Sync();
}