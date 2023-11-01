library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity async_reg_str is
    Generic (
        N: integer := 8
    );
    Port ( 
        Din : in STD_LOGIC_VECTOR (0 to N-1);
        EN : in STD_LOGIC;
        Dout : out STD_LOGIC_VECTOR (0 to N-1)
    );
end async_reg_str;

architecture Structural of async_reg_str is
    component ld is
        Port ( 
            D : in STD_LOGIC;
            G : in STD_LOGIC;
            Q : out STD_LOGIC
        );
    end component; 
begin
    GENSCH: for i in 0 to N-1 generate
        LDI: ld port map (
            D => Din(i),
            G => EN,
            Q => Dout(i)
        );
    end generate;
end Structural;