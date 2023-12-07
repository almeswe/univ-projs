library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_uart is
end tb_uart;

architecture Behavioral of tb_uart is
    component uart is
        port (clk      : in std_logic;
              reset    : in std_logic;
              data_in  : in std_logic_vector(7 downto 0);
              in_valid : in std_logic;
    
              tx        : out std_logic;
              accept_in : out std_logic;
              completed : out std_logic);
    end component;
    signal UART_CLK: STD_LOGIC := '0';
    signal UART_RST: STD_LOGIC := '1';
    signal UART_DATA_IN: STD_LOGIC_VECTOR(7 downto 0) := x"aa";
    signal UART_IN_VALID: STD_LOGIC := '1';
    signal UART_TX: STD_LOGIC;
    signal UART_ACCEPT_IN: STD_LOGIC;
    signal UART_COMPLETED: STD_LOGIC;
begin
    UUT_UART_0: uart port map (
        clk => UART_CLK,
        reset => UART_RST,
        data_in => UART_DATA_IN,
        in_valid => UART_IN_VALID,
        tx => UART_TX,
        accept_in => UART_ACCEPT_IN,
        completed => UART_COMPLETED
    );
    
    PRODUCE_UART_CLK: process begin
        wait for 1.8432ns;
        UART_CLK <= NOT UART_CLK;
    end process;
    
    PRODUCE_UART_RST: process begin
        wait for 100ns;
        UART_RST <= NOT UART_RST;
        wait for 1000us;
    end process;
    
    PRODUCE_UART_IN_VALID: process begin
        wait for 50us;
        UART_IN_VALID <= NOT UART_IN_VALID;
    end process;
end Behavioral;
