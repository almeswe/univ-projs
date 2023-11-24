

library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity sync_reg_str is
    Generic (
        N: integer := 8
    );
    Port (
        Din : in STD_LOGIC_VECTOR (0 to N-1);
        EN : in STD_LOGIC;
        CLK : in STD_LOGIC;
        Dout : out STD_LOGIC_VECTOR (0 to N-1)
    );
end sync_reg_str;

architecture Structural of sync_reg_str is 
    component fdce is
        Port ( CLR : in STD_LOGIC;
               CE : in STD_LOGIC;
               D : in STD_LOGIC;
               CLK : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
begin
    GENSCH: for i in 0 to N-1 generate
        FDCEI: fdce port map (
            CLR => '0',
            CE => EN,
            D => Din(i),
            CLK => CLK,
            Q => Dout(i)
        );
    end generate;
end Structural;