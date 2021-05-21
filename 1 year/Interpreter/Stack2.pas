unit Stack2;

interface

uses SysUtils;

type TCell = record
  Next : ^TCell;
  Data : string;
end;

type TStack = record
  Head : ^TCell;

  procedure Init();
  procedure Clear();
  procedure Push(data : string);

  function Pop() : TCell;
  function Top() : TCell;
  function Get(const index : integer) : string;

  function Size()  : integer;
  function Empty() : boolean;
end;

procedure Test();

implementation

procedure TStack.Init();
begin
  self.Head := nil;
end;

procedure TStack.Push(data : string);
var temp : ^TCell;
begin
  New(temp);
  temp.Data := data;
  if self.Head = nil then begin
    self.Head := @(temp^);
    self.Head.Next := nil;
  end
  else begin
    temp.Next := @(self.Head^);
    self.Head := @(temp^);
  end;
end;

procedure TStack.Clear();
var i     : integer;
var temp  : ^TCell;
var cells : array of ^TCell;

begin
  SetLength(cells,0);
  temp := @(self.Head^);
  while temp <> nil do begin
    SetLength(cells,Length(cells)+1);
    cells[Length(cells)-1] := @(temp^);
    temp := @(temp.Next^);
  end;

  for i := 0 to Length(cells)-1 do
    dispose(cells[i]);
  self.Head := nil;
end;

function TStack.Pop() : TCell;
var temp : TCell;
begin
  temp := self.Head^;
  dispose(self.Head);
  if self.Empty then
    raise Exception.Create('Cannot pop from empty stack!');
  if temp.Next = nil then
    self.Head := nil
  else
    self.Head := @(temp.Next^);
  exit(temp);
end;

function TStack.Top() : TCell;
begin
  if self.Head = nil then
    raise Exception.Create('Top is unaccessable, stack is empty!');
  exit(self.Head^);
end;

function TStack.Get(const index : integer) : string;
var i : integer;
var current : ^TCell;
begin
  if index < 0 then
    raise Exception.Create('Negative index is not supplied!');
  if self.Head = nil then
    raise Exception.Create('Top is unaccessable, stack is empty!');
  current := @(self.Head^);
  for i := 1 to index do begin
    if current.Next = nil then
       raise Exception.Create('Element with index ' + IntToStr(index) + ' does not exist!');
    current := @(current.Next^);
  end;
  exit(current^.Data);
end;

function TStack.Empty() : boolean;
begin
  if self.Head = nil then
    exit(true);
  exit(false);
end;

function TStack.Size() : integer;
var len  : integer;
var temp : ^TCell;
begin
   temp := @(self.Head^);
   len  := 0;
   while temp <> nil do begin
      if temp.Next = nil then
        temp := nil
      else
        temp := @(temp.Next^);
      inc(len);
   end;
   exit(len);
end;

procedure Test();
var s : TStack;
var i : integer;
var c : TCell;
var e : boolean;
begin
  s.Init();
  for i := 1 to 10000000 do
    s.Push('123');
  readln;
  for i := 1 to 10000000 do
    s.Pop();
  readln;
end;

end.
