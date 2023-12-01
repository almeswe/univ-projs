library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity sreg8 is
    Port ( 
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        SIN: in STD_LOGIC;
        SOUT: out STD_LOGIC_VECTOR(0 to 7)
    );
end sreg8;

architecture Bahavioral of sreg8 is
    component sreg is
        Generic (
            N: integer := 8
        );
        Port ( 
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            SIN: in STD_LOGIC;
            SOUT: out STD_LOGIC_VECTOR(0 to N-1)
        );
    end component;
begin
    SREG_0: sreg
    generic map (
        N => 8
    )
    port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        SIN => SIN,
        SOUT => SOUT
    );
end Bahavioral;
