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
  ESorter in 'ESorter.pas' {ESortForm},
  ETyper in 'ETyper.pas' {ETypeForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  Application.CreateForm(TEConstructorForm, EConstructorForm);
  Application.CreateForm(TECorrectForm, ECorrectForm);
  Application.CreateForm(TESortForm, ESortForm);
  Application.CreateForm(TETypeForm, ETypeForm);
  Application.Run;
end.
