unit Tests;

interface

uses SysUtils, Converter2, Lexer, Defines2;

procedure test_converter_notation(input, expected : string; test_num : int);
procedure run_converter_notation_tests();

implementation

procedure test_converter_notation(input, expected : string; test_num : int);
var conv : TConverter;
var lexer : TLexer;
begin
  lexer.init_no_file(input);
  lexer.tokenize();
  conv.init();
  conv.convert(lexer.tokens);
  if expected = conv.notation then
    writeln('Converter notation test #', test_num, ' passed.')
  else
    writeln('Converter notation test #', test_num, ' failed. Expected: [' + expected + '] met: [' + conv.notation +']');
end;

procedure run_converter_notation_tests();
begin
  Tests.test_converter_notation('(1+2)*4+3','12+4*3+',1);
  Tests.test_converter_notation('3+4*2/(1-5)^2','342*15-2^/+',2);
  Tests.test_converter_notation('(8+2*5)/(1+3*2-4)','825*+132*+4-/',3);
  Tests.test_converter_notation('(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)','825*+132*+4-/825*+132*+4-/+825*+^132*+4-/',4);
  Tests.test_converter_notation('(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)/(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)^(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)','825*+132*+4-/825*+132*+4-/+825*+^132*+4-/825*+132*+4-/825*+132*+4-/+825*+^/132*+4-825*+132*+4-/825*+132*+4-/+^825*+^/132*+4-/',5);
end;

end.
