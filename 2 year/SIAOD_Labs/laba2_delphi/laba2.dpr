program laba2;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  System.Math,
  LinkedList in 'LinkedList.pas';

function Create : TLinkedListPtr;
var n, i: int;
var input: int;
var data: ^PolyData;
var list: ^TLinkedList;
begin
  new(list);
  list.Init;

  write('Enter n:');
  readln(n);

  for i := 0 to n-1 do begin
    new(data);
    write('x^', i, ': ');
    readln(input);
    data.Power := i;
    data.Coefficient := input;
    if (data.Coefficient <> 0) then
      list.Add(data^);
  end;
  list.PrintInConsole;
  exit(@(list^));
end;

function Equality(p, q: TLinkedListPtr) : bool;
var i, j: int;
var qitem, pitem: PolyData;
var qlen, plen: int;
var foundSame, equal: bool;
begin
  qlen := q.Size;
  plen := p.Size;

  equal := true;

  for i := 0 to Max(qlen, plen)-1 do begin
    qitem := q.GetAt(i);
    pitem := p.GetAt(i);
    foundSame := false;
    if qitem.Power = pitem.Power then begin
      if qitem.Coefficient = pitem.Coefficient then
        foundSame := true;
    end;
    equal := equal and foundSame;
  end;
  exit(equal);
end;

function Meaning(p: TLinkedListPtr; x: int) : longint;
var i: int;
var res, temp: longint;
var item: PolyData;
begin
  res := 0;
  for i := 0 to p.Size-1 do begin
    item := p.GetAt(i);
    temp := Ceil(Power(x, item.Power));
    temp := temp * item.Coefficient;
    res := res + temp;
  end;
  exit(res);
end;

procedure Add(q, r: TLinkedListPtr; out p : TLinkedListPtr);
var i, j: int;
var index: int;
var qitem, ritem: PolyData;
begin
  p.Init;

  for i := 0 to q.Size-1 do begin
    index := -1;
    qitem := q.GetAt(i);
    for j := 0 to r.Size-1 do begin
      if qitem.Power = r.GetAt(j).Power then begin
        qitem.Coefficient := qitem.Coefficient +
          r.GetAt(j).Coefficient;
        index := j;
      end;
    end;
    if index >= 0 then
      r.RemoveAt(index);
    p.Add(qitem);
  end;

  for i := 0 to r.Size-1 do
    p.Add(r.GetAt(i));
end;

procedure Interact;
var x: int;
var input: string;
var list : TLinkedListPtr;
begin
  while True do begin
    write('>'); readln(input);
    if input = 'add' then begin
      Add(Create, Create, list);
      write('result: '); list.PrintInConsole;
      list.Free;
    end;
    if input = 'mean' then begin
      write('Enter x:'); readln(x);
      writeln('result: ', Meaning(Create, x));
    end;
    if input = 'equ' then begin
      writeln('result: ', Equality(Create, Create));
    end;
    if input = 'quit' then
      break;
    writeln;
  end;
end;

var data: PolyData;
var list: TLinkedListPtr;
begin
  {data.Power := 2;
  data.Coefficient := 2;
  new(list);
  list.Init;

  list.Add(data);
  data.Power := 1;
  list.Add(data);

  list.RemoveAt(0);
  list.RemoveAt(0); }

  Interact;
end.
