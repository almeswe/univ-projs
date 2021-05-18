unit Main;

interface

uses
  System.SysUtils, System.Classes, System.IOUtils,
  Vcl.Forms, Vcl.StdCtrls, Vcl.Mask, Vcl.Controls, Vcl.Dialogs, Vcl.ComCtrls,
  Defines, DB, Testing, List, Time, ETyper, ECreator, ECorrector, ESearcher;

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
    AEHRadioButton: TRadioButton;
    SearchRadioButton: TRadioButton;

    procedure FormResize(Sender: TObject);
    procedure FormCreate(Sender: TObject);

    procedure LoadButtonClick(Sender: TObject);
    procedure SaveButtonClick(Sender: TObject);
    procedure ClearButtonClick(Sender: TObject);
    procedure DeleteButtonClick(Sender: TObject);
    procedure AddNewButtonClick(Sender: TObject);
    procedure CorrectButtonClick(Sender: TObject);

    procedure AEHRadioButtonClick(Sender: TObject);
    procedure NoneRadioButtonClick(Sender: TObject);
    procedure SearchRadioButtonClick(Sender: TObject);
    procedure SETasksRadioButtonClick(Sender: TObject);
    procedure SENTasksRadioButtonClick(Sender: TObject);

    procedure DataListViewKeyPress(Sender: TObject; var Key: Char);
    procedure DataListViewColumnClick(Sender: TObject; Column: TListColumn);

    private
      type TSortKind = (None, SETasks, SENTasks, AEHours, ESearched);

      var Employees : TCustomList;
      var SearchedEmployees : TCustomList;
      var CurrentSort : TSortKind;

      var SpecifiedEmployee : TEmployee;

      procedure Refresh();
      procedure RefreshNone();
      procedure RefreshSETasks();
      procedure RefreshAEHours();
      procedure RefreshSENTasks();
      procedure RefreshSearched();
      procedure AddEmployee(employee : TEmployee);

      procedure GetSpecifiedEmployee();
  end;

var MainForm : TMainForm;

implementation

{$R *.dfm}

procedure TMainForm.Refresh();
begin
  self.DataListView.Items.Clear;

  if self.DataListView.Columns.Count = 6 then
    self.DataListView.Columns[5].Free;

  case self.CurrentSort of
    TSortKind.None:
      self.RefreshNone;

    TSortKind.SETasks:
      self.RefreshSETasks;

    TSortKind.SENTasks:
      self.RefreshSENTasks;

    TSortKind.AEHours:
      self.RefreshAEHours;

    TSortKind.ESearched:
      self.RefreshSearched;
  end;

  self.FormResize(nil);
end;

procedure TMainForm.RefreshNone();
var i : integer;
begin
  for i := 0 to self.Employees.Size()-1 do
    self.AddEmployee(self.Employees.GetData(i));
end;

procedure TMainForm.RefreshAEHours();
var i, j : integer;
var column : TListColumn;
var tempList : TCustomList;
begin
  column := self.DataListView.Columns.Add;
  column.Caption := 'WORK HOURS';

  tempList := self.Employees;
  for i := 0 to tempList.Size()-1 do begin
    for j := i+1 to tempList.Size()-1 do begin
      if tempList.GetData(j).GetMonthlyWorkHours() > tempList.GetData(i).GetMonthlyWorkHours() then
        tempList.Swap(i, j);
    end;
  end;

  for i := 0 to tempList.Size()-1 do
    self.AddEmployee(tempList.GetData(i));
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

procedure TMainForm.RefreshSearched();
var i : integer;
begin
  for i := 0 to self.SearchedEmployees.Size()-1 do
    self.AddEmployee(self.SearchedEmployees.GetData(i));
end;

procedure TMainForm.AddEmployee(employee : TEmployee);
var item : TListItem;
begin
  item := self.DataListView.Items.Add;
  item.Caption := employee.ToNameString();
  item.SubItems.Add(employee.Project.Name);
  item.SubItems.Add(employee.Project.Task);
  item.SubItems.Add(employee.Project.Deadline);
  item.SubItems.Add(employee.Shedule.ToString());
  if self.CurrentSort = TSortKind.AEHours then
    if Time.GetPreviousMonth() - Time.GetMonthFromDate(StrToDate(employee.Project.Deadline)) < 2 then
      item.SubItems.Add(IntToStr(employee.GetMonthlyWorkHours()))
    else
      item.SubItems.Add('0');
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
var columnWidth : integer;
begin
  columnWidth := round(self.DataListView.Width/self.DataListView.Columns.Count);
  for i := 0 to self.DataListView.Columns.Count-1 do
    self.DataListView.Columns[i].Width := columnWidth;
