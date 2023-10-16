library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity inv is
    Port ( X : in STD_LOGIC;
           Q : out STD_LOGIC);
end inv;

architecture Behavioral of inv is
begin
    Q <= NOT X;
end Behavioral;
