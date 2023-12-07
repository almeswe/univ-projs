library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity sreg is
    Generic (
        N: integer := 8
    );
    Port ( 
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        SIN: in STD_LOGIC;
        SOUT: out STD_LOGIC_VECTOR(0 to N-1)
    );
end sreg;

architecture Behavioral of sreg is
    signal SREG: STD_LOGIC_VECTOR(0 to N-1);
begin
    process (EN, CLK, RST, SIN) begin
        if RST = '1' then 
            SREG <= (others => '0');
        elsif EN = '1' AND RISING_EDGE(CLK) then
            for i in 1 to SREG'length-1 loop
                SREG(i) <= SREG(i-1);
            end loop;
            SREG(0) <= SIN;
        end if;
    end process;
    SOUT <= SREG;
end Behavioral;
