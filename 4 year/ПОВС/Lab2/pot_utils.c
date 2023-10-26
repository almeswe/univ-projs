#include "pot_utils.h"

uint16_t HAL_PotValue() {
		HAL_ADC_Start(&hadc1);
    uint16_t pot_value = 0;
    if (HAL_ADC_PollForConversion(&hadc1, 10) == HAL_OK) {
        pot_value = (uint16_t)HAL_ADC_GetValue(&hadc1);
			  HAL_ADC_Stop(&hadc1);
    }
    return pot_value;
}