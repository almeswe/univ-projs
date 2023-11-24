library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_migen_beh is
end tb_migen_beh;

architecture Behavioral of tb_migen_beh is
    component migen_beh is
        Port (
            RST : in STD_LOGIC;
            CLK : in STD_LOGIC;
            Q : out STD_LOGIC
        );
    end component;
    signal BEH_Q: STD_LOGIC := '0';
    signal BEH_RST: STD_LOGIC := '0';
    signal BEH_CLK: STD_LOGIC := '0';
    constant PERIOD: time := 10ns;
begin
    UUT_MIGEN_BEH0: migen_beh
    port map (
        RST => BEH_RST,
        CLK => BEH_CLK,
        Q => BEH_Q
    );
    
    PRODUCE_RST: process begin
        BEH_RST <= '1';
        wait for PERIOD/2;
        BEH_RST <= '0';
        wait for PERIOD*1000;
    end process;
    
    PRODUCE_CLK: process begin
        BEH_CLK <= '0';
        wait for PERIOD/2;
        BEH_CLK <= '1';
        wait for PERIOD/2;
    end process;
end Behavioral;
