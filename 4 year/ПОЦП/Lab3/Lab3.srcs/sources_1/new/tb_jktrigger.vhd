library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_jktrigger is
end tb_jktrigger;

architecture Behavioral of tb_jktrigger is 
    component jktrigger_beh is
        Port ( J : in STD_LOGIC;
               K : in STD_LOGIC;
               C : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL Q: STD_LOGIC := '0';
    SIGNAL nQ: STD_LOGIC := '0';
    SIGNAL X: STD_LOGIC_VECTOR(0 to 2) := ('0', '0', '0');
begin
    UUT_JKT0: jktrigger_beh port map (J => X(0), K => X(1), C => X(2), nQ => nQ, Q => Q);
    
    STIM_C: process begin
        X(2) <= NOT X(2);
        WAIT FOR PERIOD;
    end process;
    
    STIM_J: process begin
        X(0) <= NOT X(0);
        WAIT FOR PERIOD*2;
    end process;
    
    STIM_K: process begin
        X(1) <= NOT X(1);
        WAIT FOR PERIOD*4;
    end process;
end Behavioral;
