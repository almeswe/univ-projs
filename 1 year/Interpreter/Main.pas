unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ExtCtrls, Vcl.StdCtrls, Vcl.Buttons, ShellApi,
  Typer;

type TMenu = class(TForm)
    DragDropPanel: TPanel;

    ExploreButton:      TSpeedButton;
    TypeManuallyButton: TSpeedButton;
    OpenDialog: TOpenDialog;

    procedure FormCreate(Sender: TObject);
    procedure TypeManuallyButtonClick(Sender: TObject);
    procedure ExploreButtonClick(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure FormDestroy(Sender: TObject);

  private
    const DEFAULT_DRAG_DROP_IMAGE = 'images\drag.bmp';
    const ACTIVE_DRAG_DROP_IMAGE  = 'images\dragactive.bmp';


    procedure RenderImageOnPanel(path : string = DEFAULT_DRAG_DROP_IMAGE);
    procedure CreateTyperForm(input : string);
  protected
    procedure WNDropFiles(var Msg : TMessage); message WM_DROPFILES;

  public
    { Public declarations }
  end;

var Menu: TMenu;

implementation

{$R *.dfm}

procedure TMenu.RenderImageOnPanel(path : string = DEFAULT_DRAG_DROP_IMAGE);
var image : TImage;
begin
  image := TImage.Create(self.DragDropPanel);
  image.Width := 128;
  image.Height := 128;
  image.Top := 30;
  image.Left := 30;
  image.Parent := self.DragDropPanel;
  image.Picture.LoadFromFile(path);
  image.Visible := true;
end;

procedure TMenu.CreateTyperForm(input : string);
var type_form : TTypeForm;
var key : char;
begin
  key := Char(13);
  type_form := TTypeForm.Create(self);
  type_form.TypeEdit.Text := input;
  type_form.ShowModal;
  type_form.Release;
end;

procedure TMenu.WNDropFiles(var Msg : TMessage);
var s : string;
var namelen : integer;
begin
  namelen := DragQueryFile(Msg.WParam, 0, nil, 0) + 1;
  SetLength(s, namelen);
  self.RenderImageOnPanel(ACTIVE_DRAG_DROP_IMAGE);
  DragQueryFile(Msg.WParam, 0, Pointer(s), namelen);
  self.CreateTyperForm(s);
  self.DragDropPanel.Invalidate;
  DragFinish(Msg.WParam);
end;

procedure TMenu.TypeManuallyButtonClick(Sender: TObject);
begin
  CreateTyperForm('');
end;

procedure TMenu.ExploreButtonClick(Sender: TObject);
begin
  if self.OpenDialog.Execute then
    CreateTyperForm(self.OpenDialog.FileName);
end;

procedure TMenu.FormCreate(Sender: TObject);
begin
  self.OpenDialog.Create(self);
  DragAcceptFiles(self.Handle, true);
end;

procedure TMenu.FormDestroy(Sender: TObject);
begin
  DragAcceptFiles(self.Handle, false);
end;

procedure TMenu.FormPaint(Sender: TObject);
begin
  self.RenderImageOnPanel;
end;

end.
