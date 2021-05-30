unit Typer;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls,
  Interpreter, Defines, Visualizer;

type TTypeForm = class(TForm)
    TypeEdit : TEdit;

    InfoListBox : TListBox;

    InfoLabel:      TLabel;
    EnterExprLabel: TLabel;

    SaveButton: TButton;
    ShowButton: TButton;

    OpenDialog: TOpenDialog;

    procedure SaveButtonClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure TypeEditChange(Sender: TObject);
    procedure InfoListBoxKeyPress(Sender: TObject; var Key: Char);
    procedure ShowButtonClick(Sender: TObject);
  private
     Interpreter : TInterpreter;

     procedure RunInterpreter();
     procedure CreateVisualForm();
     procedure ChangeFont(bySize : int);
     procedure SetStateForButtons(state : bool);
  public

  end;

var TypeForm: TTypeForm;

implementation

{$R *.dfm}

procedure TTypeForm.FormCreate(Sender: TObject);
begin
  self.OpenDialog.Create(self);
end;

procedure TTypeForm.InfoListBoxKeyPress(Sender: TObject; var Key: Char);
begin
  case Key of
    '+' : self.ChangeFont(2);
    '-' : self.ChangeFont(-2);
  end;
  Key := #10;
end;

procedure TTypeForm.RunInterpreter();
var i : int;
begin
  self.SetStateForButtons(true);
  self.InfoListBox.Items.Clear;
  self.Interpreter.Init(self.TypeEdit.Text);
  self.Interpreter.Interpret;
  if not self.Interpreter.IsErrored() then begin
    self.InfoListBox.Font.Color := clGreen;
    self.InfoListBox.Items.Add(self.Interpreter.Notation);
  end
  else begin
    self.SetStateForButtons(false);
    self.InfoListBox.Font.Color := clRed;
    for i := 0 to length(self.Interpreter.errors)-1 do
        self.InfoListBox.Items.Add(self.Interpreter.Errors[i].ToString());
  end;
  self.Interpreter.Discard;
end;

procedure TTypeForm.CreateVisualForm();
var visualForm : TVisualForm;
begin
  visualForm := TVisualForm.Create(self);
  visualForm.SetArgs(self.Interpreter.Source, self.Interpreter.Actions);
  visualForm.DrawVisualization;
  visualForm.ShowModal;
  visualForm.Release;
end;

procedure TTypeForm.ChangeFont(bySize : int);
begin
  if bySize > 0 then begin
    if self.InfoListBox.Font.Size + bySize <= 25 then
      self.InfoListBox.Font.Size := self.InfoListBox.Font.Size + bySize;
  end
  else
    if self.InfoListBox.Font.Size + bySize >= 9 then
      self.InfoListBox.Font.Size := self.InfoListBox.Font.Size + bySize;
end;

procedure TTypeForm.SetStateForButtons(state : bool);
begin
  self.ShowButton.Enabled := state;
  self.SaveButton.Enabled := state;
end;

procedure TTypeForm.ShowButtonClick(Sender: TObject);
begin
  self.CreateVisualForm();
end;

procedure TTypeForm.SaveButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    self.Interpreter.SaveToFile(self.OpenDialog.FileName);
end;

procedure TTypeForm.TypeEditChange(Sender: TObject);
begin
  self.RunInterpreter;
end;

end.
