#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "LoadCells";

void hwbp_app_initialize(void)
{
	   /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 0;
    uint8_t fwH = 1;
    uint8_t fwL = 0;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        1232,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	
}

/************************************************************************/
/* General definitions                                                  */
/************************************************************************/
// #define NBYTES 23

/************************************************************************/
/* General used functions                                               */
/************************************************************************/
#define _1_CLOCK_CYCLES asm ( "nop \n")
#define _2_CLOCK_CYCLES _1_CLOCK_CYCLES; _1_CLOCK_CYCLES
#define _4_CLOCK_CYCLES _2_CLOCK_CYCLES; _2_CLOCK_CYCLES
#define _8_CLOCK_CYCLES _4_CLOCK_CYCLES; _4_CLOCK_CYCLES


void AD5204_set_channel(uint8_t channel,
                        uint8_t * data,
                        uint8_t n_of_devices,
                        SPI_t* spi,
                        PORT_t* cs_port,
                        uint8_t cs_pin,
                        PORT_t* spi_port)
{
   uint8_t current_spi_ctrl;
   current_spi_ctrl = spi->CTRL;
   spi->CTRL = 0;
      
   clear_io((*(PORT_t *)cs_port), cs_pin);
   _8_CLOCK_CYCLES;
   
   for (uint8_t n_dev = 0; n_dev < n_of_devices; n_dev++)
   {
      for (uint8_t add = 0; add < 3; add++)
      {
         if ((channel<<add) & 0x04)
            set_io((*(PORT_t *)spi_port), 5);
         else
            clear_io((*(PORT_t *)spi_port), 5);
         
         set_io((*(PORT_t *)spi_port), 7);
         _8_CLOCK_CYCLES;
         
         clear_io((*(PORT_t *)spi_port), 7);
         _8_CLOCK_CYCLES;
      }
      
      for (uint8_t i = 0; i < 8; i++)
      {
         if ((data[n_of_devices-n_dev-1]<<i) & 0x80)
            set_io((*(PORT_t *)spi_port), 5);
         else
            clear_io((*(PORT_t *)spi_port), 5);
         
         set_io((*(PORT_t *)spi_port), 7);
         _8_CLOCK_CYCLES;
         
         clear_io((*(PORT_t *)spi_port), 7);
         _8_CLOCK_CYCLES;
      }
   }
   
   set_io((*(PORT_t *)cs_port), cs_pin);
   
   spi->CTRL = current_spi_ctrl;
}

