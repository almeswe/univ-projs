----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 28.09.2023 23:38:11
-- Design Name: 
-- Module Name: summator2b - Behavioral
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

entity summator2b_beh is
    Port ( A1 : in STD_LOGIC;
           A2 : in STD_LOGIC;
           B1 : in STD_LOGIC;
           B2 : in STD_LOGIC;
           C1 : in STD_LOGIC;
           C2 : out STD_LOGIC;
           Z1 : out STD_LOGIC;
           Z2 : out STD_LOGIC);
end summator2b_beh;

architecture Behavioral of summator2b_beh is 
    signal CF: STD_LOGIC;
begin 
    Z1 <= C1 XOR (A1 XOR A2);
    CF <= (C1 AND (A1 XOR A2)) OR (A1 AND A2);
    Z2 <= CF XOR (B1 XOR B2);
    C2 <= (CF AND (B1 XOR B2)) OR (B1 AND B2);
end Behavioral;
