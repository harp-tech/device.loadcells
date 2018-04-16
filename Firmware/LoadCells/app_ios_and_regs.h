#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// DI0                    Description: Input DIO
// MISO0                  Description: MISO0
// MISO1                  Description: MISO1

#define read_DI0 read_io(PORTB, 0)              // DI0
#define read_MISO0 read_io(PORTC, 6)            // MISO0
#define read_MISO1 read_io(PORTD, 6)            // MISO1

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// DO0                    Description: Output DO0
// DO1                    Description: Output DO1
// DO2                    Description: Output DO2
// DO3                    Description: Output DO3
// DO4                    Description: Output DO4
// DO5                    Description: Output DO5
// DO6                    Description: Output DO6
// DO7                    Description: Output DO7
// DO8                    Description: Output DO8
// CS0_2                  Description: CS0_2 / CS_POT0
// CS0_1                  Description: CS0_1 / CS_ADC0
// CS0_0                  Description: CS0_0 / CONVST0
// MOSI0                  Description: MOSI0
// SCK0                   Description: SCK0
// CS1_2                  Description: CS1_2 / CS_POT1
// CS1_1                  Description: CS1_1 / CS_ADC1
// CS1_0                  Description: CS1_0 / CONVST1
// MOSI1                  Description: MOSI1
// SCK1                   Description: SCK1

/* DO0 */
#define set_DO0 set_io(PORTB, 1)
#define clr_DO0 clear_io(PORTB, 1)
#define tgl_DO0 toggle_io(PORTB, 1)
#define read_DO0 read_io(PORTB, 1)

/* DO1 */
#define set_DO1 set_io(PORTA, 0)
#define clr_DO1 clear_io(PORTA, 0)
#define tgl_DO1 toggle_io(PORTA, 0)
#define read_DO1 read_io(PORTA, 0)

/* DO2 */
#define set_DO2 set_io(PORTA, 1)
#define clr_DO2 clear_io(PORTA, 1)
#define tgl_DO2 toggle_io(PORTA, 1)
#define read_DO2 read_io(PORTA, 1)

/* DO3 */
#define set_DO3 set_io(PORTA, 2)
#define clr_DO3 clear_io(PORTA, 2)
#define tgl_DO3 toggle_io(PORTA, 2)
#define read_DO3 read_io(PORTA, 2)

/* DO4 */
#define set_DO4 set_io(PORTA, 3)
#define clr_DO4 clear_io(PORTA, 3)
#define tgl_DO4 toggle_io(PORTA, 3)
#define read_DO4 read_io(PORTA, 3)

/* DO5 */
#define set_DO5 set_io(PORTA, 4)
#define clr_DO5 clear_io(PORTA, 4)
#define tgl_DO5 toggle_io(PORTA, 4)
#define read_DO5 read_io(PORTA, 4)

/* DO6 */
#define set_DO6 set_io(PORTA, 5)
#define clr_DO6 clear_io(PORTA, 5)
#define tgl_DO6 toggle_io(PORTA, 5)
#define read_DO6 read_io(PORTA, 5)

/* DO7 */
#define set_DO7 set_io(PORTA, 6)
#define clr_DO7 clear_io(PORTA, 6)
#define tgl_DO7 toggle_io(PORTA, 6)
#define read_DO7 read_io(PORTA, 6)

/* DO8 */
#define set_DO8 set_io(PORTA, 7)
#define clr_DO8 clear_io(PORTA, 7)
#define tgl_DO8 toggle_io(PORTA, 7)
#define read_DO8 read_io(PORTA, 7)

/* CS0_2 */
#define set_CS0_2 set_io(PORTC, 1)
#define clr_CS0_2 clear_io(PORTC, 1)
#define tgl_CS0_2 toggle_io(PORTC, 1)
#define read_CS0_2 read_io(PORTC, 1)

/* CS0_1 */
#define set_CS0_1 set_io(PORTC, 0)
#define clr_CS0_1 clear_io(PORTC, 0)
#define tgl_CS0_1 toggle_io(PORTC, 0)
#define read_CS0_1 read_io(PORTC, 0)

