----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 28.09.2023 21:14:36
-- Design Name: 
-- Module Name: mux2x1_str - Behavioral
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

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx leaf cells in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity mux2x1 is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           S : in STD_LOGIC;
           Z : out STD_LOGIC);
end mux2x1;

architecture Structural of mux2x1 is
    component not1 is 
        Port ( A : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component and2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component or2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal X1, Y1, Y2: STD_LOGIC;
begin
    A1: not1 port map(S, X1);
    A2: and2 port map(A, X1, Y1);
    A3: and2 port map(B, S, Y2);
    A4: or2 port map(Y1, Y2, Z);
end Structural;
