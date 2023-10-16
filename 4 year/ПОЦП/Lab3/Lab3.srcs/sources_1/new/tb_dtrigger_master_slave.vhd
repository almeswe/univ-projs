library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_dtrigger_master_slave is
end tb_dtrigger_master_slave;

architecture Behavioral of tb_dtrigger_master_slave is
    component dtrigger_master_slave_struct is
        Port ( D : in STD_LOGIC;
               C : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    CONSTANT PERIOD: TIME := 10ns;
    SIGNAL Q:  STD_LOGIC := '0';
    SIGNAL nQ: STD_LOGIC := '0';
    SIGNAL X:  STD_LOGIC_VECTOR(1 downto 0) := ('0', '0');
begin
    UUT_DTMS0: dtrigger_master_slave_struct port map (D => X(0), C => X(1), Q => Q, nQ => nQ);

    STIM_C: process begin
        X(1) <= NOT X(1);
        WAIT FOR PERIOD;
    end process;
    
    STIM_D: process begin
        X(0) <= NOT X(0);
        WAIT FOR PERIOD*1.5;
    end process;
end Behavioral;
