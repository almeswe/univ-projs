unit Interpreter;

interface

uses SysUtils, Lexer, Tester, Converter2, Defines2;

type TInterpreter = record
  notation : string;

  lexer  : TLexer;
  tokens : TTokens;
  tester : TTester;
  converter : TConverter;

  actions : TActions;
  errors  : TErrors;

  procedure init(data : string);
  procedure discard();
  procedure interpret();

  procedure save_to_file(path : string);

  function is_errored() : bool;
end;

implementation

procedure TInterpreter.init(data: string);
begin
  self.lexer.init(data);
  self.converter.init();
end;

procedure TInterpreter.discard();
begin
  SetLength(self.errors, 0);
  SetLength(self.actions, 0);

  self.notation := '';
  self.lexer.discard;
  self.converter.discard;
end;

procedure TInterpreter.interpret();
begin
  self.lexer.tokenize();
  self.tokens := self.lexer.tokens;

  if self.lexer.is_errored() then
    self.errors := self.lexer.errors
  else begin
    self.tester.init(self.tokens);
    self.tester.test_for_errors();
    if self.tester.has_errors() then begin
      self.errors := self.tester.errors;
      self.tester.discard();
    end
    else begin
      self.tester.discard();
      self.converter.convert(self.tokens);
      if self.converter.is_errored() then
        self.errors := self.converter.errors
      else begin
        self.actions := self.converter.actions;
        self.notation := self.converter.notation;
      end;
    end;
  end;
end;

procedure TInterpreter.save_to_file(path : string);
var destination : TextFile;
begin
  if FileExists(path) then begin
    AssignFile(destination, path);
    Rewrite(destination);
    Writeln(destination, self.notation);
    CloseFile(destination);
  end;
end;

function TInterpreter.is_errored() : boolean;
begin
  if length(self.errors) > 0 then
    exit(true);
  exit(false);
end;

end.
