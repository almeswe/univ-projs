unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, System.Classes, Vcl.Mask,
  Vcl.StdCtrls,
  Defines,
  DB,
  Testing,
  List,
  ECreator,
  ECorrector;

type TMainForm = class(TForm)

    DataListView: TListView;

    LoadButton:   TButton;
    SaveButton:   TButton;
    SortButton:   TButton;
    DeleteButton: TButton;
    AddNewButton: TButton;

    OpenDialog: TOpenDialog;
    CorrectButton: TButton;
    ClearButton: TButton;

    procedure FormResize(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure LoadButtonClick(Sender: TObject);
    procedure SaveButtonClick(Sender: TObject);
    procedure DeleteButtonClick(Sender: TObject);
    procedure AddNewButtonClick(Sender: TObject);
    procedure CorrectButtonClick(Sender: TObject);
    procedure ClearButtonClick(Sender: TObject);

    private
      //type TSortKind = (None, );
      Data : TCustomList;

      procedure Refresh();
      procedure AddEmployee(employee : TEmployee);
  end;

var MainForm : TMainForm;

implementation

{$R *.dfm}

procedure TMainForm.Refresh();
var i : integer;
begin
  self.DataListView.Items.Clear;
  for i := 0 to self.Data.Size()-1 do
    self.AddEmployee(self.Data.GetData(i));
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

procedure TMainForm.FormCreate(Sender: TObject);
begin
  self.Data.Init();
  self.OpenDialog.Create(self);
end;
procedure TMainForm.FormResize(Sender: TObject);
const columns = 5;
var i : integer;
var c_width : integer;
begin
  c_width := round(self.Width/columns);
  for i := 0 to columns-1 do
    self.DataListView.Columns[i].Width := c_width;
end;
procedure TMainForm.LoadButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then begin
    self.Data := DB.LoadFromTypeFile(self.OpenDialog.FileName);
    self.Refresh;
  end;
end;
procedure TMainForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    DB.SaveToTypeFile(self.OpenDialog.FileName, self.Data);
end;
procedure TMainForm.AddNewButtonClick(Sender: TObject);
var ConstructorForm : TEConstructorForm;
begin
  ConstructorForm := TEConstructorForm.Create(self);
  ConstructorForm.SetArgs(self.Data);
  ConstructorForm.ShowModal;
  if ConstructorForm.IsEmployeeCreated() then begin
    self.AddEmployee(ConstructorForm.GetCreatedEmployee());
    self.Data.Append(ConstructorForm.GetCreatedEmployee());
  end;
  ConstructorForm.Release;
end;
procedure TMainForm.CorrectButtonClick(Sender: TObject);
var Index : integer;
var CorrectForm : TECorrectForm;
begin
  if self.DataListView.Selected <> nil then begin
    CorrectForm := TECorrectForm.Create(self);
    CorrectForm.SetArgs(self.Data.GetByName(self.DataListView.Selected.Caption, Index), self.Data);
    CorrectForm.ShowModal;
    if CorrectForm.IsEmployeeCorrected() then begin
      self.Data.SetData(Index, CorrectForm.GetCorrectedEmployee());
      self.Refresh;
    end;
    CorrectForm.Release;
  end
  else
    ShowErrorMessageBox(self.Handle, 'You need one selected item for correcting!');
end;
procedure TMainForm.DeleteButtonClick(Sender: TObject);
begin
  if self.DataListView.Selected <> nil then begin
    self.Data.DeleteByName(self.DataListView.Selected.Caption);
    self.DataListView.DeleteSelected;
  end
  else
    ShowErrorMessageBox(self.Handle, 'You need one selected item for deleting!');
end;
procedure TMainForm.ClearButtonClick(Sender: TObject);
begin
  self.DataListView.Clear;
  self.Data.Clear;
  self.Refresh;
end;

end.