void update_offsets (uint8_t load_cell_channel)
{  
   if (load_cell_channel == 0 || load_cell_channel == 2)
   {
      uint8_t port0_pot_channel4[2] = {0,0};
      uint8_t port0_pot_channel2[2] = {0,0};
            
      if (app_regs.REG_OFFSET_CH0 >= 0)
         port0_pot_channel4[0] = app_regs.REG_OFFSET_CH0;
      else
         port0_pot_channel2[0] = app_regs.REG_OFFSET_CH0 * -1;
   
      if (app_regs.REG_OFFSET_CH2 >= 0)
         port0_pot_channel4[1] = app_regs.REG_OFFSET_CH2;
      else
         port0_pot_channel2[1] = app_regs.REG_OFFSET_CH2 * -1;
         
      AD5204_set_channel(4-1, port0_pot_channel4, 2, &SPIC, &PORTC, 1, &PORTC);         
      AD5204_set_channel(2-1, port0_pot_channel2, 2, &SPIC, &PORTC, 1, &PORTC);
   }         
   
   if (load_cell_channel == 1 || load_cell_channel == 3)
   {
      uint8_t port0_pot_channel3[2] = {0,0};
      uint8_t port0_pot_channel1[2] = {0,0};
      
      if (app_regs.REG_OFFSET_CH1 >= 0)
         port0_pot_channel3[0] = app_regs.REG_OFFSET_CH1;
      else
         port0_pot_channel1[0] = app_regs.REG_OFFSET_CH1 * -1;
 
      if (app_regs.REG_OFFSET_CH3 >= 0)
         port0_pot_channel3[1] = app_regs.REG_OFFSET_CH3;
      else
         port0_pot_channel1[1] = app_regs.REG_OFFSET_CH3 * -1;
      
      AD5204_set_channel(3-1, port0_pot_channel3, 2, &SPIC, &PORTC, 1, &PORTC);         
      AD5204_set_channel(1-1, port0_pot_channel1, 2, &SPIC, &PORTC, 1, &PORTC);
   }         
      
      
   if (load_cell_channel == 4 || load_cell_channel == 6)
   { 
      uint8_t port1_pot_channel4[2] = {0,0};
      uint8_t port1_pot_channel2[2] = {0,0};
      
      if (app_regs.REG_OFFSET_CH4 >= 0)
         port1_pot_channel4[0] = app_regs.REG_OFFSET_CH4;
      else
         port1_pot_channel2[0] = app_regs.REG_OFFSET_CH4 * -1;
      
      if (app_regs.REG_OFFSET_CH6 >= 0)
         port1_pot_channel4[1] = app_regs.REG_OFFSET_CH6;
      else
         port1_pot_channel2[1] = app_regs.REG_OFFSET_CH6 * -1;
      
      AD5204_set_channel(4-1, port1_pot_channel4, 2, &SPID, &PORTD, 1, &PORTD);
      AD5204_set_channel(2-1, port1_pot_channel2, 2, &SPID, &PORTD, 1, &PORTD);
   }      
   
   if (load_cell_channel == 5 || load_cell_channel == 7)
   {         
      uint8_t port1_pot_channel3[2] = {0,0};
      uint8_t port1_pot_channel1[2] = {0,0};
      
      if (app_regs.REG_OFFSET_CH5 >= 0)
         port1_pot_channel3[0] = app_regs.REG_OFFSET_CH5;
      else
         port1_pot_channel1[0] = app_regs.REG_OFFSET_CH5 * -1;
   
   
      if (app_regs.REG_OFFSET_CH7 >= 0)
         port1_pot_channel3[1] = app_regs.REG_OFFSET_CH7;
      else
         port1_pot_channel1[1] = app_regs.REG_OFFSET_CH7 * -1;
      
      AD5204_set_channel(3-1, port1_pot_channel3, 2, &SPID, &PORTD, 1, &PORTD);
      AD5204_set_channel(1-1, port1_pot_channel1, 2, &SPID, &PORTD, 1, &PORTD);   
   }
}


