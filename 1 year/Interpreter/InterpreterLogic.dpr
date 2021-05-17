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
    interp.Init(expr);
    interp.Interpret;

    if not interp.IsErrored then
      writeln(interp.Notation)
    else
      for i := 0 to length(interp.Errors)-1 do
          writeln(interp.Errors[i].ToString());

    interp.Discard;
  end;
end.

