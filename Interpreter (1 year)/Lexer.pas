unit Lexer;

interface

uses SysUtils, Defines2;

type TLexer = record
  var inited : bool;

  var curr_file     : string;
  var curr_char     : char;
  var curr_char_pos : int;

  var expr : string;

  var errors : TErrors;
  var tokens : TTokens;

  procedure init(input : string);
  procedure init_no_file(expr : string);
  procedure process_file(path : string; out line : string);

  procedure discard();
  procedure get_next_char();

  procedure tokenize();
  procedure append_token(token : TToken);
  procedure append_error(msg : string; pos : integer);

  function recognize_op()  : TToken;
  function recognize_num() : TToken;
  function recognize_var() : TToken;
  function recognize_paren() : TToken;

  function is_errored() : bool;

  function is_op()     : bool;
  function is_digit()  : bool;
  function is_letter() : bool;
  function is_wspace() : bool;
  function is_paren()  : bool;
end;

implementation

procedure TLexer.init(input : string);
begin
  if FileExists(input) then begin
    self.inited := true;
    self.curr_file := input;
    self.curr_char_pos := 0;
    self.process_file(input, self.expr);
  end
  else
    self.init_no_file(input);
end;

procedure TLexer.init_no_file(expr : string);
begin
  self.inited := true;

  self.expr := expr;
  self.curr_file := NO_FILE;
  self.curr_char_pos := 0;
end;

procedure TLexer.process_file(path : string; out line : string);
var raw_data_file : TextFile;
begin
    AssignFile(raw_data_file, path);
    Reset(raw_data_file);
    if eof(raw_data_file) then
      self.append_error('File is empty.', 0)
    else
      readln(raw_data_file, line);
    CloseFile(raw_data_file);
end;

procedure TLexer.discard();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.expr      := '';
  self.curr_file := '';
  self.inited    := false;
  self.curr_char_pos := 0;
  SetLength(self.errors, 0);
  SetLength(self.tokens, 0);
end;

procedure TLexer.get_next_char();
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if self.curr_char_pos+1 > length(self.expr) then
    self.curr_char := EOL_CH
  else begin
    inc(self.curr_char_pos);
    self.curr_char := self.expr[self.curr_char_pos];
  end;
end;

procedure TLexer.tokenize();
begin
  SetLength(self.tokens, 0);
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.get_next_char;
  while self.curr_char <> EOL_CH do begin
      if self.is_op() then begin
        self.append_token(self.recognize_op());
        continue;
      end;
      if self.is_paren() then begin
        self.append_token(self.recognize_paren());
        continue;
      end;
      if self.is_letter() then begin
        self.append_token(self.recognize_var());
        continue;
      end;
      if self.is_digit() then begin
        self.append_token(self.recognize_num());
        continue;
      end;
      if not self.is_wspace() then
        self.append_error('Bad character met: [' + self.curr_char + ']', self.curr_char_pos );
      self.get_next_char;
  end;
end;

procedure TLexer.append_token(token : TToken);
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.tokens, length(self.tokens) + 1);
  tokens[length(self.tokens) - 1] := token;
end;

procedure TLexer.append_error(msg: string; pos: integer);
var error : TError;
var loc   : TPosition;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  loc   := new_position(pos, self.curr_file);
  error := new_error(msg, loc, TErrorKind.LEXER_ERROR);
  SetLength(self.errors, length(self.errors) + 1);
  self.errors[length(self.errors) - 1] := error;
end;

function TLexer.recognize_op() : TToken;
var token : TToken;
var loc   : TPosition;
begin
   if not self.inited then
    raise Exception.Create('Call init procedure first!');
   loc   := new_position(self.curr_char_pos, self.curr_file);
   token := new_token(self.curr_char, loc, TTokenKind.Op);
   self.get_next_char;
   exit(token);
end;

function TLexer.recognize_num() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
   loc   := new_position(self.curr_char_pos, self.curr_file);
   token := new_token('', loc, TTokenKind.Constant);
   while (self.curr_char <> EOL_CH) and (self.is_digit()) do begin
       token.data := token.data + self.curr_char;
       self.get_next_char;
   end;
   exit(token);
end;

function TLexer.recognize_var() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
   loc   := new_position(self.curr_char_pos, self.curr_file);
   token := new_token('', loc, TTokenKind.Variable);
   while (self.curr_char <> EOL_CH) and self.is_letter() do begin
       token.data := token.data + self.curr_char;
       self.get_next_char;
   end;
   exit(token);
end;

function TLexer.recognize_paren() : TToken;
var token : TToken;
var loc   : TPosition;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
   loc   := new_position(self.curr_char_pos, self.curr_file);
   token := new_token(self.curr_char, loc, TTokenKind.OpenParen);
   case self.curr_char of
      '(' : token.kind := TTokenKind.OpenParen;
      ')' : token.kind := TTokenKind.CloseParen;
   end;
   self.get_next_char;
   exit(token);
end;

function TLexer.is_errored() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if length(self.errors) > 0 then
    exit(true);
  exit(false);
end;

function TLexer.is_op() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  case self.curr_char of
       '+' : exit(true);
       '-' : exit(true);
       '*' : exit(true);
       '/' : exit(true);
       '^' : exit(true);
  end;
  exit(false);
end;

function TLexer.is_letter() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if (self.curr_char <= 'Z') and (self.curr_char >= 'A') or
     (self.curr_char <= 'z') and (self.curr_char >= 'a') then
    exit(true)
  else
    exit(false);
end;

function TLexer.is_digit() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if (self.curr_char <= '9') and (self.curr_char >= '0') then
    exit(true)
  else
    exit(false);
end;

function TLexer.is_wspace() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if self.curr_char = ' ' then
    exit(true);
  exit(false);
end;

function TLexer.is_paren() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if (self.curr_char = ')')  or (self.curr_char = '(') then
    exit(true);
  exit(false);
end;

end.
