----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 30.11.2023 15:29:24
-- Design Name: 
-- Module Name: a_puf_dummy - Behavioral
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

entity a_puf_dummy is
    Generic (
        N: integer := 8
    );
    Port (
        EN : in STD_LOGIC;
        CLK : in STD_LOGIC;
        RST : in STD_LOGIC;
        S : in STD_LOGIC_VECTOR (0 to N-1);
        Q : out STD_LOGIC_VECTOR (0 to N-1)
    );
end a_puf_dummy;

architecture Behavioral of a_puf_dummy is
    attribute dont_touch: string;
    attribute dont_touch of Behavioral : architecture is "true";
begin
    Q <= "00111100";
end Behavioral;
