unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, System.Classes, Vcl.Mask,
  Vcl.StdCtrls,
  Defines,
  DB,
  List,
  ECreator,
  EDeleter;

type
  TMainForm = class(TForm)

    AddNewButton: TButton;
    DataListView: TListView;
    LoadButton: TButton;
    SaveButton: TButton;
    SortButton: TButton;
    OpenDialog: TOpenDialog;
    DeleteButton: TButton;

    procedure FormResize(Sender: TObject);
    procedure AddNewButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure LoadButtonClick(Sender: TObject);
    procedure DeleteButtonClick(Sender: TObject);
    procedure SaveButtonClick(Sender: TObject);

    private
      data : TCustomList;

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
  for i := 0 to self.data.size()-1 do
    self.AddEmployee(self.data.get(i));
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
  self.data.init();
  self.OpenDialog.Create(self);
end;
procedure TMainForm.FormResize(Sender: TObject);
const COUNT_OF_COLUMNS = 5;
var i : integer;
var c_width : integer;
begin
  c_width := round(self.Width/COUNT_OF_COLUMNS);
  for i := 0 to COUNT_OF_COLUMNS-1 do
    self.DataListView.Columns[i].Width := c_width;
end;
procedure TMainForm.LoadButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then begin
    //self.data.clear;
    self.data := DB.LoadFromTypeFile(self.OpenDialog.FileName);
    self.Refresh;
  end;
end;
procedure TMainForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    DB.SaveToTypeFile(self.OpenDialog.FileName, self.data);
end;
procedure TMainForm.AddNewButtonClick(Sender: TObject);
var ConstructorForm : TEConstructorForm;
begin
  ConstructorForm := TEConstructorForm.Create(self);
  ConstructorForm.ShowModal;
  if ConstructorForm.IsEmployeeCreated() then begin
    self.AddEmployee(ConstructorForm.GetCreatedEmployee());
    self.data.append(ConstructorForm.GetCreatedEmployee());
  end;
  ConstructorForm.Release;
end;
procedure TMainForm.DeleteButtonClick(Sender: TObject);
var DeleterForm : TEDeleterForm;
begin
  DeleterForm := TEDeleterForm.Create(self);
  DeleterForm.SetDataSource(self.data);
  DeleterForm.ShowModal;
  DeleterForm.Synchronize(self.data);
  self.Refresh;
  DeleterForm.Release;
end;

end.
