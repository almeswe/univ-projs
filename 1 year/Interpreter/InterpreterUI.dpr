program InterpreterUI;

uses
  Vcl.Forms,
  Main in 'Main.pas' {Form1},
  Typer in 'Typer.pas' {TypeForm},
  Visualizer in 'Visualizer.pas' {VisualForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  //Application.CreateForm(TMenu, Menu);
  Application.CreateForm(TTypeForm, TypeForm);
  //Application.CreateForm(TVisualForm, VisualForm);
  Application.Run;
end.
