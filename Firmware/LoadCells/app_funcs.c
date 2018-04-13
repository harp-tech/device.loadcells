#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

extern void update_offsets (uint8_t load_cell_channel);

extern uint16_t pulse_counter_ms;

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_START,
	&app_read_REG_LOAD_CELLS,
	&app_read_REG_DI0,
	&app_read_REG_DO0,
	&app_read_REG_THRESHOLDS,
	&app_read_REG_RESERVED0,
	&app_read_REG_RESERVED1,
	&app_read_REG_DI0_CONF,
	&app_read_REG_DO0_CONF,
	&app_read_REG_DO0_PULSE,
	&app_read_REG_DO_SET,
	&app_read_REG_DO_CLEAR,
	&app_read_REG_DO_TOGGLE,
	&app_read_REG_DO_OUT,
	&app_read_REG_RESERVED2,
	&app_read_REG_RESERVED3,
	&app_read_REG_OFFSET_CH0,
	&app_read_REG_OFFSET_CH1,
	&app_read_REG_OFFSET_CH2,
	&app_read_REG_OFFSET_CH3,
	&app_read_REG_OFFSET_CH4,
	&app_read_REG_OFFSET_CH5,
	&app_read_REG_OFFSET_CH6,
	&app_read_REG_OFFSET_CH7,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_DO0_CH,
	&app_read_REG_DO1_CH,
	&app_read_REG_DO2_CH,
	&app_read_REG_DO3_CH,
	&app_read_REG_DO4_CH,
	&app_read_REG_DO5_CH,
	&app_read_REG_DO6_CH,
	&app_read_REG_DO7_CH,
	&app_read_REG_DO0_TH_VALUE,
	&app_read_REG_DO1_TH_VALUE,
	&app_read_REG_DO2_TH_VALUE,
	&app_read_REG_DO3_TH_VALUE,
	&app_read_REG_DO4_TH_VALUE,
	&app_read_REG_DO5_TH_VALUE,
	&app_read_REG_DO6_TH_VALUE,
	&app_read_REG_DO7_TH_VALUE,
	&app_read_REG_DO0_TH_UP_MS,
	&app_read_REG_DO1_TH_UP_MS,
	&app_read_REG_DO2_TH_UP_MS,
	&app_read_REG_DO3_TH_UP_MS,
	&app_read_REG_DO4_TH_UP_MS,
	&app_read_REG_DO5_TH_UP_MS,
	&app_read_REG_DO6_TH_UP_MS,
	&app_read_REG_DO7_TH_UP_MS,
	&app_read_REG_DO0_TH_DOWN_MS,
	&app_read_REG_DO1_TH_DOWN_MS,
	&app_read_REG_DO2_TH_DOWN_MS,
	&app_read_REG_DO3_TH_DOWN_MS,
	&app_read_REG_DO4_TH_DOWN_MS,
	&app_read_REG_DO5_TH_DOWN_MS,
	&app_read_REG_DO6_TH_DOWN_MS,
	&app_read_REG_DO7_TH_DOWN_MS,
	&app_read_REG_EVNT_ENABLE
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_START,
	&app_write_REG_LOAD_CELLS,
	&app_write_REG_DI0,
	&app_write_REG_DO0,
	&app_write_REG_THRESHOLDS,
	&app_write_REG_RESERVED0,
	&app_write_REG_RESERVED1,
	&app_write_REG_DI0_CONF,
	&app_write_REG_DO0_CONF,
	&app_write_REG_DO0_PULSE,
	&app_write_REG_DO_SET,
	&app_write_REG_DO_CLEAR,
	&app_write_REG_DO_TOGGLE,
	&app_write_REG_DO_OUT,
	&app_write_REG_RESERVED2,
	&app_write_REG_RESERVED3,
	&app_write_REG_OFFSET_CH0,
	&app_write_REG_OFFSET_CH1,
	&app_write_REG_OFFSET_CH2,
	&app_write_REG_OFFSET_CH3,
	&app_write_REG_OFFSET_CH4,
	&app_write_REG_OFFSET_CH5,
	&app_write_REG_OFFSET_CH6,
	&app_write_REG_OFFSET_CH7,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_DO0_CH,
	&app_write_REG_DO1_CH,
	&app_write_REG_DO2_CH,
	&app_write_REG_DO3_CH,
	&app_write_REG_DO4_CH,
	&app_write_REG_DO5_CH,
	&app_write_REG_DO6_CH,
	&app_write_REG_DO7_CH,
	&app_write_REG_DO0_TH_VALUE,
	&app_write_REG_DO1_TH_VALUE,
	&app_write_REG_DO2_TH_VALUE,
	&app_write_REG_DO3_TH_VALUE,
	&app_write_REG_DO4_TH_VALUE,
	&app_write_REG_DO5_TH_VALUE,
	&app_write_REG_DO6_TH_VALUE,
	&app_write_REG_DO7_TH_VALUE,
	&app_write_REG_DO0_TH_UP_MS,
	&app_write_REG_DO1_TH_UP_MS,
	&app_write_REG_DO2_TH_UP_MS,
	&app_write_REG_DO3_TH_UP_MS,
	&app_write_REG_DO4_TH_UP_MS,
	&app_write_REG_DO5_TH_UP_MS,
	&app_write_REG_DO6_TH_UP_MS,
	&app_write_REG_DO7_TH_UP_MS,
	&app_write_REG_DO0_TH_DOWN_MS,
	&app_write_REG_DO1_TH_DOWN_MS,
	&app_write_REG_DO2_TH_DOWN_MS,
	&app_write_REG_DO3_TH_DOWN_MS,
	&app_write_REG_DO4_TH_DOWN_MS,
	&app_write_REG_DO5_TH_DOWN_MS,
	&app_write_REG_DO6_TH_DOWN_MS,
	&app_write_REG_DO7_TH_DOWN_MS,
	&app_write_REG_EVNT_ENABLE
};


