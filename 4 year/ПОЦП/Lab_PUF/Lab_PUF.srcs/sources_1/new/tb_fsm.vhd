library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_fsm is
end tb_fsm;

architecture Behavioral of tb_fsm is
    component fsm is
        Port (
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            START: in STD_LOGIC;
            DUMP: in STD_LOGIC;
            EN_APUF: out STD_LOGIC;
            EN_UART: out STD_LOGIC
        );
    end component;
    constant PERIOD: time := 100ns;
    signal FSM_CLK: STD_LOGIC := '0';
    signal FSM_RST: STD_LOGIC := '0';
    signal FSM_START: STD_LOGIC := '1';
    signal FSM_DUMP: STD_LOGIC := '1';
    signal FSM_EN_APUF: STD_LOGIC := '0';
    signal FSM_EN_UART: STD_LOGIC := '0';
begin
    UUT_FSM_0: fsm port map (
        CLK => FSM_CLK,
        RST => FSM_RST,
        START => FSM_START,
        DUMP => FSM_DUMP,
        EN_APUF => FSM_EN_APUF, 
        EN_UART => FSM_EN_UART
    );
    
    PRODUCE_RST: process begin
        FSM_RST <= '1';
        wait for PERIOD/2;
        FSM_RST <= '0';
        wait for PERIOD*1000;
    end process;
    
    PRODUCE_CLK: process begin
        FSM_CLK <= '0';
        wait for PERIOD/2;
        FSM_CLK <= '1';
        wait for PERIOD/2;
    end process;
end Behavioral;