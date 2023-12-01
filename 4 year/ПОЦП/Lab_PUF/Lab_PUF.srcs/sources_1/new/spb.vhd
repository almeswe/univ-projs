library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity spb is
    Generic (
        N: integer := 8
    );
    Port (
        A: in STD_LOGIC;
        B: in STD_LOGIC;
        S: in STD_LOGIC_VECTOR(0 to N-1);
        X: out STD_LOGIC;
        Y: out STD_LOGIC
    );
end spb;

architecture Behavioral of spb is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
    component sw is
        Port (
           A: in STD_LOGIC;
           B: in STD_LOGIC;
           S: in STD_LOGIC;
           X: out STD_LOGIC;
           Y: out STD_LOGIC
        );
    end component;
    signal OUT_X, OUT_Y: STD_LOGIC_VECTOR(0 to N-1);
begin
    SW_0: sw port map (
        A => A,
        B => B,
        S => S(0),
        X => OUT_X(0),
        Y => OUT_Y(0)
    );
    GENSCH: for i in 1 to N-1 generate
        SW_I: sw port map (
            A => OUT_X(i-1),
            B => OUT_Y(i-1),
            S => S(i),
            X => OUT_X(i),
            Y => OUT_Y(i)
        );
    end generate;
    X <= OUT_X(N-1);
    Y <= OUT_Y(N-1);
end Behavioral;
