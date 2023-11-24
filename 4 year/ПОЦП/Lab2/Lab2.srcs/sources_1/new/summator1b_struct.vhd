library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity summator1b_struct is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           C1 : in STD_LOGIC;
           C2 : out STD_LOGIC;
           Z : out STD_LOGIC);
end summator1b_struct;

architecture Structural of summator1b_struct is
    component and2 is
        Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           Z : out STD_LOGIC);
    end component;
    component or2 is
        Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           Z : out STD_LOGIC);
    end component;
    component xor2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal X: STD_LOGIC_VECTOR(0 TO 2);
begin
    A1: xor2 port map (A, B, X(0));
    A2: and2 port map (A, B, X(1));
    A3: xor2 port map (C1, X(0), Z);
    A4: and2 port map (C1, X(0), X(2));
    A5: or2 port map (X(2), X(1), C2);
end Structural;