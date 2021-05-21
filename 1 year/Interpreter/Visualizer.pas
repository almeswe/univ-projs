unit Visualizer;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ExtCtrls, Defines2, Stack2,
  Vcl.StdCtrls;

type
  TVisualForm = class(TForm)
    SurfaceImage: TImage;
    Button1: TButton;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);

  private

    var Stack : TStack;
    var Source : string;
    var Notation : string;
    var Actions : TActions;

    var Caret : integer;
    var CurrentActionIndex : integer;
  public

    const SOURCE_WIDTH = 400;
    const NOTATION_WIDTH = 400;

    procedure Next;
    procedure Back;

    procedure DrawStack;
    procedure DrawNotation;
    procedure DrawSource;
    procedure DrawVisualization;
    procedure RefreshScreen;
    //procedure DrawBoundedText(x,y : integer; text : string);
    procedure SetArgs(source : string; actions : TActions);

    //procedure CaretMoveBack;
    //procedure CaretMoveForward;
  end;

var VisualForm: TVisualForm;

implementation

{$R *.dfm}

procedure TVisualForm.SetArgs(source : string; actions : TActions);
begin
  self.Source := source;
  self.Actions := actions;
end;

procedure TVisualForm.Next;
begin
  case self.Actions[self.CurrentActionIndex].Kind of
    TActionKind.PUSH   : self.Stack.Push(self.Actions[self.CurrentActionIndex].Data);
    TActionKind.POP    : begin
      if not ((self.Actions[self.CurrentActionIndex].Data = ')') or (self.Actions[self.CurrentActionIndex].Data = '('))  then
        self.Notation := self.Notation + self.Actions[self.CurrentActionIndex].Data;
      self.Stack.Pop;
    end;
    TActionKind.APPEND : self.Notation := self.Notation + self.Actions[self.CurrentActionIndex].Data;
  end;
  inc(self.CurrentActionIndex);
end;

procedure TVisualForm.Back;
begin
  case self.Actions[self.CurrentActionIndex].Kind of
    TActionKind.PUSH   : self.Stack.Pop;
    TActionKind.POP    : begin
      self.Stack.Push(self.Actions[self.CurrentActionIndex].Data);
      if not ((self.Actions[self.CurrentActionIndex].Data = ')') or (self.Actions[self.CurrentActionIndex].Data = '('))  then
        Delete(self.Notation, length(self.Notation) - length(self.Actions[self.CurrentActionIndex].Data), length(self.Actions[self.CurrentActionIndex].Data));
    end;
    TActionKind.APPEND : Delete(self.Notation, length(self.Notation) - length(self.Actions[self.CurrentActionIndex].Data), length(self.Actions[self.CurrentActionIndex].Data));
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
    for i := 1 to self.Stack.Size do begin
      if i >= StackMaxCountForRender then
        TextOut(PosX + StackCellMargin, PosY + StackMaxCountForRender*StackCellHeight, '      . . .')
      else begin
        TextOut(PosX + StackCellMargin, PosY + i*StackCellHeight - self.Canvas.TextHeight(self.Stack.Get(i-1))*2, self.Stack.Get(i-1));
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

procedure TVisualForm.Button2Click(Sender: TObject);
begin
  self.Next;
  self.DrawVisualization;
end;

procedure TVisualForm.DrawNotation;
const PosX = 450;
const PosY = 100;
const MaxNotationWidth = 300;
begin
  with self.SurfaceImage.Canvas do begin
    Font.Size := 20;
    Font.Color := clWebMidnightBlue;
    TextOut(PosX, PosY, self.Notation);
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
    Font.Size := 20;
    Font.Color := clWebMidnightBlue;
    //while TextWidth(text) < MaxSourceWidth do begin
    //  Delete(text, length(text)-1, 1);
    //end;
    TextOut(PosX, PosY, self.Source);
  end;
end;

procedure TVisualForm.DrawVisualization;
begin
  self.RefreshScreen;
  self.DrawSource;
  self.DrawNotation;
  self.DrawStack;
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
  self.DrawVisualization;
  self.CurrentActionIndex := 0;
end;

end.
