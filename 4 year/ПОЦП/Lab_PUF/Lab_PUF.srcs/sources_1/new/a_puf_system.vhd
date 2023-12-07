library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity a_puf_system is
    Port (
       TX: out STD_LOGIC;
       CLK: in STD_LOGIC;
       RST: in STD_LOGIC;
       STOP: in STD_LOGIC;
       START: in STD_LOGIC;
       INTERRUPTED: out STD_LOGIC
    );
end a_puf_system;

architecture Structural of a_puf_system is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";

    component a_puf is
        Generic (
            N: integer := 128
        );
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            Q: out STD_LOGIC_VECTOR(0 to 7)
        );
    end component;
    --component a_puf_dummy is
    --    Generic (
    --        N: integer := 8
    --    );
    --    Port (
    --        EN: in STD_LOGIC;
    --       CLK: in STD_LOGIC;
    --        RST: in STD_LOGIC;
    --        S: in STD_LOGIC_VECTOR(0 to N-1);
    --        Q: out STD_LOGIC_VECTOR(0 to N-1)
    --    );
    --end component;
    component fsm is
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
    end component;
    component clk_div is
        Generic (
            DIVISOR: INTEGER := 1
        );
        Port (
            RST: in STD_LOGIC;
            CLK_IN: in STD_LOGIC;
            CLK_OUT: out STD_LOGIC
        );
    end component;
    component clk_50Mhz_to_1p8432Mhz is
        Port (
            RST: in std_logic;
            CLK_50MHz: in std_logic;
            CLK_1p8432MHz: out std_logic
        );
    end component;
    component uart_tx is
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            DATA8: in STD_LOGIC_VECTOR(0 to 7);
            TX: out STD_LOGIC;
            DONE: out STD_LOGIC
        );
    end component;
    
    signal UART_CLK: STD_LOGIC;
    signal UART_DONE: STD_LOGIC;
    signal FSM_CLK: STD_LOGIC;
    signal FSM_EN_APUF: STD_LOGIC;
    signal FSM_EN_UART: STD_LOGIC;
    signal FSM_RST_UART: STD_LOGIC;
    signal A_PUF_OUT: STD_LOGIC_VECTOR(0 to 7);
    signal CLK_50MHz: STD_LOGIC;
begin
    A_PUF_0: a_puf port map (
        EN => FSM_EN_APUF,
        CLK => CLK,
        RST => RST,
        Q => A_PUF_OUT
    );
    FSM_0: fsm port map (
        RST => RST,
        CLK => FSM_CLK,--UART_CLK,
        STOP => STOP,
        START => START,
        UART_DONE => UART_DONE,
        EN_APUF => FSM_EN_APUF,
        EN_UART => FSM_EN_UART,
        RST_UART => FSM_RST_UART
    );
    UART_TX_0: uart_tx port map (
        EN => FSM_EN_UART,
        CLK => UART_CLK,
        RST => FSM_RST_UART,
        DATA8 => A_PUF_OUT,
        TX => TX,
        DONE => UART_DONE
    );
    CLK_DIVIDER_0: clk_div
    generic map (
        DIVISOR => 2
    )
    port map (
        RST => RST,
        CLK_IN => CLK,
        CLK_OUT => CLK_50MHz
    );
    CLK_DIVIDER_1: clk_div
    generic map (
        DIVISOR => 4
    )
    port map (
        RST => RST,
        CLK_IN => CLK,
        CLK_OUT => FSM_CLK
    );
    CLK_DIVIDER_2: clk_50Mhz_to_1p8432Mhz port map (
        RST => RST,
        CLK_50MHz => CLK_50MHz,
        CLK_1p8432Mhz => UART_CLK
    );
    INTERRUPTED <= STOP;
end Structural;