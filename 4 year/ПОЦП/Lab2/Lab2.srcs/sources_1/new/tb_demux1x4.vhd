----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:25:04
-- Design Name: 
-- Module Name: tb_demux1x4 - Behavioral
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

entity tb_demux1x4 is
end tb_demux1x4;

architecture Behavioral of tb_demux1x4 is
    component demux1x4_struct is
        Port ( X : in STD_LOGIC;
               S1 : in STD_LOGIC;
               S2 : in STD_LOGIC;
               D0 : out STD_LOGIC;
               D1 : out STD_LOGIC;
               D2 : out STD_LOGIC;
               D3 : out STD_LOGIC);
    end component;
    component demux1x4_beh is
        Port ( X : in STD_LOGIC;
               S1 : in STD_LOGIC;
               S2 : in STD_LOGIC;
               D0 : out STD_LOGIC;
               D1 : out STD_LOGIC;
               D2 : out STD_LOGIC;
               D3 : out STD_LOGIC);
    end component;
    
    signal ERROR: STD_LOGIC := '0';
    signal X, S1, S2: STD_LOGIC := '0';
    signal D_BEH, D_STRUCT: STD_LOGIC_VECTOR(0 TO 3);
    constant PERIOD: TIME := 10ns;
begin
    UUT_BEH: demux1x4_beh port map (X, S1, S2, D_BEH(0), D_BEH(1), D_BEH(2), D_BEH(3));
    UUT_STR: demux1x4_struct port map (X, S1, S2, D_STRUCT(0), D_STRUCT(1), D_STRUCT(2), D_STRUCT(3));
    X <= NOT X AFTER PERIOD;
    S1 <= NOT S1 AFTER PERIOD*2;
    S2 <= NOT S2 AFTER PERIOD*4;
    ERROR <= (D_BEH(0) XOR D_STRUCT(0)) OR
             (D_BEH(1) XOR D_STRUCT(1)) OR
             (D_BEH(2) XOR D_STRUCT(2)) OR
             (D_BEH(3) XOR D_STRUCT(3));
end Behavioral;
