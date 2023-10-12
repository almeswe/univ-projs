----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 28.09.2023 23:38:11
-- Design Name: 
-- Module Name: summator1b - Behavioral
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

entity summator1b_beh is
    Port ( A : in STD_LOGIC;
           B : in STD_LOGIC;
           C1 : in STD_LOGIC;
           C2 : out STD_LOGIC;
           Z : out STD_LOGIC);
end summator1b_beh;

architecture Behavioral of summator1b_beh is begin
    Z <= C1 XOR (A XOR B);
    C2 <= (C1 AND (A XOR B)) OR (A AND B);
end Behavioral;