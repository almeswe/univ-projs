----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:31:14
-- Design Name: 
-- Module Name: demux1x4_struct - Behavioral
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

entity demux1x4_struct is
    Port ( X : in STD_LOGIC;
           S1 : in STD_LOGIC;
           S2 : in STD_LOGIC;
           D0 : out STD_LOGIC;
           D1 : out STD_LOGIC;
           D2 : out STD_LOGIC;
           D3 : out STD_LOGIC);
end demux1x4_struct;

architecture Structural of demux1x4_struct is 
    component not1 is
        Port ( A : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component and3 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               C : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal Z: STD_LOGIC_VECTOR (3 DOWNTO 0);
begin 
    Z1: not1 port map (S1, Z(0));
    Z2: not1 port map (S2, Z(1));
    Z3: not1 port map (S1, Z(2));
    Z4: not1 port map (S2, Z(3));
    Y1: and3 port map (X, Z(0), Z(1), D0);
    Y2: and3 port map (X, Z(2), S2, D1);
    Y3: and3 port map (X, S1, Z(3), D2);
    Y4: and3 port map (X, S1, S2, D3);
end Structural;