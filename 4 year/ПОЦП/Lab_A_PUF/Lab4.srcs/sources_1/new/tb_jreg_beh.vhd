library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_jreg_beh is
end tb_jreg_beh;

architecture Behavioral of tb_jreg_beh is
    component jreg_beh is
        Generic (
            N: integer := 8
        );
        Port (
            CLR : in STD_LOGIC;
            CLK : in STD_LOGIC;
            Q : out STD_LOGIC
         );
    end component;
    signal BEH_Q: STD_LOGIC := '0';
    signal BEH_CLR: STD_LOGIC := '0';
    signal BEH_CLK: STD_LOGIC := '0';
    constant PERIOD: time := 10ns;
begin
    UUT_JREG_BEH0: jreg_beh 
    generic map (
        N => 3
    )
    port map (
        CLR => BEH_CLR,
        CLK => BEH_CLK,
        Q => BEH_Q
    );
    
    PRODUCE_CLR: process begin
        BEH_CLR <= '1';
        wait for PERIOD/2;
        BEH_CLR <= '0';
        wait for PERIOD*1000;
    end process;
    
    PRODUCE_CLK: process begin
        BEH_CLK <= '0';
        wait for PERIOD/2;
        BEH_CLK <= '1';
        wait for PERIOD/2;
    end process;
end Behavioral;
