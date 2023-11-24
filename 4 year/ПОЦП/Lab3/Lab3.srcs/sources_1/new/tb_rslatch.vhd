library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_rslatch is
end tb_rslatch;

architecture Behavioral of tb_rslatch is
    component rslatch_beh is
        Port ( R : in STD_LOGIC;
               S : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    component rslatch_struct is
        Port ( R : in STD_LOGIC;
               S : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    signal ERROR: STD_LOGIC := '0';
    signal X: STD_LOGIC_VECTOR(0 to 3) := ('0','0','0','0');
    signal Z_BEH, Z_STR: STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
    constant PERIOD: TIME := 10ns;
begin
    BEH: rslatch_beh    port map (R => X(0), S => X(1), nQ => Z_BEH(0), Q => Z_BEH(1));
    STR: rslatch_struct port map (R => X(2), S => X(3), nQ => Z_STR(0), Q => Z_STR(1));
    X(0) <= NOT X(0) AFTER PERIOD;
    X(1) <= NOT X(1) AFTER PERIOD*2;
    X(2) <= NOT X(2) AFTER PERIOD;
    X(3) <= NOT X(3) AFTER PERIOD*2;
    ERROR <= (Z_BEH(0) XOR Z_STR(0)) OR
             (Z_BEH(1) XOR Z_STR(1));
end Behavioral;
