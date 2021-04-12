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
  procedure reset();

  procedure test_for_errors();
  procedure test_expr();
  procedure test_operand();

  procedure get_next_token();
  procedure append_error(msg : string);
end;

implementation

procedure TTester.init(tokens : TTokens);
begin
  self.inited := true;
  self.tokens := tokens;
  self.curr_token_num := -1;
  SetLength(errors, 0);
end;

procedure TTester.reset();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.inited := false;
  self.curr_token_num := -1;
  SetLength(tokens, 0);
  SetLength(errors, 0);
end;

procedure TTester.test_for_errors();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.get_next_token;
  test_expr();
end;

procedure TTester.test_expr();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');

  self.test_operand();
  if self.curr_token.kind = TTokenKind.Op then begin
      self.get_next_token;
      self.test_expr;
  end
  else
    if self.curr_token_num+1 > length(self.tokens) then
      self.append_error('Operator expected, but met: ' + self.curr_token.data);
end;

procedure TTester.test_operand();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');

  if self.curr_token.kind = TTokenKind.Op then begin
    if self.curr_token.data = '(' then begin
        self.get_next_token;
        self.test_expr;
        if self.curr_token.data <> ')' then
          self.append_error('Close paren expected, but met ' + self.curr_token.data);
    end
  end
  else
    self.get_next_token;
end;

procedure TTester.get_next_token();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if self.curr_token_num + 1 >= length(self.tokens) then
    exit
  else begin
    inc(self.curr_token_num);
    self.curr_token := self.tokens[self.curr_token_num];
  end;
end;

procedure TTester.append_error(msg : string);
var error : ^TError;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  New(error);
  error.msg  := msg;
  error.kind := TErrorKind.CONVERTER_ERROR;
  error.pos  := self.curr_token.pos;

  SetLength(self.errors, length(self.errors)+1);
  self.errors[length(self.errors)-1] := error^;
end;

end.
