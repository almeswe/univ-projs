library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity ab is
    Port (
        A: in STD_LOGIC;
        B: in STD_LOGIC;
        EN: in STD_LOGIC;
        RST: in STD_LOGIC;
        Q: out STD_LOGIC
    );
end ab;

architecture Structural of ab is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component ffd is
        Port (
            D: in STD_LOGIC;
            EN: in STD_LOGIC;
            CLK: in STD_LOGIC;
            RST: in STD_LOGIC;
            Q: out STD_LOGIC
        );
    end component;
    
    component ld is
        Port (
            D: in STD_LOGIC;
            EN: in STD_LOGIC;
            Q: out STD_LOGIC
        );
    end component;
begin
    --FFD_0: ffd port map (
    --    D => B,
    --    EN => EN,
    --    CLK => A,
    --    RST => RST,
    --    Q => Q
    --);
    LD_0: ld port map (
        D => B,
        EN => A,
        Q => Q
    );
end Structural;