/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_1st_config_hw_after_boot(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
    
   /* Initialize both SPI with 4MHz */
   SPID_CTRL = SPI_MASTER_bm | SPI_ENABLE_bm | SPI_MODE_0_gc | SPI_CLK2X_bm | SPI_PRESCALER_DIV16_gc;
   SPIC_CTRL = SPI_MASTER_bm | SPI_ENABLE_bm | SPI_MODE_0_gc | SPI_CLK2X_bm | SPI_PRESCALER_DIV16_gc;
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
   app_regs.REG_START = 0;
   
   app_regs.REG_DO0 = 0;
   
   app_regs.REG_DI0_CONF = GM_DI0_SYNC;
   app_regs.REG_DO0_CONF = GM_DO0_TGL_EACH_SEC;
   app_regs.REG_DO0_PULSE = 10;
   
   app_regs.REG_DO_SET = 0;
   app_regs.REG_DO_CLEAR = 0;
   app_regs.REG_DO_CLEAR = 0;
   app_regs.REG_DO_OUT = 0;
   
   app_regs.REG_LOAD_CELLS[0] = 0;
   app_regs.REG_LOAD_CELLS[1] = 0;
   app_regs.REG_LOAD_CELLS[2] = 0;
   app_regs.REG_LOAD_CELLS[3] = 0;
   app_regs.REG_LOAD_CELLS[4] = 0;
   app_regs.REG_LOAD_CELLS[5] = 0;
   app_regs.REG_LOAD_CELLS[6] = 0;
   app_regs.REG_LOAD_CELLS[7] = 0;
   
   app_regs.REG_DO0_CH = GM_SOFTWARE;
   app_regs.REG_DO1_CH = GM_SOFTWARE;
   app_regs.REG_DO2_CH = GM_SOFTWARE;
   app_regs.REG_DO3_CH = GM_SOFTWARE;
   app_regs.REG_DO4_CH = GM_SOFTWARE;
   app_regs.REG_DO5_CH = GM_SOFTWARE;
   app_regs.REG_DO6_CH = GM_SOFTWARE;
   app_regs.REG_DO7_CH = GM_SOFTWARE;
   app_regs.REG_DO0_TH_VALUE = 20000;
   app_regs.REG_DO1_TH_VALUE = 20000;
   app_regs.REG_DO2_TH_VALUE = 20000;
   app_regs.REG_DO3_TH_VALUE = 20000;
   app_regs.REG_DO4_TH_VALUE = 20000;
   app_regs.REG_DO5_TH_VALUE = 20000;
   app_regs.REG_DO5_TH_VALUE = 20000;
   app_regs.REG_DO6_TH_VALUE = 20000;
   app_regs.REG_DO7_TH_VALUE = 20000;
   app_regs.REG_DO0_TH_UP_MS = 0;
   app_regs.REG_DO1_TH_UP_MS = 0;
   app_regs.REG_DO2_TH_UP_MS = 0;
   app_regs.REG_DO3_TH_UP_MS = 0;
   app_regs.REG_DO4_TH_UP_MS = 0;
   app_regs.REG_DO5_TH_UP_MS = 0;
   app_regs.REG_DO6_TH_UP_MS = 0;
   app_regs.REG_DO7_TH_UP_MS = 0;
   app_regs.REG_DO0_TH_DOWN_MS = 0;
   app_regs.REG_DO1_TH_DOWN_MS = 0;
   app_regs.REG_DO2_TH_DOWN_MS = 0;
   app_regs.REG_DO3_TH_DOWN_MS = 0;
   app_regs.REG_DO4_TH_DOWN_MS = 0;
   app_regs.REG_DO5_TH_DOWN_MS = 0;
   app_regs.REG_DO6_TH_DOWN_MS = 0;
   app_regs.REG_DO7_TH_DOWN_MS = 0;
   
   app_regs.REG_OFFSET_CH0 = 0;
   app_regs.REG_OFFSET_CH1 = 0;
   app_regs.REG_OFFSET_CH2 = 0;
   app_regs.REG_OFFSET_CH3 = 0;
   app_regs.REG_OFFSET_CH4 = 0;
   app_regs.REG_OFFSET_CH5 = 0;
   app_regs.REG_OFFSET_CH6 = 0;
   app_regs.REG_OFFSET_CH7 = 0;   
   
	app_regs.REG_EVNT_ENABLE = B_EVT_LOAD_CELLS | B_EVT_DI0 | B_EVT_DO0 | B_EVT_DO_OUT;
}

