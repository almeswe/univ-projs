program Sorts;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  General in 'General.pas';

var data : TIntArray;

var a,b : uint;

var now : TDateTime;

begin
  data := General.CreateArray(5,REVERSED_A);

  now := Time;
  data := General.QuickSort(data,a,b);
  writeln(TimeToStr(now - Time));
  General.PrintArray(data);
  Writeln('Сравнений: ', a, ' Перестановок: ', b);

  readln;
end.
