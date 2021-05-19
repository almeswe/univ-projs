program Logic;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  Sorts in 'Sorts.pas',
  Defines in 'Defines.pas';

var data : TIntArray;

var a,b : uint;

var now : TDateTime;

begin
  data := Sorts.CreateArray(15,RandomArray);

  now := Time;
  data := Sorts.StraightSelectionSort(data,a,b);
  //writeln(TimeToStr(now - Time));
  //General.PrintArray(data);
  writeln('Сравнений: ', a, ' Перестановок: ', b);

  readln;
end.
