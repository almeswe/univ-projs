----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:25:04
-- Design Name: 
-- Module Name: tb_circuit - Behavioral
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

entity tb_circuit is
end tb_circuit;

architecture Behavioral of tb_circuit is
    component circuit_beh is
        Port ( X : in STD_LOGIC;
               Y : in STD_LOGIC;
               Z : in STD_LOGIC;
               F : out STD_LOGIC);
    end component;
    component circuit_struct is
        Port ( X : in STD_LOGIC;
               Y : in STD_LOGIC;
               Z : in STD_LOGIC;
               F : out STD_LOGIC);
    end component;
    signal X, Y, Z: STD_LOGIC := '0';
    signal F_BEH, F_STRUCT: STD_LOGIC;
    signal ERROR: STD_LOGIC := '0';
    constant PERIOD: TIME := 10ns;
begin
    BEH: circuit_beh port map (X, Y, Z, F_BEH);
    STR: circuit_struct port map (X, Y, Z, F_STRUCT);
    X <= NOT X AFTER PERIOD;
    Y <= NOT Y AFTER PERIOD*2;
    Z <= NOT Z AFTER PERIOD*4;
    ERROR <= F_BEH XOR F_STRUCT;
end Behavioral;
