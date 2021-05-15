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

  procedure Init();
  procedure Clear();
  procedure Append(data : TEmployee);
  procedure DeleteByName(name : string);
  procedure DeleteByExtendedName(name : string);
  procedure Delete(const index : integer);
  procedure Swap(index1, index2 : integer);
  procedure SetData(const index : integer; data : TEmployee);

  function Size() : integer;
  function Empty(): boolean; inline;
  function HasWithName(name : string) : boolean;
  function GetData(const index : integer) : TEmployee;
  function GetByName(name : string; out index : integer) : TEmployee;
  function GetByExtendedName(name : string; out index : integer) : TEmployee;

end;

implementation

procedure TCustomList.Init();
begin
  self.Head := nil;
  self.Last := nil;
end;

procedure TCustomList.Clear();
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

procedure TCustomList.Append(data : TEmployee);
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

procedure TCustomList.Delete(const index : integer);
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

procedure TCustomList.Swap(index1, index2 : integer);
var buff : TEmployee;
begin
  buff := self.GetData(index1);
  self.SetData(index1, self.GetData(index2));
  self.SetData(index2, buff);
end;

procedure TCustomList.SetData(const index : integer; data : TEmployee);
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
  current^.Data := data;
end;

function TCustomList.Empty() : boolean;
begin
  if self.Size() = 0 then
    exit(true);
  exit(false);
end;

function TCustomList.Size() : integer;
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

function TCustomList.GetData(const index : integer) : TEmployee;
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

function TCustomList.HasWithName(name : string) : boolean;
var index : integer;
begin
  self.GetByName(name, index);
  if index <> -1 then
    exit(true);
  exit(false);
end;

procedure TCustomList.DeleteByName(name : string);
var i : integer;
begin
  for i := 0 to self.Size()-1 do begin
    if self.GetData(i).ToNameString() = name then begin
      self.Delete(i);
      break;
    end;
  end;
end;

procedure TCustomList.DeleteByExtendedName(name : string);
var i : integer;
begin
  for i := 0 to self.Size()-1 do begin
    if self.GetData(i).ToExtendedNameString() = name then begin
      self.Delete(i);
      break;
    end;
  end;
end;

function TCustomList.GetByName(name : string; out index : integer) : TEmployee;
var i : integer;
begin
  for i := 0 to self.Size()-1 do begin
    if self.GetData(i).ToNameString() = name then begin
      Index := i;
      exit(self.GetData(i));
    end;
  end;
  Index := -1;
end;

function TCustomList.GetByExtendedName(name : string; out index : integer) : TEmployee;
var i : integer;
begin
  for i := 0 to self.Size()-1 do begin
    if self.GetData(i).ToExtendedNameString() = name then begin
      Index := i;
      exit(self.GetData(i));
    end;
  end;
  Index := -1;
end;

end.
