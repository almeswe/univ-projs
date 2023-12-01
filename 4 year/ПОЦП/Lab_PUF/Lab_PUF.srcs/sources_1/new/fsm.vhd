library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity fsm is
    Port (
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        STOP: in STD_LOGIC;
        START: in STD_LOGIC;
        UART_DONE: in STD_LOGIC;
        EN_APUF: out STD_LOGIC;
        EN_UART: out STD_LOGIC;
        RST_UART: out STD_LOGIC
    );
end fsm;

architecture Behavioral of fsm is
    type fsm_state is (
        fsm_idle,
        fsm_gen,
        fsm_dump,
        fsm_reset,
        fsm_stop
    );
    signal CURR_STATE: fsm_state;
    signal NEXT_STATE: fsm_state;
    signal FSM_OUT_BUS: STD_LOGIC_VECTOR(0 to 2);
begin
    FSM_MEMORY: process (CURR_STATE, NEXT_STATE, FSM_OUT_BUS, CLK, RST) begin
        if RST = '1' then 
            CURR_STATE <= fsm_idle;
            FSM_OUT_BUS <= "001";
        elsif RISING_EDGE(CLK) then
            case NEXT_STATE is
                when fsm_idle =>  FSM_OUT_BUS <= "000";
                when fsm_gen  =>  FSM_OUT_BUS <= "100";
                when fsm_dump =>  FSM_OUT_BUS <= "010";
                when fsm_reset => FSM_OUT_BUS <= "001";
                when fsm_stop =>  FSM_OUT_BUS <= "000";
             end case;
             CURR_STATE <= NEXT_STATE;
        end if;
    end process;
    
   FSM_TRANSITION: process (CURR_STATE, NEXT_STATE, STOP, START, UART_DONE) begin
        case CURR_STATE is
            when fsm_idle =>
                if START = '1' AND STOP = '0' then
                    NEXT_STATE <= fsm_gen;
                else
                    NEXT_STATE <= fsm_idle;
                end if;
            when fsm_gen   => NEXT_STATE <= fsm_dump;
            when fsm_dump  => 
                if UART_DONE = '1' then
                    NEXT_STATE <= fsm_reset;
                else
                    NEXT_STATE <= fsm_dump;
                end if;
            when fsm_reset => NEXT_STATE <= fsm_stop;
            when fsm_stop  =>
                if STOP = '1' then
                    NEXT_STATE <= fsm_idle;
                else
                    NEXT_STATE <= fsm_gen;
                end if;
            when others => NEXT_STATE <= fsm_idle;
         end case;
   end process;
   EN_APUF <= FSM_OUT_BUS(0);
   EN_UART <= FSM_OUT_BUS(1);
   RST_UART <= FSM_OUT_BUS(2);
end Behavioral;