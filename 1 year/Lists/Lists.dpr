program Lists;

uses
  Vcl.Forms,
  Main in 'Main.pas' {MainForm},
  List in 'List.pas',
  Defines in 'Defines.pas',
  ECreator in 'ECreator.pas' {EConstructorForm},
  DB in 'DB.pas',
  EDeleter in 'EDeleter.pas' {EDeleterForm};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  Application.CreateForm(TEConstructorForm, EConstructorForm);
  Application.CreateForm(TEDeleterForm, EDeleterForm);
  Application.Run;
end.
