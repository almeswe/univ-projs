program Lists;

uses
  Vcl.Forms,
  Main in 'Main.pas' {MainForm},
  List in 'List.pas',
  Defines in 'Defines.pas',
  ECreator in 'ECreator.pas' {EConstructorForm},
  DB in 'DB.pas',
  ECorrector in 'ECorrector.pas' {ECorrectForm},
  Testing in 'Testing.pas',
  ETyper in 'ETyper.pas' {ETypeForm},
  Time in 'Time.pas',
  ESearcher in 'ESearcher.pas' {ESearchForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  Application.CreateForm(TEConstructorForm, EConstructorForm);
  Application.CreateForm(TECorrectForm, ECorrectForm);
  Application.CreateForm(TETypeForm, ETypeForm);
  Application.CreateForm(TESearchForm, ESearchForm);
  Application.Run;
end.
