// Copyright 1986-2018 Xilinx, Inc. All Rights Reserved.
// --------------------------------------------------------------------------------
// Tool Version: Vivado v.2018.2 (win64) Build 2258646 Thu Jun 14 20:03:12 MDT 2018
// Date        : Fri Oct 27 11:16:02 2023
// Host        : DESKTOP-TJKNBD0 running 64-bit major release  (build 9200)
// Command     : write_verilog -force -mode funcsim
//               E:/Work/Lab4/Lab4.srcs/sources_1/bd/design_1/ip/design_1_clk_7Mhz_to_1Hz_0_0/design_1_clk_7Mhz_to_1Hz_0_0_sim_netlist.v
// Design      : design_1_clk_7Mhz_to_1Hz_0_0
// Purpose     : This verilog netlist is a functional simulation representation of the design and should not be modified
//               or synthesized. This netlist cannot be used for SDF annotated simulation.
// Device      : xc7a100tcsg324-1
// --------------------------------------------------------------------------------
`timescale 1 ps / 1 ps

(* CHECK_LICENSE_TYPE = "design_1_clk_7Mhz_to_1Hz_0_0,clk_7Mhz_to_1Hz,{}" *) (* DowngradeIPIdentifiedWarnings = "yes" *) (* IP_DEFINITION_SOURCE = "module_ref" *) 
(* X_CORE_INFO = "clk_7Mhz_to_1Hz,Vivado 2018.2" *) 
(* NotValidForBitStream *)
module design_1_clk_7Mhz_to_1Hz_0_0
   (CLK_7MHz,
    CLK_1Hz);
  input CLK_7MHz;
  output CLK_1Hz;

  wire CLK_1Hz;
  wire CLK_7MHz;

  design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz inst
       (.CLK_1Hz(CLK_1Hz),
        .CLK_7MHz(CLK_7MHz));
endmodule

(* ORIG_REF_NAME = "clk_7Mhz_to_1Hz" *) 
module design_1_clk_7Mhz_to_1Hz_0_0_clk_7Mhz_to_1Hz
   (CLK_1Hz,
    CLK_7MHz);
  output CLK_1Hz;
  input CLK_7MHz;

  wire CLK_1Hz;
  wire CLK_7MHz;
  wire clear;
  wire inn_i_1_n_0;
  wire [31:0]p_0_in;
  wire reg0_carry__0_i_1_n_0;
  wire reg0_carry__0_i_2_n_0;
  wire reg0_carry__0_i_3_n_0;
  wire reg0_carry__0_i_4_n_0;
  wire reg0_carry__0_i_5_n_0;
  wire reg0_carry__0_i_5_n_1;
  wire reg0_carry__0_i_5_n_2;
  wire reg0_carry__0_i_5_n_3;
  wire reg0_carry__0_i_6_n_0;
  wire reg0_carry__0_i_6_n_1;
  wire reg0_carry__0_i_6_n_2;
  wire reg0_carry__0_i_6_n_3;
  wire reg0_carry__0_i_7_n_0;
  wire reg0_carry__0_i_7_n_1;
  wire reg0_carry__0_i_7_n_2;
  wire reg0_carry__0_i_7_n_3;
  wire reg0_carry__0_n_0;
  wire reg0_carry__0_n_1;
  wire reg0_carry__0_n_2;
  wire reg0_carry__0_n_3;
  wire reg0_carry__1_i_1_n_0;
  wire reg0_carry__1_i_2_n_0;
  wire reg0_carry__1_i_3_n_0;
  wire reg0_carry__1_i_4_n_2;
  wire reg0_carry__1_i_4_n_3;
  wire reg0_carry__1_i_5_n_0;
  wire reg0_carry__1_i_5_n_1;
  wire reg0_carry__1_i_5_n_2;
  wire reg0_carry__1_i_5_n_3;
  wire reg0_carry__1_n_2;
  wire reg0_carry__1_n_3;
  wire reg0_carry_i_1_n_0;
  wire reg0_carry_i_2_n_0;
  wire reg0_carry_i_3_n_0;
  wire reg0_carry_i_4_n_0;
  wire reg0_carry_i_5_n_0;
  wire reg0_carry_i_5_n_1;
  wire reg0_carry_i_5_n_2;
  wire reg0_carry_i_5_n_3;
  wire reg0_carry_i_6_n_0;
  wire reg0_carry_i_6_n_1;
  wire reg0_carry_i_6_n_2;
  wire reg0_carry_i_6_n_3;
  wire reg0_carry_i_7_n_0;
  wire reg0_carry_i_7_n_1;
  wire reg0_carry_i_7_n_2;
  wire reg0_carry_i_7_n_3;
  wire reg0_carry_n_0;
  wire reg0_carry_n_1;
  wire reg0_carry_n_2;
  wire reg0_carry_n_3;
  wire [31:0]reg_reg;
  wire \reg_reg[0]_i_1_n_0 ;
  wire \reg_reg[0]_i_1_n_1 ;
  wire \reg_reg[0]_i_1_n_2 ;
  wire \reg_reg[0]_i_1_n_3 ;
  wire \reg_reg[0]_i_1_n_4 ;
  wire \reg_reg[0]_i_1_n_5 ;
  wire \reg_reg[0]_i_1_n_6 ;
  wire \reg_reg[0]_i_1_n_7 ;
  wire \reg_reg[12]_i_1_n_0 ;
  wire \reg_reg[12]_i_1_n_1 ;
  wire \reg_reg[12]_i_1_n_2 ;
  wire \reg_reg[12]_i_1_n_3 ;
  wire \reg_reg[12]_i_1_n_4 ;
  wire \reg_reg[12]_i_1_n_5 ;
  wire \reg_reg[12]_i_1_n_6 ;
  wire \reg_reg[12]_i_1_n_7 ;
  wire \reg_reg[16]_i_1_n_0 ;
  wire \reg_reg[16]_i_1_n_1 ;
  wire \reg_reg[16]_i_1_n_2 ;
  wire \reg_reg[16]_i_1_n_3 ;
  wire \reg_reg[16]_i_1_n_4 ;
  wire \reg_reg[16]_i_1_n_5 ;
  wire \reg_reg[16]_i_1_n_6 ;
  wire \reg_reg[16]_i_1_n_7 ;
  wire \reg_reg[20]_i_1_n_0 ;
  wire \reg_reg[20]_i_1_n_1 ;
  wire \reg_reg[20]_i_1_n_2 ;
  wire \reg_reg[20]_i_1_n_3 ;
  wire \reg_reg[20]_i_1_n_4 ;
  wire \reg_reg[20]_i_1_n_5 ;
  wire \reg_reg[20]_i_1_n_6 ;
  wire \reg_reg[20]_i_1_n_7 ;
  wire \reg_reg[24]_i_1_n_0 ;
  wire \reg_reg[24]_i_1_n_1 ;
  wire \reg_reg[24]_i_1_n_2 ;
  wire \reg_reg[24]_i_1_n_3 ;
  wire \reg_reg[24]_i_1_n_4 ;
  wire \reg_reg[24]_i_1_n_5 ;
  wire \reg_reg[24]_i_1_n_6 ;
  wire \reg_reg[24]_i_1_n_7 ;
  wire \reg_reg[28]_i_1_n_1 ;
  wire \reg_reg[28]_i_1_n_2 ;
  wire \reg_reg[28]_i_1_n_3 ;
  wire \reg_reg[28]_i_1_n_4 ;
  wire \reg_reg[28]_i_1_n_5 ;
  wire \reg_reg[28]_i_1_n_6 ;
  wire \reg_reg[28]_i_1_n_7 ;
  wire \reg_reg[4]_i_1_n_0 ;
  wire \reg_reg[4]_i_1_n_1 ;
  wire \reg_reg[4]_i_1_n_2 ;
  wire \reg_reg[4]_i_1_n_3 ;
  wire \reg_reg[4]_i_1_n_4 ;
  wire \reg_reg[4]_i_1_n_5 ;
  wire \reg_reg[4]_i_1_n_6 ;
  wire \reg_reg[4]_i_1_n_7 ;
  wire \reg_reg[8]_i_1_n_0 ;
  wire \reg_reg[8]_i_1_n_1 ;
  wire \reg_reg[8]_i_1_n_2 ;
  wire \reg_reg[8]_i_1_n_3 ;
  wire \reg_reg[8]_i_1_n_4 ;
  wire \reg_reg[8]_i_1_n_5 ;
  wire \reg_reg[8]_i_1_n_6 ;
  wire \reg_reg[8]_i_1_n_7 ;
  wire [3:0]NLW_reg0_carry_O_UNCONNECTED;
  wire [3:0]NLW_reg0_carry__0_O_UNCONNECTED;
  wire [3:3]NLW_reg0_carry__1_CO_UNCONNECTED;
  wire [3:0]NLW_reg0_carry__1_O_UNCONNECTED;
  wire [3:2]NLW_reg0_carry__1_i_4_CO_UNCONNECTED;
  wire [3:3]NLW_reg0_carry__1_i_4_O_UNCONNECTED;
  wire [3:3]\NLW_reg_reg[28]_i_1_CO_UNCONNECTED ;

  LUT2 #(
    .INIT(4'h6)) 
    inn_i_1
       (.I0(clear),
        .I1(CLK_1Hz),
        .O(inn_i_1_n_0));
  FDRE #(
    .INIT(1'b0)) 
    inn_reg
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(inn_i_1_n_0),
        .Q(CLK_1Hz),
        .R(1'b0));
  CARRY4 reg0_carry
       (.CI(1'b0),
        .CO({reg0_carry_n_0,reg0_carry_n_1,reg0_carry_n_2,reg0_carry_n_3}),
        .CYINIT(1'b1),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(NLW_reg0_carry_O_UNCONNECTED[3:0]),
        .S({reg0_carry_i_1_n_0,reg0_carry_i_2_n_0,reg0_carry_i_3_n_0,reg0_carry_i_4_n_0}));
  CARRY4 reg0_carry__0
       (.CI(reg0_carry_n_0),
        .CO({reg0_carry__0_n_0,reg0_carry__0_n_1,reg0_carry__0_n_2,reg0_carry__0_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(NLW_reg0_carry__0_O_UNCONNECTED[3:0]),
        .S({reg0_carry__0_i_1_n_0,reg0_carry__0_i_2_n_0,reg0_carry__0_i_3_n_0,reg0_carry__0_i_4_n_0}));
  LUT3 #(
    .INIT(8'h02)) 
    reg0_carry__0_i_1
       (.I0(p_0_in[21]),
        .I1(p_0_in[23]),
        .I2(p_0_in[22]),
        .O(reg0_carry__0_i_1_n_0));
  LUT3 #(
    .INIT(8'h20)) 
    reg0_carry__0_i_2
       (.I0(p_0_in[18]),
        .I1(p_0_in[19]),
        .I2(p_0_in[20]),
        .O(reg0_carry__0_i_2_n_0));
  LUT3 #(
    .INIT(8'h04)) 
    reg0_carry__0_i_3
       (.I0(p_0_in[17]),
        .I1(p_0_in[16]),
        .I2(p_0_in[15]),
        .O(reg0_carry__0_i_3_n_0));
  LUT3 #(
    .INIT(8'h08)) 
    reg0_carry__0_i_4
       (.I0(p_0_in[14]),
        .I1(p_0_in[13]),
        .I2(p_0_in[12]),
        .O(reg0_carry__0_i_4_n_0));
  CARRY4 reg0_carry__0_i_5
       (.CI(reg0_carry__0_i_6_n_0),
        .CO({reg0_carry__0_i_5_n_0,reg0_carry__0_i_5_n_1,reg0_carry__0_i_5_n_2,reg0_carry__0_i_5_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[24:21]),
        .S(reg_reg[24:21]));
  CARRY4 reg0_carry__0_i_6
       (.CI(reg0_carry__0_i_7_n_0),
        .CO({reg0_carry__0_i_6_n_0,reg0_carry__0_i_6_n_1,reg0_carry__0_i_6_n_2,reg0_carry__0_i_6_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[20:17]),
        .S(reg_reg[20:17]));
  CARRY4 reg0_carry__0_i_7
       (.CI(reg0_carry_i_5_n_0),
        .CO({reg0_carry__0_i_7_n_0,reg0_carry__0_i_7_n_1,reg0_carry__0_i_7_n_2,reg0_carry__0_i_7_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[16:13]),
        .S(reg_reg[16:13]));
  CARRY4 reg0_carry__1
       (.CI(reg0_carry__0_n_0),
        .CO({NLW_reg0_carry__1_CO_UNCONNECTED[3],clear,reg0_carry__1_n_2,reg0_carry__1_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(NLW_reg0_carry__1_O_UNCONNECTED[3:0]),
        .S({1'b0,reg0_carry__1_i_1_n_0,reg0_carry__1_i_2_n_0,reg0_carry__1_i_3_n_0}));
  LUT2 #(
    .INIT(4'h1)) 
    reg0_carry__1_i_1
       (.I0(p_0_in[30]),
        .I1(p_0_in[31]),
        .O(reg0_carry__1_i_1_n_0));
  LUT3 #(
    .INIT(8'h01)) 
    reg0_carry__1_i_2
       (.I0(p_0_in[29]),
        .I1(p_0_in[28]),
        .I2(p_0_in[27]),
        .O(reg0_carry__1_i_2_n_0));
  LUT3 #(
    .INIT(8'h01)) 
    reg0_carry__1_i_3
       (.I0(p_0_in[26]),
        .I1(p_0_in[25]),
        .I2(p_0_in[24]),
        .O(reg0_carry__1_i_3_n_0));
  CARRY4 reg0_carry__1_i_4
       (.CI(reg0_carry__1_i_5_n_0),
        .CO({NLW_reg0_carry__1_i_4_CO_UNCONNECTED[3:2],reg0_carry__1_i_4_n_2,reg0_carry__1_i_4_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({NLW_reg0_carry__1_i_4_O_UNCONNECTED[3],p_0_in[31:29]}),
        .S({1'b0,reg_reg[31:29]}));
  CARRY4 reg0_carry__1_i_5
       (.CI(reg0_carry__0_i_5_n_0),
        .CO({reg0_carry__1_i_5_n_0,reg0_carry__1_i_5_n_1,reg0_carry__1_i_5_n_2,reg0_carry__1_i_5_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[28:25]),
        .S(reg_reg[28:25]));
  LUT3 #(
    .INIT(8'h20)) 
    reg0_carry_i_1
       (.I0(p_0_in[9]),
        .I1(p_0_in[11]),
        .I2(p_0_in[10]),
        .O(reg0_carry_i_1_n_0));
  LUT3 #(
    .INIT(8'h80)) 
    reg0_carry_i_2
       (.I0(p_0_in[6]),
        .I1(p_0_in[8]),
        .I2(p_0_in[7]),
        .O(reg0_carry_i_2_n_0));
  LUT3 #(
    .INIT(8'h20)) 
    reg0_carry_i_3
       (.I0(p_0_in[3]),
        .I1(p_0_in[5]),
        .I2(p_0_in[4]),
        .O(reg0_carry_i_3_n_0));
  LUT3 #(
    .INIT(8'h08)) 
    reg0_carry_i_4
       (.I0(p_0_in[2]),
        .I1(p_0_in[1]),
        .I2(reg_reg[0]),
        .O(reg0_carry_i_4_n_0));
  CARRY4 reg0_carry_i_5
       (.CI(reg0_carry_i_6_n_0),
        .CO({reg0_carry_i_5_n_0,reg0_carry_i_5_n_1,reg0_carry_i_5_n_2,reg0_carry_i_5_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[12:9]),
        .S(reg_reg[12:9]));
  CARRY4 reg0_carry_i_6
       (.CI(reg0_carry_i_7_n_0),
        .CO({reg0_carry_i_6_n_0,reg0_carry_i_6_n_1,reg0_carry_i_6_n_2,reg0_carry_i_6_n_3}),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[8:5]),
        .S(reg_reg[8:5]));
  CARRY4 reg0_carry_i_7
       (.CI(1'b0),
        .CO({reg0_carry_i_7_n_0,reg0_carry_i_7_n_1,reg0_carry_i_7_n_2,reg0_carry_i_7_n_3}),
        .CYINIT(reg_reg[0]),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O(p_0_in[4:1]),
        .S(reg_reg[4:1]));
  LUT1 #(
    .INIT(2'h1)) 
    \reg[0]_i_2 
       (.I0(reg_reg[0]),
        .O(p_0_in[0]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[0] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[0]_i_1_n_7 ),
        .Q(reg_reg[0]),
        .R(clear));
  CARRY4 \reg_reg[0]_i_1 
       (.CI(1'b0),
        .CO({\reg_reg[0]_i_1_n_0 ,\reg_reg[0]_i_1_n_1 ,\reg_reg[0]_i_1_n_2 ,\reg_reg[0]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b1}),
        .O({\reg_reg[0]_i_1_n_4 ,\reg_reg[0]_i_1_n_5 ,\reg_reg[0]_i_1_n_6 ,\reg_reg[0]_i_1_n_7 }),
        .S({reg_reg[3:1],p_0_in[0]}));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[10] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[8]_i_1_n_5 ),
        .Q(reg_reg[10]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[11] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[8]_i_1_n_4 ),
        .Q(reg_reg[11]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[12] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[12]_i_1_n_7 ),
        .Q(reg_reg[12]),
        .R(clear));
  CARRY4 \reg_reg[12]_i_1 
       (.CI(\reg_reg[8]_i_1_n_0 ),
        .CO({\reg_reg[12]_i_1_n_0 ,\reg_reg[12]_i_1_n_1 ,\reg_reg[12]_i_1_n_2 ,\reg_reg[12]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[12]_i_1_n_4 ,\reg_reg[12]_i_1_n_5 ,\reg_reg[12]_i_1_n_6 ,\reg_reg[12]_i_1_n_7 }),
        .S(reg_reg[15:12]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[13] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[12]_i_1_n_6 ),
        .Q(reg_reg[13]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[14] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[12]_i_1_n_5 ),
        .Q(reg_reg[14]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[15] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[12]_i_1_n_4 ),
        .Q(reg_reg[15]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[16] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[16]_i_1_n_7 ),
        .Q(reg_reg[16]),
        .R(clear));
  CARRY4 \reg_reg[16]_i_1 
       (.CI(\reg_reg[12]_i_1_n_0 ),
        .CO({\reg_reg[16]_i_1_n_0 ,\reg_reg[16]_i_1_n_1 ,\reg_reg[16]_i_1_n_2 ,\reg_reg[16]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[16]_i_1_n_4 ,\reg_reg[16]_i_1_n_5 ,\reg_reg[16]_i_1_n_6 ,\reg_reg[16]_i_1_n_7 }),
        .S(reg_reg[19:16]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[17] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[16]_i_1_n_6 ),
        .Q(reg_reg[17]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[18] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[16]_i_1_n_5 ),
        .Q(reg_reg[18]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[19] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[16]_i_1_n_4 ),
        .Q(reg_reg[19]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[1] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[0]_i_1_n_6 ),
        .Q(reg_reg[1]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[20] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[20]_i_1_n_7 ),
        .Q(reg_reg[20]),
        .R(clear));
  CARRY4 \reg_reg[20]_i_1 
       (.CI(\reg_reg[16]_i_1_n_0 ),
        .CO({\reg_reg[20]_i_1_n_0 ,\reg_reg[20]_i_1_n_1 ,\reg_reg[20]_i_1_n_2 ,\reg_reg[20]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[20]_i_1_n_4 ,\reg_reg[20]_i_1_n_5 ,\reg_reg[20]_i_1_n_6 ,\reg_reg[20]_i_1_n_7 }),
        .S(reg_reg[23:20]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[21] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[20]_i_1_n_6 ),
        .Q(reg_reg[21]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[22] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[20]_i_1_n_5 ),
        .Q(reg_reg[22]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[23] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[20]_i_1_n_4 ),
        .Q(reg_reg[23]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[24] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[24]_i_1_n_7 ),
        .Q(reg_reg[24]),
        .R(clear));
  CARRY4 \reg_reg[24]_i_1 
       (.CI(\reg_reg[20]_i_1_n_0 ),
        .CO({\reg_reg[24]_i_1_n_0 ,\reg_reg[24]_i_1_n_1 ,\reg_reg[24]_i_1_n_2 ,\reg_reg[24]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[24]_i_1_n_4 ,\reg_reg[24]_i_1_n_5 ,\reg_reg[24]_i_1_n_6 ,\reg_reg[24]_i_1_n_7 }),
        .S(reg_reg[27:24]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[25] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[24]_i_1_n_6 ),
        .Q(reg_reg[25]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[26] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[24]_i_1_n_5 ),
        .Q(reg_reg[26]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[27] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[24]_i_1_n_4 ),
        .Q(reg_reg[27]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[28] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[28]_i_1_n_7 ),
        .Q(reg_reg[28]),
        .R(clear));
  CARRY4 \reg_reg[28]_i_1 
       (.CI(\reg_reg[24]_i_1_n_0 ),
        .CO({\NLW_reg_reg[28]_i_1_CO_UNCONNECTED [3],\reg_reg[28]_i_1_n_1 ,\reg_reg[28]_i_1_n_2 ,\reg_reg[28]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[28]_i_1_n_4 ,\reg_reg[28]_i_1_n_5 ,\reg_reg[28]_i_1_n_6 ,\reg_reg[28]_i_1_n_7 }),
        .S(reg_reg[31:28]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[29] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[28]_i_1_n_6 ),
        .Q(reg_reg[29]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[2] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[0]_i_1_n_5 ),
        .Q(reg_reg[2]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[30] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[28]_i_1_n_5 ),
        .Q(reg_reg[30]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[31] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[28]_i_1_n_4 ),
        .Q(reg_reg[31]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[3] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[0]_i_1_n_4 ),
        .Q(reg_reg[3]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[4] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[4]_i_1_n_7 ),
        .Q(reg_reg[4]),
        .R(clear));
  CARRY4 \reg_reg[4]_i_1 
       (.CI(\reg_reg[0]_i_1_n_0 ),
        .CO({\reg_reg[4]_i_1_n_0 ,\reg_reg[4]_i_1_n_1 ,\reg_reg[4]_i_1_n_2 ,\reg_reg[4]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[4]_i_1_n_4 ,\reg_reg[4]_i_1_n_5 ,\reg_reg[4]_i_1_n_6 ,\reg_reg[4]_i_1_n_7 }),
        .S(reg_reg[7:4]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[5] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[4]_i_1_n_6 ),
        .Q(reg_reg[5]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[6] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[4]_i_1_n_5 ),
        .Q(reg_reg[6]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[7] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[4]_i_1_n_4 ),
        .Q(reg_reg[7]),
        .R(clear));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[8] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[8]_i_1_n_7 ),
        .Q(reg_reg[8]),
        .R(clear));
  CARRY4 \reg_reg[8]_i_1 
       (.CI(\reg_reg[4]_i_1_n_0 ),
        .CO({\reg_reg[8]_i_1_n_0 ,\reg_reg[8]_i_1_n_1 ,\reg_reg[8]_i_1_n_2 ,\reg_reg[8]_i_1_n_3 }),
        .CYINIT(1'b0),
        .DI({1'b0,1'b0,1'b0,1'b0}),
        .O({\reg_reg[8]_i_1_n_4 ,\reg_reg[8]_i_1_n_5 ,\reg_reg[8]_i_1_n_6 ,\reg_reg[8]_i_1_n_7 }),
        .S(reg_reg[11:8]));
  FDRE #(
    .INIT(1'b0)) 
    \reg_reg[9] 
       (.C(CLK_7MHz),
        .CE(1'b1),
        .D(\reg_reg[8]_i_1_n_6 ),
        .Q(reg_reg[9]),
        .R(clear));
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
