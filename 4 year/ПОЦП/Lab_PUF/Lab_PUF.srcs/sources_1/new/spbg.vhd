library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity spbg is
    Generic (
        N: integer := 128
    );
    Port (
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        S: out STD_LOGIC_VECTOR (0 to N-1)
    );
end spbg;

architecture Structural of spbg is
    component mgen is
        Generic (
            SEED: STD_LOGIC_VECTOR(0 to 31) := NOT x"10010111" 
        );
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            Q: out STD_LOGIC
        );
    end component;
    component sreg is
        Generic (
            N: integer := N
        );
        Port ( 
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            SIN: in STD_LOGIC;
            SOUT: out STD_LOGIC_VECTOR(0 to N-1)
        );
    end component;
    signal MGEN_Q: STD_LOGIC; 
begin
    MGEN32_0: mgen port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        Q => MGEN_Q
    );
    SREG_0: sreg port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        SIN => MGEN_Q,
        SOUT => S
    );
end Structural;
