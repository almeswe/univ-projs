library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity analyzer_beh is
    Port ( 
       Sin : in STD_LOGIC;
       CLK : in STD_LOGIC;
       CLR : in STD_LOGIC;
       INT : out STD_LOGIC_VECTOR(0 to 4);
       Q : out STD_LOGIC
     );
end analyzer_beh;

architecture behavioral of analyzer_beh is
    signal state: std_logic_vector(0 to 4);	
begin
    MAIN: process (CLR, CLK) begin
        if CLR = '1' then
            state <= "00000";
        elsif RISING_EDGE(CLK) then            
            state <= (state(4) xor Sin)      & 
                     (state(0))              & 
                     (state(1) xor state(4)) &
                     (state(2 to 3));
        end if;
    end process;    
    Q <= state(4);
    INT <= state;
end architecture behavioral;