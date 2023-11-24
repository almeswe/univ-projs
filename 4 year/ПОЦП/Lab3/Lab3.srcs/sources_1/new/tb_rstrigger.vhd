library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_rsrigger is
end tb_rsrigger;

architecture Behavioral of tb_rsrigger is
    component rstrigger_beh is
        Port ( R : in STD_LOGIC;
               S : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL Q: STD_LOGIC := '0';
    SIGNAL nQ: STD_LOGIC := '0';
    SIGNAL X: STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
begin
    UUT_RST0: rstrigger_beh port map (R => X(0), S => X(1), nQ => nQ, Q => Q);
    
    STIM_R: process begin
        X(0) <= NOT X(0);
        WAIT FOR PERIOD;
    end process;
    
    STIM_S: process begin
        X(1) <= NOT X(1);
        WAIT FOR PERIOD*2;
    end process;
end Behavioral;