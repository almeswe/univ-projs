#ifndef _74HC595_UTILS_H_
#define _74HC595_UTILS_H_

#include "main.h"
#include <stdio.h>

#define DS     GPIO_PIN_9
#define SH_CP  GPIO_PIN_8
#define ST_CP  GPIO_PIN_5

#define DS_Port     GPIOA
#define SH_CP_Port  GPIOA
#define ST_CP_Port  GPIOB

void HAL_Reg_Init();
void HAL_Reg_SetDigit(uint8_t order, uint8_t digit);

#endif