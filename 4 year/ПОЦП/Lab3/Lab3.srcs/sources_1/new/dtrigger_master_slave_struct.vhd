library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity dtrigger_master_slave_struct is
    Port ( D : in STD_LOGIC;
           C : in STD_LOGIC;
           Q : out STD_LOGIC;
           nQ : out STD_LOGIC);
end dtrigger_master_slave_struct;

architecture Structural of dtrigger_master_slave_struct is
    component dtrigger_beh is
        Port ( D : in STD_LOGIC;
               C : in STD_LOGIC;
               Q : out STD_LOGIC;
               nQ : out STD_LOGIC);
    end component;
    component inv is 
        Port ( X : in STD_LOGIC;
               Q : out STD_LOGIC);
    end component;
    SIGNAL inv_c: STD_LOGIC;
    SIGNAL master_q:  STD_LOGIC;
    SIGNAL master_nq: STD_LOGIC;
begin
    INV_CLK: inv port map (X => C, Q => inv_c);
    MASTER: dtrigger_beh port map (D => D,        C => C,     Q => master_q, nQ => master_nq);
    SLAVE:  dtrigger_beh port map (D => master_q, C => inv_c, Q => Q,        nQ => nQ);
end Structural;