void core_callback_registers_were_reinitialized(void)
{   
   /* Update outputs with the saved state */
   app_write_REG_DO_OUT(&app_regs.REG_DO_OUT);
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update channels enable indicators */
	//update_enabled_pwmx();
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
bool port0_is_plugged = false;
bool port1_is_plugged = false;

uint8_t update_pots_port0_counter = 0;
uint8_t update_pots_port1_counter = 0;

uint16_t second_counter = 0;

uint16_t pulse_counter_ms = 0;

void update_pots_on_port0(void)
{
     if (update_pots_port0_counter)
     {
        update_pots_port0_counter--;
        
        if (update_pots_port0_counter == 0) update_offsets(0);
        if (update_pots_port0_counter == 1) update_offsets(1);
        if (update_pots_port0_counter == 2) update_offsets(2);
        if (update_pots_port0_counter == 3) update_offsets(3);
     }
}

void update_pots_on_port1(void)
{
     if (update_pots_port1_counter)
     { 
        update_pots_port1_counter--;
        
        if (update_pots_port1_counter == 0) update_offsets(4);
        if (update_pots_port1_counter == 1) update_offsets(5);
        if (update_pots_port1_counter == 2) update_offsets(6);
        if (update_pots_port1_counter == 3) update_offsets(7);
     }
}

void core_callback_t_before_exec(void)
{
   if (++second_counter == 2000)
   {
      if (app_regs.REG_START)
      {
         if (app_regs.REG_DO0_CONF == GM_DO0_TGL_EACH_SEC)
         {
            app_read_REG_DO0();
            app_regs.REG_DO0 ^= 1;
            app_write_REG_DO0(&app_regs.REG_DO0);
                
            if (app_regs.REG_EVNT_ENABLE & B_EVT_DO0)
            {
               core_func_send_event(ADD_REG_DO0, true);
            }
         }
      }
   }
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void)
{
   second_counter = 0;
}



void process_thresholds(void);

void core_callback_t_500us(void)
{  
   /* Pulse on DO0 */
	if (pulse_counter_ms)
	{
		if (!(--pulse_counter_ms))
		{
			if (app_regs.REG_DO0_CONF == GM_DO0_PULSE)
         {
            clr_DO0;            
			   app_regs.REG_DO0 = 0;
			   app_regs.REG_DO_OUT &= ~(1<<0);
         }            
		}
	}
   
   process_thresholds();
}

void core_callback_t_1ms(void)
{
   /* Update pots on Port 0 if the board is re-connected */
   if (read_CS0_1)
   {      
      if (!port0_is_plugged)
      {
         update_pots_port0_counter = 4;
      }
      
      port0_is_plugged = true;
   }
   else
   {
      port0_is_plugged = false;
   }
   
   /* Update pots on Port 1 if the board is re-connected */ 
   if (read_CS1_1)
   {          
      if (!port1_is_plugged)
      {
         update_pots_port1_counter = 4;
      }
          
      port1_is_plugged = true;
   }
   else
   {
      port1_is_plugged = false;
   }
   
   /* Read Load Cells */
   if (app_regs.REG_START)
   {
      core_func_mark_user_timestamp();      
      
      if (read_CS0_1)
      {
         /* If ADC board is plugged into Port0, set ADC CONVST */
         set_CS0_0;
      }
      
      if (read_CS1_1)
      {
         /* If ADC board is plugged into Port1, set ADC CONVST */
         set_CS1_0;
      }
      
      /* Start timer with 30 us */
      timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV64, 15, INT_LEVEL_LOW);
   }
   
   update_pots_on_port0();
   update_pots_on_port1();
}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}

/************************************************************************/
/* Process Thresholds                                                   */
/************************************************************************/
uint16_t ch0_up_counter = 0;
uint16_t ch1_up_counter = 0;
uint16_t ch2_up_counter = 0;
uint16_t ch3_up_counter = 0;
uint16_t ch4_up_counter = 0;
uint16_t ch5_up_counter = 0;
uint16_t ch6_up_counter = 0;
uint16_t ch7_up_counter = 0;
uint16_t ch0_down_counter = 0;
uint16_t ch1_down_counter = 0;
uint16_t ch2_down_counter = 0;
uint16_t ch3_down_counter = 0;
uint16_t ch4_down_counter = 0;
uint16_t ch5_down_counter = 0;
uint16_t ch6_down_counter = 0;
uint16_t ch7_down_counter = 0;

