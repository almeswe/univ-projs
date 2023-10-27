library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity async_reg_beh is
    Generic (
        N: integer := 8
    );
    Port ( 
        Din : in STD_LOGIC_VECTOR (0 to N-1);
        EN : in STD_LOGIC;
        Dout : out STD_LOGIC_VECTOR (0 to N-1)
    );  
end async_reg_beh;

architecture Behavioral of async_reg_beh is begin
    MAIN: process (EN, Din) begin
        if (EN = '1') then 
            for i in 0 to N-1 loop
               Dout(i) <= Din(i); 
            end loop;
        end if;
    end process;
end Behavioral;