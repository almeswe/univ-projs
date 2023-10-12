library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity expr is
    Port ( in1 : in STD_LOGIC;
           in2 : in STD_LOGIC;
           in3 : in STD_LOGIC;
           Q : out STD_LOGIC);
end expr;

architecture Behavioral of expr is begin
    Q <= (in1 AND in2) OR (in3 AND (NOT in2));
end Behavioral;