/************************************************************************/
/* REG_START                                                            */
/************************************************************************/
void app_read_REG_START(void) {}
bool app_write_REG_START(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~B_START)
      return false;
      
   app_regs.REG_START = reg;
	return true;
}


/************************************************************************/
/* REG_LOAD_CELLS                                                       */
/************************************************************************/
// This register is an array with 8 positions
void app_read_REG_LOAD_CELLS(void) {}
bool app_write_REG_LOAD_CELLS(void *a) { return false; }


/************************************************************************/
/* REG_DI0                                                              */
/************************************************************************/
void app_read_REG_DI0(void) { app_regs.REG_DI0 = read_DI0 ? B_DI0 : 0; }
bool app_write_REG_DI0(void *a) { return false; }

/************************************************************************/
/* REG_DO0                                                              */
/************************************************************************/
void app_read_REG_DO0(void)
{
   app_regs.REG_DO0 = app_regs.REG_DO_OUT & (1<<8) ? B_DO0 : 0;
}

bool app_write_REG_DO0(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   
   if (reg & 1)
   {
      PORTB_OUTSET = (1<<1);
      app_regs.REG_DO_OUT |= (1<<8);
      pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;
   }
   else
   {
      PORTB_OUTCLR = (1<<1);
      app_regs.REG_DO_OUT &= ~(1<<8);
   }
   
   app_regs.REG_DO0 = reg;
   return false;
}


/************************************************************************/
/* REG_THRESHOLDS                                                       */
/************************************************************************/
void app_read_REG_THRESHOLDS(void)
{
	//app_regs.REG_THRESHOLDS = 0;

}

bool app_write_REG_THRESHOLDS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_THRESHOLDS = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void) {}
bool app_write_REG_RESERVED0(void *a)
{
   app_regs.REG_RESERVED0 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void) {}
bool app_write_REG_RESERVED1(void *a)
{
	app_regs.REG_RESERVED1 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_DI0_CONF                                                         */
/************************************************************************/
void app_read_REG_DI0_CONF(void) {}
bool app_write_REG_DI0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & (~MSK_DI0_SEL))
      return false;

	app_regs.REG_DI0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_CONF                                                         */
/************************************************************************/
void app_read_REG_DO0_CONF(void) {}
bool app_write_REG_DO0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & (~MSK_DO0_SEL))
      return false;

	app_regs.REG_DO0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_PULSE                                                        */
