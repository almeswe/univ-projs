library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity rslatch_beh is
    Port ( R : in STD_LOGIC;
           S : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end rslatch_beh;

architecture Behavioral of rslatch_beh is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal nor_out: STD_LOGIC_VECTOR(0 to 1);
begin
    nor_out(0) <= NOT (S OR nor_out(1));
    nor_out(1) <= NOT (R OR nor_out(0));
    nQ <= nor_out(0);
    Q  <= nor_out(1);
end Behavioral;
