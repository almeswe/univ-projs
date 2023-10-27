//Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
//--------------------------------------------------------------------------------
//Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
//Date        : Fri Oct 27 11:15:24 2023
//Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
//Command     : generate_target design_1_wrapper.bd
//Design      : design_1_wrapper
//Purpose     : IP block netlist
//--------------------------------------------------------------------------------
`timescale 1 ps / 1 ps

module design_1_wrapper
   (Pout_0,
    RST_0,
    sys_clock);
  output [0:4]Pout_0;
  input RST_0;
  input sys_clock;

  wire [0:4]Pout_0;
  wire RST_0;
  wire sys_clock;

  design_1 design_1_i
       (.Pout_0(Pout_0),
        .RST_0(RST_0),
        .sys_clock(sys_clock));
endmodule
