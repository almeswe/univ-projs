unit Lexer;

interface

uses SysUtils, Defines2;

type TLexer = record
  Inited : bool;

  CurrentCharPos : int;
  CurrentChar    : char;
  CurrentFile    : string;

  Expression : string;

  Errors : TErrors;
  Tokens : TTokens;

  procedure Init(input : string);
  procedure InitNoFile(expr : string);
  procedure ProcessFile(path : string; out line : string);

  procedure Discard();
  procedure GetNextChar();

  procedure Tokenize();
  procedure AppendToken(token : TToken);
  procedure AppendError(msg : string; pos : integer);

  function RecognizeOp()  : TToken;
  function RecognizeNum() : TToken;
  function RecognizeVar() : TToken;
  function RecognizeParen() : TToken;

  function IsErrored() : bool;

  function IsOp()     : bool;
  function IsDigit()  : bool;
  function IsLetter() : bool;
  function IsWspace() : bool;
  function IsParen()  : bool;
end;

implementation

procedure TLexer.Init(input : string);
begin
  if FileExists(input) then begin
    self.Inited := true;
    self.CurrentFile := input;
    self.CurrentCharPos := 0;
    self.ProcessFile(input, self.Expression);
  end
  else
    self.InitNoFile(input);
end;

procedure TLexer.InitNoFile(expr : string);
begin
  self.Inited := true;

  self.Expression := expr;
  self.CurrentFile := NO_FILE;
  self.CurrentCharPos := 0;
end;

procedure TLexer.ProcessFile(path : string; out line : string);
var rawDataFile : TextFile;
begin
    AssignFile(rawDataFile, path);
    Reset(rawDataFile);
    if eof(rawDataFile) then
      self.AppendError('File is empty.', 0)
    else
      readln(rawDataFile, line);
    CloseFile(rawDataFile);
end;

procedure TLexer.Discard();
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  self.Expression      := '';
  self.CurrentFile := '';
  self.Inited    := false;
  self.CurrentCharPos := 0;
  SetLength(self.Errors, 0);
  SetLength(self.Tokens, 0);
end;

procedure TLexer.GetNextChar();
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if self.CurrentCharPos+1 > length(self.Expression) then
    self.CurrentChar := EOL_CH
  else begin
    inc(self.CurrentCharPos);
    self.CurrentChar := self.Expression[self.CurrentCharPos];
  end;
end;

procedure TLexer.Tokenize();
begin
  SetLength(self.Tokens, 0);
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  self.GetNextChar;
  while self.CurrentChar <> EOL_CH do begin
      if self.IsOp() then begin
        self.AppendToken(self.RecognizeOp());
        continue;
      end;
      if self.IsParen() then begin
        self.AppendToken(self.RecognizeParen());
        continue;
      end;
      if self.IsLetter() then begin
        self.AppendToken(self.RecognizeVar());
        continue;
      end;
      if self.IsDigit() then begin
        self.AppendToken(self.RecognizeNum());
        continue;
      end;
      if not self.IsWspace() then
        self.AppendError('Bad character met: [' + self.CurrentChar + ']', self.CurrentCharPos );
      self.GetNextChar;
  end;
end;

procedure TLexer.AppendToken(token : TToken);
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.Tokens, length(self.Tokens) + 1);
  Tokens[length(self.Tokens) - 1] := token;
end;

procedure TLexer.AppendError(msg: string; pos: integer);
var error : TError;
var loc   : TPosition;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  loc   := NewPosition(pos, self.CurrentFile);
  error := NewError(msg, loc, TErrorKind.LexerError);
  SetLength(self.Errors, length(self.Errors) + 1);
  self.Errors[length(self.Errors) - 1] := error;
end;

function TLexer.RecognizeOp() : TToken;
var token : TToken;
var loc   : TPosition;
begin
   if not self.Inited then
    raise Exception.Create('Call init procedure first!');
   loc   := NewPosition(self.CurrentCharPos, self.CurrentFile);
   token := NewToken(self.CurrentChar, loc, TTokenKind.Op);
   self.GetNextChar;
   exit(token);
end;

function TLexer.RecognizeNum() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
   loc   := NewPosition(self.CurrentCharPos, self.CurrentFile);
   token := NewToken('', loc, TTokenKind.Constant);
   while (self.CurrentChar <> EOL_CH) and (self.IsDigit()) do begin
       token.Data := token.Data + self.CurrentChar;
       self.GetNextChar;
   end;
   exit(token);
end;

function TLexer.RecognizeVar() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
   loc   := NewPosition(self.CurrentCharPos, self.CurrentFile);
   token := NewToken('', loc, TTokenKind.Variable);
   while (self.CurrentChar <> EOL_CH) and self.IsLetter() do begin
       token.Data := token.Data + self.CurrentChar;
       self.GetNextChar;
   end;
   exit(token);
end;

function TLexer.RecognizeParen() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
   loc   := NewPosition(self.CurrentCharPos, self.CurrentFile);
   token := NewToken(self.CurrentChar, loc, TTokenKind.OpenParen);
   case self.CurrentChar of
      '(' : token.Kind := TTokenKind.OpenParen;
      ')' : token.Kind := TTokenKind.CloseParen;
   end;
   self.GetNextChar;
   exit(token);
end;

function TLexer.IsErrored() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if length(self.Errors) > 0 then
    exit(true);
  exit(false);
end;

function TLexer.IsOp() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  case self.CurrentChar of
       '+' : exit(true);
       '-' : exit(true);
       '*' : exit(true);
       '/' : exit(true);
       '^' : exit(true);
  end;
  exit(false);
end;

function TLexer.IsLetter() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if (self.CurrentChar <= 'Z') and (self.CurrentChar >= 'A') or
     (self.CurrentChar <= 'z') and (self.CurrentChar >= 'a') then
    exit(true)
  else
    exit(false);
end;

function TLexer.IsDigit() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if (self.CurrentChar <= '9') and (self.CurrentChar >= '0') then
    exit(true)
  else
    exit(false);
end;

function TLexer.IsWspace() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if self.CurrentChar = ' ' then
    exit(true);
  exit(false);
end;

function TLexer.IsParen() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if (self.CurrentChar = ')')  or (self.CurrentChar = '(') then
    exit(true);
  exit(false);
end;

end.
