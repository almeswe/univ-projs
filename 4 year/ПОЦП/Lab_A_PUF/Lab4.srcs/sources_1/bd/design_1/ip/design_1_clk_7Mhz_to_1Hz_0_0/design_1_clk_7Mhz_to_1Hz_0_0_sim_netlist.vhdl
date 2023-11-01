-- Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
-- --------------------------------------------------------------------------------
-- Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
-- Date        : Fri Oct 27 11:16:02 2023
-- Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
-- Command     : write_vhdl -force -mode funcsim
--               E:/Work/Lab4/Lab4.srcs/sources_1/bd/design_1/ip/design_1_clk_7Mhz_to_1Hz_0_0/design_1_clk_7Mhz_to_1Hz_0_0_sim_netlist.vhdl
-- Design      : design_1_clk_7Mhz_to_1Hz_0_0
-- Purpose     : This VHDL netlist is a functional simulation representation of the design and should not be modified or
--               synthesized. This netlist cannot be used for SDF annotated simulation.
-- Device      : xc7a100tcsg324-1
-- --------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz is
  port (
    CLK_1Hz : out STD_LOGIC;
    CLK_7MHz : in STD_LOGIC
  );
  attribute ORIG_REF_NAME : string;
  attribute ORIG_REF_NAME of design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz : entity is "clk_7Mhz_to_1Hz";
end design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz;

architecture STRUCTURE of design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz is
  signal \^clk_1hz\ : STD_LOGIC;
  signal clear : STD_LOGIC;
  signal inn_i_1_n_0 : STD_LOGIC;
  signal p_0_in : STD_LOGIC_VECTOR ( 31 downto 0 );
  signal \reg0_carry__0_i_1_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_2_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_3_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_4_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_5_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_5_n_1\ : STD_LOGIC;
  signal \reg0_carry__0_i_5_n_2\ : STD_LOGIC;
  signal \reg0_carry__0_i_5_n_3\ : STD_LOGIC;
  signal \reg0_carry__0_i_6_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_6_n_1\ : STD_LOGIC;
  signal \reg0_carry__0_i_6_n_2\ : STD_LOGIC;
  signal \reg0_carry__0_i_6_n_3\ : STD_LOGIC;
  signal \reg0_carry__0_i_7_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_i_7_n_1\ : STD_LOGIC;
  signal \reg0_carry__0_i_7_n_2\ : STD_LOGIC;
  signal \reg0_carry__0_i_7_n_3\ : STD_LOGIC;
  signal \reg0_carry__0_n_0\ : STD_LOGIC;
  signal \reg0_carry__0_n_1\ : STD_LOGIC;
  signal \reg0_carry__0_n_2\ : STD_LOGIC;
  signal \reg0_carry__0_n_3\ : STD_LOGIC;
  signal \reg0_carry__1_i_1_n_0\ : STD_LOGIC;
  signal \reg0_carry__1_i_2_n_0\ : STD_LOGIC;
  signal \reg0_carry__1_i_3_n_0\ : STD_LOGIC;
  signal \reg0_carry__1_i_4_n_2\ : STD_LOGIC;
  signal \reg0_carry__1_i_4_n_3\ : STD_LOGIC;
  signal \reg0_carry__1_i_5_n_0\ : STD_LOGIC;
  signal \reg0_carry__1_i_5_n_1\ : STD_LOGIC;
  signal \reg0_carry__1_i_5_n_2\ : STD_LOGIC;
  signal \reg0_carry__1_i_5_n_3\ : STD_LOGIC;
  signal \reg0_carry__1_n_2\ : STD_LOGIC;
  signal \reg0_carry__1_n_3\ : STD_LOGIC;
  signal reg0_carry_i_1_n_0 : STD_LOGIC;
  signal reg0_carry_i_2_n_0 : STD_LOGIC;
  signal reg0_carry_i_3_n_0 : STD_LOGIC;
  signal reg0_carry_i_4_n_0 : STD_LOGIC;
  signal reg0_carry_i_5_n_0 : STD_LOGIC;
  signal reg0_carry_i_5_n_1 : STD_LOGIC;
  signal reg0_carry_i_5_n_2 : STD_LOGIC;
  signal reg0_carry_i_5_n_3 : STD_LOGIC;
  signal reg0_carry_i_6_n_0 : STD_LOGIC;
  signal reg0_carry_i_6_n_1 : STD_LOGIC;
  signal reg0_carry_i_6_n_2 : STD_LOGIC;
  signal reg0_carry_i_6_n_3 : STD_LOGIC;
  signal reg0_carry_i_7_n_0 : STD_LOGIC;
  signal reg0_carry_i_7_n_1 : STD_LOGIC;
  signal reg0_carry_i_7_n_2 : STD_LOGIC;
  signal reg0_carry_i_7_n_3 : STD_LOGIC;
  signal reg0_carry_n_0 : STD_LOGIC;
  signal reg0_carry_n_1 : STD_LOGIC;
  signal reg0_carry_n_2 : STD_LOGIC;
  signal reg0_carry_n_3 : STD_LOGIC;
  signal reg_reg : STD_LOGIC_VECTOR ( 31 downto 0 );
  signal \reg_reg[0]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[0]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[12]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[16]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[20]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[24]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[28]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[4]_i_1_n_7\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_0\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_1\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_2\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_3\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_4\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_5\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_6\ : STD_LOGIC;
  signal \reg_reg[8]_i_1_n_7\ : STD_LOGIC;
  signal NLW_reg0_carry_O_UNCONNECTED : STD_LOGIC_VECTOR ( 3 downto 0 );
  signal \NLW_reg0_carry__0_O_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 downto 0 );
  signal \NLW_reg0_carry__1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 to 3 );
  signal \NLW_reg0_carry__1_O_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 downto 0 );
  signal \NLW_reg0_carry__1_i_4_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 downto 2 );
  signal \NLW_reg0_carry__1_i_4_O_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 to 3 );
  signal \NLW_reg_reg[28]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 to 3 );
