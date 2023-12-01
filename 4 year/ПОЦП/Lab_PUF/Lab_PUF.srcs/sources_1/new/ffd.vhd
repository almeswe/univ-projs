library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity ffd is
    Port (
        D: in STD_LOGIC;
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        Q: out STD_LOGIC
    );
end ffd;

architecture Behavioral of ffd is begin
    MAIN: process (D, EN, CLK, RST) begin
        if RST = '1' then
            Q <= '0';
        elsif EN = '1' AND RISING_EDGE(CLK) then
            Q <= D;
        end if;
    end process;
end Behavioral;