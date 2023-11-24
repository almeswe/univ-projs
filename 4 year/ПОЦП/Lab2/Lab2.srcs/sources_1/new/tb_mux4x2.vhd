----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:25:04
-- Design Name: 
-- Module Name: tb_mux4x2 - Behavioral
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

entity tb_mux4x2 is
end tb_mux4x2;

architecture Behavioral of tb_mux4x2 is
    component mux4x2_beh is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           A1 : in STD_LOGIC;
           B1 : in STD_LOGIC;
           S : in STD_LOGIC;
           Z : out STD_LOGIC;
           Z1 : out STD_LOGIC);
    end component;
    component mux4x2_struct is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               A1 : in STD_LOGIC;
               B1 : in STD_LOGIC;
               S : in STD_LOGIC;
               Z : out STD_LOGIC;
               Z1 : out STD_LOGIC);
    end component;
    signal A, B, A1, B1, S: STD_LOGIC := '0';
    signal Z_BEH, Z_STRUCT: STD_LOGIC_VECTOR(0 TO 1) := ('0','0');
    signal ERROR: STD_LOGIC := '0';
    constant PERIOD: TIME := 10ns;
begin
    BEH: mux4x2_beh port map (A, B, A1, B1, S, Z_BEH(0), Z_BEH(1));
    STR: mux4x2_struct port map (A, B, A1, B1, S, Z_STRUCT(0), Z_STRUCT(1));
    A <= NOT A AFTER PERIOD;
    B <= NOT B AFTER PERIOD*2;
    A1 <= NOT A1 AFTER PERIOD*4;
    B1 <= NOT B1 AFTER PERIOD*8;
    S <= NOT S AFTER PERIOD*16;
    ERROR <= (Z_BEH(0) XOR Z_STRUCT(0)) OR
             (Z_BEH(1) XOR Z_STRUCT(1));
end Behavioral;