begin
  CLK_1Hz <= \^clk_1hz\;
inn_i_1: unisim.vcomponents.LUT2
    generic map(
      INIT => X"6"
    )
        port map (
      I0 => clear,
      I1 => \^clk_1hz\,
      O => inn_i_1_n_0
    );
inn_reg: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => inn_i_1_n_0,
      Q => \^clk_1hz\,
      R => '0'
    );
reg0_carry: unisim.vcomponents.CARRY4
     port map (
      CI => '0',
      CO(3) => reg0_carry_n_0,
      CO(2) => reg0_carry_n_1,
      CO(1) => reg0_carry_n_2,
      CO(0) => reg0_carry_n_3,
      CYINIT => '1',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => NLW_reg0_carry_O_UNCONNECTED(3 downto 0),
      S(3) => reg0_carry_i_1_n_0,
      S(2) => reg0_carry_i_2_n_0,
      S(1) => reg0_carry_i_3_n_0,
      S(0) => reg0_carry_i_4_n_0
    );
\reg0_carry__0\: unisim.vcomponents.CARRY4
     port map (
      CI => reg0_carry_n_0,
      CO(3) => \reg0_carry__0_n_0\,
      CO(2) => \reg0_carry__0_n_1\,
      CO(1) => \reg0_carry__0_n_2\,
      CO(0) => \reg0_carry__0_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => \NLW_reg0_carry__0_O_UNCONNECTED\(3 downto 0),
      S(3) => \reg0_carry__0_i_1_n_0\,
      S(2) => \reg0_carry__0_i_2_n_0\,
      S(1) => \reg0_carry__0_i_3_n_0\,
      S(0) => \reg0_carry__0_i_4_n_0\
    );
\reg0_carry__0_i_1\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"02"
    )
        port map (
      I0 => p_0_in(21),
      I1 => p_0_in(23),
      I2 => p_0_in(22),
      O => \reg0_carry__0_i_1_n_0\
    );
\reg0_carry__0_i_2\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"20"
    )
        port map (
      I0 => p_0_in(18),
      I1 => p_0_in(19),
      I2 => p_0_in(20),
      O => \reg0_carry__0_i_2_n_0\
    );
\reg0_carry__0_i_3\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"04"
    )
        port map (
      I0 => p_0_in(17),
      I1 => p_0_in(16),
      I2 => p_0_in(15),
      O => \reg0_carry__0_i_3_n_0\
    );
