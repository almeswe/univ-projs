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

entity mux4x2_struct is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           A1 : in STD_LOGIC;
           B1 : in STD_LOGIC;
           S : in STD_LOGIC;
           Z : out STD_LOGIC;
           Z1 : out STD_LOGIC);
end mux4x2_struct;

architecture Structural of mux4x2_struct is
    component mux2x1 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               S : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
begin
    X1: mux2x1 port map (A, B, S, Z);
    X2: mux2x1 port map (A1, B1, S, Z1);
end Structural;
