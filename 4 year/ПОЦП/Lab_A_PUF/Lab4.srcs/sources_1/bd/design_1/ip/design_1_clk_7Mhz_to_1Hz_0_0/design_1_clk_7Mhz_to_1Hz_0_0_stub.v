// Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
// --------------------------------------------------------------------------------
// Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
// Date        : Fri Oct 27 11:16:02 2023
// Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
// Command     : write_verilog -force -mode synth_stub
//               E:/Work/Lab4/Lab4.srcs/sources_1/bd/design_1/ip/design_1_clk_7Mhz_to_1Hz_0_0/design_1_clk_7Mhz_to_1Hz_0_0_stub.v
// Design      : design_1_clk_7Mhz_to_1Hz_0_0
// Purpose     : Stub declaration of top-level module interface
// Device      : xc7a100tcsg324-1
// --------------------------------------------------------------------------------

// This empty module with port declaration file causes synthesis tools to infer a black box for IP.
// The synthesis directives are for Synopsys Synplify support to prevent IO buffer insertion.
// Please paste the declaration into a Verilog source file or add the file as an additional source.
(* X_CORE_INFO = "clk_7Mhz_to_1Hz,Vivado 2018.2" *)
module design_1_clk_7Mhz_to_1Hz_0_0(CLK_7MHz, CLK_1Hz)
/* synthesis syn_black_box black_box_pad_pin="CLK_7MHz,CLK_1Hz" */;
  input CLK_7MHz;
  output CLK_1Hz;
endmodule
