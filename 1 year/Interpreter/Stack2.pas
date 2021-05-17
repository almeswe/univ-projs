unit Stack2;

interface

uses SysUtils;

type TCell = record
  next : ^TCell;
  data : string;
end;
type TStack = record
  Head : ^TCell;

  procedure Init();
  procedure Clear();
  procedure Push(data : string);

  function Pop() : TCell;
  function Top() : TCell;

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
  temp.data := data;
  if self.Head = nil then begin
    self.Head := @(temp^);
    self.Head.next := nil;
  end
  else begin
    temp.next := @(self.Head^);
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
    temp := @(temp.next^);
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
  if temp.next = nil then
    self.Head := nil
  else
    self.Head := @(temp.next^);
  exit(temp);
end;

function TStack.Top() : TCell;
begin
  if self.Head = nil then
    raise Exception.Create('Top is unaccessable, stack is empty!');
  exit(self.Head^);
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
      if temp.next = nil then
        temp := nil
      else
        temp := @(temp.next^);
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
