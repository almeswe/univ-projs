unit Tests;

interface

uses SysUtils, Converter2, Lexer, Defines2;

procedure TestConverterNotation(input, expected : string; test_num : int);
procedure RunConverterNotationTests();

implementation

procedure TestConverterNotation(input, expected : string; test_num : int);
var conv : TConverter;
var lexer : TLexer;
begin
  lexer.InitNoFile(input);
  lexer.Tokenize();
  conv.Init();
  conv.Convert(lexer.Tokens);
  if expected = conv.Notation then
    writeln('Converter notation test #', test_num, ' passed.')
  else
    writeln('Converter notation test #', test_num, ' failed. Expected: [' + expected + '] met: [' + conv.Notation +']');
end;

procedure RunConverterNotationTests();
begin
  Tests.TestConverterNotation('(1+2)*4+3','12+4*3+',1);
  Tests.TestConverterNotation('3+4*2/(1-5)^2','342*15-2^/+',2);
  Tests.TestConverterNotation('(8+2*5)/(1+3*2-4)','825*+132*+4-/',3);
  Tests.TestConverterNotation('(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)','825*+132*+4-/825*+132*+4-/+825*+^132*+4-/',4);
  Tests.TestConverterNotation('(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)/(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)^(((8+2*5)/(1+3*2-4))+(8+2*5)/(1+3*2-4))^(8+2*5)/(1+3*2-4)','825*+132*+4-/825*+132*+4-/+825*+^132*+4-/825*+132*+4-/825*+132*+4-/+825*+^/132*+4-825*+132*+4-/825*+132*+4-/+^825*+^/132*+4-/',5);
end;

end.
