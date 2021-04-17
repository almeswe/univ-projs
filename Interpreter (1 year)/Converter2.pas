unit Converter2;

interface

uses SysUtils, Stack2, Defines2;

type TConverter = record
  var inited : bool;

  var notation : string;
  var stack    : TStack;
  var errors   : TErrors;
  var actions  : TActions;

  procedure init();
  procedure convert(tokens : TTokens);

  procedure append_action(data : string; kind : TActionKind; out value : string);
  procedure execute_action(data : string; kind : TActionKind; out value : string);

  function is_errored() : bool;

  function is_paren(str : string) : bool;
  function is_cl_paren(str : string) : bool;
  function is_op_paren(str : string) : bool;

  function get_op_priority(op : string) : int;
end;

implementation

procedure TConverter.init();
begin
  self.inited := true;
  self.notation := '';
  self.stack.init();
end;

begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  self.stack.clear();
  self.inited := false;
  self.notation := '';
  SetLength(self.errors, 0);
  SetLength(self.actions, 0);
end;

procedure TConverter.convert(tokens : TTokens);
var i : int;
var out_value : string;
begin
    if not self.inited then
      raise Exception.Create('Call init procedure first!');
   self.notation := '';
   SetLength(self.actions, 0);
   for i := 0 to length(tokens)-1 do begin
       case tokens[i].kind of
          TTokenKind.Constant, TTokenKind.Variable: begin
            self.append_action(tokens[i].data, TActionKind.APPEND, out_value);
          end;

          TTokenKind.Op: begin
            while (not self.stack.empty()) and (self.get_op_priority(self.stack.top().data) >= self.get_op_priority(tokens[i].data)) do begin
              self.append_action(self.stack.top().data, TActionKind.POP, out_value);
              self.notation := self.notation + out_value;
            end;
            self.append_action(tokens[i].data, TActionKind.PUSH, out_value);
          end;

          TTokenKind.OpenParen, TTokenKind.CloseParen: begin
            if self.is_cl_paren(tokens[i].data) then begin
              while (not self.stack.empty()) and (not self.is_op_paren(self.stack.top().data)) do begin
                if not self.is_paren(self.stack.top().data) then begin
                  self.append_action(self.stack.top().data, TActionKind.POP, out_value);
                  self.notation := self.notation + out_value;
                end;
              end;
              if not self.stack.empty() then
                self.append_action(self.stack.top().data, TActionKind.POP, out_value);
            end
            else
              //if not self.stack.empty() then
                self.append_action(tokens[i].data, TActionKind.PUSH, out_value);
          end;
       end;
   end;

   while not self.stack.empty() do begin
      self.append_action(self.stack.top().data, TActionKind.POP, out_value);
      self.notation := self.notation + out_value;
   end;
end;

procedure TConverter.append_action(data: string; kind: TActionKind; out value : string);
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  SetLength(self.actions, length(self.actions)+1);
  self.actions[length(self.actions)-1] := new_action(data, kind);;
  self.execute_action(data, kind, value);
end;

procedure TConverter.execute_action(data : string; kind : TActionKind; out value : string);
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  case kind of
    TActionKind.POP   : value := self.stack.pop().data;
    TActionKind.PUSH  : self.stack.push(data);
    TActionKind.APPEND: self.notation := self.notation + data;
  end;
end;

function TConverter.is_errored() : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if length(self.errors) > 0 then
    exit(true);
  exit(false);
end;

function TConverter.is_paren(str : string) : bool;
begin
   if not self.inited then
    raise Exception.Create('Call init procedure first!');
   case str[1] of
      '(',')' : exit(true);
   end;
   exit(false);
end;

function TConverter.is_cl_paren(str : string) : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if str[1] = ')' then
      exit(true);
   exit(false);
end;

function TConverter.is_op_paren(str : string) : bool;
begin
  if not self.inited then
    raise Exception.Create('Call init procedure first!');
  if str[1] = '(' then
      exit(true);
   exit(false);
end;

function TConverter.get_op_priority(op: string) : int;
begin
  if not self.inited then
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
