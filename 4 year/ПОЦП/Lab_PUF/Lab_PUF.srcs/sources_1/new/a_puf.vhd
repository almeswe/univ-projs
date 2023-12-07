library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity a_puf is
    Generic (
        N: integer := 128
    );
    Port (
        EN: in STD_LOGIC;
        CLK: in STD_LOGIC;
        RST: in STD_LOGIC;
        Q: out STD_LOGIC_VECTOR(0 to 7)
    );
end a_puf;

architecture Structural of a_puf is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component tpg is
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            X: out STD_LOGIC;
            Y: out STD_LOGIC
        );
    end component;
    component spb is
        Generic (
            N: integer := N
        );
        Port (
            A: in STD_LOGIC;
            B: in STD_LOGIC;
            S: in STD_LOGIC_VECTOR (0 to N-1);
            X: out STD_LOGIC;
            Y: out STD_LOGIC
        );
    end component;
    component spbg is
        Generic (
            N: integer := N
        );
        Port (
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            S: out STD_LOGIC_VECTOR (0 to N-1)
        );
    end component;
    component ab is
        Port (
            A: in STD_LOGIC;
            B: in STD_LOGIC;
            EN: in STD_LOGIC;
            RST: in STD_LOGIC;
            Q: out STD_LOGIC
        );
    end component;
    component mem8 is
        Port (
           EN: in STD_LOGIC;
           CLK: in STD_LOGIC;
           RST: in STD_LOGIC;
           SIN: in STD_LOGIC;
           SOUT: out STD_LOGIC_VECTOR (0 to 7)
        );
    end component;
    signal AB_Q: STD_LOGIC;
    signal TPG_X, TPG_Y: STD_LOGIC;
    signal SPB_X, SPB_Y: STD_LOGIC;
    signal SPBG_S: STD_LOGIC_VECTOR(0 to N-1);
begin
    TPG_0: tpg port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        X => TPG_X,
        Y => TPG_Y
    );
    SPB_0: spb port map (
        A => TPG_X,
        B => TPG_Y,
        S => SPBG_S,
        X => SPB_X,
        Y => SPB_Y
    );
    SPBG_0: spbg port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        S => SPBG_S
    );
    AB_0: ab port map (
        A => SPB_X,
        B => SPB_Y,
        EN => EN,
        RST => RST,
        Q => AB_Q
    );
    MEM8_0: mem8 port map (
        EN => EN,
        CLK => CLK,
        RST => RST,
        SIN => AB_Q,
        SOUT => Q
    );
end Structural;
