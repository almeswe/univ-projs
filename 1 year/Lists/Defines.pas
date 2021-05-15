unit Defines;

interface

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

function TEmployee.ToNameString() : string;
begin
  exit(self.Name + ' ' + self.Surname + ' ' + self.Midname);
end;

function TEmployee.ToExtendedNameString() : string;
begin
  exit(self.ToNameString() + ' ' + self.Project.Name + ' ' + self.Project.Task);
end;

end.
