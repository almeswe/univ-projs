//Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
//--------------------------------------------------------------------------------
//Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
//Date        : Fri Oct 27 11:15:24 2023
//Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
//Command     : generate_target design_1.bd
//Design      : design_1
//Purpose     : IP block netlist
//--------------------------------------------------------------------------------
`timescale 1 ps / 1 ps

(* CORE_GENERATION_INFO = "design_1,IP_Integrator,{x_ipVendor=xilinx.com,x_ipLibrary=BlockDiagram,x_ipName=design_1,x_ipVersion=1.00.a,x_ipLanguage=VERILOG,numBlks=3,numReposBlks=3,numNonXlnxBlks=0,numHierBlks=0,maxHierDepth=0,numSysgenBlks=0,numHlsBlks=0,numHdlrefBlks=2,numPkgbdBlks=0,bdsource=USER,da_board_cnt=2,synth_mode=OOC_per_IP}" *) (* HW_HANDOFF = "design_1.hwdef" *) 
module design_1
   (Pout_0,
    RST_0,
    sys_clock);
  output [0:4]Pout_0;
  (* X_INTERFACE_INFO = "xilinx.com:signal:reset:1.0 RST.RST_0 RST" *) (* X_INTERFACE_PARAMETER = "XIL_INTERFACENAME RST.RST_0, POLARITY ACTIVE_LOW" *) input RST_0;
  (* X_INTERFACE_INFO = "xilinx.com:signal:clock:1.0 CLK.SYS_CLOCK CLK" *) (* X_INTERFACE_PARAMETER = "XIL_INTERFACENAME CLK.SYS_CLOCK, CLK_DOMAIN design_1_sys_clock, FREQ_HZ 100000000, PHASE 0.000" *) input sys_clock;

  wire RST_0_1;
  wire clk_7Mhz_to_1Hz_0_CLK_1Hz;
  wire clk_wiz_0_CLK_7MHz;
  wire [0:4]mgen_impl_beh_0_Pout;
  wire sys_clock_1;

  assign Pout_0[0:4] = mgen_impl_beh_0_Pout;
  assign RST_0_1 = RST_0;
  assign sys_clock_1 = sys_clock;
  design_1_clk_7Mhz_to_1Hz_0_0 clk_7Mhz_to_1Hz_0
       (.CLK_1Hz(clk_7Mhz_to_1Hz_0_CLK_1Hz),
        .CLK_7MHz(clk_wiz_0_CLK_7MHz));
  design_1_clk_wiz_0_0 clk_wiz_0
       (.CLK_7MHz(clk_wiz_0_CLK_7MHz),
        .clk_in1(sys_clock_1),
        .reset(1'b0));
  design_1_mgen_impl_beh_0_0 mgen_impl_beh_0
       (.CLK(clk_7Mhz_to_1Hz_0_CLK_1Hz),
        .Pout(mgen_impl_beh_0_Pout),
        .RST(RST_0_1));
endmodule
