library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity and5_beh is
    Port ( X : in STD_LOGIC_VECTOR (0 to 4);
           Z : out STD_LOGIC);
end and5_beh;

architecture Behavioral of and5_beh is begin
    Z <= X(0) AND X(1) AND X(2) AND X(3) AND X(4);
end Behavioral;