\reg0_carry__0_i_4\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"08"
    )
        port map (
      I0 => p_0_in(14),
      I1 => p_0_in(13),
      I2 => p_0_in(12),
      O => \reg0_carry__0_i_4_n_0\
    );
\reg0_carry__0_i_5\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg0_carry__0_i_6_n_0\,
      CO(3) => \reg0_carry__0_i_5_n_0\,
      CO(2) => \reg0_carry__0_i_5_n_1\,
      CO(1) => \reg0_carry__0_i_5_n_2\,
      CO(0) => \reg0_carry__0_i_5_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(24 downto 21),
      S(3 downto 0) => reg_reg(24 downto 21)
    );
\reg0_carry__0_i_6\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg0_carry__0_i_7_n_0\,
      CO(3) => \reg0_carry__0_i_6_n_0\,
      CO(2) => \reg0_carry__0_i_6_n_1\,
      CO(1) => \reg0_carry__0_i_6_n_2\,
      CO(0) => \reg0_carry__0_i_6_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(20 downto 17),
      S(3 downto 0) => reg_reg(20 downto 17)
    );
\reg0_carry__0_i_7\: unisim.vcomponents.CARRY4
     port map (
      CI => reg0_carry_i_5_n_0,
      CO(3) => \reg0_carry__0_i_7_n_0\,
      CO(2) => \reg0_carry__0_i_7_n_1\,
      CO(1) => \reg0_carry__0_i_7_n_2\,
      CO(0) => \reg0_carry__0_i_7_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(16 downto 13),
      S(3 downto 0) => reg_reg(16 downto 13)
    );
\reg0_carry__1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg0_carry__0_n_0\,
      CO(3) => \NLW_reg0_carry__1_CO_UNCONNECTED\(3),
      CO(2) => clear,
      CO(1) => \reg0_carry__1_n_2\,
      CO(0) => \reg0_carry__1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => \NLW_reg0_carry__1_O_UNCONNECTED\(3 downto 0),
      S(3) => '0',
      S(2) => \reg0_carry__1_i_1_n_0\,
      S(1) => \reg0_carry__1_i_2_n_0\,
      S(0) => \reg0_carry__1_i_3_n_0\
    );
\reg0_carry__1_i_1\: unisim.vcomponents.LUT2
    generic map(
      INIT => X"1"
    )
        port map (
      I0 => p_0_in(30),
      I1 => p_0_in(31),
      O => \reg0_carry__1_i_1_n_0\
    );
\reg0_carry__1_i_2\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"01"
    )
        port map (
      I0 => p_0_in(29),
      I1 => p_0_in(28),
      I2 => p_0_in(27),
      O => \reg0_carry__1_i_2_n_0\
    );
\reg0_carry__1_i_3\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"01"
    )
        port map (
      I0 => p_0_in(26),
      I1 => p_0_in(25),
      I2 => p_0_in(24),
      O => \reg0_carry__1_i_3_n_0\
    );
\reg0_carry__1_i_4\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg0_carry__1_i_5_n_0\,
      CO(3 downto 2) => \NLW_reg0_carry__1_i_4_CO_UNCONNECTED\(3 downto 2),
      CO(1) => \reg0_carry__1_i_4_n_2\,
      CO(0) => \reg0_carry__1_i_4_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \NLW_reg0_carry__1_i_4_O_UNCONNECTED\(3),
      O(2 downto 0) => p_0_in(31 downto 29),
      S(3) => '0',
      S(2 downto 0) => reg_reg(31 downto 29)
    );
\reg0_carry__1_i_5\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg0_carry__0_i_5_n_0\,
      CO(3) => \reg0_carry__1_i_5_n_0\,
      CO(2) => \reg0_carry__1_i_5_n_1\,
      CO(1) => \reg0_carry__1_i_5_n_2\,
      CO(0) => \reg0_carry__1_i_5_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(28 downto 25),
      S(3 downto 0) => reg_reg(28 downto 25)
    );
reg0_carry_i_1: unisim.vcomponents.LUT3
    generic map(
      INIT => X"20"
    )
        port map (
      I0 => p_0_in(9),
      I1 => p_0_in(11),
      I2 => p_0_in(10),
      O => reg0_carry_i_1_n_0
    );
