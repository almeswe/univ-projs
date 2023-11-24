library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity mux4x2 is
    port (
        A1 : in STD_LOGIC;
        A2 : in STD_LOGIC;
        B1 : in STD_LOGIC;
        B2 : in STD_LOGIC;
        S : in STD_LOGIC;
        Q1 : out STD_LOGIC;
        Q2 : out STD_LOGIC
    );
end mux4x2;

architecture Behavioral of mux4x2 is begin
    mux_proc: process(A1, A2, B1, B2) begin
        if S='0' then
            Q1 <= A1; Q2 <= A2;
        end if;
        if S='1' then
            Q1 <= B1; Q2 <= B2;
        end if;
    end process mux_proc;
end Behavioral;