/* CS0_0 */
#define set_CS0_0 set_io(PORTC, 4)
#define clr_CS0_0 clear_io(PORTC, 4)
#define tgl_CS0_0 toggle_io(PORTC, 4)
#define read_CS0_0 read_io(PORTC, 4)

/* MOSI0 */
#define set_MOSI0 set_io(PORTC, 5)
#define clr_MOSI0 clear_io(PORTC, 5)
#define tgl_MOSI0 toggle_io(PORTC, 5)
#define read_MOSI0 read_io(PORTC, 5)

/* SCK0 */
#define set_SCK0 set_io(PORTC, 7)
#define clr_SCK0 clear_io(PORTC, 7)
#define tgl_SCK0 toggle_io(PORTC, 7)
#define read_SCK0 read_io(PORTC, 7)

/* CS1_2 */
#define set_CS1_2 set_io(PORTD, 1)
#define clr_CS1_2 clear_io(PORTD, 1)
#define tgl_CS1_2 toggle_io(PORTD, 1)
#define read_CS1_2 read_io(PORTD, 1)

/* CS1_1 */
#define set_CS1_1 set_io(PORTD, 0)
#define clr_CS1_1 clear_io(PORTD, 0)
#define tgl_CS1_1 toggle_io(PORTD, 0)
#define read_CS1_1 read_io(PORTD, 0)

/* CS1_0 */
#define set_CS1_0 set_io(PORTD, 4)
#define clr_CS1_0 clear_io(PORTD, 4)
#define tgl_CS1_0 toggle_io(PORTD, 4)
#define read_CS1_0 read_io(PORTD, 4)

/* MOSI1 */
#define set_MOSI1 set_io(PORTD, 5)
#define clr_MOSI1 clear_io(PORTD, 5)
#define tgl_MOSI1 toggle_io(PORTD, 5)
#define read_MOSI1 read_io(PORTD, 5)

/* SCK1 */
#define set_SCK1 set_io(PORTD, 7)
#define clr_SCK1 clear_io(PORTD, 7)
#define tgl_SCK1 toggle_io(PORTD, 7)
#define read_SCK1 read_io(PORTD, 7)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_START;
	int16_t REG_LOAD_CELLS[8];
	uint8_t REG_DI0;
	uint8_t REG_DO0;
	uint8_t REG_THRESHOLDS;
	uint8_t REG_RESERVED0;
	uint8_t REG_RESERVED1;
	uint8_t REG_DI0_CONF;
	uint8_t REG_DO0_CONF;
	uint8_t REG_DO0_PULSE;
	uint16_t REG_DO_SET;
	uint16_t REG_DO_CLEAR;
	uint16_t REG_DO_TOGGLE;
	uint16_t REG_DO_OUT;
	uint8_t REG_RESERVED2;
	uint8_t REG_RESERVED3;
	int16_t REG_OFFSET_CH0;
	int16_t REG_OFFSET_CH1;
	int16_t REG_OFFSET_CH2;
	int16_t REG_OFFSET_CH3;
	int16_t REG_OFFSET_CH4;
	int16_t REG_OFFSET_CH5;
	int16_t REG_OFFSET_CH6;
	int16_t REG_OFFSET_CH7;
	uint8_t REG_RESERVED4;
	uint8_t REG_RESERVED5;
	uint8_t REG_DO0_CH;
	uint8_t REG_DO1_CH;
	uint8_t REG_DO2_CH;
	uint8_t REG_DO3_CH;
	uint8_t REG_DO4_CH;
	uint8_t REG_DO5_CH;
	uint8_t REG_DO6_CH;
	uint8_t REG_DO7_CH;
	int16_t REG_DO0_TH_VALUE;
	int16_t REG_DO1_TH_VALUE;
	int16_t REG_DO2_TH_VALUE;
	int16_t REG_DO3_TH_VALUE;
	int16_t REG_DO4_TH_VALUE;
	int16_t REG_DO5_TH_VALUE;
	int16_t REG_DO6_TH_VALUE;
	int16_t REG_DO7_TH_VALUE;
	uint16_t REG_DO0_TH_UP_MS;
	uint16_t REG_DO1_TH_UP_MS;
	uint16_t REG_DO2_TH_UP_MS;
	uint16_t REG_DO3_TH_UP_MS;
	uint16_t REG_DO4_TH_UP_MS;
	uint16_t REG_DO5_TH_UP_MS;
	uint16_t REG_DO6_TH_UP_MS;
	uint16_t REG_DO7_TH_UP_MS;
	uint16_t REG_DO0_TH_DOWN_MS;
	uint16_t REG_DO1_TH_DOWN_MS;
	uint16_t REG_DO2_TH_DOWN_MS;
	uint16_t REG_DO3_TH_DOWN_MS;
	uint16_t REG_DO4_TH_DOWN_MS;
	uint16_t REG_DO5_TH_DOWN_MS;
	uint16_t REG_DO6_TH_DOWN_MS;
	uint16_t REG_DO7_TH_DOWN_MS;
	uint8_t REG_EVNT_ENABLE;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_START                       32 // U8     Writing any value above ZERO will start the acquisition. ZERO will stop it.
