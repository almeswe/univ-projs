library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity fdce is
    Port ( CLR : in STD_LOGIC;
           CE : in STD_LOGIC;
           D : in STD_LOGIC;
           CLK : in STD_LOGIC;
           Q : out STD_LOGIC);
end fdce;

architecture Behavioral of fdce is begin
    MAIN: process (CLR, CE, D, CLK) begin
        if (CLR = '1') then 
            Q <= '0';
        elsif (CE = '1' and RISING_EDGE(CLK)) then
            Q <= D;
        end if;
    end process;
end Behavioral;
