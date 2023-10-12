library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity and5_struct is
    Port ( X : in STD_LOGIC_VECTOR (0 to 4);
           Z : out STD_LOGIC);
end and5_struct;

architecture Structural of and5_struct is 
    component and2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal Y: STD_LOGIC_VECTOR(0 to 3);
begin
    G0: and2 port map (X(0), X(1), Y(0));
    SCH: FOR J IN 1 TO 3 GENERATE 
        GJ: and2 port map (X(J+1), Y(J-1), Y(J));
    End GENERATE;
    Z <= Y(3);
end Structural;
