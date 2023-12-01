library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity ld is
    Port (
        D: in STD_LOGIC;
        EN: in STD_LOGIC;
        Q: out STD_LOGIC
    );
end ld;

architecture Behavioral of ld is 
    signal TEMP: STD_LOGIC;
begin
    process (D, EN) begin
        if EN = '1' then
            TEMP <= D;
        end if;
    end process;
    Q <= TEMP;
end Behavioral;
