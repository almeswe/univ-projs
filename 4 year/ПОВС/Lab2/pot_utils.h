#ifndef _POTENTIOMETER_UTILS_H_
#define _POTENTIOMETER_UTILS_H_

#include "main.h"

extern ADC_HandleTypeDef hadc1;

#define ADC_REG_CAP 12
#define ADC_MAX_VAL ((1 << ADC_REG_CAP) - 1)

#define HAL_PotPercent(x) ((uint8_t)(HAL_PotValue() / (float)ADC_MAX_VAL * 100))
uint16_t HAL_PotValue();

#endif