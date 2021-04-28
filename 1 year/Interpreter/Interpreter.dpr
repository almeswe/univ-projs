program Interpreter;

uses
  SysUtils,
  Stack2 in 'Stack2.pas',
  Converter2 in 'Converter2.pas',
  Lexer in 'Lexer.pas',
  Defines2 in 'Defines2.pas',
  Tester in 'Tester.pas',
  Tests in 'Tests.pas';

var lexer  : TLexer;
var tokens : TTokens;
var tester : TTester;
var conv   : TConverter;

var expr : string;
var i    : integer;

begin
  while true do begin
    write('>');
    readln(expr);
    lexer.init(expr);
    lexer.tokenize();
    tokens := lexer.tokens;

    if lexer.is_errored() then begin
      for i := 0 to length(lexer.errors)-1 do
        writeln(lexer.errors[i].to_string());
    end
    else begin
       tester.init(tokens);
       tester.test_for_errors;
       if length(tester.errors) > 0 then begin
         for i := 0 to length(tester.errors)-1 do
            writeln(tester.errors[i].to_string());
       end
       else begin
         conv.init();
         conv.convert(tokens);
         //for i := 0 to length(conv.actions)-1 do
         //  writeln(conv.actions[i].to_string());
         writeln(conv.notation);
         conv.discard;
       end;
       tester.discard;
    end;

    lexer.discard;
    writeln;
  end;
end.

