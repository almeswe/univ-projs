library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity rstrigger_beh is
    Port ( R : in STD_LOGIC;
           S : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end rstrigger_beh;

architecture Behavioral of rstrigger_beh is begin
    p: process (R, S) begin
        if (R = '0' AND S = '1') THEN
            Q <= '1';
            nQ <= '0';
        elsif (R = '1' AND S = '0') THEN
            Q <= '0';
            nQ <= '1';
        end if;
    end process;
end Behavioral;