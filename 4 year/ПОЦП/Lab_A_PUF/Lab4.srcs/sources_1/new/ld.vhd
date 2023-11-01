library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity ld is
    Port ( D : in STD_LOGIC;
           G : in STD_LOGIC;
           Q : out STD_LOGIC);
end ld;

architecture Behavioral of ld is begin
    MAIN: process (D,G) begin
        if (G = '1') then
            Q <= D;
        end if;
    end process;
end Behavioral;
