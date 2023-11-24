library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_ttrigger is
end tb_ttrigger;

architecture Behavioral of tb_ttrigger is
    component ttrigger_beh is
        Port ( T : in STD_LOGIC;
               C : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL Q: STD_LOGIC  := '0';
    SIGNAL nQ: STD_LOGIC := '0';
    SIGNAL X: STD_LOGIC_VECTOR(0 to 1) := ('0', '0');
begin
    UUT_TT0: ttrigger_beh port map (T => X(0), C => X(1), nQ => nQ, Q => Q);

    STIM_C: process begin
        X(1) <= NOT X(1);
        WAIT FOR PERIOD;
    end process;
    
    STIM_T: process begin
        X(0) <= NOT X(0);
        WAIT FOR PERIOD*1.5;
    end process;
end Behavioral;