reg0_carry_i_2: unisim.vcomponents.LUT3
    generic map(
      INIT => X"80"
    )
        port map (
      I0 => p_0_in(6),
      I1 => p_0_in(8),
      I2 => p_0_in(7),
      O => reg0_carry_i_2_n_0
    );
reg0_carry_i_3: unisim.vcomponents.LUT3
    generic map(
      INIT => X"20"
    )
        port map (
      I0 => p_0_in(3),
      I1 => p_0_in(5),
      I2 => p_0_in(4),
      O => reg0_carry_i_3_n_0
    );
reg0_carry_i_4: unisim.vcomponents.LUT3
    generic map(
      INIT => X"08"
    )
        port map (
      I0 => p_0_in(2),
      I1 => p_0_in(1),
      I2 => reg_reg(0),
      O => reg0_carry_i_4_n_0
    );
reg0_carry_i_5: unisim.vcomponents.CARRY4
     port map (
      CI => reg0_carry_i_6_n_0,
      CO(3) => reg0_carry_i_5_n_0,
      CO(2) => reg0_carry_i_5_n_1,
      CO(1) => reg0_carry_i_5_n_2,
      CO(0) => reg0_carry_i_5_n_3,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(12 downto 9),
      S(3 downto 0) => reg_reg(12 downto 9)
    );
reg0_carry_i_6: unisim.vcomponents.CARRY4
     port map (
      CI => reg0_carry_i_7_n_0,
      CO(3) => reg0_carry_i_6_n_0,
      CO(2) => reg0_carry_i_6_n_1,
      CO(1) => reg0_carry_i_6_n_2,
      CO(0) => reg0_carry_i_6_n_3,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(8 downto 5),
      S(3 downto 0) => reg_reg(8 downto 5)
    );
reg0_carry_i_7: unisim.vcomponents.CARRY4
     port map (
      CI => '0',
      CO(3) => reg0_carry_i_7_n_0,
      CO(2) => reg0_carry_i_7_n_1,
      CO(1) => reg0_carry_i_7_n_2,
      CO(0) => reg0_carry_i_7_n_3,
      CYINIT => reg_reg(0),
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => p_0_in(4 downto 1),
      S(3 downto 0) => reg_reg(4 downto 1)
    );
\reg[0]_i_2\: unisim.vcomponents.LUT1
    generic map(
      INIT => X"1"
    )
        port map (
      I0 => reg_reg(0),
      O => p_0_in(0)
    );
\reg_reg[0]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[0]_i_1_n_7\,
      Q => reg_reg(0),
      R => clear
    );
\reg_reg[0]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => '0',
      CO(3) => \reg_reg[0]_i_1_n_0\,
      CO(2) => \reg_reg[0]_i_1_n_1\,
      CO(1) => \reg_reg[0]_i_1_n_2\,
      CO(0) => \reg_reg[0]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0001",
      O(3) => \reg_reg[0]_i_1_n_4\,
      O(2) => \reg_reg[0]_i_1_n_5\,
      O(1) => \reg_reg[0]_i_1_n_6\,
      O(0) => \reg_reg[0]_i_1_n_7\,
      S(3 downto 1) => reg_reg(3 downto 1),
      S(0) => p_0_in(0)
    );
\reg_reg[10]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[8]_i_1_n_5\,
      Q => reg_reg(10),
      R => clear
    );
\reg_reg[11]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[8]_i_1_n_4\,
      Q => reg_reg(11),
      R => clear
    );
\reg_reg[12]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[12]_i_1_n_7\,
      Q => reg_reg(12),
      R => clear
    );
\reg_reg[12]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[8]_i_1_n_0\,
      CO(3) => \reg_reg[12]_i_1_n_0\,
      CO(2) => \reg_reg[12]_i_1_n_1\,
      CO(1) => \reg_reg[12]_i_1_n_2\,
      CO(0) => \reg_reg[12]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[12]_i_1_n_4\,
      O(2) => \reg_reg[12]_i_1_n_5\,
      O(1) => \reg_reg[12]_i_1_n_6\,
      O(0) => \reg_reg[12]_i_1_n_7\,
      S(3 downto 0) => reg_reg(15 downto 12)
    );
\reg_reg[13]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[12]_i_1_n_6\,
      Q => reg_reg(13),
      R => clear
    );
