program laba2;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  LinkedList in 'LinkedList.pas';

type TStringArray = array of string;

var surnames: TStringArray;
var abonents: TLinkedListPtr;

function LoadSurnames : TStringArray;
var i: int;
var line: string;
var source: TextFile;
var surnames: TStringArray;
begin
  i := 0;
  SetLength(surnames, 50);
  AssignFile(source, 'Surnames.txt');
  Reset(source);
  while not Eof(source) do begin
    Readln(source, line);
    surnames[i] := line;
    inc(i);
  end;
  CloseFile(source);
  exit(surnames);
end;

function GetRandomSurname(pool: TStringArray) : string;
begin
  randomize;
  exit(pool[Random(Length(pool)-1)]);
end;

function GetRandomPhone : string;
const size = 7;
var i: int;
var phone: string;
begin
  randomize;
  phone := '';
  for i := 1 to size do
    phone := phone + IntToStr(Random(10));
  exit(phone);
end;

function FillList(size: int) : TLinkedListPtr;
var i: int;
var item: Abonent;
var list: TLinkedListPtr;
begin
  new(list);
  list.Init;
  for i := 0 to size-1 do begin
    item.Phone := GetRandomPhone;
    item.Surname := GetRandomSurname(surnames);
    list.Add(item);
  end;
  exit(list);
end;

procedure LexSortList(abonents: TLinkedListPtr);
var i, j: int;
var buf: Abonent;
begin
  for i := 0 to abonents.Size-1 do begin
    for j := i+1 to abonents.Size-1 do begin
      if CompareStr(abonents.GetAt(i).Surname, abonents.GetAt(j).Surname) > 0 then begin
        buf := abonents.GetAt(i);
        abonents.SetAt(i, abonents.GetAt(j));
        abonents.SetAt(j, buf);
      end;
    end;
  end;
end;

procedure FindPhone;
var i: int;
var found: bool;
var surname: string;
begin
  Writeln;
  Writeln('By surname: ');
  Readln(surname);
  found := false;
  for i := 0 to abonents.Size-1 do
    if abonents.GetAt(i).Surname = surname then begin
      found := true;
      Writeln(i+1, ') ', abonents.GetAt(i).Phone);
    end;
  if not found then
    writeln('No results found!');
  Writeln;
end;

procedure FindSurname;
var i: int;
var found: bool;
var phone : string;
begin
  Writeln;
  Write('By phone number: ');
  Readln(phone);
  found := false;
  for i := 0 to abonents.Size-1 do
    if abonents.GetAt(i).Phone = phone then begin
      found := true;
      Writeln(i+1, ') ', abonents.GetAt(i).Surname);
    end;
  if not found then
    writeln('No results found!');
  Writeln;
end;

procedure Interact;
var input: string;
begin
  abonents.PrintInConsole;
  while true do begin
    write('>'); readln(input);
    if input = 'q' then
      break;
    if input = 'bphone' then
      FindSurname;
    if input = 'bsname' then
      FindPhone;
  end;
end;

var list: TLinkedListPtr;
var a : Abonent;

begin

  {new(list);
  list.Init;
  a.Phone := '1';
  a.Surname := '1';
  list.Add(a);
  a.Phone := '2';
  list.SetAt(0, a);}

  surnames := LoadSurnames;
  abonents := FillList(15);
  LexSortList(abonents);
  Interact;
end.
