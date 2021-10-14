unit LinkedList;

interface

uses SysUtils;

type int = integer;
type bool = boolean;

type Abonent = record
  Phone: string;
  Surname: string;
end;

type TLinkedListItem = record
  Data: Abonent;
  Next: ^TLinkedListItem;
end;

type TLinkedListItemPtr = ^TLinkedListItem;

type TLinkedList = record
  Head: ^TLinkedListItem;
  Last: ^TLinkedListItem;

  procedure Init;
  procedure Free;

  procedure PrintInConsole;
  procedure Add(data: Abonent);
  procedure RemoveAt(index: int);

  procedure SetAt(index: int; data: Abonent);

  function Size() : int;
  function GetAt(index: int) : Abonent;
  function GetItemAt(index: int) : TLinkedListItemPtr;
end;

type TLinkedListPtr = ^TLinkedList;

implementation

procedure TLinkedList.Init;
begin
  self.Head := nil;
  self.Last := nil;
end;

procedure TLinkedList.Free;
var i: int;
var item, dup: ^TLinkedListItem;
begin
  item := @(self.Head^);
  for i := 0 to self.Size()-1 do begin
    dup := @(item^);
    item := @(item.Next^);
    FreeMem(dup);
  end;
end;

procedure TLinkedList.PrintInConsole;
var i: int;
var item: Abonent;
begin
  Writeln;
  for i := 0 to self.Size-1 do begin
    item := self.GetAt(i);
    Writeln('----', i+1, ') ', item.Surname, ' > ', item.Phone);
  end;
end;

procedure TLinkedList.Add(data: Abonent);
var item: ^TLinkedListItem;
begin
  New(item);
  item.Next := nil;
  item.Data := data;

  if self.Head = nil then begin
    self.Head := @(item^);
    self.Last := @(self.Head^);
  end
  else begin
    if self.Head = @(self.Last^) then begin
      self.Last := @(item^);
      self.Head.Next := @(self.Last^);
    end
    else begin
      self.Last.Next := @(item^);
      self.Last := @(item^);
    end;
  end;
end;

procedure TLinkedList.RemoveAt(index: int);
var i: int;
var prev, curr, next: ^TLinkedListItem;
begin
  prev := nil;
  if index - 1 >= 0 then
    prev := @(self.GetItemAt(index - 1)^);
  if prev = nil then
    curr := @(self.Head^)
  else
    curr := @(prev.Next^);
  if curr = nil then
    raise Exception.Create('Cannot get item by this index!');
  next := @(curr.Next^);

  if (prev = nil) and (next = nil) then begin
     self.Head := nil;
     self.Last := nil;
  end
  else begin
    if prev = nil then begin
      self.Head := @(next^);
    end;
    if next = nil then begin
      self.Last := @(prev^);
      self.Last.Next := nil;
    end;
    if (prev <> nil) and (next <> nil) then
      prev.Next := @(next^);
  end;
  FreeMem(curr);
end;

procedure TLinkedList.SetAt(index: int; data: Abonent);
var i: int;
var item: ^TLinkedListItem;
begin
  item := @(self.Head^);
  for i := 0 to index-1 do begin
    if item = nil then
      raise Exception.Create('Cannot get item by this index!');
    item := @(item.Next^);
  end;
  item^.Data := data;
end;

function TLinkedList.Size() : int;
var len: int;
var item: ^TLinkedListItem;
begin
  len := 0;
  item := @(self.Head^);
  while item <> nil do begin
    inc(len);
    item := @(item.Next^);
  end;
  exit(len);
end;

function TLinkedList.GetAt(index: int) : Abonent;
var i: int;
var item: ^TLinkedListItem;
begin
  item := @(self.Head^);
  for i := 0 to index-1 do begin
    if item = nil then
      raise Exception.Create('Cannot get item by this index!');
    item := @(item.Next^);
  end;
  exit(item.Data);
end;


function TLinkedList.GetItemAt(index: int) : TLinkedListItemPtr;
var i: int;
var item: ^TLinkedListItem;
begin
  item := @(self.Head^);
  for i := 0 to index-1 do begin
    if item = nil then
      raise Exception.Create('Cannot get item by this index!');
    item := @(item.Next^);
  end;
  exit(@(item^));
end;

end.
