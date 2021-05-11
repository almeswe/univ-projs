unit EDeleter;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, List, Defines;

type
  TEDeleterForm = class(TForm)
    DeleteInfoGroupBox : TGroupBox;

    DeleterIndexEdit : TEdit;

    DeleterNameLabel    : TLabel;
    DeleterSurnameLabel : TLabel;
    DeleterMidnameLabel : TLabel;
    DeleterIndexLabel   : TLabel;
    DeleterDeleteButton: TButton;
    procedure DeleterIndexEditMouseLeave(Sender: TObject);
    procedure DeleterDeleteButtonClick(Sender: TObject);

  private
    data : TCustomList;
  public
    procedure SetDataSource(var data : TCustomList);
    procedure Synchronize(var data : TCustomList);
  end;

var EDeleterForm: TEDeleterForm;

implementation

{$R *.dfm}

procedure TEDeleterForm.Synchronize(var data : TCustomList);
begin
  //data.clear;
  data := self.data;
end;
procedure TEDeleterForm.SetDataSource(var data : TCustomList);
begin
  self.data := data;
end;
procedure TEDeleterForm.DeleterDeleteButtonClick(Sender: TObject);
begin
  if Trim(self.DeleterIndexEdit.Text) = '' then
    exit;
  self.data.delete(StrToInt(self.DeleterIndexEdit.Text)-1);
  self.Close;
end;
procedure TEDeleterForm.DeleterIndexEditMouseLeave(Sender: TObject);
var index    : integer;
var employee : TEmployee;
begin
  if Trim(self.DeleterIndexEdit.Text) = '' then
    exit;
  try
    index := StrToInt(self.DeleterIndexEdit.Text);
    employee := self.data.get(index-1);

    self.DeleterNameLabel.Caption    := employee.Name;
    self.DeleterSurnameLabel.Caption := employee.Surname;
    self.DeleterMidnameLabel.Caption := employee.Midname;
  except
    self.DeleterIndexEdit.Text := '';
    self.DeleterNameLabel.Caption    := 'None';
    self.DeleterSurnameLabel.Caption := 'None';
    self.DeleterMidnameLabel.Caption := 'None';
  end;
end;

end.
