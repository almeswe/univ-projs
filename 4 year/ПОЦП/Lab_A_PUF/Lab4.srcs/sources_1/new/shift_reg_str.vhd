library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity shift_reg_str is
    Generic (
        N: integer := 8
    );
    Port (
       Sin : in STD_LOGIC;
       SE : in STD_LOGIC;
       CLK : in STD_LOGIC;
       RST : in STD_LOGIC;
       Pout : out STD_LOGIC_VECTOR (0 to N-1)
     );
end shift_reg_str;

architecture Structural of shift_reg_str is
    component fdce is
        Port (
           CLR : in STD_LOGIC;
           CE : in STD_LOGIC;
           D : in STD_LOGIC;
           CLK : in STD_LOGIC;
           Q : out STD_LOGIC
        );
    end component;
    signal Ptemp: STD_LOGIC_VECTOR (0 to N-1);
begin
    FDCE0: fdce port map (
        CLR => RST,
        CE  => SE,
        D   => Sin,
        CLK => CLK,
        Q   => Ptemp(0)
    );
    GENSCH: for i in 1 to N-1 generate
        FDCEI: fdce port map (
            CLR => RST,
            CE  => SE,
            D   => Ptemp(i-1),
            CLK => CLK,
            Q   => Ptemp(i)
        );
    end generate;
    Pout <= Ptemp;  
end Structural;
