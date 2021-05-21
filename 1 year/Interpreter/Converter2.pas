unit Converter2;

interface

uses SysUtils, Stack2, Defines2;

type TConverter = record
  Inited : bool;

  Notation : string;
  Stack    : TStack;
  Errors   : TErrors;
  Actions  : TActions;

  procedure Init();
  procedure Discard();
  procedure Convert(tokens : TTokens);

  procedure AppendAction(data : string; kind : TActionKind; out value : string);
  procedure ExecuteAction(data : string; kind : TActionKind; out value : string);

  function IsErrored() : bool;

  function IsParen(str : string) : bool;
  function IsCloseParen(str : string) : bool;
  function IsOpenParen(str : string) : bool;

  function GetOperatorPriority(op : string) : int;
end;

implementation

procedure TConverter.Init();
begin
  self.Inited := true;
  self.Notation := '';
  self.Stack.Init();
end;

procedure TConverter.Discard();
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  self.Stack.Clear();
  self.Inited := false;
  SetLength(self.Errors, 0);
  SetLength(self.Actions, 0);
end;

procedure TConverter.Convert(tokens : TTokens);
var i : int;
var outValue : string;
begin
    if not self.Inited then
      raise Exception.Create('Call init procedure first!');
   self.Notation := '';
   SetLength(self.Actions, 0);
   for i := 0 to length(tokens)-1 do begin
       case tokens[i].Kind of
          TTokenKind.Constant, TTokenKind.Variable: begin
            self.AppendAction(tokens[i].Data, TActionKind.APPEND, outValue);
          end;

          TTokenKind.Op: begin
            while (not self.Stack.Empty()) and (self.GetOperatorPriority(self.Stack.Top().Data) >= self.GetOperatorPriority(tokens[i].Data)) do begin
              self.AppendAction(self.Stack.Top().Data, TActionKind.POP, outValue);
              self.Notation := self.Notation + outValue;
            end;
            self.AppendAction(tokens[i].Data, TActionKind.PUSH, outValue);
          end;

          TTokenKind.OpenParen, TTokenKind.CloseParen: begin
            if self.IsCloseParen(tokens[i].Data) then begin
              while (not self.Stack.Empty()) and (not self.IsOpenParen(self.Stack.Top().Data)) do begin
                if not self.IsParen(self.Stack.Top().Data) then begin
                  self.AppendAction(self.Stack.Top().Data, TActionKind.POP, outValue);
                  self.Notation := self.Notation + outValue;
                end;
              end;
              if not self.Stack.Empty() then
                self.AppendAction(self.Stack.Top().Data, TActionKind.POP, outValue);
            end
            else
              self.AppendAction(tokens[i].Data, TActionKind.PUSH, outValue);
          end;
       end;
   end;

   while not self.Stack.Empty() do begin
      self.AppendAction(self.Stack.Top().Data, TActionKind.POP, outValue);
      self.Notation := self.Notation + outValue;
   end;
end;

procedure TConverter.AppendAction(data: string; kind: TActionKind; out value : string);
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.Actions, length(self.Actions)+1);
  self.Actions[length(self.Actions)-1] := NewAction(data, kind);;
  self.ExecuteAction(data, kind, value);
end;

procedure TConverter.ExecuteAction(data : string; kind : TActionKind; out value : string);
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  case kind of
    TActionKind.POP   : value := self.Stack.Pop().Data;
    TActionKind.PUSH  : self.Stack.Push(data);
    TActionKind.APPEND: self.Notation := self.Notation + data;
  end;
end;

function TConverter.IsErrored() : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if length(self.Errors) > 0 then
    exit(true);
  exit(false);
end;

function TConverter.IsParen(str : string) : bool;
begin
   if not self.Inited then
    raise Exception.Create('Call init procedure first!');
   case str[1] of
      '(',')' : exit(true);
   end;
   exit(false);
end;

function TConverter.IsCloseParen(str : string) : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if str[1] = ')' then
      exit(true);
   exit(false);
end;

function TConverter.IsOpenParen(str : string) : bool;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  if str[1] = '(' then
      exit(true);
   exit(false);
end;

function TConverter.GetOperatorPriority(op: string) : int;
begin
  if not self.Inited then
    raise Exception.Create('Call init procedure first!');
  case op[1] of
       '(',')' : exit(1);
       '+','-' : exit(2);
       '*','/' : exit(3);
       '^'     : exit(4);
  end;
  raise Exception.Create('Unknown operator: ' + op);
end;

end.
