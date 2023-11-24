library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.NUMERIC_STD.ALL;

entity tb_dtrigger_en is
end tb_dtrigger_en;

architecture Behavioral of tb_dtrigger_en is
    component dtrigger_en_beh is
        Port ( D : in STD_LOGIC;
               E : in STD_LOGIC;
               C : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL Q:  STD_LOGIC := '0';
    SIGNAL X:  STD_LOGIC_VECTOR(0 to 2) := ('0', '0', '0');
begin
    UUT_DTE0: dtrigger_en_beh port map (D => X(0), E => X(1), C => X(2), Q => Q);
    
    STIM_C: process begin
        X(2) <= NOT X(2);
        WAIT FOR PERIOD;
    end process;

    STIM_E: process begin
        X(1) <= NOT X(1);
        WAIT FOR PERIOD*2;
    end process;

    STIM_D: process begin
        X(0) <= NOT X(0);
        WAIT FOR PERIOD*4;
    end process;
end Behavioral;