end;

procedure TMainForm.LoadButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then begin
    if TPath.GetExtension(self.OpenDialog.FileName) = '.bin' then
      self.Employees := DB.LoadFromTypeFile(self.OpenDialog.FileName)
    else
      self.Employees := DB.LoadFromTextFile(self.OpenDialog.FileName);
    self.Refresh;
  end;
end;

procedure TMainForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    if TPath.GetExtension(self.OpenDialog.FileName) = '.bin' then
      DB.SaveToTypeFile(self.OpenDialog.FileName, self.Employees)
    else
      DB.SaveToTextFile(self.OpenDialog.FileName, self.Employees);
end;

procedure TMainForm.SearchRadioButtonClick(Sender: TObject);
var SearchForm : TESearchForm;
begin
  SearchForm := TESearchForm.Create(self);
  SearchForm.SetArgs(self.Employees);
  SearchForm.Position := TPosition.poScreenCenter;
  SearchForm.ShowModal;
  if SearchForm.IsEmployeesSearched() then begin
    self.SearchedEmployees := SearchForm.GetSearchedEmployees();
    self.CurrentSort := TSortKind.ESearched;
    self.Refresh;
  end
  else
    self.NoneRadioButton.Checked := true;
  SearchForm.Release;
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

procedure TMainForm.AEHRadioButtonClick(Sender: TObject);
begin
  self.CurrentSort := TSortKind.AEHours;
  self.Refresh;
end;

procedure TMainForm.CorrectButtonClick(Sender: TObject);
var Name : string;
var Index, SearchedIndex : integer;
var CorrectForm : TECorrectForm;
begin
  if self.DataListView.Selected <> nil then begin
    Name := self.DataListView.Selected.Caption + ' ' + self.DataListView.Selected.SubItems[0] + ' ' + self.DataListView.Selected.SubItems[1];
    CorrectForm := TECorrectForm.Create(self);
    CorrectForm.SetArgs(self.Employees.GetByExtendedName(Name, Index), self.Employees);
    CorrectForm.Position := TPosition.poScreenCenter;
    CorrectForm.ShowModal;
    if CorrectForm.IsEmployeeCorrected() then begin
      if self.CurrentSort = TSortKind.ESearched then begin
        self.SearchedEmployees.GetByExtendedName(Name, SearchedIndex);
        self.SearchedEmployees.SetData(SearchedIndex, CorrectForm.GetCorrectedEmployee());
      end;
      self.Employees.SetData(Index, CorrectForm.GetCorrectedEmployee());
      self.Refresh;
    end;
    CorrectForm.Release;
  end
  else
    ShowErrorMessageBox(self.Handle, 'Select item first!');
end;

procedure TMainForm.DataListViewColumnClick(Sender: TObject; Column: TListColumn);
begin
  if Column.Index = 0 then begin
    self.DataListView.SortType := stText;
    self.DataListView.SortType := stNone;
  end;
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
  Key := #10;
end;

procedure TMainForm.DeleteButtonClick(Sender: TObject);
var Name : string;
begin
  if self.DataListView.Selected <> nil then begin
    Name := self.DataListView.Selected.Caption + ' ' + self.DataListView.Selected.SubItems[0] + ' ' + self.DataListView.Selected.SubItems[1];
    if self.CurrentSort = TSortKind.ESearched then
      self.SearchedEmployees.DeleteByExtendedName(Name);
    self.Employees.DeleteByExtendedName(Name);
    self.DataListView.DeleteSelected;
  end
  else
    ShowErrorMessageBox(self.Handle, 'Select item first!');
end;

procedure TMainForm.ClearButtonClick(Sender: TObject);
begin
  self.DataListView.Clear;
  if self.CurrentSort = TSortKind.ESearched then
    self.SearchedEmployees.Clear;
  self.Employees.Clear;
  self.Refresh;
end;

end.
