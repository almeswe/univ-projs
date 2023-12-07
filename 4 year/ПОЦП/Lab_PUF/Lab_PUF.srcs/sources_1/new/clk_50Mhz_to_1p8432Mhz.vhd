-----------------------------------------------------------
--           UART | 1.8432 MHz Clock Generator
-----------------------------------------------------------
--
-- Copyright (c) 2008, Thijs van As <t.vanas@gmail.com>
--
-----------------------------------------------------------
-- Input:      clk        | System clock
--             reset      | System reset
--
-- Output:     clk_18432  | 1.8432 MHz clock
-----------------------------------------------------------
-- Generates a 1.8432 MHz clock signal from a 50 MHz input
-- clock signal. From this signal, 115200 bps baud ticks
-- can be easily generated (divide by 16)
--
-- 50 MHz * 1152/15625 = 3.6864 MHz (high/low switching
-- frequency)
-----------------------------------------------------------
-- clk_18432.vhd
-----------------------------------------------------------

library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;
use ieee.numeric_std.all;

entity clk_50Mhz_to_1p8432Mhz is
    Port (
        RST: in std_logic;
        CLK_50MHz: in std_logic;
        CLK_1p8432MHz: out std_logic
    );
end clk_50Mhz_to_1p8432Mhz;

architecture behavioural of clk_50Mhz_to_1p8432Mhz is
    constant NUMERATOR   : std_logic_vector(14 downto 0) := "000010010000000";  --  1152;
    constant DENOMINATOR : std_logic_vector(14 downto 0) := "011110100001001";  -- 15625;

    signal clk_out : std_logic                     := '0';
    signal counter : std_logic_vector(14 downto 0) := (others => '0');
begin
    CLK_1p8432MHz <= clk_out;

    process (CLK_50MHz, RST)
    begin
        if (RST = '1') then
            clk_out <= '1';
            counter <= DENOMINATOR - NUMERATOR - 1;
        elsif (CLK_50MHz = '1' and CLK_50MHz'event) then
            if counter(14) = '1' then
                clk_out <= not clk_out;
                counter <= counter + DENOMINATOR - NUMERATOR;
            else
                counter <= counter - NUMERATOR;
            end if;
        end if;
    end process;

end architecture behavioural;