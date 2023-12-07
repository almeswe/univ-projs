library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity mem8 is
    Port ( 
       EN: in STD_LOGIC;
       CLK: in STD_LOGIC;
       RST: in STD_LOGIC;
       SIN: in STD_LOGIC;
       SOUT: out STD_LOGIC_VECTOR (0 to 7)
     );
end mem8;

architecture Structural of mem8 is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component sreg8 is
        Port ( 
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            SIN: in STD_LOGIC;
            SOUT: out STD_LOGIC_VECTOR(0 to 7)
        );
    end component;
begin
    SREG8_0: sreg8 port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        SIN => SIN,
        SOUT => SOUT
    );
end Structural;