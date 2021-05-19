program UI;

uses
  Vcl.Forms,
  Calc in 'Calc.pas' {CalcForm},
  Main in 'Main.pas' {MainForm},
  Diagram in 'Diagram.pas' {DiagramForm},
  DiagramOptions in 'DiagramOptions.pas' {DiagramOptionsForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  //Application.CreateForm(TDiagramForm, DiagramForm);
  //Application.CreateForm(TCalcForm, CalcForm);
  //Application.CreateForm(TDiagramForm, DiagramForm);
  Application.CreateForm(TDiagramOptionsForm, DiagramOptionsForm);
  Application.Run;
end.
