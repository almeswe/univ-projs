unit Tester;

interface

uses SysUtils, Defines2;

type TTester = record
  var inited : bool;

  var curr_token_num : int;
  var curr_token     : TToken;
  var tokens         : TTokens;
  var errors         : TErrors;

  procedure init(tokens : TTokens);
  procedure discard();

  procedure test_for_errors();
  procedure test_operand();
  procedure test_operator();

  procedure get_next_token();

  //
  procedure append_error(msg : string; pos : TPosition);
  procedure append_error_formatted(expected : string; met : string);
  //

end;

implementation

procedure TTester.init(tokens : TTokens);
begin
  self.inited := true;
  self.tokens := tokens;
  self.curr_token_num := -1;
  SetLength(errors, 0);
end;

procedure TTester.discard();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.inited := false;
  self.curr_token_num := -1;
  SetLength(tokens, 0);
  SetLength(errors, 0);
end;

procedure TTester.test_for_errors();
var i : int;
var paren_pairs : int;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  paren_pairs := 0;
  get_next_token;
  //
  for i := 0 to length(self.tokens)-1 do begin
  //
    case self.curr_token.kind of
      TTokenKind.Constant, TTokenKind.Variable: begin
        self.test_operand;
      end;

      TTokenKind.Op: begin
        self.test_operator;
      end;

      TTokenKind.OpenParen: begin
        inc(paren_pairs);
      end;

      TTokenKind.CloseParen: begin
        dec(paren_pairs);
      end;
    end;
    get_next_token;
  end;
  if paren_pairs <> 0 then
    self.append_error('Paren-pairs balance expected.', self.curr_token.pos);
end;

procedure TTester.test_operand();
var prev_token : TToken;
var forw_token : TToken;
begin
  //check right
  if self.curr_token_num > 0 then begin
    prev_token := self.tokens[self.curr_token_num-1];
    case prev_token.kind of
      TTokenKind.CloseParen, TTokenKind.Constant, TTokenKind.Variable:
        self.append_error('Operator before operand expected, but met: [' + prev_token.data + ']',prev_token.pos);
    end;
  end;
  //check left
  if self.curr_token_num < length(self.tokens)-1 then begin
    forw_token := self.tokens[self.curr_token_num+1];
    case forw_token.kind of
      TTokenKind.OpenParen, TTokenKind.Constant, TTokenKind.Variable:
        self.append_error('Operator after operand expected, but met: [' + forw_token.data + ']',forw_token.pos);
    end;
  end;
end;

procedure TTester.test_operator();
var prev_token : TToken;
var forw_token : TToken;
begin
  //check right
  if self.curr_token_num > 0 then begin
    prev_token := self.tokens[self.curr_token_num-1];
    case prev_token.kind of
      TTokenKind.Op, TTokenKind.OpenParen:
        self.append_error('Operand before operator expected, but met: [' + prev_token.data + ']',prev_token.pos);
    end;
  end
  else
    self.append_error('Operand before operator expected, but met: [' + self.curr_token.data + ']', self.curr_token.pos);
  //check left
  if self.curr_token_num < length(self.tokens)-1 then begin
    forw_token := self.tokens[self.curr_token_num+1];
    case forw_token.kind of
      TTokenKind.Op, TTokenKind.CloseParen:
        self.append_error('Operand after operator expected, but met: [' + forw_token.data + ']',forw_token.pos);
    end;
  end
  else
  //fix printing
    self.append_error('Operand after operator expected, but met: [' + self.curr_token.data + ']',self.curr_token.pos);
end;

procedure TTester.get_next_token();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if self.curr_token_num + 1 > length(self.tokens)-1 then begin
    self.curr_token := new_token('EOL', new_position(self.curr_token.pos.pos_in + length(self.curr_token.data), self.curr_token.pos.target),TTokenKind.EOL);
  end
  else begin
    inc(self.curr_token_num);
    self.curr_token := self.tokens[self.curr_token_num];
  end;
end;

procedure TTester.append_error(msg : string; pos : TPosition);
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.errors, length(self.errors)+1);
  self.errors[length(self.errors)-1] := new_error(msg, pos, TErrorKind.CONVERTER_ERROR);
end;

procedure TTester.append_error_formatted(expected : string; met : string);
begin
  self.append_error(expected + ' expected, but met: [' + met + ']', self.curr_token.pos);
end;

end.

