unit Time;

interface

uses SysUtils, DateUtils;

const Jan1 = 5; //2021 only

function GetDaysIn(month : integer) : integer;
function IsLeapYear(year : integer) : boolean;
function DateToInteger(date : TDateTime) : integer;
function GetWorkDays(daysToDate : integer) : integer;
function GetPreviousMonth() : integer;

implementation

function IsLeapYear(year : integer) : boolean;
begin
  if (year mod 4 = 0)   or
     (year mod 100 = 0) and
     (year mod 400 = 0) then begin
     exit(true);
   end;
  exit(false)
end;

function GetDaysIn(month : integer) : integer;
begin
  if month = 2 then begin
     if IsLeapYear(2021) then
      exit(29)
     else
      exit(28);
  end;
  case month of
      1, 3, 5, 7, 8, 10, 12 : exit(31);
  end;
  exit(30);
end;

function DateToInteger(date : TDateTime) : integer;
begin
  exit(DateUtils.DaysBetween(StrToDate('01.01.2021'), date));
end;

function GetWorkDays(daysToDate : integer) : integer;
var i : integer;
var start : integer;
var workDays : integer;
begin
  start := Jan1;
  workDays := 0;
  for i := 0 to daysToDate do begin
    if (start <> 6) and (start <> 7) then
      inc(workDays)
    else
      if start = 7 then
        start := -1;
    inc(start);
  end;
  exit(workDays);
end;

function GetPreviousMonth() : integer;
var day, month, year : word;
begin
  DecodeDate(now, year, month, day);
  if month = 1 then
    exit(12);
  exit(month-1);
end;

end.
