#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_START(void);
void app_read_REG_LOAD_CELLS(void);
void app_read_REG_DI0(void);
void app_read_REG_DO0(void);
void app_read_REG_THRESHOLDS(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_RESERVED1(void);
void app_read_REG_DI0_CONF(void);
void app_read_REG_DO0_CONF(void);
void app_read_REG_DO0_PULSE(void);
void app_read_REG_DO_SET(void);
void app_read_REG_DO_CLEAR(void);
void app_read_REG_DO_TOGGLE(void);
void app_read_REG_DO_OUT(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_OFFSET_CH0(void);
void app_read_REG_OFFSET_CH1(void);
void app_read_REG_OFFSET_CH2(void);
void app_read_REG_OFFSET_CH3(void);
void app_read_REG_OFFSET_CH4(void);
void app_read_REG_OFFSET_CH5(void);
void app_read_REG_OFFSET_CH6(void);
void app_read_REG_OFFSET_CH7(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_DOS_TH_INV(void);
void app_read_REG_DO0_CH(void);
void app_read_REG_DO1_CH(void);
void app_read_REG_DO2_CH(void);
void app_read_REG_DO3_CH(void);
void app_read_REG_DO4_CH(void);
void app_read_REG_DO5_CH(void);
void app_read_REG_DO6_CH(void);
void app_read_REG_DO7_CH(void);
void app_read_REG_DO0_TH_VALUE(void);
void app_read_REG_DO1_TH_VALUE(void);
void app_read_REG_DO2_TH_VALUE(void);
void app_read_REG_DO3_TH_VALUE(void);
void app_read_REG_DO4_TH_VALUE(void);
void app_read_REG_DO5_TH_VALUE(void);
void app_read_REG_DO6_TH_VALUE(void);
void app_read_REG_DO7_TH_VALUE(void);
void app_read_REG_DO0_TH_UP_MS(void);
void app_read_REG_DO1_TH_UP_MS(void);
void app_read_REG_DO2_TH_UP_MS(void);
void app_read_REG_DO3_TH_UP_MS(void);
void app_read_REG_DO4_TH_UP_MS(void);
void app_read_REG_DO5_TH_UP_MS(void);
void app_read_REG_DO6_TH_UP_MS(void);
void app_read_REG_DO7_TH_UP_MS(void);
void app_read_REG_DO0_TH_DOWN_MS(void);
void app_read_REG_DO1_TH_DOWN_MS(void);
void app_read_REG_DO2_TH_DOWN_MS(void);
void app_read_REG_DO3_TH_DOWN_MS(void);
void app_read_REG_DO4_TH_DOWN_MS(void);
void app_read_REG_DO5_TH_DOWN_MS(void);
void app_read_REG_DO6_TH_DOWN_MS(void);
void app_read_REG_DO7_TH_DOWN_MS(void);
void app_read_REG_EVNT_ENABLE(void);

bool app_write_REG_START(void *a);
bool app_write_REG_LOAD_CELLS(void *a);
bool app_write_REG_DI0(void *a);
bool app_write_REG_DO0(void *a);
bool app_write_REG_THRESHOLDS(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_RESERVED1(void *a);
bool app_write_REG_DI0_CONF(void *a);
bool app_write_REG_DO0_CONF(void *a);
bool app_write_REG_DO0_PULSE(void *a);
bool app_write_REG_DO_SET(void *a);
bool app_write_REG_DO_CLEAR(void *a);
bool app_write_REG_DO_TOGGLE(void *a);
bool app_write_REG_DO_OUT(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_OFFSET_CH0(void *a);
bool app_write_REG_OFFSET_CH1(void *a);
bool app_write_REG_OFFSET_CH2(void *a);
bool app_write_REG_OFFSET_CH3(void *a);
bool app_write_REG_OFFSET_CH4(void *a);
bool app_write_REG_OFFSET_CH5(void *a);
bool app_write_REG_OFFSET_CH6(void *a);
bool app_write_REG_OFFSET_CH7(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_DOS_TH_INV(void *a);
bool app_write_REG_DO0_CH(void *a);
bool app_write_REG_DO1_CH(void *a);
bool app_write_REG_DO2_CH(void *a);
bool app_write_REG_DO3_CH(void *a);
bool app_write_REG_DO4_CH(void *a);
bool app_write_REG_DO5_CH(void *a);
bool app_write_REG_DO6_CH(void *a);
bool app_write_REG_DO7_CH(void *a);
bool app_write_REG_DO0_TH_VALUE(void *a);
bool app_write_REG_DO1_TH_VALUE(void *a);
bool app_write_REG_DO2_TH_VALUE(void *a);
bool app_write_REG_DO3_TH_VALUE(void *a);
bool app_write_REG_DO4_TH_VALUE(void *a);
bool app_write_REG_DO5_TH_VALUE(void *a);
bool app_write_REG_DO6_TH_VALUE(void *a);
bool app_write_REG_DO7_TH_VALUE(void *a);
bool app_write_REG_DO0_TH_UP_MS(void *a);
bool app_write_REG_DO1_TH_UP_MS(void *a);
bool app_write_REG_DO2_TH_UP_MS(void *a);
bool app_write_REG_DO3_TH_UP_MS(void *a);
bool app_write_REG_DO4_TH_UP_MS(void *a);
bool app_write_REG_DO5_TH_UP_MS(void *a);
bool app_write_REG_DO6_TH_UP_MS(void *a);
bool app_write_REG_DO7_TH_UP_MS(void *a);
bool app_write_REG_DO0_TH_DOWN_MS(void *a);
bool app_write_REG_DO1_TH_DOWN_MS(void *a);
bool app_write_REG_DO2_TH_DOWN_MS(void *a);
bool app_write_REG_DO3_TH_DOWN_MS(void *a);
bool app_write_REG_DO4_TH_DOWN_MS(void *a);
bool app_write_REG_DO5_TH_DOWN_MS(void *a);
bool app_write_REG_DO6_TH_DOWN_MS(void *a);
bool app_write_REG_DO7_TH_DOWN_MS(void *a);
bool app_write_REG_EVNT_ENABLE(void *a);


#endif /* _APP_FUNCTIONS_H_ */