library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity or4x1 is
    Port ( A1 : in STD_LOGIC;
           A2 : in STD_LOGIC;
           A3 : in STD_LOGIC;
           A4 : in STD_LOGIC;
           Q : out STD_LOGIC);
end or4x1;

architecture Behavioral of or4x1 is begin
    Q <= A1 OR A2 OR A3 OR A4;
end Behavioral;