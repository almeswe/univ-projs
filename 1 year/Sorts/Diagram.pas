unit Diagram;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, VclTee.TeeGDIPlus, VCLTee.TeEngine,
  Vcl.ExtCtrls, VCLTee.TeeProcs, VCLTee.Chart;

type TPoint = record
  PosX : integer;
  PosY : integer;
end;

type TLocation = record
  Left  : TPoint;
  Right : TPoint;
end;

type TPie = record
  Data  : integer;
  Color : TColor;
  Name  : string;
end;

type TPies = array of TPie;

type TPieGroup = record
  Pies : TPies;
  Header : string;
  Canvas : TCanvas;
  Location : TLocation;

  procedure RenderPies();
  procedure RenderHeader();
  procedure RenderCoordinates();
  procedure RenderOn(canvas : TCanvas);

  procedure SortPies();
  function GetWidth() : integer;
  function GetHeight() : integer;
end;

function CreatePoint(x,y : integer) : TPoint;
function CreateLocation(x1, y1, x2, y2 : integer) : TLocation;
function CreatePie(data : integer; name : string; color : TColor) : TPie;
function CreatePieGroup(pies : TPies; location : TLocation; header : string) : TPieGroup;

type
  TDiagramForm = class(TForm)
    Surface: TImage;
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
  private
    var Group : TPieGroup;

  public
    procedure SetArgs(group : TPieGroup);
  end;

var DiagramForm: TDiagramForm;

implementation

{$R *.dfm}


function CreatePoint(x,y : integer) : TPoint;
var point : TPoint;
begin
  point.PosX := x;
  point.PosY := y;
  exit(point);
end;

function CreateLocation(x1, y1, x2, y2 : integer) : TLocation;
var location : TLocation;
begin
  location.Left.PosX := x1;
  location.Left.PosY := y1;
  location.Right.PosX := x2;
  location.Right.PosY := y2;
  exit(location);
end;

function CreatePie(data : integer; name : string; color : TColor) : TPie;
var pie : TPie;
begin
  pie.Data  := data;
  pie.Color := color;
  pie.Name  := name;
  exit(pie);
end;

function CreatePieGroup(pies : TPies; location : TLocation; header : string) : TPieGroup;
var piegroup : TPieGroup;
begin
  piegroup.Pies := pies;
  piegroup.Header := header;
  piegroup.Location := location;
  exit(piegroup);
end;

procedure TPieGroup.RenderOn(canvas : TCanvas);
begin
  self.Canvas := canvas;
  self.SortPies;
  with canvas do begin
    Pen.Color := clBlack;
    with self.Location do
      Rectangle(Left.PosX, Left.PosY, Right.PosX, Right.PosY);
  end;
  self.RenderHeader;
  self.RenderCoordinates;
  self.RenderPies;
end;

procedure TPieGroup.RenderPies();
var i : integer;
var maxdata : integer;
var maxlength, pielength : integer;
var piewidth : integer;
begin
  if length(self.Pies) <= 0 then
    exit;
  piewidth := round(self.GetWidth() / length(self.Pies));
  maxdata := self.Pies[0].Data;
  maxlength := self.GetWidth() - round(self.GetWidth() * 0.25);
  for i := 0 to Length(self.Pies)-1 do begin
    with self.Canvas do begin
      Brush.Color := self.Pies[i].Color;
      pielength := round(self.Pies[i].Data * maxlength / maxdata);
      Rectangle(self.Location.Left.PosX + i*piewidth, self.Location.Right.PosY - pielength,
                self.Location.Left.PosX + (i+1)*piewidth, self.Location.Right.PosY);
      Brush.Color := clWhite;
      TextOut(self.Location.Left.PosX + i*piewidth + round(piewidth/2) - round(self.Canvas.TextWidth(IntToStr(self.Pies[i].Data))/2),
              self.Location.Right.PosY - round(pielength/2),
              IntToStr(self.Pies[i].Data));
      TextOut(self.Location.Left.PosX + i*piewidth + round(piewidth/2) - round(self.Canvas.TextWidth(self.Pies[i].Name)/2),
              self.Location.Right.PosY + self.Canvas.TextHeight(self.Pies[i].Name),
              self.Pies[i].Name);
    end;
  end;
end;

procedure TPieGroup.RenderHeader();
var center : integer;
begin
  with self.Canvas do begin
     Brush.Color := clWhite;
     center := self.Location.Left.PosX + round(self.GetWidth()/2);
     TextOut(center-round(self.Canvas.TextWidth(self.Header)/2), self.Location.Left.PosY - self.Canvas.TextHeight(self.Header), self.Header);
  end;
end;

procedure TPieGroup.RenderCoordinates();
var i,j : integer;
var maxdata : integer;
var maxlength, coordlength : integer;
begin
  if length(self.Pies) <= 0 then
    exit;
  maxdata := self.Pies[0].Data;
  maxlength := self.GetWidth() - round(self.GetWidth() * 0.25);
  for i := 0 to length(self.Pies)-1 do begin
    with self.Canvas do begin
      coordlength := round(self.Pies[i].Data * maxlength / maxdata);
      MoveTo(self.Location.Right.PosX+round(self.GetWidth() * 0.01), self.Location.Right.PosY - coordlength);
      LineTo(self.Location.Right.PosX-round(self.GetWidth() * 0.02), self.Location.Right.PosY - coordlength);
    end;
  end;
end;

procedure TPieGroup.SortPies();
var i,j : integer;
var buff : TPie;
begin
  for i := 0 to length(self.Pies)-2 do begin
    for j := i+1 to length(self.Pies)-1 do begin
      if self.Pies[i].Data < self.Pies[j].Data then begin
        buff := self.Pies[i];
        self.Pies[i] := self.Pies[j];
        self.Pies[j] := buff;
      end;
    end;
  end;
end;

function TPieGroup.GetWidth() : integer;
begin
  exit(self.Location.Right.PosX - self.Location.Left.PosX);
end;

function TPieGroup.GetHeight() : integer;
begin
  exit(self.Location.Right.PosY - self.Location.Left.PosY);
end;

procedure TDiagramForm.FormCreate(Sender: TObject);
begin
  self.Group.RenderOn(self.Surface.Canvas);
end;

procedure TDiagramForm.FormShow(Sender: TObject);
begin
 self.Group.RenderOn(self.Surface.Canvas);
end;

procedure TDiagramForm.SetArgs(group : TPieGroup);
begin
  self.Group := group;
end;

end.
