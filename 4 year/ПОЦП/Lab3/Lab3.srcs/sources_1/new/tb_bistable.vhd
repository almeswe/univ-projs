library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_bistable is
end tb_bistable;

architecture Behavioral of tb_bistable is
    component bistable_beh is
        Port ( Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    component bistable_struct is
        Port ( Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    signal ERROR: STD_LOGIC := '0';
    signal Z_BEH, Z_STR: STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
begin
    BEH: bistable_beh    port map (nQ => Z_BEH(0), Q => Z_BEH(1));
    STR: bistable_struct port map (nQ => Z_STR(0), Q => Z_STR(1));
    ERROR <= (Z_BEH(0) XOR Z_STR(0)) OR
             (Z_BEH(1) XOR Z_STR(1));
end Behavioral;
