unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ExtCtrls, Vcl.StdCtrls, Vcl.Buttons,
  Typer;

type TMenu = class(TForm)
    DragDropPanel: TPanel;

    ExploreButton:      TSpeedButton;
    TypeManuallyButton: TSpeedButton;
    OpenDialog: TOpenDialog;

    procedure FormCreate(Sender: TObject);
    procedure TypeManuallyButtonClick(Sender: TObject);
    procedure ExploreButtonClick(Sender: TObject);

  private

    procedure render_image_on_panel();
    procedure create_typer_form(input : string);

  public
    { Public declarations }
  end;

var Menu: TMenu;

implementation

{$R *.dfm}

procedure TMenu.render_image_on_panel();
var image : TImage;
begin
  image := TImage.Create(self.DragDropPanel);
  image.Parent := self.DragDropPanel;
  image.Picture.LoadFromFile('images\drag and drop.png');
  image.Visible := true;
end;

procedure TMenu.create_typer_form(input : string);
var type_form : TTypeForm;
var key : char;
begin
  key := Char(13);
  type_form := TTypeForm.Create(self);
  type_form.Position := poScreenCenter;
  type_form.TypeEdit.Text := input;
  type_form.TypeEditKeyPress(nil, key);
  type_form.ShowModal;
  type_form.Release;
end;

procedure TMenu.TypeManuallyButtonClick(Sender: TObject);
begin
  create_typer_form('');
end;

procedure TMenu.ExploreButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    create_typer_form(self.OpenDialog.FileName);
end;

procedure TMenu.FormCreate(Sender: TObject);
begin
  self.OpenDialog.Create(self);
end;

end.
