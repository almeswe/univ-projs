library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tpg is
    Port (
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        X: out STD_LOGIC;
        Y: out STD_LOGIC
    );
end tpg;

architecture Structural of tpg is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component mgen is
        Generic (
            SEED: STD_LOGIC_VECTOR(0 to 31) := x"10010111" 
        );
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            Q: out STD_LOGIC
        );
    end component;
    signal TEMP_Q: STD_LOGIC;
begin
    MGEN32_0: mgen port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        Q => TEMP_Q
    );
    X <= TEMP_Q;
    Y <= TEMP_Q;
end Structural;