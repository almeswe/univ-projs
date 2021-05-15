unit ECreator;

interface

uses
  System.SysUtils, System.Classes,
  Vcl.Forms, Vcl.StdCtrls, Vcl.Mask, Vcl.Controls,
  List, Defines, Math, Testing;

type TEConstructorForm = class(TForm)

    InfoGroupBox   : TGroupBox;
    AddNewGroupBox : TGroupBox;
    ProjectGroupBox: TGroupBox;
    SheduleGroupBox: TGroupBox;

    NameEdit       : TEdit;
    SurnameEdit    : TEdit;
    MiddlenameEdit : TEdit;
    ProjectNameEdit: TEdit;

    NameLabel        : TLabel;
    SuurnameLabel    : TLabel;
    MiddlenameLabel  : TLabel;
    TaskLabel        : TLabel;
    ProjectNameLabel : TLabel;
    DeadlineLabel    : TLabel;
    FromLabel        : TLabel;
    ToLabel          : TLabel;
    
    ProjectDeadlineMaskEdit: TMaskEdit;
    SheduleEndMaskEdit     : TMaskEdit;
    SheduleStartMaskEdit   : TMaskEdit;
    
    ProjectTaskComboBox: TComboBox;

    SubmitButton: TButton;
    RandomButton: TButton;

    procedure SubmitButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure RandomButtonClick(Sender: TObject);
     
    private
      Employee : TEmployee;
      Employees : TCustomList;
      EmployeeCreated : boolean;

      function CreateEmployee() : boolean;
      function CheckForMistakes() : boolean;

      procedure FillRandomly();

    public
      function GetCreatedEmployee() : TEmployee; inline;
      function IsEmployeeCreated() : boolean; inline;

      procedure SetArgs(employees : TCustomList);
  end;

var EConstructorForm: TEConstructorForm;

implementation

{$R *.dfm}

function TEConstructorForm.CheckForMistakes() : boolean;
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
function TEConstructorForm.CreateEmployee() : boolean;
var Employee : ^TEmployee;
var Project  : ^TEmployeeProject;
var Shedule  : ^TEmployeeShedule;
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

  self.EmployeeCreated := true;
  exit(true);
end;

function  TEConstructorForm.GetCreatedEmployee() : TEmployee;
begin
  exit(self.Employee);
end;
function  TEConstructorForm.IsEmployeeCreated() : boolean;
begin
  exit(self.EmployeeCreated);
end;
procedure TEConstructorForm.SetArgs(employees : TCustomList);
begin
  self.Employees := employees;
end;
procedure TEConstructorForm.FillRandomly();
type TStringPool = array[1..10] of string[20];

const names : TStringPool = ('Ivan',
                             'Alexey',
                             'Anton',
                             'Viktor',
                             'Sergei',
                             'Valeriy',
                             'Nikita',
                             'Grigoriy',
                             'Vaitaliy',
                             'Vladimir');

const surnames : TStringPool = ('Dzhumayev',
                                'Kuznetsov',
                                'Chistovich',
                                'Tatishev',
                                'Hasymov',
                                'Podyomny',
                                'Yeremin',
                                'Raykin',
                                'Shalyapin',
                                'Raykin');

const midnames : TStringPool = ('Ivanov',
                               'Alexeevich',
                               'Antonovich',
                               'Viktorovich',
                               'Sergeevich',
                               'Valerievich',
                               'Nikitin',
                               'Grigorievich',
                               'Vaitalievich',
                               'Vladimirovich');

const projnames : TStringPool = ('Arctic code',
                                 'Google Kubernetes',
                                 'Apache Spark',
                                 'Visual Studio Code',
                                 'NixOS Collection',
                                 'Rust',
                                 'Firehol IP Lists',
                                 'Red Hat OpenShift',
                                 'Ansible',
                                 'WordPress Calypso');

const tasks : TStringPool = ('UX Design',
                             'UI Design',
                             'Management',
                             'Front-end',
                             'Back-end',
                             'Full-stack',
                             'SE', '', '', '');
begin
  self.NameEdit.Text       := names[RandomRange(1, 10)];
  self.SurnameEdit.Text    := surnames[RandomRange(1, 10)];
  self.MiddlenameEdit.Text := midnames[RandomRange(1, 10)];

  self.ProjectNameEdit.Text := projnames[RandomRange(1, 10)];
  self.ProjectTaskComboBox.Text := tasks[RandomRange(1, 7)];

  self.ProjectDeadlineMaskEdit.Text := IntToStr(RandomRange(0, 2)) + IntToStr(RandomRange(1, 9)) + '.0' + IntToStr(RandomRange(1, 9)) + '.21';
  self.SheduleStartMaskEdit.Text := IntToStr(RandomRange(10, 15)) + ':00';
  self.SheduleEndMaskEdit.Text := IntToStr(RandomRange(16, 23)) + ':00';
end;

procedure TEConstructorForm.FormCreate(Sender: TObject);
begin
  randomize;
  self.EmployeeCreated := false;
end;
procedure TEConstructorForm.SubmitButtonClick(Sender: TObject);
begin
  if self.CreateEmployee() then
    self.Close;
end;
procedure TEConstructorForm.RandomButtonClick(Sender: TObject);
begin
   self.FillRandomly();
end;
end.
