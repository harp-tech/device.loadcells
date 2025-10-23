#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTB, 0, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // DI0
	io_pin2in(&PORTC, 6, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // MISO0
	io_pin2in(&PORTD, 6, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // MISO1

	/* Configure input interrupts */
	io_set_int(&PORTB, INT_LEVEL_LOW, 0, (1<<0), false);                 // DI0

	/* Configure output pins */
	io_pin2out(&PORTB, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO0
	io_pin2out(&PORTA, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO1
	io_pin2out(&PORTA, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO2
	io_pin2out(&PORTA, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO3
	io_pin2out(&PORTA, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO4
	io_pin2out(&PORTA, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO5
	io_pin2out(&PORTA, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO6
	io_pin2out(&PORTA, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO7
	io_pin2out(&PORTA, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO8
	io_pin2out(&PORTC, 1, OUT_IO_WIREDAND, IN_EN_IO_EN);                 // CS0_2
	io_pin2out(&PORTC, 0, OUT_IO_WIREDAND, IN_EN_IO_EN);                 // CS0_1
	io_pin2out(&PORTC, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CS0_0
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // MOSI0
	io_pin2out(&PORTC, 7, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // SCK0
	io_pin2out(&PORTD, 1, OUT_IO_WIREDAND, IN_EN_IO_EN);                 // CS1_2
	io_pin2out(&PORTD, 0, OUT_IO_WIREDAND, IN_EN_IO_EN);                 // CS1_1
	io_pin2out(&PORTD, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CS1_0
	io_pin2out(&PORTD, 5, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // MOSI1
	io_pin2out(&PORTD, 7, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // SCK1

	/* Initialize output pins */
	clr_DO0;
	clr_DO1;
	clr_DO2;
	clr_DO3;
	clr_DO4;
	clr_DO5;
	clr_DO6;
	clr_DO7;
	clr_DO8;
	set_CS0_2;
	set_CS0_1;
	set_CS0_0;
	clr_MOSI0;
	clr_SCK0;
	set_CS1_2;
	set_CS1_1;
	set_CS1_0;
	clr_MOSI1;
	clr_SCK1;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	8,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_START),
	(uint8_t*)(app_regs.REG_LOAD_CELLS),
	(uint8_t*)(&app_regs.REG_DI0),
	(uint8_t*)(&app_regs.REG_DO0),
	(uint8_t*)(&app_regs.REG_THRESHOLDS),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_DI0_CONF),
	(uint8_t*)(&app_regs.REG_DO0_CONF),
	(uint8_t*)(&app_regs.REG_DO0_PULSE),
	(uint8_t*)(&app_regs.REG_DO_SET),
	(uint8_t*)(&app_regs.REG_DO_CLEAR),
	(uint8_t*)(&app_regs.REG_DO_TOGGLE),
	(uint8_t*)(&app_regs.REG_DO_OUT),
	(uint8_t*)(&app_regs.REG_RESERVED2),
	(uint8_t*)(&app_regs.REG_RESERVED3),
	(uint8_t*)(&app_regs.REG_OFFSET_CH0),
	(uint8_t*)(&app_regs.REG_OFFSET_CH1),
	(uint8_t*)(&app_regs.REG_OFFSET_CH2),
	(uint8_t*)(&app_regs.REG_OFFSET_CH3),
	(uint8_t*)(&app_regs.REG_OFFSET_CH4),
	(uint8_t*)(&app_regs.REG_OFFSET_CH5),
	(uint8_t*)(&app_regs.REG_OFFSET_CH6),
	(uint8_t*)(&app_regs.REG_OFFSET_CH7),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_DOS_TH_INV),
	(uint8_t*)(&app_regs.REG_DO0_CH),
	(uint8_t*)(&app_regs.REG_DO1_CH),
	(uint8_t*)(&app_regs.REG_DO2_CH),
	(uint8_t*)(&app_regs.REG_DO3_CH),
	(uint8_t*)(&app_regs.REG_DO4_CH),
	(uint8_t*)(&app_regs.REG_DO5_CH),
	(uint8_t*)(&app_regs.REG_DO6_CH),
	(uint8_t*)(&app_regs.REG_DO7_CH),
	(uint8_t*)(&app_regs.REG_DO0_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO1_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO2_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO3_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO4_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO5_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO6_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO7_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO0_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO1_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO2_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO3_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO4_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO5_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO6_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO7_TH_UP_MS),
	(uint8_t*)(&app_regs.REG_DO0_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO1_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO2_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO3_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO4_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO5_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO6_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_DO7_TH_DOWN_MS),
	(uint8_t*)(&app_regs.REG_EVNT_ENABLE)
};