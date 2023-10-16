library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity jktrigger_beh is
    Port ( J : in STD_LOGIC;
           K : in STD_LOGIC;
           C : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end jktrigger_beh;

architecture Behavioral of jktrigger_beh is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    signal tmp: std_logic_vector(0 to 1);
begin
    Q  <= tmp(0);
    nQ <= tmp(1);
    p: process(J, K, C) begin
        if (rising_edge(C)) then
            if (J = '1' AND K = '0') then 
                tmp(0) <= '1';
                tmp(1) <= '0';
            elsif (J = '0' AND K = '1') then
                tmp(0) <= '0';
                tmp(1) <= '1';
            elsif (J = '1' AND K = '1') then
                tmp(0) <= NOT tmp(0);
                tmp(1) <= NOT tmp(1);
            end if;
        end if;
    end process;
end Behavioral;