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
  procedure test_operand();
  procedure test_operator();

  procedure get_next_token();
  procedure append_error(msg : string; pos : TPosition);
  procedure append_error_formatted(expected : string; met : string);

  function match(kind : TTokenKind; offset : int = 0) : bool;
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
        self.append_error_formatted('Operator before operand',prev_token.data);
    end;
  end;
  //check left
  if self.curr_token_num < length(self.tokens)-1 then begin
    forw_token := self.tokens[self.curr_token_num+1];
    case forw_token.kind of
      TTokenKind.OpenParen, TTokenKind.Constant, TTokenKind.Variable:
        self.append_error_formatted('Operator after operand',forw_token.data);
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
        self.append_error_formatted('Operand before operator',prev_token.data);
    end;
  end
  else
    self.append_error_formatted('Operand before operator',self.curr_token.data);

  //check left
  if self.curr_token_num < length(self.tokens)-1 then begin
    forw_token := self.tokens[self.curr_token_num+1];
    case forw_token.kind of
      TTokenKind.Op, TTokenKind.CloseParen:
        self.append_error_formatted('Operator after operand',forw_token.data);
    end;
  end
  else
    self.append_error_formatted('Operand after operator',self.curr_token.data);
end;

procedure TTester.get_next_token();
var eol_token : ^TToken;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if self.curr_token_num + 1 > length(self.tokens)-1 then begin
  //
    new(eol_token);
    eol_token.data := 'EOL';
    eol_token.kind := TTokenKind.EOL;
    eol_token.pos.pos_in := self.curr_token.pos.pos_in + length(self.curr_token.data);
    self.curr_token := eol_token^;
    dispose(eol_token);
  //
  end
  else begin
    inc(self.curr_token_num);
    self.curr_token := self.tokens[self.curr_token_num];
  end;
end;

procedure TTester.append_error(msg : string; pos : TPosition);
var error : ^TError;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  //
  New(error);
  error.msg  := msg;
  error.pos  := pos;
  error.kind := TErrorKind.CONVERTER_ERROR;
  //
  SetLength(self.errors, length(self.errors)+1);
  self.errors[length(self.errors)-1] := error^;
end;

procedure TTester.append_error_formatted(expected : string; met : string);
begin
  self.append_error(expected + ' expected, but met: [' + met + ']', self.curr_token.pos);
end;

function TTester.match(kind : TTokenKind; offset : int = 0) : bool;
begin
  if length(self.tokens) > self.curr_token_num + offset then
    if self.tokens[self.curr_token_num + offset].kind = kind then
      exit(true);
  exit(false);
end;

end.

