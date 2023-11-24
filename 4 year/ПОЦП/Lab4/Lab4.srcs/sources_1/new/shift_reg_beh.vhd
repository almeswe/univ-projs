library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity shift_reg_beh is
    Generic (
        N: integer := 8
    );
    Port (
        Sin : in STD_LOGIC;
        SE : in STD_LOGIC;
        CLK : in STD_LOGIC;
        RST : in STD_LOGIC;
        Pout : out STD_LOGIC_VECTOR (0 to N-1)
     );
end shift_reg_beh;

architecture Behavioral of shift_reg_beh is 
    signal Ptemp: STD_LOGIC_VECTOR(0 to N-1);
begin
    MAIN: process (Sin, SE, CLK, RST) begin
        if (RST = '1') then
            for i in 0 to N-1 loop
                Ptemp(i) <= '0';
            end loop;
        elsif (SE = '1' and RISING_EDGE(CLK)) then
            Ptemp(0) <= Sin;
            for i in 1 to N-1 loop
                Ptemp(i) <= Ptemp(i-1);
            end loop;
        end if;
    end process;
    Pout <= Ptemp;
end Behavioral;