/************************************************************************/
void app_read_REG_DO0_PULSE(void) {}
bool app_write_REG_DO0_PULSE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg < 1)
      return false;

	app_regs.REG_DO0_PULSE = reg;
	return true;
}


/************************************************************************/
/* REG_DO_SET                                                           */
/************************************************************************/
void app_read_REG_DO_SET(void) {}
bool app_write_REG_DO_SET(void *a)
{
   uint16_t reg = *((uint16_t*)a);
	PORTA_OUTSET = (uint8_t)(reg >> 1);
   PORTB_OUTSET = reg & (1<<0) ? (1<<1) : 0;
   
   if (reg & (1<<0))
   {
      pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;
   }
   
   app_regs.REG_DO_OUT |= reg;
	return true;
}


/************************************************************************/
/* REG_DO_CLEAR                                                         */
/************************************************************************/
void app_read_REG_DO_CLEAR(void) {}
bool app_write_REG_DO_CLEAR(void *a)
{
   uint16_t reg = *((uint16_t*)a);
   PORTA_OUTCLR = (uint8_t)(reg >> 1);
   PORTB_OUTCLR = reg & (1<<0) ? (1<<1) : 0;
   
   app_regs.REG_DO_OUT &= ~reg;
	return true;
}


/************************************************************************/
/* REG_DO_TOGGLE                                                        */
/************************************************************************/
void app_read_REG_DO_TOGGLE(void) {}
bool app_write_REG_DO_TOGGLE(void *a)
{
   uint16_t reg = *((uint16_t*)a);
   PORTA_OUTTGL = (uint8_t)(reg >> 1);
   PORTB_OUTTGL = reg & (1<<0) ? (1<<1) : 0;
   
   app_regs.REG_DO_OUT = (app_regs.REG_DO_OUT ^ reg) & 0x01FF;
   
   if (app_regs.REG_DO_OUT & (1<<0))
   {
      pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;
   }
      
	return true;
}


/************************************************************************/
/* REG_DO_OUT                                                           */
/************************************************************************/
void app_read_REG_DO_OUT(void) {}
bool app_write_REG_DO_OUT(void *a)
{
   uint16_t reg = *((uint16_t*)a);
   
   PORTA_OUT = (uint8_t)(reg >> 1);
   
   if (reg & (1<<0))
      PORTB_OUTSET = (1<<1);
   else
      PORTB_OUTCLR = (1<<1);
   
   if (reg & (1<<0))
   {
      pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;
   }
   
   app_regs.REG_DO_OUT = reg;   
	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void) {}
bool app_write_REG_RESERVED2(void *a)
{
	app_regs.REG_RESERVED2 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void) {}
bool app_write_REG_RESERVED3(void *a)
{
	app_regs.REG_RESERVED3 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_OFFSET_CH0                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH0(void) {}
bool app_write_REG_OFFSET_CH0(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

	app_regs.REG_OFFSET_CH0 = *((int16_t*)a) * -1;
   update_offsets(0);
	return true;
}


/************************************************************************/
/* REG_OFFSET_CH1                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH1(void) {}
bool app_write_REG_OFFSET_CH1(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH1 = *((int16_t*)a) * -1;
   update_offsets(1);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH2                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH2(void) {}
bool app_write_REG_OFFSET_CH2(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH2 = *((int16_t*)a) * -1;
   update_offsets(2);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH3                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH3(void) {}
bool app_write_REG_OFFSET_CH3(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH3 = *((int16_t*)a) * -1;
   update_offsets(3);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH4                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH4(void) {}
bool app_write_REG_OFFSET_CH4(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH4 = *((int16_t*)a) * -1;
   update_offsets(4);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH5                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH5(void) {}
bool app_write_REG_OFFSET_CH5(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH5 = *((int16_t*)a) * -1;
   update_offsets(5);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH6                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH6(void) {}
bool app_write_REG_OFFSET_CH6(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH6 = *((int16_t*)a) * -1;
   update_offsets(6);
   return true;
}


/************************************************************************/
/* REG_OFFSET_CH6                                                       */
/************************************************************************/
void app_read_REG_OFFSET_CH7(void) {}
bool app_write_REG_OFFSET_CH7(void *a)
{
   if (*((int16_t*)a) < -255 || *((int16_t*)a) > 255)
      return false;

   app_regs.REG_OFFSET_CH7 = *((int16_t*)a) * -1;
   update_offsets(7);
   return true;
}


/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void) {}
bool app_write_REG_RESERVED4(void *a)
{
	app_regs.REG_RESERVED4 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void) {}
