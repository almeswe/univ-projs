library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity mgen_impl_beh is
    Port (
        RST : in STD_LOGIC;
        CLK : in STD_LOGIC;
        Pout : out STD_LOGIC_VECTOR(0 to 4)
    );
end mgen_impl_beh;

architecture Behavioral of mgen_impl_beh is
    -- poly: 1 xor x^2 xor x^5
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal Atemp: STD_LOGIC_VECTOR(0 to 4);
begin
    MAIN: process (RST, CLK, Atemp) begin
        if RST = '1' then
            Atemp <= "11111";
        elsif (RISING_EDGE(CLK)) then
            Atemp(0) <= Atemp(1) xor Atemp(4);
            Atemp(1) <= Atemp(0);
            Atemp(2) <= Atemp(1);
            Atemp(3) <= Atemp(2);
            Atemp(4) <= Atemp(3);
        end if;
    end process;
    Pout <= Atemp;
end Behavioral;
