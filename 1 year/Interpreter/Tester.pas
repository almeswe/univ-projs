unit Tester;

interface

uses SysUtils, Defines2;

type TTester = record
  Inited : bool;

  CurrTokenNum : int;
  CurrToken    : TToken;
  Tokens       : TTokens;
  Errors       : TErrors;

  procedure Init(tokens : TTokens);
  procedure Discard();

  procedure TestForErrors();
  procedure TestOperand();
  procedure TestOperator();

  procedure GetNextToken();

  procedure AppendError(msg : string; pos : TPosition);
  procedure AppendErrorFormatted(expected : string; met : string);

  function HasErrors() : bool;

end;

implementation

procedure TTester.Init(tokens : TTokens);
begin
  self.Inited := true;
  self.Tokens := tokens;
  self.CurrTokenNum := -1;
  SetLength(Errors, 0);
end;

procedure TTester.Discard();
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  self.Inited := false;
  self.CurrTokenNum := -1;
  SetLength(Tokens, 0);
  SetLength(Errors, 0);
end;

procedure TTester.TestForErrors();
var i : int;
var paren_pairs : int;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  paren_pairs := 0;
  GetNextToken;
  //
  for i := 0 to length(self.Tokens)-1 do begin
  //
    case self.CurrToken.Kind of
      TTokenKind.Constant, TTokenKind.Variable: begin
        self.TestOperand;
      end;

      TTokenKind.Op: begin
        self.TestOperator;
      end;

      TTokenKind.OpenParen: begin
        inc(paren_pairs);
      end;

      TTokenKind.CloseParen: begin
        dec(paren_pairs);
      end;
    end;
    GetNextToken;
  end;
  if paren_pairs <> 0 then
    self.AppendError('Paren-pairs balance expected.', self.CurrToken.Pos);
end;

procedure TTester.TestOperand();
var prev_token : TToken;
var forw_token : TToken;
begin
  //check right
  if self.CurrTokenNum > 0 then begin
    prev_token := self.Tokens[self.CurrTokenNum-1];
    case prev_token.Kind of
      TTokenKind.CloseParen, TTokenKind.Constant, TTokenKind.Variable:
        self.AppendError('Operator before operand expected, but met: [' + prev_token.Data + ']',prev_token.Pos);
    end;
  end;
  //check left
  if self.CurrTokenNum < length(self.Tokens)-1 then begin
    forw_token := self.Tokens[self.CurrTokenNum+1];
    case forw_token.Kind of
      TTokenKind.OpenParen, TTokenKind.Constant, TTokenKind.Variable:
        self.AppendError('Operator after operand expected, but met: [' + forw_token.Data + ']',forw_token.Pos);
    end;
  end;
end;

procedure TTester.TestOperator();
var prev_token : TToken;
var forw_token : TToken;
begin
  //check right
  if self.CurrTokenNum > 0 then begin
    prev_token := self.Tokens[self.CurrTokenNum-1];
    case prev_token.Kind of
      TTokenKind.Op, TTokenKind.OpenParen:
        self.AppendError('Operand before operator expected, but met: [' + prev_token.Data + ']',prev_token.Pos);
    end;
  end
  else
    self.AppendError('Operand before operator expected, but met: [' + self.CurrToken.Data + ']', self.CurrToken.Pos);
  //check left
  if self.CurrTokenNum < length(self.Tokens)-1 then begin
    forw_token := self.Tokens[self.CurrTokenNum+1];
    case forw_token.Kind of
      TTokenKind.Op, TTokenKind.CloseParen:
        self.AppendError('Operand after operator expected, but met: [' + forw_token.Data + ']',forw_token.Pos);
    end;
  end
  else
  //fix printing
    self.AppendError('Operand after operator expected, but met: [' + self.CurrToken.Data + ']',self.CurrToken.Pos);
end;

procedure TTester.GetNextToken();
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if self.CurrTokenNum + 1 > length(self.Tokens)-1 then begin
    self.CurrToken := NewToken('EOL', NewPosition(self.CurrToken.Pos.PosIn + length(self.CurrToken.Data), self.CurrToken.Pos.Target),TTokenKind.EOL);
  end
  else begin
    inc(self.CurrTokenNum);
    self.CurrToken := self.Tokens[self.CurrTokenNum];
  end;
end;

procedure TTester.AppendError(msg : string; pos : TPosition);
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.Errors, length(self.Errors)+1);
  self.Errors[length(self.Errors)-1] := NewError(msg, pos, TErrorKind.CONVERTER_ERROR);
end;

procedure TTester.AppendErrorFormatted(expected : string; met : string);
begin
  self.AppendError(expected + ' expected, but met: [' + met + ']', self.CurrToken.Pos);
end;

function TTester.HasErrors() : bool;
begin
  if length(self.Errors) > 0 then
    exit(true);
  exit(false);
end;

end.

