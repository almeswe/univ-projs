unit Typer;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls,
  Interpreter, Defines2;

type TTypeForm = class(TForm)
    TypeEdit : TEdit;

    InfoListBox : TListBox;

    InfoLabel:      TLabel;
    EnterExprLabel: TLabel;
    SaveButton: TButton;
    ShowButton: TButton;
    OpenDialog: TOpenDialog;

    procedure TypeEditKeyPress(Sender: TObject; var Key: Char);
    procedure SaveButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);

  private
     interpreter : TInterpreter;

     procedure run();
  public

  end;

var TypeForm: TTypeForm;

implementation

{$R *.dfm}

procedure TTypeForm.FormCreate(Sender: TObject);
begin
  self.OpenDialog.Create(self);
end;

procedure TTypeForm.run();
var i : int;
begin
  self.interpreter.init(self.TypeEdit.Text);
  self.interpreter.interpret;
  if not self.interpreter.is_errored then begin
    self.InfoListBox.Font.Color := clGreen;
    self.InfoListBox.Items.Clear;
    self.InfoListBox.Items.Add(self.interpreter.notation);
  end
  else begin
    self.InfoListBox.Font.Color := clRed;
    self.InfoListBox.Items.Clear;
    for i := 0 to length(self.interpreter.errors)-1 do
        self.InfoListBox.Items.Add(self.interpreter.errors[i].to_string());
  end;
  self.interpreter.discard;
end;

procedure TTypeForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then begin
    self.interpreter.notation := self.InfoListBox.Items[0];
    self.interpreter.save_to_file(self.OpenDialog.FileName);
    self.interpreter.discard;
  end;
end;

procedure TTypeForm.TypeEditKeyPress(Sender: TObject; var Key: Char);
begin
  if ord(Key) = VK_RETURN then begin
     Key := #0;
     self.run;
  end;
end;

end.
