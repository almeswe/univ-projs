----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 28.09.2023 23:29:45
-- Design Name: 
-- Module Name: xor2 - Behavioral
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

entity xor2 is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           Z : out STD_LOGIC);
end xor2;

architecture Structural of xor2 is
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
    component not1 is
        Port ( A : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    signal X: STD_LOGIC_VECTOR(0 to 3);
begin
    Z1: not1 port map (A, X(0));
    Z2: not1 port map (B, X(1));
    Z3: and2 port map (X(0), B, X(2));
    Z4: and2 port map (A, X(1), X(3));
    Z5: or2 port map (X(2), X(3), Z);
end Structural;
