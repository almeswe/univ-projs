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
begin
  exit(abs(StrToInt(Copy(self.Shedule.Finish, 0, 2)) - StrToInt(Copy(self.Shedule.Start, 0, 2))));
end;

function TEmployee.GetMonthlyWorkHours() : integer;
begin
  exit(self.GetDaylyWorkHours() * Time.GetWorkDays(Time.GetDaysIn(Time.GetPreviousMonth())));
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
