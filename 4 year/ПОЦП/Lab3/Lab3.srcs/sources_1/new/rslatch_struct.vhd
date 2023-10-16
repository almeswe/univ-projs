library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity rslatch_struct is
    Port ( R : in STD_LOGIC;
           S : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end rslatch_struct;

architecture Structural of rslatch_struct is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component nor2 is
        Port ( X1 : in STD_LOGIC;
               X2 : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
    signal nor_out: STD_LOGIC_VECTOR(0 to 1);
begin
    NOR1: nor2 port map (X1 => S, X2 => nor_out(1), Q => nor_out(0));
    NOT2: nor2 port map (X1 => R, X2 => nor_out(0), Q => nor_out(1));
    nQ <= nor_out(0);
    Q  <= nor_out(1);
end Structural;
