library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_and5 is
end tb_and5;

architecture Behavioral of tb_and5 is
    component and5_beh is
        Port ( X : in STD_LOGIC_VECTOR (0 to 4);
               Z : out STD_LOGIC);
    end component;
    component and5_struct is
        Port ( X : in STD_LOGIC_VECTOR (0 to 4);
               Z : out STD_LOGIC);
    end component;
    signal X: STD_LOGIC_VECTOR(0 TO 4) := ('0','0','0','0','0');
    signal ERROR: STD_LOGIC := '0';
    signal Z_BEH, Z_STRUCT: STD_LOGIC;
    constant PERIOD: TIME := 10ns;
begin
    BEH: and5_beh port map (X, Z_BEH);
    STR: and5_struct port map (X, Z_STRUCT);
    X(0) <= NOT X(0) AFTER PERIOD;
    X(1) <= NOT X(1) AFTER PERIOD*2;
    X(2) <= NOT X(2) AFTER PERIOD*4;
    X(3) <= NOT X(3) AFTER PERIOD*8;
    X(4) <= NOT X(4) AFTER PERIOD*16;
    ERROR <= Z_BEH XOR Z_STRUCT;
end Behavioral;