bool app_write_REG_RESERVED5(void *a)
{
	app_regs.REG_RESERVED5 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_DO0_CH                                                           */
/************************************************************************/
void app_read_REG_DO0_CH(void) {}
bool app_write_REG_DO0_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO0_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_CH                                                           */
/************************************************************************/
void app_read_REG_DO1_CH(void) {}
bool app_write_REG_DO1_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO1_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_CH                                                           */
/************************************************************************/
void app_read_REG_DO2_CH(void) {}
bool app_write_REG_DO2_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO2_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_CH                                                           */
/************************************************************************/
void app_read_REG_DO3_CH(void) {}
bool app_write_REG_DO3_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO3_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO4_CH                                                           */
/************************************************************************/
void app_read_REG_DO4_CH(void) {}
bool app_write_REG_DO4_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO4_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO5_CH                                                           */
/************************************************************************/
void app_read_REG_DO5_CH(void) {}
bool app_write_REG_DO5_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO5_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO6_CH                                                           */
/************************************************************************/
void app_read_REG_DO6_CH(void) {}
bool app_write_REG_DO6_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO6_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO7_CH                                                           */
/************************************************************************/
void app_read_REG_DO7_CH(void) {}
bool app_write_REG_DO7_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO7_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO0_TH_VALUE(void) {}
bool app_write_REG_DO0_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO0_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO1_TH_VALUE(void) {}
bool app_write_REG_DO1_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO1_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO2_TH_VALUE(void) {}
bool app_write_REG_DO2_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO2_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO3_TH_VALUE(void) {}
bool app_write_REG_DO3_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO3_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO4_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO4_TH_VALUE(void) {}
bool app_write_REG_DO4_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO4_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO5_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO5_TH_VALUE(void) {}
bool app_write_REG_DO5_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO5_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO6_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO6_TH_VALUE(void) {}
bool app_write_REG_DO6_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO6_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO7_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO7_TH_VALUE(void) {}
bool app_write_REG_DO7_TH_VALUE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO7_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO0_TH_UP_MS(void) {}
bool app_write_REG_DO0_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO0_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO1_TH_UP_MS(void) {}
bool app_write_REG_DO1_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO1_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO2_TH_UP_MS(void) {}
bool app_write_REG_DO2_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO2_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO3_TH_UP_MS(void) {}
bool app_write_REG_DO3_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO3_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO4_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO4_TH_UP_MS(void) {}
bool app_write_REG_DO4_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO4_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO5_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO5_TH_UP_MS(void) {}
bool app_write_REG_DO5_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO5_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO6_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO6_TH_UP_MS(void) {}
bool app_write_REG_DO6_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO6_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO7_TH_UP_MS                                                     */
/************************************************************************/
void app_read_REG_DO7_TH_UP_MS(void) {}
bool app_write_REG_DO7_TH_UP_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO7_TH_UP_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO0_TH_DOWN_MS(void) {}
bool app_write_REG_DO0_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO0_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO1_TH_DOWN_MS(void) {}
bool app_write_REG_DO1_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO1_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO2_TH_DOWN_MS(void) {}
bool app_write_REG_DO2_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO2_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO3_TH_DOWN_MS(void) {}
bool app_write_REG_DO3_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO3_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO4_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO4_TH_DOWN_MS(void) {}
bool app_write_REG_DO4_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO4_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO5_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO5_TH_DOWN_MS(void) {}
bool app_write_REG_DO5_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO5_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO6_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO6_TH_DOWN_MS(void) {}
bool app_write_REG_DO6_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO6_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_DO7_TH_DOWN_MS                                                   */
/************************************************************************/
void app_read_REG_DO7_TH_DOWN_MS(void) {}
bool app_write_REG_DO7_TH_DOWN_MS(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO7_TH_DOWN_MS = reg;
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void) {}
bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}