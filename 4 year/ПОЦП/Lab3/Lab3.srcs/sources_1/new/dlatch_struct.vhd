library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dlatch_struct is
    Port ( D : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end dlatch_struct;

architecture Structural of dlatch_struct is
    component nor2 is
    Port ( X1 : in STD_LOGIC;
           X2 : in STD_LOGIC;
           Q : out STD_LOGIC);
    end component;
    component inv is
        Port ( X : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
    SIGNAL inv_out: STD_LOGIC;
    SIGNAL nor_out: STD_LOGIC_VECTOR(0 to 1);
begin
    UUT_INV0: inv port map (X => D, Q => inv_out);
    UUT_NOR0: nor2 port map (X1 => D,       X2 => nor_out(1), Q => nor_out(0));
    UUT_NOR1: nor2 port map (X1 => inv_out, X2 => nor_out(0), Q => nor_out(1));
    nQ <= nor_out(0);
    Q  <= nor_out(1);
end Structural;
