library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dlatch_en_beh is
    Port ( D : in STD_LOGIC;
           E : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end dlatch_en_beh;

architecture Behavioral of dlatch_en_beh is
    SIGNAL and_out: STD_LOGIC_VECTOR(0 to 1);
    SIGNAL nor_out: STD_LOGIC_VECTOR(0 to 1);
begin
    and_out(0) <= (NOT D) AND E;
    and_out(1) <= D AND E;
    nor_out(0) <= and_out(0) NOR nor_out(1);
    nor_out(1) <= and_out(1) NOR nor_out(0);
    nQ <= nor_out(0);
    Q  <= nor_out(1);
end Behavioral;
