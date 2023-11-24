library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dtrigger_en_beh is
    Port ( D : in STD_LOGIC;
           E : in STD_LOGIC;
           C : in STD_LOGIC;
           Q : out STD_LOGIC);
end dtrigger_en_beh;

architecture Behavioral of dtrigger_en_beh is begin
    p: process(D, C) begin
        if (E = '1' AND rising_edge(C)) then
            Q <= D;
        end if;
    end process;
end Behavioral;