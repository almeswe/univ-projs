library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dtrigger_ar_beh is
    Port ( D : in STD_LOGIC;
           C : in STD_LOGIC;
           R : in STD_LOGIC;
           Q : out STD_LOGIC);
end dtrigger_ar_beh;

architecture Behavioral of dtrigger_ar_beh is begin
    p: process(D, C, R) begin
        if (R = '1') then 
            Q <= '0';
        elsif (rising_edge(C)) then
            Q <= D;
        end if; 
    end process;
end Behavioral;