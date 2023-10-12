----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 10:36:17
-- Design Name: 
-- Module Name: circuit_struct - Behavioral
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


entity circuit_struct is
    Port ( X : in STD_LOGIC;
           Y : in STD_LOGIC;
           Z : in STD_LOGIC;
           F : out STD_LOGIC);
end circuit_struct;

architecture Structural of circuit_struct is
    component and3 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               C : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component or2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component and2 is
        Port ( A : in STD_LOGIC;
               B : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    component not1 is
        Port ( A : in STD_LOGIC;
               Z : out STD_LOGIC);
    end component;
    
    signal X1, X2, X3: STD_LOGIC;
    signal Y1: STD_LOGIC;
    signal Z1, Z2: STD_LOGIC;
begin
    A1: not1 port map (X, X2);
    A2: not1 port map (Y, X1);
    A3: not1 port map (Z, X3);
    B1: or2 port map (X, X1, Y1);
    C1: and2 port map (Y1, Z, Z1);
    C2: and3 port map (X2, Y, X3, Z2);
    D1: or2 port map (Z1, Z2, F);
end Structural;