void process_thresholds(void)
{
   uint16_t do_set = 0;
   uint16_t do_clr = 0;
   
   int16_t output_thresholds[8];
   
   
   /* Map load cell values into threshold comparison */
   for (uint8_t i = 0; i < 8; i++)
   {
      switch (*((&app_regs.REG_DO0_CH)+i))
      {
         case GM_CH0: output_thresholds[i] = app_regs.REG_LOAD_CELLS[0]; break;
         case GM_CH1: output_thresholds[i] = app_regs.REG_LOAD_CELLS[1]; break;
         case GM_CH2: output_thresholds[i] = app_regs.REG_LOAD_CELLS[2]; break;
         case GM_CH3: output_thresholds[i] = app_regs.REG_LOAD_CELLS[3]; break;
         case GM_CH4: output_thresholds[i] = app_regs.REG_LOAD_CELLS[4]; break;
         case GM_CH5: output_thresholds[i] = app_regs.REG_LOAD_CELLS[5]; break;
         case GM_CH6: output_thresholds[i] = app_regs.REG_LOAD_CELLS[6]; break;
         case GM_CH7: output_thresholds[i] = app_regs.REG_LOAD_CELLS[7]; break;
      }
   }
   
   
   /* Output channel 1 */
   if (app_regs.REG_DO0_CH != GM_SOFTWARE)
   {
      if (output_thresholds[0] >= app_regs.REG_DO0_TH_VALUE)
      {
         if (++ch0_up_counter == app_regs.REG_DO0_TH_UP_MS + 1)
         do_set |= (1<<(1+0));
         if (ch0_up_counter > app_regs.REG_DO0_TH_UP_MS)
         ch0_up_counter--;
         
         ch0_down_counter = 0;
      }
      else
      {
         if (++ch0_down_counter == app_regs.REG_DO0_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+0));
         if (ch0_down_counter > app_regs.REG_DO0_TH_DOWN_MS)
         ch0_down_counter--;
         
         ch0_up_counter = 0;
      }
   }
   
   /* Output channel 2 */
   if (app_regs.REG_DO1_CH != GM_SOFTWARE)
   {
      if (output_thresholds[1] >= app_regs.REG_DO1_TH_VALUE)
      {
         if (++ch1_up_counter == app_regs.REG_DO1_TH_UP_MS + 1)
         do_set |= (1<<(1+1));
         if (ch1_up_counter > app_regs.REG_DO1_TH_UP_MS)
         ch1_up_counter--;
         
         ch1_down_counter = 0;
      }
      else
      {
         if (++ch1_down_counter == app_regs.REG_DO1_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+1));
         if (ch1_down_counter > app_regs.REG_DO1_TH_DOWN_MS)
         ch1_down_counter--;
         
         ch1_up_counter = 0;
      }
   }
   
   /* Output channel 3 */
   if (app_regs.REG_DO2_CH != GM_SOFTWARE)
   {
      if (output_thresholds[2] >= app_regs.REG_DO2_TH_VALUE)
      {
         if (++ch2_up_counter == app_regs.REG_DO2_TH_UP_MS + 1)
         do_set |= (1<<(1+2));
         if (ch2_up_counter > app_regs.REG_DO2_TH_UP_MS)
         ch2_up_counter--;
         
         ch2_down_counter = 0;
      }
      else
      {
         if (++ch2_down_counter == app_regs.REG_DO2_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+2));
         if (ch2_down_counter > app_regs.REG_DO2_TH_DOWN_MS)
         ch2_down_counter--;
         
         ch2_up_counter = 0;
      }
   }
   
   /* Output channel 4 */
   if (app_regs.REG_DO3_CH != GM_SOFTWARE)
   {
      if (output_thresholds[3] >= app_regs.REG_DO3_TH_VALUE)
      {
         if (++ch3_up_counter == app_regs.REG_DO3_TH_UP_MS + 1)
         do_set |= (1<<(1+3));
         if (ch3_up_counter > app_regs.REG_DO3_TH_UP_MS)
         ch3_up_counter--;
         
         ch3_down_counter = 0;
      }
      else
      {
         if (++ch3_down_counter == app_regs.REG_DO3_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+3));
         if (ch3_down_counter > app_regs.REG_DO3_TH_DOWN_MS)
         ch3_down_counter--;
         
         ch3_up_counter = 0;
      }
   }
   
   /* Output channel 5 */
   if (app_regs.REG_DO4_CH != GM_SOFTWARE)
   {
      if (output_thresholds[4] >= app_regs.REG_DO4_TH_VALUE)
      {
         if (++ch4_up_counter == app_regs.REG_DO4_TH_UP_MS + 1)
         do_set |= (1<<(1+4));
         if (ch4_up_counter > app_regs.REG_DO4_TH_UP_MS)
         ch4_up_counter--;
         
         ch4_down_counter = 0;
      }
      else
      {
         if (++ch4_down_counter == app_regs.REG_DO4_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+4));
         if (ch4_down_counter > app_regs.REG_DO4_TH_DOWN_MS)
         ch4_down_counter--;
         
         ch4_up_counter = 0;
      }
   }
   
   /* Output channel 6 */
   if (app_regs.REG_DO5_CH != GM_SOFTWARE)
   {
      if (output_thresholds[5] >= app_regs.REG_DO5_TH_VALUE)
      {
         if (++ch5_up_counter == app_regs.REG_DO5_TH_UP_MS + 1)
         do_set |= (1<<(1+5));
         if (ch5_up_counter > app_regs.REG_DO5_TH_UP_MS)
         ch5_up_counter--;
         
         ch5_down_counter = 0;
      }
      else
      {
         if (++ch5_down_counter == app_regs.REG_DO5_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+5));
         if (ch5_down_counter > app_regs.REG_DO5_TH_DOWN_MS)
         ch5_down_counter--;
         
         ch5_up_counter = 0;
      }
   }
   
   /* Output channel 7 */
   if (app_regs.REG_DO6_CH != GM_SOFTWARE)
   {
      if (output_thresholds[6] >= app_regs.REG_DO6_TH_VALUE)
      {
         if (++ch6_up_counter == app_regs.REG_DO6_TH_UP_MS + 1)
         do_set |= (1<<(1+6));
         if (ch6_up_counter > app_regs.REG_DO6_TH_UP_MS)
         ch6_up_counter--;
         
         ch6_down_counter = 0;
      }
      else
      {
         if (++ch6_down_counter == app_regs.REG_DO6_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+6));
         if (ch6_down_counter > app_regs.REG_DO6_TH_DOWN_MS)
         ch6_down_counter--;
         
         ch6_up_counter = 0;
      }
   }   
   
   /* Output channel 8 */
   if (app_regs.REG_DO7_CH != GM_SOFTWARE)
   {
      if (output_thresholds[7] >= app_regs.REG_DO7_TH_VALUE)
      {
         if (++ch7_up_counter == app_regs.REG_DO7_TH_UP_MS + 1)
         do_set |= (1<<(1+7));
         if (ch7_up_counter > app_regs.REG_DO7_TH_UP_MS)
         ch7_up_counter--;
         
         ch7_down_counter = 0;
      }
      else
      {
         if (++ch7_down_counter == app_regs.REG_DO7_TH_DOWN_MS + 1)
         do_clr |= (1<<(1+7));
         if (ch7_down_counter > app_regs.REG_DO7_TH_DOWN_MS)
         ch7_down_counter--;
         
         ch7_up_counter = 0;
      }
   }   
   
   
   /* Send Event */
   bool send_event = false;
   if ((app_regs.REG_DO_OUT ^ do_set) & do_set)
   {
      app_write_REG_DO_SET(&do_set);
      send_event = true;
   }
   if ((app_regs.REG_DO_OUT ^ ~do_clr) & do_clr)
   {
      app_write_REG_DO_CLEAR(&do_clr);
      send_event = true;
   }
   if(send_event)
   {
      if (app_regs.REG_EVNT_ENABLE & B_EVT_DO_OUT)
      {
         core_func_send_event(ADD_REG_DO_OUT, true);
      }
   }
}   