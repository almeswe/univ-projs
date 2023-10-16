library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dlatch_beh is
    Port ( D : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end dlatch_beh;

architecture Behavioral of dlatch_beh is
    SIGNAL and_out: STD_LOGIC_VECTOR(0 to 1);
    SIGNAL nor_out: STD_LOGIC_VECTOR(0 to 1);
begin
    nor_out(0) <= D NOR nor_out(1);
    nor_out(1) <= (NOT D) NOR nor_out(0);
    nQ <= nor_out(0);
    Q  <= nor_out(1);
end Behavioral;
