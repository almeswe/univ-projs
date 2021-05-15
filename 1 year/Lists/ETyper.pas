unit ETyper;

interface

uses
  System.SysUtils, System.Classes,
  Vcl.Forms, Vcl.StdCtrls, Vcl.Mask, Vcl.Controls,
  List, Defines, Testing;

type
  TETypeForm = class(TForm)
    TypeNameEdit:     TEdit;
    TypeSubmitButton: TButton;

    procedure TypeSubmitButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);

  private
    var Employees : TCustomList;
    var EmployeeName : string;

    var EmployeeSpecified : boolean;

  public
    procedure SetArgs(employees : TCustomList);

    function GetSpecifiedEmployeeName() : string;
    function IsEmployeeSpecified() : boolean;
  end;

var
  ETypeForm: TETypeForm;

implementation

{$R *.dfm}

procedure TETypeForm.SetArgs(employees : TCustomList);
begin
  self.Employees := employees;
end;

function TETypeForm.GetSpecifiedEmployeeName() : string;
begin
  exit(self.EmployeeName);
end;

function TETypeForm.IsEmployeeSpecified() : boolean;
begin
  exit(self.EmployeeSpecified);
end;

procedure TETypeForm.FormCreate(Sender: TObject);
begin
  self.EmployeeSpecified := false;
end;

procedure TETypeForm.TypeSubmitButtonClick(Sender: TObject);
begin
  if self.Employees.HasWithName(self.TypeNameEdit.Text) then begin
    self.EmployeeName := self.TypeNameEdit.Text;
    self.EmployeeSpecified := true;
    self.Close;
  end
  else
    ShowErrorMessageBox(self.Handle, 'No such employees with specified name!');
end;

end.
