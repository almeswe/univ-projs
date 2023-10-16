library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
entity bistable_struct is
    Port ( Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end bistable_struct;

architecture Structural of bistable_struct is
    attribute dont_touch: string;
    attribute dont_touch of Structural : architecture is "true";
    component inv is
        Port ( X : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
    signal inv_out: std_logic_vector(0 to 2);
begin
    INV0: inv port map (X => inv_out(1), Q => inv_out(0));
    INV1: inv port map (X => inv_out(0), Q => inv_out(1));
    nQ <= inv_out(0);
    Q  <= inv_out(1); 
end Structural;
