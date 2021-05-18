unit Sorts;

interface

uses Defines;

function QuickSort(data : TIntArray; out comps,perms : uint) : TIntArray;
function ShakeSort(data : TIntArray; out comps,perms : uint) : TIntArray;
function BubbleSort(data : TIntArray; out comps,perms : uint) : TIntArray;
function SSelectionSort(data : TIntArray; out comps,perms : uint) : TIntArray;

procedure PrintArray(data : TIntArray);
function CreateArray(size : int; kind : TArrayCreationKind = RANDOM_A) : TIntArray;

implementation

function Len(arr1:TIntArray) : int; inline;
begin
  exit(Length(arr1));
end;

function Equal(a,b : int; var count : uint) : bool; inline;
begin
  inc(count);
  if a = b then
    exit(true);
  exit(false);
end;

function GreaterEqualThen(a,b : int; var count : uint) : bool; inline;
begin
  inc(count);
  if a >= b then
    exit(true);
  exit(false);
end;

function GreaterThan(a,b : int; var count : uint) : bool; inline;
begin
  inc(count);
  if a > b then
    exit(true);
  exit(false);
end;

function LessEqualThen(a,b : int; var count : uint) : bool; inline;
begin
  inc(count);
  if a <= b then
    exit(true);
  exit(false);
end;

function LessThan(a,b : int; var count : uint) : bool; inline;
begin
  inc(count);
  if a < b then
    exit(true);
  exit(false);
end;

procedure Append(element : int; var arr : TIntArray; var count : uint); inline;
begin
  inc(count);
  SetLength(arr, len(arr)+1);
  arr[len(arr)-1] := element;
end;

procedure Swap(var a,b : int; var count : uint); inline;
var buff : int;
begin
  buff := a;
  a := b;
  b := buff;
  inc(count);
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

function BubbleSort(data : TIntArray; out comps, perms : uint) : TIntArray;
var i,j : int;
var swaps : bool;
begin
  for i := 0 to len(data)-1 do begin
      swaps := false;
      for j := 0 to len(data)-2-i do begin
        if LessThan(data[j+1], data[j], comps) then begin
          swaps := true;
          swap(data[j], data[j+1], perms);
        end;
      end;
      if not swaps then
        break;
  end;
  exit(data);
end;

function ShakeSort(data : TIntArray; out comps, perms : uint) : TIntArray;
var i,j : int;
var swaps : bool;
var rbound, lbound : int;
begin
  swaps := true;

  lbound := 0;
  rbound := len(data)-1;

  while (lbound < rbound) and swaps do begin
    swaps := false;
    for i := lbound to rbound-1 do begin
      if LessThan(data[i+1], data[i], comps) then begin
        swaps := true;
        swap(data[i+1], data[i], perms);
      end;
    end;
    for i := rbound-1 downto lbound+1 do begin
      if GreaterThan(data[i-1], data[i], comps) then begin
        swaps := true;
        swap(data[i-1], data[i], perms);
      end;
    end;
    inc(lbound);
    dec(rbound);
  end;
  exit(data);
end;

function SSelectionSort(data : TIntArray; out comps, perms : uint) : TIntArray;
var i,j : int;
var min : int;
begin
  for i := 0 to len(data)-1 do begin
    min := data[i];
    for j := i+1 to len(data)-1 do
      if LessThan(data[j], min, comps) then begin
        min := data[j];
        swap(data[j], data[i], perms);
      end;
  end;
  exit(data);
end;

function QuickSort(data : TIntArray; out comps, perms : uint) : TIntArray;
var i : int;
var pivot : int;
var less_arr, equal_arr, greater_arr : TIntArray;
begin
  if len(data) <= 1 then
    exit(data);

  pivot := data[len(data) div 2];

  for i := 0 to len(data)-1 do begin
    if LessThan(data[i], pivot, comps) then begin
      append(data[i], less_arr, perms);
      continue;
    end;

    if Equal(data[i], pivot, comps) then begin
      append(data[i], equal_arr, perms);
      continue;
    end;

    if GreaterThan(data[i], pivot, comps) then begin
      append(data[i], greater_arr, perms);
      continue;
    end;
  end;
  exit(ConcatArrays(QuickSort(less_arr, comps, perms), equal_arr, QuickSort(greater_arr, comps, perms)));
end;

procedure PrintArray(data : TIntArray);
var i : int;
begin
  for i := 0 to len(data)-1 do
      writeln(data[i]);
end;

function CreateArray(size : int; kind : TArrayCreationKind = RANDOM_A) : TIntArray;
var i : int;
var data : TIntArray;
begin
  randomize;
  SetLength(data,size);
  for i := 0 to size-1 do begin
    case kind of
      SORTED_A   : data[i] := i+1;
      RANDOM_A   : data[i] := 1+random(15);
      REVERSED_A : data[i] := size-i;
    end;
  end;
  exit(data);
end;

end.
