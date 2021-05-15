unit Main;

interface

uses
  System.SysUtils, System.Classes,
  Vcl.Forms, Vcl.StdCtrls, Vcl.Mask, Vcl.Controls, Vcl.Dialogs, Vcl.ComCtrls,
  Defines, DB, Testing, List, ETyper, ECreator, ECorrector;

type TMainForm = class(TForm)

    DataListView: TListView;

    LoadButton:   TButton;
    SaveButton:   TButton;
    DeleteButton: TButton;
    AddNewButton: TButton;
    CorrectButton: TButton;
    ClearButton: TButton;

    OpenDialog: TOpenDialog;
    SETasksRadioButton: TRadioButton;
    NoneRadioButton: TRadioButton;
    SENTasksRadioButton: TRadioButton;

    procedure FormResize(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure LoadButtonClick(Sender: TObject);
    procedure SaveButtonClick(Sender: TObject);
    procedure DeleteButtonClick(Sender: TObject);
    procedure AddNewButtonClick(Sender: TObject);
    procedure CorrectButtonClick(Sender: TObject);
    procedure ClearButtonClick(Sender: TObject);
    procedure NoneRadioButtonClick(Sender: TObject);
    procedure SETasksRadioButtonClick(Sender: TObject);
    procedure SENTasksRadioButtonClick(Sender: TObject);
    procedure DataListViewKeyPress(Sender: TObject; var Key: Char);

    private
      type TSortKind = (None, SETasks, SENTasks);

      var Employees : TCustomList;
      var CurrentSort : TSortKind;

      var SpecifiedEmployee : TEmployee;

      procedure Refresh();
      procedure RefreshNone();
      procedure RefreshSETasks();
      procedure RefreshSENTasks();
      procedure AddEmployee(employee : TEmployee);

      procedure GetSpecifiedEmployee();
  end;

var MainForm : TMainForm;

implementation

{$R *.dfm}

procedure TMainForm.Refresh();
begin
  self.DataListView.Items.Clear;
  case self.CurrentSort of
    TSortKind.None:
      self.RefreshNone;

    TSortKind.SETasks:
      self.RefreshSETasks;

    TSortKind.SENTasks:
      self.RefreshSENTasks;
  end;
end;

procedure TMainForm.RefreshNone();
var i : integer;
begin
  for i := 0 to self.Employees.Size()-1 do
    self.AddEmployee(self.Employees.GetData(i));
end;

procedure TMainForm.RefreshSETasks();
var i, j : integer;
var list : TCustomList;
begin
  list.Init();
  for i := 0 to self.Employees.Size()-1 do
    if self.Employees.GetData(i).ToNameString() = self.SpecifiedEmployee.ToNameString() then
      list.Append(self.Employees.GetData(i));

  for i := 0 to list.Size()-1 do
    for j := i+1 to list.Size()-1 do
      if StrToDate(list.GetData(j).Project.Deadline) < StrToDate(list.GetData(i).Project.Deadline) then
        list.Swap(i, j);

  for i := 0 to list.Size()-1 do
    self.AddEmployee(list.GetData(i));

  list.Clear;
end;

procedure TMainForm.RefreshSENTasks();
var i : integer;
var list : TCustomList;
begin
  list.Init();
  for i := 0 to self.Employees.Size()-1 do
    if self.Employees.GetData(i).ToNameString() = self.SpecifiedEmployee.ToNameString() then
      if now < StrToDate(self.Employees.GetData(i).Project.Deadline) then
        list.Append(self.Employees.GetData(i));

  for i := 0 to list.Size()-1 do
    self.AddEmployee(list.GetData(i));

  list.Clear;
end;

procedure TMainForm.AddEmployee(employee: TEmployee);
var item : TListItem;
begin
  item := self.DataListView.Items.Add;
  item.Caption := employee.Name + ' ' + employee.Surname + ' ' + employee.Midname;
  item.SubItems.Add(employee.Project.Name);
  item.SubItems.Add(employee.Project.Task);
  item.SubItems.Add(employee.Project.Deadline);
  item.SubItems.Add(employee.Shedule.Start + ' - ' + employee.Shedule.Finish);
end;

procedure TMainForm.GetSpecifiedEmployee();
var index : integer;
var TypeForm : TETypeForm;
begin
  TypeForm := TETypeForm.Create(self);
  TypeForm.SetArgs(self.Employees);
  TypeForm.Position := TPosition.poScreenCenter;
  TypeForm.ShowModal;
  if TypeForm.IsEmployeeSpecified() then
    self.SpecifiedEmployee := self.Employees.GetByName(TypeForm.GetSpecifiedEmployeeName(), index)
  else
    self.SpecifiedEmployee := NullEmployee();
  TypeForm.Release;
end;

procedure TMainForm.FormCreate(Sender: TObject);
begin
  self.Employees.Init();
  self.OpenDialog.Create(self);
  self.SpecifiedEmployee := NullEmployee();
end;

procedure TMainForm.FormResize(Sender: TObject);
var i : integer;
var c_width : integer;
begin
  c_width := round(self.DataListView.Width/self.DataListView.Columns.Count);
  for i := 0 to self.DataListView.Columns.Count-1 do
    self.DataListView.Columns[i].Width := c_width;
end;

procedure TMainForm.LoadButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then begin
    self.Employees := DB.LoadFromTypeFile(self.OpenDialog.FileName);
    self.Refresh;
  end;
end;

procedure TMainForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    DB.SaveToTypeFile(self.OpenDialog.FileName, self.Employees);
end;

procedure TMainForm.NoneRadioButtonClick(Sender: TObject);
begin
  self.CurrentSort := TSortKind.None;
  self.Refresh();
end;

procedure TMainForm.SENTasksRadioButtonClick(Sender: TObject);
begin
  self.GetSpecifiedEmployee();
  if Trim(self.SpecifiedEmployee.ToNameString()) <> '' then begin
    self.CurrentSort := TSortKind.SENTasks;
    self.Refresh;
  end
  else
    self.NoneRadioButton.Checked := true;
end;

procedure TMainForm.SETasksRadioButtonClick(Sender: TObject);
var employee : TEmployee;
begin
  self.GetSpecifiedEmployee();
  if Trim(self.SpecifiedEmployee.ToNameString()) <> '' then begin
    self.CurrentSort := TSortKind.SETasks;
    self.Refresh;
  end
  else
    self.NoneRadioButton.Checked := true;
end;

procedure TMainForm.AddNewButtonClick(Sender: TObject);
var ConstructorForm : TEConstructorForm;
begin
  ConstructorForm := TEConstructorForm.Create(self);
  ConstructorForm.SetArgs(self.Employees);
  ConstructorForm.Position := TPosition.poScreenCenter;
  ConstructorForm.ShowModal;
  if ConstructorForm.IsEmployeeCreated() then begin
    self.AddEmployee(ConstructorForm.GetCreatedEmployee());
    self.Employees.Append(ConstructorForm.GetCreatedEmployee());
    self.Refresh;
  end;
  ConstructorForm.Release;
end;

procedure TMainForm.CorrectButtonClick(Sender: TObject);
var Name : string;
var Index : integer;
var CorrectForm : TECorrectForm;
begin
  if self.DataListView.Selected <> nil then begin
    Name := self.DataListView.Selected.Caption + ' ' + self.DataListView.Selected.SubItems[0] + ' ' + self.DataListView.Selected.SubItems[1];
    CorrectForm := TECorrectForm.Create(self);
    CorrectForm.SetArgs(self.Employees.GetByExtendedName(Name, Index), self.Employees);
    CorrectForm.Position := TPosition.poScreenCenter;
    CorrectForm.ShowModal;
    if CorrectForm.IsEmployeeCorrected() then begin
      self.Employees.SetData(Index, CorrectForm.GetCorrectedEmployee());
      self.Refresh;
    end;
    CorrectForm.Release;
  end
  else
    ShowErrorMessageBox(self.Handle, 'Select item first!');
end;

procedure TMainForm.DataListViewKeyPress(Sender: TObject; var Key: Char);
begin
  case Key of

    '+': begin
       if self.DataListView.Font.Size <= 18 then
          self.DataListView.Font.Size := self.DataListView.Font.Size + 1;
    end;


    '-': begin
       if self.DataListView.Font.Size >= 9 then
          self.DataListView.Font.Size := self.DataListView.Font.Size - 1;
    end;
  end;

end;

procedure TMainForm.DeleteButtonClick(Sender: TObject);
var Name : string;
begin
  if self.DataListView.Selected <> nil then begin
    Name := self.DataListView.Selected.Caption + ' ' + self.DataListView.Selected.SubItems[0] + ' ' + self.DataListView.Selected.SubItems[1];
    self.Employees.DeleteByExtendedName(Name);
    self.DataListView.DeleteSelected;
  end
  else
    ShowErrorMessageBox(self.Handle, 'Select item first!');
end;

procedure TMainForm.ClearButtonClick(Sender: TObject);
begin
  self.DataListView.Clear;
  self.Employees.Clear;
  self.Refresh;
end;

end.
