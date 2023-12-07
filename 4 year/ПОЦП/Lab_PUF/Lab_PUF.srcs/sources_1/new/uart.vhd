-----------------------------------------------------------
--               UART | Transmitter unit
-----------------------------------------------------------
--
-- Copyright (c) 2008, Thijs van As <t.vanas@gmail.com>
--
-----------------------------------------------------------
-- Input:      clk        | System clock at 1.8432 MHz
--             reset      | System reset
--             data_in    | Input data
--             in_valid   | Input data valid
--
-- Output:     tx         | TX line
--             accept_in  | '1' when transmitter accepts
-----------------------------------------------------------
-- uart_tx.vhd
-----------------------------------------------------------

library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_unsigned.all;
use ieee.numeric_std.all;

entity uart is
    port (clk      : in std_logic;
          reset    : in std_logic;
          data_in  : in std_logic_vector(7 downto 0);
          in_valid : in std_logic;

          tx        : out std_logic;
          accept_in : out std_logic;
          completed : out std_logic);
end entity uart;


architecture behavioural of uart is
    attribute dont_touch: string;
    attribute dont_touch of behavioural : architecture is "true";
    type   tx_state is (reset_state, idle, start_bit, send_data, stop_bit, wait_for_fsm);
    signal current_state, next_state : tx_state;
    signal data_counter              : std_logic_vector(2 downto 0) := (others => '0');
    signal ticker                    : std_logic_vector(3 downto 0) := (others => '0');
begin
    -- Updates the states in the statemachine at a 115200 bps rate
    clkgen_115k2 : process(clk, reset)
    begin
        if (reset = '1') then
            ticker        <= (others => '0');
            current_state <= reset_state;
            data_counter  <= "000";
        elsif (clk = '1' and clk'event) then
            if (ticker = 15 or (current_state = idle and next_state = idle)) then
                ticker        <= (others => '0');
                current_state <= next_state;
                if (current_state = send_data) then
                    data_counter <= data_counter + 1;
                else
                    data_counter <= "000";
                end if;
            else
                current_state <= current_state;
                ticker        <= ticker + 1;
            end if;
        end if;
    end process clkgen_115k2;

    tx_control : process (current_state, data_in, in_valid, data_counter)
    begin
        case current_state is
            when reset_state =>
                completed <= '0';
                accept_in <= '0';
                tx        <= '1';

                next_state <= idle;
            when idle =>
                completed <= '0';
                accept_in <= '1';
                tx        <= '1';

                if (in_valid = '1') then
                    next_state <= start_bit;
                else
                    next_state <= idle;
                end if;
            when start_bit =>
                completed <= '0';
                accept_in <= '0';
                tx        <= '0';

                next_state <= send_data;
            when send_data =>
                completed <= '0';
                accept_in <= '0';
                tx        <= data_in(conv_integer(data_counter));

                if (data_counter = 7) then
                    next_state <= stop_bit;
                else
                    next_state <= send_data;
                end if;
            when stop_bit =>
                completed  <= '1';
                accept_in  <= '0';
                tx         <= '1';
                next_state <= wait_for_fsm;
            when wait_for_fsm =>
                if in_valid = '0' then
                    next_state <= idle;
                else
                    next_state <= wait_for_fsm;
                end if;
            when others =>
                completed <= '0';
                accept_in <= '0';
                tx        <= '1';

                next_state <= reset_state;
        end case;
    end process tx_control;
end architecture behavioural;