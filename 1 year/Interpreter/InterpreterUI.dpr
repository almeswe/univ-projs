program InterpreterUI;

uses
  Vcl.Forms,
  Main in 'Main.pas' {Form1},
  Typer in 'Typer.pas' {TypeForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMenu, Menu);
  Application.CreateForm(TTypeForm, TypeForm);
  Application.Run;
end.
