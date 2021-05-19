unit DiagramOptions;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls;

type
  TDiagramOptionsForm = class(TForm)
    OptionsSizeComboBox: TComboBox;
    OptionsGenComboBox: TComboBox;
    OptionsDataComboBox: TComboBox;
    OptionsSubmitButton: TButton;
    procedure OptionsSubmitButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
     var OptionsSelected : boolean;
  public
    function IsOptionsSelected() : boolean;
  end;

var DiagramOptionsForm: TDiagramOptionsForm;

implementation

{$R *.dfm}

function TDiagramOptionsForm.IsOptionsSelected() : boolean;
begin
  exit(self.OptionsSelected);
end;

procedure TDiagramOptionsForm.FormCreate(Sender: TObject);
begin
  self.Position := TPosition.poScreenCenter;
  self.OptionsSelected := false;
end;

procedure TDiagramOptionsForm.OptionsSubmitButtonClick(Sender: TObject);
begin
  self.OptionsSelected := true;
  self.Close;
end;

end.