\reg_reg[14]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[12]_i_1_n_5\,
      Q => reg_reg(14),
      R => clear
    );
\reg_reg[15]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[12]_i_1_n_4\,
      Q => reg_reg(15),
      R => clear
    );
\reg_reg[16]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[16]_i_1_n_7\,
      Q => reg_reg(16),
      R => clear
    );
\reg_reg[16]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[12]_i_1_n_0\,
      CO(3) => \reg_reg[16]_i_1_n_0\,
      CO(2) => \reg_reg[16]_i_1_n_1\,
      CO(1) => \reg_reg[16]_i_1_n_2\,
      CO(0) => \reg_reg[16]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[16]_i_1_n_4\,
      O(2) => \reg_reg[16]_i_1_n_5\,
      O(1) => \reg_reg[16]_i_1_n_6\,
      O(0) => \reg_reg[16]_i_1_n_7\,
      S(3 downto 0) => reg_reg(19 downto 16)
    );
\reg_reg[17]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[16]_i_1_n_6\,
      Q => reg_reg(17),
      R => clear
    );
\reg_reg[18]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[16]_i_1_n_5\,
      Q => reg_reg(18),
      R => clear
    );
\reg_reg[19]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[16]_i_1_n_4\,
      Q => reg_reg(19),
      R => clear
    );
\reg_reg[1]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[0]_i_1_n_6\,
      Q => reg_reg(1),
      R => clear
    );
\reg_reg[20]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[20]_i_1_n_7\,
      Q => reg_reg(20),
      R => clear
    );
\reg_reg[20]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[16]_i_1_n_0\,
      CO(3) => \reg_reg[20]_i_1_n_0\,
      CO(2) => \reg_reg[20]_i_1_n_1\,
      CO(1) => \reg_reg[20]_i_1_n_2\,
      CO(0) => \reg_reg[20]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[20]_i_1_n_4\,
      O(2) => \reg_reg[20]_i_1_n_5\,
      O(1) => \reg_reg[20]_i_1_n_6\,
      O(0) => \reg_reg[20]_i_1_n_7\,
      S(3 downto 0) => reg_reg(23 downto 20)
    );
\reg_reg[21]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[20]_i_1_n_6\,
      Q => reg_reg(21),
      R => clear
    );
\reg_reg[22]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[20]_i_1_n_5\,
      Q => reg_reg(22),
      R => clear
    );
\reg_reg[23]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[20]_i_1_n_4\,
      Q => reg_reg(23),
      R => clear
    );
\reg_reg[24]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[24]_i_1_n_7\,
      Q => reg_reg(24),
      R => clear
    );
\reg_reg[24]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[20]_i_1_n_0\,
      CO(3) => \reg_reg[24]_i_1_n_0\,
      CO(2) => \reg_reg[24]_i_1_n_1\,
      CO(1) => \reg_reg[24]_i_1_n_2\,
      CO(0) => \reg_reg[24]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[24]_i_1_n_4\,
      O(2) => \reg_reg[24]_i_1_n_5\,
      O(1) => \reg_reg[24]_i_1_n_6\,
      O(0) => \reg_reg[24]_i_1_n_7\,
      S(3 downto 0) => reg_reg(27 downto 24)
    );
\reg_reg[25]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[24]_i_1_n_6\,
      Q => reg_reg(25),
      R => clear
    );
\reg_reg[26]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[24]_i_1_n_5\,
      Q => reg_reg(26),
      R => clear
    );
\reg_reg[27]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[24]_i_1_n_4\,
      Q => reg_reg(27),
      R => clear
    );
\reg_reg[28]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[28]_i_1_n_7\,
      Q => reg_reg(28),
      R => clear
    );
\reg_reg[28]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[24]_i_1_n_0\,
      CO(3) => \NLW_reg_reg[28]_i_1_CO_UNCONNECTED\(3),
      CO(2) => \reg_reg[28]_i_1_n_1\,
      CO(1) => \reg_reg[28]_i_1_n_2\,
      CO(0) => \reg_reg[28]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[28]_i_1_n_4\,
      O(2) => \reg_reg[28]_i_1_n_5\,
      O(1) => \reg_reg[28]_i_1_n_6\,
      O(0) => \reg_reg[28]_i_1_n_7\,
      S(3 downto 0) => reg_reg(31 downto 28)
    );
