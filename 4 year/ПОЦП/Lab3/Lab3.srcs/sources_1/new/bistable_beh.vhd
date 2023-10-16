library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity bistable_beh is
    Port ( Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end bistable_beh;

architecture Behavioral of bistable_beh is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal inv_out: std_logic_vector(0 to 2);
begin
    inv_out(0) <= not inv_out(1);
    inv_out(1) <= not inv_out(0);
    nQ <= inv_out(0);
    Q <= inv_out(1);
end Behavioral;
