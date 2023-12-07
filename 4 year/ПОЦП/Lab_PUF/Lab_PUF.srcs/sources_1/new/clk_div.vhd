library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity clk_div is
    Generic (
        DIVISOR: INTEGER := 1
    );
    Port (
        RST: in STD_LOGIC;
        CLK_IN: in STD_LOGIC;
        CLK_OUT: out STD_LOGIC
    );
end clk_div;

architecture Behavioral of clk_div is
    signal CLK_OUT_INT: STD_LOGIC := '0';
    signal COUNTER: INTEGER range 0 to DIVISOR-1 := 0;
begin
    process (CLK_IN, RST) begin
        if RST = '1' then 
            COUNTER <= 0;
            CLK_OUT_INT <= '0';
        elsif RISING_EDGE(CLK_IN) then
            if COUNTER = DIVISOR-1 then
                COUNTER <= 0;
                CLK_OUT_INT <= NOT CLK_OUT_INT;
            else
                COUNTER <= COUNTER + 1;
            end if;
        end if;
    end process;
    CLK_OUT <= CLK_OUT_INT;
end Behavioral;
