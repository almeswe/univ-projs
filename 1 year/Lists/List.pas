unit List;

interface

uses SysUtils, Defines;

type TCustomListItem = record
  Next : ^TCustomListItem;
  Data : TEmployee;
end;

type TCustomList = record

  Head : ^TCustomListItem;
  Last : ^TCustomListItem;

  procedure init();
  procedure clear();
  //procedure swap()
  procedure append(data : TEmployee);
  procedure delete(const index : integer);

  function empty(): boolean; inline;
  function size() : integer;
  function get(const index : integer) : TEmployee;

end;

implementation

procedure TCustomList.init();
begin
  self.Head := nil;
  self.Last := nil;
end;
procedure TCustomList.clear();
var i : integer;
var current : ^TCustomListItem;
var items : array of ^TCustomListItem;
begin
  if self.Head = nil then
    exit;
  current := @(self.Head^);
  while current <> nil do begin
    SetLength(items, length(items) + 1);
    items[length(items)-1] := @(current^);
    current := @(current.Next^);
  end;
  for i := 0 to length(items)-1 do
    dispose(items[i]);
  SetLength(items, 0);
  self.Head := nil;
  self.Last := nil;
end;
procedure TCustomList.append(data : TEmployee);
var temp : ^TCustomListItem;
begin
  new(temp);
  temp.Next := nil;
  temp.Data := data;
  if (self.Last = nil) and (self.Head = nil) then begin
    self.Last := @(temp^);
    self.Head := @(temp^);
  end
  else begin
    self.Last.Next := @(temp^);
    self.Last := @(temp^);
    if self.Head.Next = nil then
      self.Head.Next := @(self.Last^);
  end;
end;
procedure TCustomList.delete(const index : integer);
var i: integer;
var current, previous : ^TCustomListItem;
begin
  if index < 0 then
    raise Exception.Create('Negative index is not supplied!');
  if self.Head = nil then
    raise Exception.Create('List is empty!');
  current := @(self.Head^);
  if current = @(self.Last^) then begin
    dispose(current);
    self.Head := nil;
    self.Last := nil;
    exit;
  end;
  if index = 0 then begin
    self.Head := @(current.Next^);
    dispose(current);
    exit;
  end;
  for i := 0 to index-1 do begin
    if current = nil then
      raise Exception.Create('Item with index: [' + inttostr(index) + '] does not exist!');
    previous := @(current^);
    current  := @(current.Next^);
  end;
  if current = @(self.Last^) then begin
    previous.Next := nil;
    self.Last := @(previous^);
  end
  else
    previous.Next := current.Next;
  dispose(current);
end;

function TCustomList.empty() : boolean;
begin
  if self.size() = 0 then
    exit(true);
  exit(false);
end;
function TCustomList.size() : integer;
var len : integer;
var current : ^TCustomListItem;
begin
  if self.Head = nil then
    exit(0);
  len := 0;
  current := @(self.Head^);
  while current <> nil do begin
    inc(len);
    current := @(current.Next^);
  end;
  exit(len);
end;
function TCustomList.get(const index : integer) : TEmployee;
var i : integer;
var current : ^TCustomListItem;
begin
  if index < 0 then
    raise Exception.Create('Negative index is not supplied!');
  if self.Head = nil then
    raise Exception.Create('List is empty!');
  current := @(self.Head^);
  for i := 0 to index-1 do begin
    if current = nil then
      raise Exception.Create('Item with index: [' + inttostr(index) + '] does not exist!');
    current := @(current.Next^);
  end;
  exit(current^.Data);
end;

end.
