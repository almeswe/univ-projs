library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_dlatch_en is
end tb_dlatch_en;

architecture Behavioral of tb_dlatch_en is
    component dlatch_en_beh is
        Port ( D : in STD_LOGIC;
               E : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    component dlatch_en_struct is
        Port ( D : in STD_LOGIC;
               E : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL ERROR: STD_LOGIC := '0';
    SIGNAL X:  STD_LOGIC_VECTOR(0 to 3) := ('0', '0', '0', '0');
    SIGNAL Q:  STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
    SIGNAL nQ: STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
begin
    BEH: dlatch_en_beh    port map (D => X(0), E => X(1), Q => Q(0), nQ => nQ(0));
    STR: dlatch_en_struct port map (D => X(2), E => X(3), Q => Q(1), nQ => nQ(1)); 
    X(0) <= NOT X(0) AFTER PERIOD;
    X(1) <= NOT X(1) AFTER PERIOD*2;
    X(2) <= NOT X(2) AFTER PERIOD;
    X(3) <= NOT X(3) AFTER PERIOD*2;
    ERROR <= (Q(0)  XOR Q(1)) OR
             (nQ(0) XOR nQ(1));
end Behavioral;
