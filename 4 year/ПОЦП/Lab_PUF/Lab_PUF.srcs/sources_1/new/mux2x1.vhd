library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity mux2x1 is
    Port (
       A : in STD_LOGIC;
       B : in STD_LOGIC;
       S : in STD_LOGIC;
       Q : out STD_LOGIC
    );
end mux2x1;

architecture Behavioral of mux2x1 is begin
    Q <= A when S = '1' else B;
end Behavioral;
