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

  function SameEmployee(other : string) : boolean;
end;
type TEmployees = array of TEmployee;

type TStringArray = array of string;

implementation

function TEmployee.SameEmployee(other : string) : boolean;
begin
  if self.Name + ' ' + self.Surname + ' ' + self.Midname = other then
      exit(true);
  exit(false);
end;

end.
