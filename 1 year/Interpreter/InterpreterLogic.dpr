program InterpreterLogic;

uses
  SysUtils,
  Stack2 in 'Stack2.pas',
  Converter2 in 'Converter2.pas',
  Lexer in 'Lexer.pas',
  Defines2 in 'Defines2.pas',
  Tester in 'Tester.pas',
  Tests in 'Tests.pas',
  Interpreter in 'Interpreter.pas';

var interp : TInterpreter;
var expr : string;
var i : integer;

begin
  while true do begin
    write('>');
    readln(expr);
    interp.init(expr);
    interp.interpret;

    if not interp.is_errored then
      writeln(interp.notation)
    else
      for i := 0 to length(interp.errors)-1 do
          writeln(interp.errors[i].to_string());

    interp.discard;
  end;
end.

