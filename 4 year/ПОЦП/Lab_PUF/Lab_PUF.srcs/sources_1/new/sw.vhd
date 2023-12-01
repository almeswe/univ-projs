library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity sw is
    Port (
       A : in STD_LOGIC;
       B : in STD_LOGIC;
       S : in STD_LOGIC;
       X : out STD_LOGIC;
       Y : out STD_LOGIC
    );
end sw;

architecture Structural of sw is
    component mux2x1 is
        Port (
           A : in STD_LOGIC;
           B : in STD_LOGIC;
           S : in STD_LOGIC;
           Q : out STD_LOGIC
        );
    end component;
begin
    MUX_0: mux2x1 port map (
        A => A,
        B => B,
        S => S,
        Q => X
    );
    MUX_1: mux2x1 port map (
        A => B,
        B => A,
        S => S,
        Q => Y
    );
end Structural;
