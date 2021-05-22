unit Visualizer;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ExtCtrls, Defines2, Stack2,
  Vcl.StdCtrls, Vcl.Buttons;

type
  TVisualForm = class(TForm)
    SurfaceImage: TImage;
    NextStepButton: TSpeedButton;
    PreviousStepButton: TSpeedButton;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure NextStepButtonClick(Sender: TObject);
    procedure PreviousStepButtonClick(Sender: TObject);
    procedure FormKeyPress(Sender: TObject; var Key: Char);

  private

    var Stack : TStack;
    var Source : string;
    var Notation : string;
    var Actions : TActions;

    var CurrentActionIndex : integer;
  public

    procedure Next;
    procedure Back;

    procedure DrawLog;
    procedure DrawCaret;
    procedure DrawStack;
    procedure DrawSource;
    procedure DrawNotation;
    procedure RefreshScreen;
    procedure DrawVisualization;
    
    procedure SetArgs(source : string; actions : TActions);
    function DeleteAllSpaces(source : string) : string;
    function DeleteLastAction(source : string; action : TAction) : string;
  end;

var VisualForm: TVisualForm;

implementation

{$R *.dfm}

procedure TVisualForm.SetArgs(source : string; actions : TActions);
begin
  self.Actions := actions;
  self.Source := self.DeleteAllSpaces(source);
end;

function TVisualForm.DeleteAllSpaces(source : string) : string;
var i : integer;
var newstr : string;
begin
  for i := 1 to length(source) do
    if source[i] <> ' ' then
      newstr := newstr + source[i];
  exit(newstr);
end;

function TVisualForm.DeleteLastAction(source : string; action : TAction) : string;
var i : integer;
var str : string;
begin
  str := '';
  for i := 1 to length(source)-length(action.Data) do begin
    str := str + source[i];
  end;
  exit(str);
end;

procedure TVisualForm.Next;
begin
  inc(self.CurrentActionIndex);
  case self.Actions[self.CurrentActionIndex].Kind of
    TActionKind.PUSH   : self.Stack.Push(self.Actions[self.CurrentActionIndex].Data);
    TActionKind.POP    : begin
      if not ((self.Actions[self.CurrentActionIndex].Data = ')') or (self.Actions[self.CurrentActionIndex].Data = '('))  then
        self.Notation := self.Notation + self.Actions[self.CurrentActionIndex].Data;
      self.Stack.Pop;
    end;
    TActionKind.APPEND : self.Notation := self.Notation + self.Actions[self.CurrentActionIndex].Data;
  end;
end;

procedure TVisualForm.Back;
begin
  case self.Actions[self.CurrentActionIndex].Kind of
    TActionKind.PUSH   : self.Stack.Pop;
    TActionKind.POP    : begin
      self.Stack.Push(self.Actions[self.CurrentActionIndex].Data);
      if not ((self.Actions[self.CurrentActionIndex].Data = ')') or (self.Actions[self.CurrentActionIndex].Data = '('))  then
        self.Notation := self.DeleteLastAction(self.Notation, self.Actions[self.CurrentActionIndex]);
    end;
    TActionKind.APPEND : self.Notation := self.DeleteLastAction(self.Notation, self.Actions[self.CurrentActionIndex]);
  end;
  dec(self.CurrentActionIndex);
end;

procedure TVisualForm.DrawStack;
const PosX = 25;
const PosY = 150;
const StackBorderWidth = 300;
const StackUpperBorderWidth = 150;
const StackCellHeight = 30;
const StackCellMargin = 20;
const StackSeparator = StackUpperBorderWidth - StackCellMargin;
const StackMaxCountForRender = 10;
const StackImagePath = 'images\stack.bmp';

var i : integer;
var bmp : TBitmap;
begin
  bmp := TBitmap.Create;
  bmp.Transparent := True;
  bmp.LoadFromFile(StackImagePath);

  with self.SurfaceImage.Canvas do begin
    Draw(38, 15, bmp);

    Font.Size := 15;
    Font.Color := clWebTeal;

    Pen.Width := 5;
    Pen.Color := clWebCadetBlue;

    MoveTo(PosX + StackUpperBorderWidth, PosY + StackBorderWidth);
    LineTo(PosX + StackUpperBorderWidth, PosY);
    LineTo(PosX, PosY);
    LineTo(PosX, PosY + StackBorderWidth);

    Pen.Width := 2;
    Pen.Color := clWebTeal;
    MoveTo(PosX + StackUpperBorderWidth + 2, PosY + StackBorderWidth);
    LineTo(PosX + StackUpperBorderWidth, PosY + 2);
    LineTo(PosX, PosY + 2);
    LineTo(PosX, PosY + StackBorderWidth + 2);
  end;

  with self.SurfaceImage.Canvas do begin
    Pen.Width := 3;
    Font.Size := 10;
    if self.Stack.Empty then
      TextOut(166, 123, '*EMPTY*')
    else
      TextOut(166, 123, IntToStr(self.Stack.Size));
    Font.Size := 15;
    for i := 1 to self.Stack.Size do begin
      if i >= StackMaxCountForRender then
        TextOut(PosX + StackCellMargin, PosY + StackMaxCountForRender*StackCellHeight, '  . . .')
      else begin
        TextOut(PosX + StackCellMargin*3 + 10, PosY + i*StackCellHeight - self.Canvas.TextHeight(self.Stack.Get(i-1))*2, self.Stack.Get(i-1));
        MoveTo(PosX + StackCellMargin, PosY + i*StackCellHeight);
        LineTo(PosX + StackSeparator, PosY + i*StackCellHeight);
      end;
    end;
  end;