\reg_reg[29]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[28]_i_1_n_6\,
      Q => reg_reg(29),
      R => clear
    );
\reg_reg[2]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[0]_i_1_n_5\,
      Q => reg_reg(2),
      R => clear
    );
\reg_reg[30]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[28]_i_1_n_5\,
      Q => reg_reg(30),
      R => clear
    );
\reg_reg[31]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[28]_i_1_n_4\,
      Q => reg_reg(31),
      R => clear
    );
\reg_reg[3]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[0]_i_1_n_4\,
      Q => reg_reg(3),
      R => clear
    );
\reg_reg[4]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[4]_i_1_n_7\,
      Q => reg_reg(4),
      R => clear
    );
\reg_reg[4]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[0]_i_1_n_0\,
      CO(3) => \reg_reg[4]_i_1_n_0\,
      CO(2) => \reg_reg[4]_i_1_n_1\,
      CO(1) => \reg_reg[4]_i_1_n_2\,
      CO(0) => \reg_reg[4]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[4]_i_1_n_4\,
      O(2) => \reg_reg[4]_i_1_n_5\,
      O(1) => \reg_reg[4]_i_1_n_6\,
      O(0) => \reg_reg[4]_i_1_n_7\,
      S(3 downto 0) => reg_reg(7 downto 4)
    );
\reg_reg[5]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[4]_i_1_n_6\,
      Q => reg_reg(5),
      R => clear
    );
\reg_reg[6]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[4]_i_1_n_5\,
      Q => reg_reg(6),
      R => clear
    );
\reg_reg[7]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[4]_i_1_n_4\,
      Q => reg_reg(7),
      R => clear
    );
\reg_reg[8]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[8]_i_1_n_7\,
      Q => reg_reg(8),
      R => clear
    );
\reg_reg[8]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \reg_reg[4]_i_1_n_0\,
      CO(3) => \reg_reg[8]_i_1_n_0\,
      CO(2) => \reg_reg[8]_i_1_n_1\,
      CO(1) => \reg_reg[8]_i_1_n_2\,
      CO(0) => \reg_reg[8]_i_1_n_3\,
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \reg_reg[8]_i_1_n_4\,
      O(2) => \reg_reg[8]_i_1_n_5\,
      O(1) => \reg_reg[8]_i_1_n_6\,
      O(0) => \reg_reg[8]_i_1_n_7\,
      S(3 downto 0) => reg_reg(11 downto 8)
    );
\reg_reg[9]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => CLK_7MHz,
      CE => '1',
      D => \reg_reg[8]_i_1_n_6\,
      Q => reg_reg(9),
      R => clear
    );
end STRUCTURE;
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity design_1_clk_7Mhz_to_1Hz_0_0 is
  port (
    CLK_7MHz : in STD_LOGIC;
    CLK_1Hz : out STD_LOGIC
  );
  attribute NotValidForBitStream : boolean;
  attribute NotValidForBitStream of design_1_clk_7Mhz_to_1Hz_0_0 : entity is true;
  attribute CHECK_LICENSE_TYPE : string;
  attribute CHECK_LICENSE_TYPE of design_1_clk_7Mhz_to_1Hz_0_0 : entity is "design_1_clk_7Mhz_to_1Hz_0_0,clk_7Mhz_to_1Hz,{}";
  attribute DowngradeIPIdentifiedWarnings : string;
  attribute DowngradeIPIdentifiedWarnings of design_1_clk_7Mhz_to_1Hz_0_0 : entity is "yes";
  attribute IP_DEFINITION_SOURCE : string;
  attribute IP_DEFINITION_SOURCE of design_1_clk_7Mhz_to_1Hz_0_0 : entity is "module_ref";
  attribute X_CORE_INFO : string;
  attribute X_CORE_INFO of design_1_clk_7Mhz_to_1Hz_0_0 : entity is "clk_7Mhz_to_1Hz,Vivado 2018.2";
end design_1_clk_7Mhz_to_1Hz_0_0;

architecture STRUCTURE of design_1_clk_7Mhz_to_1Hz_0_0 is
begin
inst: entity work.design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz
     port map (
      CLK_1Hz => CLK_1Hz,
      CLK_7MHz => CLK_7MHz
    );
end STRUCTURE;
