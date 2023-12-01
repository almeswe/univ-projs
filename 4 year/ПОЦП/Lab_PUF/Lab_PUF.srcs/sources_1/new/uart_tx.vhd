library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity uart_tx is
    Port (
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        DATA8: in STD_LOGIC_VECTOR(0 to 7);
        TX: out STD_LOGIC;
        DONE: out STD_LOGIC
    );
end uart_tx;

architecture Structural of uart_tx is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component uart is
        port (clk      : in std_logic;
              reset    : in std_logic;
              data_in  : in std_logic_vector(7 downto 0);
              in_valid : in std_logic;
    
              tx        : out std_logic;
              accept_in : out std_logic;
              completed : out std_logic);
    end component;
    signal UART_DONE: STD_LOGIC;
begin
    UART_0: uart port map (
        clk => CLK,
        reset => RST,
        data_in => DATA8,
        in_valid => EN,
        tx => TX,
        accept_in => open,
        completed => UART_DONE
    ); 
    DONE <= UART_DONE;
end Structural;
