----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:25:04
-- Design Name: 
-- Module Name: tb_summator1b - Behavioral
-- Project Name: 
-- Target Devices: 
-- Tool Versions: 
-- Description: 
-- 
-- Dependencies: 
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
-- 
----------------------------------------------------------------------------------


library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity tb_summator1b is
end tb_summator1b;

architecture Behavioral of tb_summator1b is
    component summator1b_beh is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           C1 : in STD_LOGIC;
           C2 : out STD_LOGIC;
           Z : out STD_LOGIC);
    end component;
    component summator1b_struct is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               C1 : in STD_LOGIC;
               C2 : out STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal ERROR: STD_LOGIC := '0';
    signal A, B, C1, C2: STD_LOGIC := '0';
    signal Z_BEH, Z_STRUCT: STD_LOGIC;
    constant PERIOD: TIME := 10ns;
begin
   BEH: summator1b_beh port map(A, B, C1, C2, Z_BEH);
   STR: summator1b_struct port map(A, B, C1, C2, Z_STRUCT);
   A <= NOT A AFTER PERIOD;
   B <= NOT B AFTER PERIOD*2;
   C1 <= NOT C1 AFTER PERIOD*8;
   C2 <= NOT C2 AFTER PERIOD*16;
   ERROR <= Z_BEH XOR Z_STRUCT;
End Behavioral;