-- Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
-- --------------------------------------------------------------------------------
-- Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
-- Date        : Fri Oct 27 00:15:29 2023
-- Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
-- Command     : write_vhdl -force -mode funcsim
--               e:/Work/Lab4/Lab4.srcs/sources_1/bd/design_1/ip/design_1_mgen_impl_beh_0_0/design_1_mgen_impl_beh_0_0_sim_netlist.vhdl
-- Design      : design_1_mgen_impl_beh_0_0
-- Purpose     : This VHDL netlist is a functional simulation representation of the design and should not be modified or
--               synthesized. This netlist cannot be used for SDF annotated simulation.
-- Device      : xc7a100tcsg324-1
-- --------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity design_1_mgen_impl_beh_0_0_mgen_impl_beh is
  port (
    RST : in STD_LOGIC;
    CLK : in STD_LOGIC;
    Pout : out STD_LOGIC_VECTOR ( 0 to 4 )
  );
  attribute ORIG_REF_NAME : string;
  attribute ORIG_REF_NAME of design_1_mgen_impl_beh_0_0_mgen_impl_beh : entity is "mgen_impl_beh";
  attribute dont_touch : string;
  attribute dont_touch of design_1_mgen_impl_beh_0_0_mgen_impl_beh : entity is "true";
end design_1_mgen_impl_beh_0_0_mgen_impl_beh;

architecture STRUCTURE of design_1_mgen_impl_beh_0_0_mgen_impl_beh is
  signal \^pout\ : STD_LOGIC_VECTOR ( 0 to 4 );
  signal p_4_out : STD_LOGIC_VECTOR ( 4 to 4 );
begin
  Pout(0 to 4) <= \^pout\(0 to 4);
\Atemp[0]_i_1\: unisim.vcomponents.LUT2
    generic map(
      INIT => X"6"
    )
        port map (
      I0 => \^pout\(4),
      I1 => \^pout\(1),
      O => p_4_out(4)
    );
\Atemp_reg[0]\: unisim.vcomponents.FDPE
     port map (
      C => CLK,
      CE => '1',
      D => p_4_out(4),
      PRE => RST,
      Q => \^pout\(0)
    );
\Atemp_reg[1]\: unisim.vcomponents.FDPE
     port map (
      C => CLK,
      CE => '1',
      D => \^pout\(0),
      PRE => RST,
      Q => \^pout\(1)
    );
\Atemp_reg[2]\: unisim.vcomponents.FDPE
     port map (
      C => CLK,
      CE => '1',
      D => \^pout\(1),
      PRE => RST,
      Q => \^pout\(2)
    );
\Atemp_reg[3]\: unisim.vcomponents.FDPE
     port map (
      C => CLK,
      CE => '1',
      D => \^pout\(2),
      PRE => RST,
      Q => \^pout\(3)
    );
\Atemp_reg[4]\: unisim.vcomponents.FDPE
     port map (
      C => CLK,
      CE => '1',
      D => \^pout\(3),
      PRE => RST,
      Q => \^pout\(4)
    );
end STRUCTURE;
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity design_1_mgen_impl_beh_0_0 is
  port (
    RST : in STD_LOGIC;
    CLK : in STD_LOGIC;
    Pout : out STD_LOGIC_VECTOR ( 0 to 4 )
  );
  attribute NotValidForBitStream : boolean;
  attribute NotValidForBitStream of design_1_mgen_impl_beh_0_0 : entity is true;
  attribute CHECK_LICENSE_TYPE : string;
  attribute CHECK_LICENSE_TYPE of design_1_mgen_impl_beh_0_0 : entity is "design_1_mgen_impl_beh_0_0,mgen_impl_beh,{}";
  attribute DowngradeIPIdentifiedWarnings : string;
  attribute DowngradeIPIdentifiedWarnings of design_1_mgen_impl_beh_0_0 : entity is "yes";
  attribute IP_DEFINITION_SOURCE : string;
  attribute IP_DEFINITION_SOURCE of design_1_mgen_impl_beh_0_0 : entity is "module_ref";
  attribute X_CORE_INFO : string;
  attribute X_CORE_INFO of design_1_mgen_impl_beh_0_0 : entity is "mgen_impl_beh,Vivado 2018.2";
end design_1_mgen_impl_beh_0_0;

architecture STRUCTURE of design_1_mgen_impl_beh_0_0 is
  attribute DONT_TOUCH : boolean;
  attribute DONT_TOUCH of inst : label is std.standard.true;
  attribute X_INTERFACE_INFO : string;
  attribute X_INTERFACE_INFO of CLK : signal is "xilinx.com:signal:clock:1.0 CLK CLK";
  attribute X_INTERFACE_PARAMETER : string;
  attribute X_INTERFACE_PARAMETER of CLK : signal is "XIL_INTERFACENAME CLK, ASSOCIATED_RESET RST, FREQ_HZ 100000000, PHASE 0.000";
  attribute X_INTERFACE_INFO of RST : signal is "xilinx.com:signal:reset:1.0 RST RST";
  attribute X_INTERFACE_PARAMETER of RST : signal is "XIL_INTERFACENAME RST, POLARITY ACTIVE_LOW";
begin
inst: entity work.design_1_mgen_impl_beh_0_0_mgen_impl_beh
     port map (
      CLK => CLK,
      Pout(0 to 4) => Pout(0 to 4),
      RST => RST
    );
end STRUCTURE;
