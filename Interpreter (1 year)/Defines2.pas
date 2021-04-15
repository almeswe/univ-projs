unit Defines2;

interface

uses SysUtils;

const EOL_CH  = #10;
const NO_FILE = 'No file';

type int  = integer;
type bool = boolean;

type TPosition = record
   pos_in : int;
   target : string;
end;

type TErrorKind = (LEXER_ERROR, CONVERTER_ERROR);
type TError = record
  msg  : string;
  pos  : TPosition;
  kind : TErrorKind;

  function to_string() : string;
end;
type TErrors = array of TError;

type TTokenKind = (Op, OpenParen, CloseParen, Constant, Variable, EOL);
type TToken = record
  data : string;
  pos  : TPosition;
  kind : TTokenKind;

  function to_string() : string;
end;
type TTokens = array of TToken;

type TActionKind = (PUSH, POP, APPEND);
type TAction = record
  data : string;
  kind : TActionKind;

  function to_string() : string;
end;
type TActions = array of TAction;

implementation

function TToken.to_string() : string;
begin
  case self.kind of
    TTokenKind.Op:         exit('Operator: ' + self.data);
    TTokenKind.Constant:   exit('Constant: ' + self.data);
    TTokenKind.Variable:   exit('Variable: ' + self.data);
    TTokenKind.OpenParen:  exit('Op-Paren: ' + self.data);
    TTokenKind.CloseParen: exit('Cl-Paren: ' + self.data);
  end;
end;
function TError.to_string() : string;
var str : string;
begin
  case self.kind of
    TErrorKind.LEXER_ERROR:     str := '[Lexer error]: ';
    TErrorKind.CONVERTER_ERROR: str := '[Converter error]: ';
  end;

  str := str + msg + ' <in pos : ' + inttostr(self.pos.pos_in);
  if self.pos.target <> NO_FILE then
    str := str + ' ' + self.pos.target;
  str := str + '>';
  exit(str);
end;
function TAction.to_string(): string;
begin
  case self.kind of
    TActionKind.PUSH:   exit('PUSH  : ' + self.data);
    TActionKind.POP:    exit('POP   : ' + self.data);
    TActionKind.APPEND: exit('APPEND: ' + self.data);
  end;
end;

end.
