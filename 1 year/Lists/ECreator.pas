unit ECreator;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.Mask, Defines, Math;

type
  TEConstructorForm = class(TForm)

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
      Employee        : TEmployee;
      EmployeeCreated : boolean;

      function CreateEmployee() : boolean;
      function CheckForMistakes() : boolean;
      function CheckLongDate(date : string) : boolean;
      function CheckShortDate(date : string) : boolean;
      function IsTextEditEmpty(edit : TEdit) : boolean;
      function IsComboBoxEmpty(box: TComboBox) : boolean;
      function IsMaskEditEmpty(maskedit: TMaskEdit; shorttime : boolean = false) : boolean;

      procedure FillRandomly();
      procedure ShowErrorMessageBox(msg : string; title : string = '');

    public
      function GetCreatedEmployee() : TEmployee; inline;
      function IsEmployeeCreated() : boolean; inline;
  end;

var EConstructorForm: TEConstructorForm;

implementation

{$R *.dfm}

function TEConstructorForm.CheckLongDate(date : string) : boolean;
var day, mnth, year : string;
begin
  day  := Copy(date, 0, 2);
  mnth := Copy(date, 4, 2);
  year := Copy(date, 7, 2);
  if (StrToInt(day) < 0) or (StrToInt(day) > 30) then
    exit(false);
  if (StrToInt(mnth) < 0) or (StrToInt(mnth) > 12) then
    exit(false);
  if (StrToInt(year) < 20) or (StrToInt(year) > 50) then
    exit(false);
  exit(true);
end;
function TEConstructorForm.CheckShortDate(date: string) : boolean;
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
function TEConstructorForm.IsTextEditEmpty(edit: TEdit) : boolean;
begin
  if Trim(edit.Text) <> '' then
    exit(true);
  exit(false);
end;

procedure TEConstructorForm.RandomButtonClick(Sender: TObject);
begin
   self.FillRandomly();
end;

function TEConstructorForm.IsComboBoxEmpty(box: TComboBox) : boolean;
begin
  if Trim(box.Text) <> '' then
    exit(true);
  exit(false);
end;
function TEConstructorForm.IsMaskEditEmpty(maskedit: TMaskEdit; shorttime : boolean = false) : boolean;
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
function TEConstructorForm.CheckForMistakes() : boolean;
begin
  if not self.IsTextEditEmpty(self.NameEdit)        or
     not self.IsTextEditEmpty(self.SurnameEdit)     or
     not self.IsTextEditEmpty(self.MiddlenameEdit)  or
     not self.IsTextEditEmpty(self.ProjectNameEdit) then begin
        self.ShowErrorMessageBox('One or more text fields are empty.', 'Error');
        exit(false);
     end;

  if not self.IsComboBoxEmpty(self.ProjectTaskComboBox) then begin
      self.ShowErrorMessageBox('Project task is empty.', 'Error');
      exit(false);
  end;

  if not self.IsMaskEditEmpty(self.ProjectDeadlineMaskEdit)   or
     not self.IsMaskEditEmpty(self.SheduleEndMaskEdit , true) or
     not self.IsMaskEditEmpty(self.SheduleStartMaskEdit,true) then begin
        self.ShowErrorMessageBox('One or more date fields are empty.', 'Error');
        exit(false);
     end;

  if not self.CheckLongDate(self.ProjectDeadlineMaskEdit.Text) or
     not self.CheckShortDate(self.SheduleEndMaskEdit.Text)     or
     not self.CheckShortDate(self.SheduleStartMaskEdit.Text)   then begin
      self.ShowErrorMessageBox('One or more date fields are filled incorrect.', 'Error');
      exit(false);
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

  new(Shedule);
  Shedule.Start  := self.SheduleStartMaskEdit.Text;
  Shedule.Finish := self.SheduleEndMaskEdit.Text;

  new(Project);
  Project.Name     := self.ProjectNameEdit.Text;
  Project.Task     := self.ProjectTaskComboBox.Text;
  Project.Deadline := self.ProjectDeadlineMaskEdit.Text;

  new(Employee);
  Employee.Name    := self.NameEdit.Text;
  Employee.Surname := self.SurnameEdit.Text;
  Employee.Midname := self.MiddlenameEdit.Text;
  Employee.Project := Project^;
  Employee.Shedule := Shedule^;

  self.Employee        := Employee^;
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

  self.ProjectDeadlineMaskEdit.Text := IntToStr(RandomRange(0, 2)) + IntToStr(RandomRange(1, 9)) + '.0' + IntToStr(RandomRange(1, 9)) + '.' + IntToStr(RandomRange(21, 30));
  self.SheduleStartMaskEdit.Text := IntToStr(RandomRange(10, 15)) + ':00';
  self.SheduleEndMaskEdit.Text := IntToStr(RandomRange(16, 23)) + ':00';
end;
procedure TEConstructorForm.ShowErrorMessageBox(msg: string; title: string = '');
begin
  MessageBox(self.Handle, PChar(msg), PChar(title), MB_OK + MB_ICONERROR);
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
end.
