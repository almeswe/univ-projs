unit Sorts;

interface

uses Defines;

procedure QuickSort(var data : TIntArray; lbound, rbound: uint; out comps,perms : uint; fcall : bool = true);
procedure ShakeSort(var data : TIntArray; out comps,perms : uint);
procedure StraightSelectionSort(var data : TIntArray; out comps,perms : uint);

procedure PrintArray(data : TIntArray);
function CreateArray(size : int; kind : TGenKind = RandomArray) : TIntArray;
function CopyArray(arr : TIntArray) : TIntArray;

implementation

type TSwapPerms = (Zero  = 0,
                   One   = 1,
                   Two   = 2,
                   Three = 3);

var comps, perms : uint;

procedure InitCounters();
begin
  comps := 0;
  perms := 0;
end;

procedure SetCounters(var locperms, loccomps : uint);
begin
  locperms := perms;
  loccomps := comps;
end;

function Len(arr1:TIntArray) : int; inline;
begin
  exit(Length(arr1));
end;

function Equal(a,b : int) : bool;
begin
  inc(comps);
  if a = b then
    exit(true);
  exit(false);
end;

function GreaterEqualThen(a,b : int) : bool; inline;
begin
  inc(comps);
  if a >= b then
    exit(true);
  exit(false);
end;

function GreaterThan(a,b : int) : bool;
begin
  inc(comps);
  if a > b then
    exit(true);
  exit(false);
end;

function LessEqualThan(a,b : int) : bool; inline;
begin
  inc(comps);
  if a <= b then
    exit(true);
  exit(false);
end;

function LessThan(a,b : int) : bool;
begin
  inc(comps);
  if a < b then
    exit(true);
  exit(false);
end;

procedure Append(element : int; var arr : TIntArray);
begin
  inc(perms);
  SetLength(arr, len(arr)+1);
  arr[len(arr)-1] := element;
end;

procedure IndirectSwap(var a,b,c : int; elems : TSwapPerms = TSwapPerms.Two);
begin
  perms := perms+ord(elems);
  a := b;
  b := c;
end;

procedure Swap(var a,b : int; elems : TSwapPerms = TSwapPerms.Three);
var buff : int;
begin
  perms := perms+ord(elems);
  buff := a;
  a := b;
  b := buff;
end;

procedure Assign(var a,b : int);
begin
  inc(perms);
  a := b;
end;

function ConcatArrays(arr1, arr2, arr3 : TIntArray) : TIntArray;
var i,counter : int;
var resultArr : TIntArray;
begin
    counter := 0;
    SetLength(resultArr, len(arr1)+len(arr2)+len(arr3));

    for i := 0 to len(arr1)-1 do begin
      resultArr[counter] := arr1[i];
      inc(counter);
    end;
    for i := 0 to len(arr2)-1 do begin
      resultArr[counter] := arr2[i];
      inc(counter);
    end;
    for i := 0 to len(arr3)-1 do begin
      resultArr[counter] := arr3[i];
      inc(counter);
    end;

    exit(resultArr);
end;

procedure QuickSort(var data: TIntArray; lbound, rbound: uint; out comps, perms : uint; fcall : bool = true);
var pivot : int;
var i, j : int;
begin
  if fcall then
    InitCounters();

  i := lbound;
  j := rbound;
  pivot := data[(lbound + rbound) div 2];

  while i <= j do
  begin
    while LessThan(data[i], pivot) do
      inc(i);
    while GreaterThan(data[j], pivot) do
      dec(j);
    if i <= j then begin
      Swap(data[i], data[j]);
      inc(i);
      dec(j);
    end;
  end;
  if lbound < j then
    QuickSort(data, lbound, j, perms, comps, false);
  if rbound > i then
    QuickSort(data, i, rbound, perms, comps, false);
  if fcall then
    SetCounters(perms, comps);
end;

procedure ShakeSort(var data : TIntArray; out comps, perms : uint);
var i, k : int;
var rbound, lbound : int;
begin
  InitCounters();
  lbound := 1;
  rbound := len(data)-1;
  k := len(data)-1;
  repeat
    for i := rbound downto lbound do begin
      if GreaterThan(data[i-1], data[i]) then begin
        k := i;
        Swap(data[i-1], data[i]);
      end;
    end;
    lbound := k + 1;

    for i := lbound to rbound do begin
      if GreaterThan(data[i-1], data[i]) then begin
        k := i;
        Swap(data[i-1], data[i]);
      end;
    end;
    rbound := k - 1;

  until lbound > rbound;
  SetCounters(perms, comps);
end;

procedure StraightSelectionSort(var data : TIntArray; out comps, perms : uint);
var x : int;
var i,j,k : int;
begin
  InitCounters();
  for i := 0 to len(data)-2 do begin
    k := i;
    Assign(x, data[i]);
    for j := i+1 to len(data)-1 do begin
      if LessThan(data[j], x) then begin
        k := j;
        Assign(x, data[k]);
      end;
    end;
    IndirectSwap(data[k], data[i], x, TSwapPerms.Two);
  end;
  SetCounters(perms, comps);
end;

{function QuickSort(data : TIntArray; out comps, perms : uint; fcall : bool = true) : TIntArray;
var i : int;
var pivot : int;
var lessArr, equalArr, greaterArr : TIntArray;
begin
  if fcall then
    InitCounters();

  if len(data) <= 1 then
    exit(data);

  //Assign(pivot, data[len(data) div 2]);
  pivot := data[len(data) div 2];
  for i := 0 to len(data)-1 do begin
    if GreaterThan(data[i], pivot) then begin
      Sorts.Append(data[i], greaterArr);
      continue;
    end;

    if Equal(data[i], pivot) then begin
      Sorts.Append(data[i], equalArr);
      continue;
    end;

    if LessThan(data[i], pivot) then begin
      Sorts.Append(data[i], lessArr);
      continue;
    end;
  end;
  data := ConcatArrays(QuickSort(lessArr, comps, perms, false), equalArr, QuickSort(greaterArr, comps, perms, false));
  if fcall then
    SetCounters(perms, comps);
  exit(data);
end;}

procedure PrintArray(data : TIntArray);
var i : int;
begin
  for i := 0 to len(data)-1 do
      writeln(data[i]);
end;

function CreateArray(size : int; kind : TGenKind = RandomArray) : TIntArray;
var i : int;
var data : TIntArray;
begin
  randomize;
  SetLength(data,size);
  for i := 0 to size-1 do begin
    case kind of
      SortedArray   : data[i] := i+1;
      RandomArray   : data[i] := 1+random(15);
      ReversedArray : data[i] := size-i;
    end;
  end;
  exit(data);
end;

function CopyArray(arr : TIntArray) : TIntArray;
var i : int;
var newarr : TIntArray;
begin
  SetLength(newarr, length(arr));
  for i := 0 to length(arr)-1 do
    newarr[i] := arr[i];
  exit(newarr);
end;

end.
