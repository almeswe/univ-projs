unit ECorrector;

interface

uses
  System.SysUtils, System.Classes,
  Vcl.Forms, Vcl.StdCtrls, Vcl.Mask, Vcl.Controls,
  List, Defines, Testing;

type TECorrectForm = class(TForm)

    CorrectGroupBox:        TGroupBox;
    CorrectInfoGroupBox:    TGroupBox;
    CorrectProjectGroupBox: TGroupBox;
    CorrectSheduleGroupBox: TGroupBox;

    NameLabel:        TLabel;
    MiddlenameLabel:  TLabel;
    SurnameLabel:     TLabel;
    ProjectNameLabel: TLabel;
    TaskLabel:        TLabel;
    DeadlineLabel:    TLabel;
    FromLabel:        TLabel;
    ToLabel:          TLabel;

    NameEdit:        TEdit;
    MiddlenameEdit:  TEdit;
    SurnameEdit:     TEdit;
    ProjectNameEdit: TEdit;

    ProjectTaskComboBox: TComboBox;

    ProjectDeadlineMaskEdit: TMaskEdit;
    SheduleEndMaskEdit:      TMaskEdit;
    SheduleStartMaskEdit:    TMaskEdit;

    CorrectSubmitButton: TButton;
    procedure CorrectSubmitButtonClick(Sender: TObject);

  private
    var Employees : TCustomList;
    var Employee : TEmployee;
    var EmployeeCorrected : boolean;

    function CheckForMistakes() : boolean;
    function CorrectEmployee() : boolean;

    procedure ShowOnForm(employee : TEmployee);

  public
    function IsEmployeeCorrected() : boolean; inline;
    function GetCorrectedEmployee() : TEmployee; inline;

    procedure SetArgs(employee : TEmployee; employees : TCustomList);
  end;

var ECorrectForm: TECorrectForm;

implementation

{$R *.dfm}

function TECorrectForm.CheckForMistakes() : boolean;
var i : integer;
begin
  if not IsTextEditEmpty(self.NameEdit)        or
     not IsTextEditEmpty(self.SurnameEdit)     or
     not IsTextEditEmpty(self.MiddlenameEdit)  or
     not IsTextEditEmpty(self.ProjectNameEdit) then begin
        ShowErrorMessageBox(self.Handle, 'One or more text fields are empty.', 'Error');
        exit(false);
     end;

  if not IsComboBoxEmpty(self.ProjectTaskComboBox) then begin
      ShowErrorMessageBox(self.Handle, 'Project task is empty.', 'Error');
      exit(false);
  end;

  if not IsMaskEditEmpty(self.ProjectDeadlineMaskEdit)   or
     not IsMaskEditEmpty(self.SheduleEndMaskEdit , true) or
     not IsMaskEditEmpty(self.SheduleStartMaskEdit,true) then begin
        ShowErrorMessageBox(self.Handle, 'One or more date fields are empty.', 'Error');
        exit(false);
     end;

  if not CheckLongDate(self.ProjectDeadlineMaskEdit.Text) or
     not CheckShortDate(self.SheduleEndMaskEdit.Text)     or
     not CheckShortDate(self.SheduleStartMaskEdit.Text)   then begin
      ShowErrorMessageBox(self.Handle, 'One or more date fields are filled incorrect.', 'Error');
      exit(false);
     end;

  if not IsEditValid(self.NameEdit)        or
     not IsEditValid(self.SurnameEdit)     or
     not IsEditValid(self.MiddlenameEdit)  or
     not IsEditValid(self.ProjectNameEdit) then begin
        ShowErrorMessageBox(self.Handle, 'One or more text fields are filled incorrect.', 'Error');
        exit(false);
     end;

  if StrToInt(Copy(self.SheduleStartMaskEdit.Text, 0, 2)) = StrToInt(Copy(self.SheduleEndMaskEdit.Text, 0, 2)) then begin
    ShowErrorMessageBox(self.Handle, 'The work day must be at least 1 hour!', 'Error');
    exit(false);
  end;

  for i := 0 to self.Employees.Size()-1 do begin
    if self.Employees.GetData(i).ToExtendedNameString() <> self.Employee.ToExtendedNameString() then begin
       if self.Employees.GetData(i).ToNameString() =  self.NameEdit.Text + ' ' + self.SurnameEdit.Text + ' ' + self.MiddlenameEdit.Text then begin
          if (self.Employees.GetData(i).Project.Name <> self.ProjectNameEdit.Text) or (self.Employees.GetData(i).Project.Task = self.ProjectTaskComboBox.Text) then begin
            ShowErrorMessageBox(self.Handle, 'Something went wrong when trying to correct employee!', 'Error');
            exit(false);
          end;
       end;
    end;
  end;

  exit(true);
end;
function TECorrectForm.CorrectEmployee() : boolean;
begin
  if not self.CheckForMistakes() then
    exit(false);

  self.Employee.Shedule.Start  := self.SheduleStartMaskEdit.Text;
  self.Employee.Shedule.Finish := self.SheduleEndMaskEdit.Text;

  self.Employee.Project.Name     := self.ProjectNameEdit.Text;
  self.Employee.Project.Task     := self.ProjectTaskComboBox.Text;
  self.Employee.Project.Deadline := self.ProjectDeadlineMaskEdit.Text;

  self.Employee.Name    := self.NameEdit.Text;
  self.Employee.Surname := self.SurnameEdit.Text;
  self.Employee.Midname := self.MiddlenameEdit.Text;

  self.EmployeeCorrected := true;
  exit(true);
end;
function TECorrectForm.IsEmployeeCorrected() : boolean;
begin
  exit(self.EmployeeCorrected);
end;
function TECorrectForm.GetCorrectedEmployee() : TEmployee;
begin
  exit(self.Employee);
end;
procedure TECorrectForm.SetArgs(employee : TEmployee; employees : TCustomList);
begin
  self.Employee := employee;
  self.Employees := employees;
  self.ShowOnForm(self.Employee);
end;

procedure TECorrectForm.ShowOnForm(employee : TEmployee);
begin
  self.NameEdit.Text       := employee.Name;
  self.SurnameEdit.Text    := employee.Surname;
  self.MiddlenameEdit.Text := employee.Midname;

  self.ProjectNameEdit.Text         := employee.Project.Name;
  self.ProjectTaskComboBox.Text     := employee.Project.Task;
  self.ProjectDeadlineMaskEdit.Text := employee.Project.Deadline;

  self.SheduleStartMaskEdit.Text := employee.Shedule.Start;
  self.SheduleEndMaskEdit.Text   := employee.Shedule.Finish;
end;

procedure TECorrectForm.CorrectSubmitButtonClick(Sender: TObject);
begin
  if self.CorrectEmployee() then
    self.Close;
end;

end.
