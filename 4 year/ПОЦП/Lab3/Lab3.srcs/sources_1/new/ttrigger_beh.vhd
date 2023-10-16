library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity ttrigger_beh is
    Port ( T : in STD_LOGIC;
           C : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end ttrigger_beh;

architecture Behavioral of ttrigger_beh is 
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal tmp: STD_LOGIC_VECTOR(0 to 1) := ('0', '1');
begin
    Q  <= tmp(0);
    nQ <= tmp(1);
    p: process(T, C) begin
        if (rising_edge(C)) then
            if (T = '1') then
                tmp(0) <= NOT tmp(0);
                tmp(1) <= NOT tmp(1);            
            end if;
        end if;
     end process;
end Behavioral;
