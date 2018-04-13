#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/ 
/* DI0                                                                  */
/************************************************************************/
ISR(PORTB_INT0_vect, ISR_NAKED)
{
   if (read_DI0)
   {
      switch (app_regs.REG_DI0_CONF)
      {
         case GM_DI0_SYNC:
            if (app_regs.REG_EVNT_ENABLE & B_EVT_DI0)
            {               
               app_regs.REG_DI0 = B_DI0;
               core_func_send_event(ADD_REG_DI0, true);
            }
            break;
            
         case GM_DI0_RISE_START_ACQ:
            app_regs.REG_START = B_START;
            break;
            
         case GM_DI0_FALL_START_ACQ:
            app_regs.REG_START = 0;
            break;
      }
   }
   else
   {
      switch (app_regs.REG_DI0_CONF)
      {
         case GM_DI0_SYNC:
         if (app_regs.REG_EVNT_ENABLE & B_EVT_DI0)
         {
            app_regs.REG_DI0 = 0;
            core_func_send_event(ADD_REG_DI0, true);
         }
         break;
         
         case GM_DI0_RISE_START_ACQ:
         app_regs.REG_START = 0;
         break;
         
         case GM_DI0_FALL_START_ACQ:
         app_regs.REG_START = B_START;
         break;
      }
   }
   
	reti();
}

/************************************************************************/
/* ADC is ready to be read                                              */
/************************************************************************/
ISR(TCC0_OVF_vect, ISR_NAKED)
{
   timer_type0_stop(&TCC0);
 
   clr_CS0_1;     // Clear Port0 ADC !CS
   clr_CS1_1;     // Clear Port1 ADC !CS
      
   for (uint8_t i = 0; i < 4; i++)
   {
      SPIC_DATA = 0;
      SPID_DATA = 0;
      //loop_until_bit_is_set(SPIC_STATUS, SPI_IF_bp);
      loop_until_bit_is_set(SPID_STATUS, SPI_IF_bp);
         
      *(((uint8_t*)(&app_regs.REG_LOAD_CELLS[0])) + i*2 + 1) = SPIC_DATA;
      *(((uint8_t*)(&app_regs.REG_LOAD_CELLS[0])) + i*2 + 9) = SPID_DATA;
         
      SPIC_DATA = 0;
      SPID_DATA = 0;
      //loop_until_bit_is_set(SPIC_STATUS, SPI_IF_bp);
      loop_until_bit_is_set(SPID_STATUS, SPI_IF_bp);
         
      *(((uint8_t*)(&app_regs.REG_LOAD_CELLS[0])) + i*2 + 0) = SPIC_DATA;
      *(((uint8_t*)(&app_regs.REG_LOAD_CELLS[0])) + i*2 + 8) = SPID_DATA;
   }      

   clr_CS0_0;     // CLear Port0 ADC CONVST
   clr_CS1_0;     // CLear Port1 ADC CONVST
   set_CS0_1;     // Set Port0 ADC !CS
   set_CS1_1;     // Set Port1 ADC !CS

   if (!read_CS0_1)
   {
      app_regs.REG_LOAD_CELLS[0] = 0;
      app_regs.REG_LOAD_CELLS[1] = 0;
      app_regs.REG_LOAD_CELLS[2] = 0;
      app_regs.REG_LOAD_CELLS[3] = 0;
   }
   
   if (!read_CS1_1)
   {
      app_regs.REG_LOAD_CELLS[4] = 0;
      app_regs.REG_LOAD_CELLS[5] = 0;
      app_regs.REG_LOAD_CELLS[6] = 0;
      app_regs.REG_LOAD_CELLS[7] = 0;
   }
   
   if (app_regs.REG_EVNT_ENABLE & B_EVT_LOAD_CELLS)
   {
      core_func_send_event(ADD_REG_LOAD_CELLS, false);      
   }
   
   reti();
}