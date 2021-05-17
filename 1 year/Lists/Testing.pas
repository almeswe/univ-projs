unit Testing;

interface

uses Winapi.Windows, System.Character, Vcl.Mask, Vcl.StdCtrls, SysUtils, Defines, Time;

function IsEditValid(edit : TEdit) : boolean;
function CheckLongDate(date : string) : boolean;
function CheckShortDate(date : string) : boolean;
function IsTextEditEmpty(edit : TEdit) : boolean;
function IsComboBoxEmpty(box: TComboBox) : boolean;
function IsMaskEditEmpty(maskedit: TMaskEdit; shorttime : boolean = false) : boolean;

procedure ShowErrorMessageBox(handle : HWND; msg : string; title : string = 'Error');

implementation

function IsEditValid(edit : TEdit) : boolean;
var i : integer;
var a : string;
begin
  a := edit.Text;
  for i := 1 to length(Trim(edit.Text)) do
  if not (((edit.Text[i] <= 'Z') and (edit.Text[i] >= 'A') or (edit.Text[i] <= 'z') and (edit.Text[i] >= 'a')) or (edit.Text[i] = ' ')) then
    exit(false);
  exit(true);
end;

function CheckLongDate(date : string) : boolean;
var day, mnth, year : string;
begin
  day  := Copy(date, 0, 2);
  mnth := Copy(date, 4, 2);
  year := Copy(date, 7, 2);
  if (StrToInt(day) < 1) or (StrToInt(day) > Time.GetDaysIn(StrToInt(mnth))) then
    exit(false);
  if (StrToInt(mnth) < 1) or (StrToInt(mnth) > 12) then
    exit(false);
  if StrToInt(year) <> 21 then
    exit(false);
  exit(true);
end;

function CheckShortDate(date: string) : boolean;
var hrs, mins : string;
begin
  hrs  := Copy(date, 0, 2);
  mins := Copy(date, 4, 2);
  if (StrToInt(hrs) < 0) or (StrToInt(hrs) > 23) then
    exit(false);
  if (StrToInt(mins) < 0) or (StrToInt(mins) > 59) then
    exit(false);
  exit(true);
end;

function IsTextEditEmpty(edit: TEdit) : boolean;
begin
  if Trim(edit.Text) <> '' then
    exit(true);
  exit(false);
end;
function IsComboBoxEmpty(box: TComboBox) : boolean;
begin
  if Trim(box.Text) <> '' then
    exit(true);
  exit(false);
end;

function IsMaskEditEmpty(maskedit: TMaskEdit; shorttime : boolean = false) : boolean;
var a : string;
begin
  a := maskedit.Text;
  if not shorttime then
    if Trim(maskedit.Text) = '..' then
      exit(false);
  if shorttime then
    if Trim(maskedit.Text) = ':' then
      exit(false);
  exit(true);
end;

procedure ShowErrorMessageBox(handle : HWND; msg: string; title: string = 'Error');
begin
  MessageBox(handle, PChar(msg), PChar(title), MB_OK + MB_ICONERROR);
end;

end.
