unit Defines2;

interface

uses SysUtils;

const EOL_CH  = #10;
const NO_FILE = 'No file';

type int  = integer;
type bool = boolean;
type float = real;

type TPosition = record
   PosIn  : int;
   Target : string;
end;

type TErrorKind = (LexerError, ConverterError);

type TError = record
  Msg  : string;
  Pos  : TPosition;
  Kind : TErrorKind;

  function ToString() : string;
end;
type TErrors = array of TError;

type TTokenKind = (Op, OpenParen, CloseParen, Constant, Variable, EOL);

type TToken = record
  Data : string;
  Pos  : TPosition;
  Kind : TTokenKind;

  function ToString() : string;
end;
type TTokens = array of TToken;

type TActionKind = (Push, Pop, Append);

type TAction = record
  Data : string;
  Kind : TActionKind;

  function ToString() : string;
  function ToLogString() : string;
end;

type TActions = array of TAction;

function NewAction(data : string; kind : TActionKind) : TAction;
function NewPosition(pos_in : int; target : string = NO_FILE) : TPosition;
function NewError(msg : string; pos : TPosition; kind : TErrorKind) : TError;
function NewToken(data : string; pos : TPosition; kind : TTokenKind) : TToken;

implementation

function TToken.ToString() : string;
begin
  case self.Kind of
    TTokenKind.Op:         exit('Operator: ' + self.Data);
    TTokenKind.Constant:   exit('Constant: ' + self.Data);
    TTokenKind.Variable:   exit('Variable: ' + self.Data);
    TTokenKind.OpenParen:  exit('Op-Paren: ' + self.Data);
    TTokenKind.CloseParen: exit('Cl-Paren: ' + self.Data);
  end;
end;

function TError.ToString() : string;
var str : string;
begin
  case self.Kind of
    TErrorKind.LexerError:     str := '[Lexer error]: ';
    TErrorKind.ConverterError: str := '[Converter error]: ';
  end;

  str := str + Msg + ' <';

  if self.Pos.Target <> NO_FILE then
    str := str + 'file: '+ self.Pos.Target + ' ';
  str := str + 'in pos : ' + inttostr(self.Pos.PosIn);
  str := str + '>';
  exit(str);
end;

function TAction.ToString(): string;
begin
  case self.Kind of
    TActionKind.Push:   exit('PUSH  : ' + self.Data);
    TActionKind.Pop:    exit('POP   : ' + self.Data);
    TActionKind.Append: exit('APPEND: ' + self.Data);
  end;
end;

function TAction.ToLogString() : string;
begin
  case self.Kind of
    TActionKind.Push:   exit('Push operator [' + self.Data + '] on to the stack.');
    TActionKind.Pop:    exit('Pop element [' + self.Data + '] from the stack.');
    TActionKind.Append: exit('Append operand [' + self.Data + '] to the notation.');
  end;
end;

function NewAction(data : string; kind : TActionKind) : TAction;
var action : ^TAction;
begin
  new(action);
  action.Data := data;
  action.Kind := kind;
  exit(action^);
end;

function NewPosition(pos_in : int; target : string = NO_FILE) : TPosition;
var position : ^TPosition;
begin
  new(position);
  position.PosIn := pos_in;
  position.Target := target;
  exit(position^);
end;

function NewError(msg : string; pos : TPosition; kind : TErrorKind) : TError;
var error : ^TError;
begin
  new(error);
  error.Msg := msg;
  error.Pos := pos;
  error.Kind := kind;
  exit(error^);
end;

function NewToken(data : string; pos : TPosition; kind : TTokenKind) : TToken;
var token : ^TToken;
begin
  new(token);
  token.Data := data;
  token.Pos := pos;
  token.Kind := kind;
  exit(token^);
end;

end.
