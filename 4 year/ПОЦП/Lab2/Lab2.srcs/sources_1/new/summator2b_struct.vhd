library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity summator2b_struct is
    Port ( A1 : in STD_LOGIC;
           A2 : in STD_LOGIC;
           B1 : in STD_LOGIC;
           B2 : in STD_LOGIC;
           C1 : in STD_LOGIC;
           C2 : out STD_LOGIC;
           Z1 : out STD_LOGIC;
           Z2 : out STD_LOGIC);
end summator2b_struct;

architecture Structural of summator2b_struct is
    component summator1b_struct is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               C1 : in STD_LOGIC;
               C2 : out STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal CF1, CF2: STD_LOGIC;
begin
    D1: summator1b_struct port map (A1, A2, '0', CF1, Z1);
    D2: summator1b_struct port map (B1, B2, CF1, CF2, Z2);
end Structural;