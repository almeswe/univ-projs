library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity mgen is
    Generic (
        SEED: STD_LOGIC_VECTOR(0 to 31) := x"10010111" 
    );
    Port (
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        Q: out STD_LOGIC
    );
end mgen;

architecture Behavioral of mgen is
    -- 1,5,6,31 taps
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    
    signal TEMP: STD_LOGIC_VECTOR(0 to 31);
    signal Q_TEMP: STD_LOGIC;
begin
    MAIN: process (EN, CLK, RST, TEMP) begin
        if RST = '1' then
            TEMP <= SEED;
        elsif EN = '1' AND RISING_EDGE(CLK) then
            for i in 1 to 31 loop
                TEMP(i) <= TEMP(i-1);
            end loop;
            if TEMP = x"00000000" then
                TEMP(0) <= '1';
            else
                TEMP(0) <= TEMP(4) XOR 
                           TEMP(5) XOR
                           TEMP(30);
            end if;
            Q_TEMP <= TEMP(31);
        end if;
    end process;
    Q  <= Q_TEMP;
end Behavioral;
