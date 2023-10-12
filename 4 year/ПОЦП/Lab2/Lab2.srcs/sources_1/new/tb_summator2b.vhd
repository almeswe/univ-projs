----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 29.09.2023 09:25:04
-- Design Name: 
-- Module Name: tb_summator2b - Behavioral
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

entity tb_summator2b is
--  Port ( );
end tb_summator2b;

architecture Behavioral of tb_summator2b is
    component summator2b_beh is
        Port ( A1 : in STD_LOGIC;
               A2 : in STD_LOGIC;
               B1 : in STD_LOGIC;
               B2 : in STD_LOGIC;
               C1 : in STD_LOGIC;
               C2 : out STD_LOGIC;
               Z1 : out STD_LOGIC;
               Z2 : out STD_LOGIC);
    end component;
    component summator2b_struct is
        Port ( A1 : in STD_LOGIC;
               A2 : in STD_LOGIC;
               B1 : in STD_LOGIC;
               B2 : in STD_LOGIC;
               C1 : in STD_LOGIC;
               C2 : out STD_LOGIC;
               Z1 : out STD_LOGIC;
               Z2 : out STD_LOGIC);
    end component;
    signal ERROR: STD_LOGIC := '0';
    signal A1, A2, B1, B2, C1, C2: STD_LOGIC := '0';
    signal Z_BEH, Z_STRUCT: STD_LOGIC_VECTOR(0 to 1) := ('0','0');
    constant PERIOD: TIME := 10ns;
begin
   BEH: summator2b_beh port map (A1, A2, B1, B2, C1, C2, Z_BEH(0), Z_BEH(1));
   STR: summator2b_struct port map (A1, A2, B1, B2, C1, C2, Z_STRUCT(0), Z_STRUCT(1));
   A1 <= NOT A1 AFTER PERIOD;
   A2 <= NOT A2 AFTER PERIOD*2;
   B1 <= NOT B1 AFTER PERIOD*4;
   B2 <= NOT B2 AFTER PERIOD*8;
   C1 <= NOT C1 AFTER PERIOD*16;
   C2 <= NOT C2 AFTER PERIOD*32;
   ERROR <= (Z_BEH(0) XOR Z_STRUCT(0)) OR
            (Z_BEH(1) XOR Z_STRUCT(1));
End Behavioral;
