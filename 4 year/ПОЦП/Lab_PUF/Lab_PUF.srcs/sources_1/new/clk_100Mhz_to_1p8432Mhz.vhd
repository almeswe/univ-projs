library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity clk_100Mhz_to_1p8432Mhz is
    Port (
        RST: in STD_LOGIC;
        CLK_100MHz: in STD_LOGIC;
        CLK_1p8432MHz: out STD_LOGIC
    );
end clk_100Mhz_to_1p8432Mhz;

architecture Behavioral of clk_100Mhz_to_1p8432Mhz is
    signal DIVIDED_CLK: STD_LOGIC;
begin
    MAIN: process (DIVIDED_CLK, CLK_100MHz, RST) 
        variable COUNTER: integer := 0;
    begin
        if RISING_EDGE(CLK_100MHz) then
            if RST = '1' then
                COUNTER := 0;
                DIVIDED_CLK <= '0';
            -- 54 / 2
            else
                if COUNTER < 54 then
                    COUNTER := COUNTER + 1;
                else
                    DIVIDED_CLK <= NOT DIVIDED_CLK;
                    COUNTER := 0;
                end if;
            end if;
        end if;
    end process;
    CLK_1p8432MHz <= DIVIDED_CLK;
end Behavioral;