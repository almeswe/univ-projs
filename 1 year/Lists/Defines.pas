unit Defines;

interface

uses SysUtils, Time;

type TEmployeeShedule = record
  Start  : string[5];
  Finish : string[5];
end;
type TEmployeeProject = record
  Name : string[20];
  Task : string[20];
  Deadline : string[8];
end;
type TEmployee = record
  Name    : string[20];
  Surname : string[20];
  Midname : string[20];
  Project : TEmployeeProject;
  Shedule : TEmployeeShedule;

  function GetDaylyWorkHours() : integer;
  function GetMonthlyWorkHours() : integer;
  function ToNameString() : string;
  function ToExtendedNameString() : string;
end;
type TEmployees = array of TEmployee;

type TStringArray = array of string;

function NullEmployee() : TEmployee;

implementation

function NullEmployee() : TEmployee;
var employee : TEmployee;
begin
  employee.Shedule.Start  := '';
  employee.Shedule.Finish := '';

  employee.Project.Name     := '';
  employee.Project.Task     := '';
  employee.Project.Deadline := '';

  employee.Name    := '';
  employee.Surname := '';
  employee.Midname := '';

  exit(employee);
end;

function TEmployee.GetDaylyWorkHours() : integer;
var start, finish : integer;
begin
  start := StrToInt(Copy(self.Shedule.Start, 0, 2));
  finish := StrToInt(Copy(self.Shedule.Finish, 0, 2));

  if finish < start then
    finish := finish + 24;

  exit(finish - start);
end;

function TEmployee.GetMonthlyWorkHours() : integer;
var a : integer;
begin
  a := Time.GetMonthFromDate(StrToDate(self.Project.Deadline));
  if Time.GetMonthFromDate(Now)-1 = a then begin
    exit(self.GetDaylyWorkHours() * Time.GetWorkDaysInBound(a, 1, Time.GetDayFromDate(StrToDate(self.Project.Deadline))));
  end;
  if (Time.GetMonthFromDate(Now)-1 < a) and (a <> 1) then
    exit(self.GetDaylyWorkHours() * Time.GetWorkDaysInMonth(a-1))
  else
    exit(0);
end;

function TEmployee.ToNameString() : string;
begin
  exit(self.Name + ' ' + self.Surname + ' ' + self.Midname);
end;

function TEmployee.ToExtendedNameString() : string;
begin
  exit(self.ToNameString() + ' ' + self.Project.Name + ' ' + self.Project.Task);
end;

end.
