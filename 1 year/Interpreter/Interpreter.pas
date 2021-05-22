unit Interpreter;

interface

uses SysUtils, Lexer, Tester, Converter2, Defines2;

type TInterpreter = record
  Source : string;
  Notation : string;

  Lexer  : TLexer;
  Tokens : TTokens;
  Tester : TTester;
  Converter : TConverter;

  Actions : TActions;
  Errors  : TErrors;

  procedure Init(input : string);
  procedure Discard();
  procedure Interpret();

  procedure SaveToFile(path : string);

  function IsErrored() : bool;
end;

implementation

procedure TInterpreter.Init(input : string);
begin
  self.Lexer.Init(input);
  self.Converter.Init();
  self.Source := self.Lexer.Expression;
end;

procedure TInterpreter.Discard();
begin
  self.Lexer.Discard;
  self.Converter.Discard;
  SetLength(self.Errors, 0);
end;

procedure TInterpreter.Interpret();
begin
  self.Lexer.Tokenize();
  self.Tokens := self.Lexer.Tokens;

  if self.Lexer.IsErrored() then
    self.Errors := self.Lexer.Errors
  else begin
    self.Tester.Init(self.Tokens);
    self.Tester.TestForErrors();
    if self.Tester.HasErrors() then begin
      self.Errors := self.Tester.Errors;
      self.Tester.Discard();
    end
    else begin
      self.Tester.Discard();
      self.Converter.Convert(self.Tokens);
      if self.Converter.IsErrored() then
        self.Errors := self.Converter.Errors
      else begin
        self.Actions := self.Converter.Actions;
        self.Notation := self.Converter.Notation;
      end;
    end;
  end;
end;

procedure TInterpreter.SaveToFile(path : string);
var destination : TextFile;
begin
  if FileExists(path) then begin
    AssignFile(destination, path);
    Rewrite(destination);
    Writeln(destination, self.Notation);
    CloseFile(destination);
  end;
end;

function TInterpreter.IsErrored() : boolean;
begin
  if length(self.Errors) > 0 then
    exit(true);
  exit(false);
end;

end.
