// Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
// --------------------------------------------------------------------------------
// Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
// Date        : Fri Oct 27 00:15:29 2023
// Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
// Command     : write_verilog -force -mode funcsim
//               e:/Work/Lab4/Lab4.srcs/sources_1/bd/design_1/ip/design_1_mgen_impl_beh_0_0/design_1_mgen_impl_beh_0_0_sim_netlist.v
// Design      : design_1_mgen_impl_beh_0_0
// Purpose     : This verilog netlist is a functional simulation representation of the design and should not be modified
//               or synthesized. This netlist cannot be used for SDF annotated simulation.
// Device      : xc7a100tcsg324-1
// --------------------------------------------------------------------------------
`timescale 1 ps / 1 ps

(* CHECK_LICENSE_TYPE = "design_1_mgen_impl_beh_0_0,mgen_impl_beh,{}" *) (* DowngradeIPIdentifiedWarnings = "yes" *) (* IP_DEFINITION_SOURCE = "module_ref" *) 
(* X_CORE_INFO = "mgen_impl_beh,Vivado 2018.2" *) 
(* NotValidForBitStream *)
module design_1_mgen_impl_beh_0_0
   (RST,
    CLK,
    Pout);
  (* X_INTERFACE_INFO = "xilinx.com:signal:reset:1.0 RST RST" *) (* X_INTERFACE_PARAMETER = "XIL_INTERFACENAME RST, POLARITY ACTIVE_LOW" *) input RST;
  (* X_INTERFACE_INFO = "xilinx.com:signal:clock:1.0 CLK CLK" *) (* X_INTERFACE_PARAMETER = "XIL_INTERFACENAME CLK, ASSOCIATED_RESET RST, FREQ_HZ 100000000, PHASE 0.000" *) input CLK;
  output [0:4]Pout;

  wire CLK;
  wire [0:4]Pout;
  wire RST;

  (* DONT_TOUCH *) 
  design_1_mgen_impl_beh_0_0_mgen_impl_beh inst
       (.CLK(CLK),
        .Pout(Pout),
        .RST(RST));
endmodule

(* ORIG_REF_NAME = "mgen_impl_beh" *) (* dont_touch = "true" *) 
module design_1_mgen_impl_beh_0_0_mgen_impl_beh
   (RST,
    CLK,
    Pout);
  input RST;
  input CLK;
  output [0:4]Pout;

  wire CLK;
  wire [0:4]Pout;
  wire RST;
  wire [4:4]p_4_out;

  LUT2 #(
    .INIT(4'h6)) 
    \Atemp[0]_i_1 
       (.I0(Pout[4]),
        .I1(Pout[1]),
        .O(p_4_out));
  FDPE \Atemp_reg[0] 
       (.C(CLK),
        .CE(1'b1),
        .D(p_4_out),
        .PRE(RST),
        .Q(Pout[0]));
  FDPE \Atemp_reg[1] 
       (.C(CLK),
        .CE(1'b1),
        .D(Pout[0]),
        .PRE(RST),
        .Q(Pout[1]));
  FDPE \Atemp_reg[2] 
       (.C(CLK),
        .CE(1'b1),
        .D(Pout[1]),
        .PRE(RST),
        .Q(Pout[2]));
  FDPE \Atemp_reg[3] 
       (.C(CLK),
        .CE(1'b1),
        .D(Pout[2]),
        .PRE(RST),
        .Q(Pout[3]));
  FDPE \Atemp_reg[4] 
       (.C(CLK),
        .CE(1'b1),
        .D(Pout[3]),
        .PRE(RST),
        .Q(Pout[4]));
endmodule
`ifndef GLBL
`define GLBL
`timescale  1 ps / 1 ps

module glbl ();

    parameter ROC_WIDTH = 100000;
    parameter TOC_WIDTH = 0;

//--------   STARTUP Globals --------------
    wire GSR;
    wire GTS;
    wire GWE;
    wire PRLD;
    tri1 p_up_tmp;
    tri (weak1, strong0) PLL_LOCKG = p_up_tmp;

    wire PROGB_GLBL;
    wire CCLKO_GLBL;
    wire FCSBO_GLBL;
    wire [3:0] DO_GLBL;
    wire [3:0] DI_GLBL;
   
    reg GSR_int;
    reg GTS_int;
    reg PRLD_int;

//--------   JTAG Globals --------------
    wire JTAG_TDO_GLBL;
    wire JTAG_TCK_GLBL;
    wire JTAG_TDI_GLBL;
    wire JTAG_TMS_GLBL;
    wire JTAG_TRST_GLBL;

    reg JTAG_CAPTURE_GLBL;
    reg JTAG_RESET_GLBL;
    reg JTAG_SHIFT_GLBL;
    reg JTAG_UPDATE_GLBL;
    reg JTAG_RUNTEST_GLBL;

    reg JTAG_SEL1_GLBL = 0;
    reg JTAG_SEL2_GLBL = 0 ;
    reg JTAG_SEL3_GLBL = 0;
    reg JTAG_SEL4_GLBL = 0;

    reg JTAG_USER_TDO1_GLBL = 1'bz;
    reg JTAG_USER_TDO2_GLBL = 1'bz;
    reg JTAG_USER_TDO3_GLBL = 1'bz;
    reg JTAG_USER_TDO4_GLBL = 1'bz;

    assign (strong1, weak0) GSR = GSR_int;
    assign (strong1, weak0) GTS = GTS_int;
    assign (weak1, weak0) PRLD = PRLD_int;

    initial begin
	GSR_int = 1'b1;
	PRLD_int = 1'b1;
	#(ROC_WIDTH)
	GSR_int = 1'b0;
	PRLD_int = 1'b0;
    end

    initial begin
	GTS_int = 1'b1;
	#(TOC_WIDTH)
	GTS_int = 1'b0;
    end

endmodule
`endif
