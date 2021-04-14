unit Stack2;

interface

uses SysUtils;

type TCell = record
  next : ^TCell;
  data : string;
end;
type TStack = record
  head : ^TCell;

  procedure init();
  procedure clear();
  procedure push(data : string);

  function pop() : TCell;
  function top() : TCell;

  function size()  : integer;
  function empty() : boolean;
end;

procedure test();

implementation

procedure TStack.init();
begin
  self.head := nil;
end;

procedure TStack.push(data : string);
var temp : ^TCell;
begin
  New(temp);
  temp.data := data;
  if self.head = nil then begin
    self.head := @(temp^);
    self.head.next := nil;
  end
  else begin
    temp.next := @(self.head^);
    self.head := @(temp^);
  end;
end;

procedure TStack.clear();
var i     : integer;
var temp  : ^TCell;
var cells : array of ^TCell;
begin
  SetLength(cells,0);
  temp := @(self.head^);
  while temp <> nil do begin
    SetLength(cells,Length(cells)+1);
    cells[Length(cells)-1] := @(temp^);
    temp := @(temp.next^);
  end;

  for i := 0 to Length(cells)-1 do
    dispose(cells[i]);
  self.head := nil;
end;

function TStack.pop() : TCell;
var temp : TCell;
begin
  temp := self.head^;
  dispose(self.head);
  if self.empty then
    raise Exception.Create('Cannot pop from empty stack!');
  if temp.next = nil then
    self.head := nil
  else
    self.head := @(temp.next^);
  exit(temp);
end;

function TStack.top() : TCell;
begin
  if self.head = nil then
    raise Exception.Create('Top is unaccessable, stack is empty!');
  exit(self.head^);
end;

function TStack.empty() : boolean;
begin
  if self.head = nil then
    exit(true);
  exit(false);
end;

function TStack.size() : integer;
var len  : integer;
var temp : ^TCell;
begin
   temp := @(self.head^);
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

procedure test();
var s : TStack;
var i : integer;
var c : TCell;
var e : boolean;
begin
  s.init();
  for i := 1 to 10000000 do
    s.push('123');
  readln;
  for i := 1 to 10000000 do
    s.pop();
  readln;
end;

end.