#define ADD_REG_LOAD_CELLS                  33 // I16    Analog value of the Load Cells
#define ADD_REG_DI0                         34 // U8     State of digital input 0 (DI0)
#define ADD_REG_DO0                         35 // U8     State of digital output 0 (DO0)
#define ADD_REG_THRESHOLDS                  36 // U8     State of the filter thresholds -- This regiter is not used!!!
#define ADD_REG_RESERVED0                   37 // U8     Reserved for future purposes
#define ADD_REG_RESERVED1                   38 // U8     Reserved for future purposes
#define ADD_REG_DI0_CONF                    39 // U8     Configuration of the digital input 0 (DI0)
#define ADD_REG_DO0_CONF                    40 // U8     Configuration of the digital output 0 (DO0)
#define ADD_REG_DO0_PULSE                   41 // U8     Pulse for the digital output 0 (DO0) [1:255]
#define ADD_REG_DO_SET                      42 // U16    Set the digital outputs
#define ADD_REG_DO_CLEAR                    43 // U16    Clear the digital outputs
#define ADD_REG_DO_TOGGLE                   44 // U16    Toggle the digital outputs
#define ADD_REG_DO_OUT                      45 // U16    Writes to the digital output
#define ADD_REG_RESERVED2                   46 // U8     Reserved for future purposes
#define ADD_REG_RESERVED3                   47 // U8     Reserved for future purposes
#define ADD_REG_OFFSET_CH0                  48 // I16    Offset value of Load Cell channel 0 [-255:255]
#define ADD_REG_OFFSET_CH1                  49 // I16    Offset value of Load Cell channel 1 [-255:255]
#define ADD_REG_OFFSET_CH2                  50 // I16    Offset value of Load Cell channel 2 [-255:255]
#define ADD_REG_OFFSET_CH3                  51 // I16    Offset value of Load Cell channel 3 [-255:255]
#define ADD_REG_OFFSET_CH4                  52 // I16    Offset value of Load Cell channel 4 [-255:255]
#define ADD_REG_OFFSET_CH5                  53 // I16    Offset value of Load Cell channel 5 [-255:255]
#define ADD_REG_OFFSET_CH6                  54 // I16    Offset value of Load Cell channel 6 [-255:255]
#define ADD_REG_OFFSET_CH7                  55 // I16    Offset value of Load Cell channel 7 [-255:255]
#define ADD_REG_RESERVED4                   56 // U8     Reserved for future purposes
#define ADD_REG_RESERVED5                   57 // U8     Reserved for future purposes
#define ADD_REG_DO0_CH                      58 // U8     Load Cell channel to be used to feed the threshold filter
#define ADD_REG_DO1_CH                      59 // U8     
#define ADD_REG_DO2_CH                      60 // U8     
#define ADD_REG_DO3_CH                      61 // U8     
#define ADD_REG_DO4_CH                      62 // U8     
#define ADD_REG_DO5_CH                      63 // U8     
#define ADD_REG_DO6_CH                      64 // U8     
#define ADD_REG_DO7_CH                      65 // U8     
#define ADD_REG_DO0_TH_VALUE                66 // I16    Value to be compared
#define ADD_REG_DO1_TH_VALUE                67 // I16    
#define ADD_REG_DO2_TH_VALUE                68 // I16    
#define ADD_REG_DO3_TH_VALUE                69 // I16    
#define ADD_REG_DO4_TH_VALUE                70 // I16    
#define ADD_REG_DO5_TH_VALUE                71 // I16    
#define ADD_REG_DO6_TH_VALUE                72 // I16    
#define ADD_REG_DO7_TH_VALUE                73 // I16    
#define ADD_REG_DO0_TH_UP_MS                74 // U16    Time (ms) above the configured threshold to set the digital output
#define ADD_REG_DO1_TH_UP_MS                75 // U16    
#define ADD_REG_DO2_TH_UP_MS                76 // U16    
#define ADD_REG_DO3_TH_UP_MS                77 // U16    
#define ADD_REG_DO4_TH_UP_MS                78 // U16    
#define ADD_REG_DO5_TH_UP_MS                79 // U16    
#define ADD_REG_DO6_TH_UP_MS                80 // U16    
#define ADD_REG_DO7_TH_UP_MS                81 // U16    
#define ADD_REG_DO0_TH_DOWN_MS              82 // U16    Time (ms) below the configured threshold to set the digital output
#define ADD_REG_DO1_TH_DOWN_MS              83 // U16    
#define ADD_REG_DO2_TH_DOWN_MS              84 // U16    
#define ADD_REG_DO3_TH_DOWN_MS              85 // U16    
#define ADD_REG_DO4_TH_DOWN_MS              86 // U16    
#define ADD_REG_DO5_TH_DOWN_MS              87 // U16    
#define ADD_REG_DO6_TH_DOWN_MS              88 // U16    
#define ADD_REG_DO7_TH_DOWN_MS              89 // U16    
#define ADD_REG_EVNT_ENABLE                 90 // U8     Enable the Events

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x5A
#define APP_NBYTES_OF_REG_BANK              110

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_START                            (1<<0)       // 
#define B_DI0                              (1<<0)       // 
#define B_DO0                              (1<<0)       // 
#define MSK_DI0_SEL                        (3<<0)       // 
#define GM_DI0_SYNC                        (0<<0)       // Use as a pure digital input
#define GM_DI0_RISE_START_ACQ              (1<<0)       // Start acquisition when rising edge and stop when falling edge
#define GM_DI0_FALL_START_ACQ              (2<<0)       // Start acquisition when falling edge and stop when rising edge
#define MSK_DO0_SEL                        (3<<0)       // 
#define GM_DO0_DIG                         (0<<0)       // Use as a pure digital output
#define GM_DO0_TGL_EACH_SEC                (1<<0)       // Toogle each second when acquiring
#define GM_DO0_PULSE                       (2<<0)       // The digital output will be ONE during period specified by register DO0_PULSE
#define MSK_DO_CH                          (16<<0)      // 
#define GM_CH0                             (0<<0)       // Load Cell channel 0
#define GM_CH1                             (1<<0)       // Load Cell channel 1
#define GM_CH2                             (2<<0)       // Load Cell channel 2
#define GM_CH3                             (3<<0)       // Load Cell channel 3
#define GM_CH4                             (4<<0)       // Load Cell channel 4
#define GM_CH5                             (5<<0)       // Load Cell channel 5
#define GM_CH6                             (6<<0)       // Load Cell channel 6
#define GM_CH7                             (7<<0)       // Load Cell channel 7
#define GM_SOFTWARE                        (8<<0)       // Filter not used -- Use pin as a pure digital output
#define B_EVT_LOAD_CELLS                   (1<<0)       // Event of register LOAD_CELLS
#define B_EVT_DI0                          (1<<1)       // Event of register DI0
#define B_EVT_DO0                          (1<<2)       // Event of register DO0
#define B_EVT_DO_OUT                       (1<<3)       // Event of THRESHOLDS

#endif /* _APP_REGS_H_ */