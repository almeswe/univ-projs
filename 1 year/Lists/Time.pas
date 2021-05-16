unit Time;

interface

uses SysUtils, DateUtils;

const Jan1 = 5; //2021 only

function GetDaysIn(month : integer) : integer;
function IsLeapYear(year : integer) : boolean;
function DateToInt(date : TDateTime) : integer;
function GetWorkDays(fromDate, toDate : integer) : integer;
function GetPreviousMonth() : integer;
function GetPreiviousMonthDate(date : TDateTime) : TDateTime;
function GetDayFromDate(date : TDateTime) : integer;
function GetYearFromDate(date : TDateTime) : integer;
function GetMonthFromDate(date : TDateTime) : integer;

function GetWorkDaysInMonth(month : integer) : integer;
function GetWorkDaysInBound(month, fromDay, toDay : integer) : integer;

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

function DateToInt(date : TDateTime) : integer;
begin
  exit(DateUtils.DaysBetween(StrToDate('01.01.2021'), date));
end;

function GetWorkDays(fromDate, toDate : integer) : integer;
var i : integer;
var start : integer;
var workDays : integer;
begin
  start := Jan1;
  workDays := 0;
  for i := 1 to fromDate do begin
    if start = 7 then
      start := 0;
    inc(start);
  end;

  for i := 1 to toDate - fromDate do begin
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
var month : integer;
begin
  month := GetMonthFromDate(Now);
  if month = 1 then
    exit(12);
  exit(month-1);
end;

function GetPreiviousMonthDate(date : TDateTime) : TDateTime;
var day, month, year : integer;
begin
  day   := GetDayFromDate(now);
  month := GetMonthFromDate(now);
  year  := GetYearFromDate(now);

  dec(month);
  if month = 0 then
    month := 12;

  exit(StrToDate(IntToStr(day) + '.' + IntToStr(month) + '.' + IntToStr(year)));
end;

function GetMonthFromDate(date : TDateTime) : integer;
var day, month, year : word;
begin
  DecodeDate(date, year, month, day);
  exit(month);
end;

function GetDayFromDate(date : TDateTime) : integer;
var day, month, year : word;
begin
  DecodeDate(date, year, month, day);
  exit(day);
end;

function GetYearFromDate(date : TDateTime) : integer;
var day, month, year : word;
begin
  DecodeDate(date, year, month, day);
  exit(year);
end;

function GetWorkDaysInBound(month, fromDay, toDay : integer) : integer;
var start, finish : TDateTime;
begin
  start  := StrToDate(IntToStr(fromDay) + '.' + IntToStr(month) + '.21');
  finish := StrToDate(IntToStr(toDay)   + '.' + IntToStr(month) + '.21');
  exit(GetWorkDays(DateToInt(start), DateToInt(finish)));
end;

function GetWorkDaysInMonth(month : integer) : integer;
var start, finish : TDateTime;
begin
  start  := StrToDate('01.' + IntToStr(month) + '.21');
  finish := StrToDate(IntToStr(GetDaysIn(month))   + '.' + IntToStr(month) + '.21');
  exit(GetWorkDays(DateToInt(start), DateToInt(finish)));
end;

end.