end;

procedure TVisualForm.Button1Click(Sender: TObject);
begin
  self.Back;
  self.DrawVisualization;
end;

procedure TVisualForm.NextStepButtonClick(Sender: TObject);
begin
  if self.CurrentActionIndex >= length(self.Actions)-1 then
    exit;
  self.Next;
  self.DrawVisualization;
end;

procedure TVisualForm.PreviousStepButtonClick(Sender: TObject);
begin
  if self.CurrentActionIndex < 0 then
    exit;
  self.Back;
  self.DrawVisualization;
end;

procedure TVisualForm.DrawNotation;
const PosX = 450;
const PosY = 100;
const MaxNotationWidth = 300;
begin
  with self.SurfaceImage.Canvas do begin
    Font.Size := 15;
    Font.Color := clGreen;
    TextOut(PosX, PosY, self.Notation);
  end;

  with self.SurfaceImage.Canvas do begin
    Font.Size := 10;
    TextOut(PosX, PosY - 20, 'NOTATION: ');
  end;
end;

procedure TVisualForm.DrawSource;
const PosX = 450;
const PosY = 300;
const MaxSourceWidth = 300;

var text : string;
begin
  text := self.Source;
  with self.SurfaceImage.Canvas do begin
    Font.Size := 15;
    Font.Color := clBlack;
    TextOut(PosX, PosY, self.Source);
  end;

  with self.SurfaceImage.Canvas do begin
    Font.Size := 10;
    TextOut(PosX, PosY - 20, 'INPUT: ');
  end;
end;

procedure TVisualForm.DrawLog;
const PosX = 300;
const PosY = 400;
const BorderLineWidth = 300;
const BorderLineHeight = 75;

begin
  with self.SurfaceImage.Canvas do begin
    Font.Size := 10;
    Font.Color := clPurple;
    
    TextOut(PosX, PosY, 'LOG: ');
  end;

  with self.SurfaceImage.Canvas do begin
    Font.Size := 15;
    Font.Color := clPurple;

    TextOut(PosX + 10, PosY + 20, self.Actions[self.CurrentActionIndex].ToLogString());
  end;
end;

procedure TVisualForm.DrawCaret;
const PosX = 450;
const PosY = 330;

var i : integer;
var caret : string;
var offset : integer;
begin
  caret := '';
  offset := 0;    
  with self.SurfaceImage.Canvas do begin
    Font.Size := 15;
    for i := 0 to self.CurrentActionIndex-1 do
      if (self.Actions[i].Kind <> TActionKind.POP) then
        offset := offset + TextWidth(self.Actions[i].Data)
      else
       if (self.Actions[i].Data = '(') then
        offset := offset + TextWidth(self.Actions[i].Data);
        
    if (self.CurrentActionIndex <> -1) and (self.CurrentActionIndex <> length(self.Actions)-1) then
      for i := 1 to length(self.Actions[self.CurrentActionIndex].Data) do
        caret := caret + '^';

    Font.Color := clRed;
    TextOut(PosX + offset, PosY, caret);
  end;
end;

procedure TVisualForm.DrawVisualization;
begin
  self.RefreshScreen;
  self.DrawLog;
  self.DrawCaret;
  self.DrawStack;
  self.DrawSource;
  self.DrawNotation;
end;

procedure TVisualForm.RefreshScreen;
begin
  with self.SurfaceImage.Canvas do begin
    Pen.Color := clWhite;
    Brush.Color := clWhite;
    Font.Name := 'Consolas';
    Rectangle(0, 0, self.Width, self.Height);
  end;
end;

procedure TVisualForm.FormCreate(Sender: TObject);
begin
  self.Stack.Init;
  self.Notation := '';
  self.CurrentActionIndex := -1;
end;

procedure TVisualForm.FormKeyPress(Sender: TObject; var Key: Char);
begin
  case Key of
    'd' : self.NextStepButtonClick(nil);
    'a' : self.PreviousStepButtonClick(nil);
  end;
end;

end.
