library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity clk_7Mhz_to_1Hz is
    Port (
        CLK_7MHz : in STD_LOGIC;
        CLK_1Hz : out STD_LOGIC
    );
end clk_7Mhz_to_1Hz;

architecture Behavioral of clk_7Mhz_to_1Hz is
    signal inn: STD_LOGIC := '0';
begin
    MAIN: process (CLK_7MHz) 
    variable reg: integer := 0;
    begin
        if (RISING_EDGE(CLK_7MHz)) then
            reg := reg + 1;
            if (reg = 3500000 - 1) then
                inn <= not inn;
                reg := 0;
            end if;        
        end if;
    end process;
    CLK_1Hz <= inn;
end Behavioral;
