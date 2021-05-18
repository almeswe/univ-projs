unit ESearcher;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Defines, List;

type
  TESearchForm = class(TForm)
    SearchSubmitButton: TButton;
    SearchNameEdit: TEdit;
    SearchOptionsComboBox: TComboBox;

    procedure FormCreate(Sender: TObject);
    procedure SearchSubmitButtonClick(Sender: TObject);

  private
    var Employees : TCustomList;
    var EmployeesSearched : boolean;

  public
    procedure SetArgs(employees : TCustomList);

    function IsEmployeesSearched() : boolean;
    function GetSearchedEmployees() : TCustomList;
  end;

var ESearchForm: TESearchForm;

implementation

{$R *.dfm}

procedure TESearchForm.SetArgs(employees : TCustomList);
begin
  self.Employees := employees;
end;

function TESearchForm.GetSearchedEmployees() : TCustomList;
begin
  exit(self.Employees);
end;

function TESearchForm.IsEmployeesSearched() : boolean;
begin
  exit(self.EmployeesSearched);
end;

procedure TESearchForm.FormCreate(Sender: TObject);
begin
  self.EmployeesSearched := false;
  self.SearchOptionsComboBox.ItemIndex := 0;
end;

procedure TESearchForm.SearchSubmitButtonClick(Sender: TObject);
var a : TSearchOptions;
begin
  self.Employees := self.Employees.Search(self.SearchNameEdit.Text, TSearchOptions(self.SearchOptionsComboBox.ItemIndex));
  self.EmployeesSearched := true;
  self.Close();
end;

end.
