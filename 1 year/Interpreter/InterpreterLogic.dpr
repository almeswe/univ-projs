program InterpreterLogic;

uses
  SysUtils,
  Stack in 'Stack.pas',
  Converter in 'Converter.pas',
  Lexer in 'Lexer.pas',
  Defines in 'Defines.pas',
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

