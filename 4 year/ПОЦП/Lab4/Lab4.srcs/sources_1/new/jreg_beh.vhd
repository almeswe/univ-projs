library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity jreg_beh is
    Generic (
        N: integer := 8
    );
    Port (
        CLR : in STD_LOGIC;
        CLK : in STD_LOGIC;
        Q : out STD_LOGIC
     );
end jreg_beh;

architecture Behavioral of jreg_beh is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal Ptemp: STD_LOGIC_VECTOR(0 to N-1);
begin
    MAIN: process (CLR, CLK, Ptemp) begin
        if CLR = '1' then 
           for i in 0 to N-1 loop
                Ptemp(i) <= '0';
           end loop; 
        elsif (RISING_EDGE(CLK)) then
            Ptemp(0) <= not Ptemp(N-1);
            for i in 1 to N-1 loop
                Ptemp(i) <= Ptemp(i-1);
            end loop;
        end if;
    end process;
    Q <= Ptemp(N-1);
end Behavioral;