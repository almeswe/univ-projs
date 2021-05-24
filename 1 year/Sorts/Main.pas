unit Main;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, Calc, Defines, Sorts, Diagram, DiagramOptions,
  Vcl.StdCtrls;

type
  TMainForm = class(TForm)
    DataListView: TListView;
    OpenDiagramsButton: TButton;
    RegenerateButton: TButton;
    OpenCalcButton: TButton;
    OpenButtonsGroupBox: TGroupBox;
    RandomArraysGroupBox: TGroupBox;
    RandomArray10ListBox: TListBox;
    RandomArray100ListBox: TListBox;
    RandomArray2000ListBox: TListBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    procedure FormResize(Sender: TObject);
    procedure DataListViewKeyPress(Sender: TObject; var Key: Char);
    procedure FormCreate(Sender: TObject);
    procedure RegenerateButtonClick(Sender: TObject);
    procedure OpenCalcButtonClick(Sender: TObject);
    procedure OpenDiagramsButtonClick(Sender: TObject);

  private
    type TArraySize = (Ten = 10, Hundred = 100, TwoThousand = 2000);

    procedure FillListBox(arr : TIntArray; listbox : TListBox);
    procedure FillListView();
    procedure OpenDiagram();
    procedure OpenCalculator();
    procedure ChangeFont(bySize : integer);
    procedure AddItem(size : TArraySize; kind : TGenKind);

    function CreateDiagram(options : TDiagramOptionsForm) : TPieGroup;

  public
    { Public declarations }
  end;

var MainForm: TMainForm;

implementation

{$R *.dfm}

procedure TMainForm.AddItem(size : TArraySize; kind : TGenKind);
var data, copied : TIntArray;
var item : TListItem;
var perms, comps : uint;
begin
   data := Sorts.CreateArray(ord(size), kind);
   if kind = TGenKind.RandomArray then
    case size of
      TArraySize.Ten :         self.FillListBox(data, self.RandomArray10ListBox);
      TArraySize.Hundred :     self.FillListBox(data, self.RandomArray100ListBox);
      TArraySize.TwoThousand : self.FillListBox(data, self.RandomArray2000ListBox);
    end;

   copied := CopyArray(data);
   item := self.DataListView.Items.Add;
   item.Caption := Defines.GenKindToString(kind) + ' (' + IntToStr(ord(size)) + ' elems)';

   Sorts.ShakeSort(data, comps, perms);
   item.SubItems.Add(IntToStr(perms));
   item.SubItems.Add(IntToStr(comps));
   data := CopyArray(copied);

   Sorts.StraightSelectionSort(data, comps, perms);
   item.SubItems.Add(IntToStr(perms));
   item.SubItems.Add(IntToStr(comps));
   data := CopyArray(copied);

   Sorts.QuickSort(data, 0, length(data)-1, comps, perms);
   item.SubItems.Add(IntToStr(perms));
   item.SubItems.Add(IntToStr(comps));
end;

procedure TMainForm.FillListView();
begin
  self.DataListView.Items.Clear;
  self.AddItem(TArraySize.Ten,         TGenKind.RandomArray);
  self.AddItem(TArraySize.Ten,         TGenKind.SortedArray);
  self.AddItem(TArraySize.Ten,         TGenKind.ReversedArray);
  self.AddItem(TArraySize.Hundred,     TGenKind.RandomArray);
  self.AddItem(TArraySize.Hundred,     TGenKind.SortedArray);
  self.AddItem(TArraySize.Hundred,     TGenKind.ReversedArray);
  self.AddItem(TArraySize.TwoThousand, TGenKind.RandomArray);
  self.AddItem(TArraySize.TwoThousand, TGenKind.SortedArray);
  self.AddItem(TArraySize.TwoThousand, TGenKind.ReversedArray);
end;

procedure TMainForm.FillListBox(arr : TIntArray; listbox : TListBox);
var i,j : integer;
var caption : string;
begin
  listbox.Clear;
  j := 0;
  caption := '';
  for i := 0 to length(arr)-1 do begin
    if (j >= (listbox.Width / 15)) or (i = length(arr)-1) then begin
      j := 0;
      listbox.AddItem(caption, nil);
      caption := '';
    end;
    caption := caption + IntToStr(arr[i]) + ', ';
    inc(j);
  end;
end;

procedure TMainForm.OpenCalcButtonClick(Sender: TObject);
begin
  self.OpenCalculator;
end;

procedure TMainForm.OpenCalculator();
var calculatorForm : TCalcForm;
begin
  calculatorForm := TCalcForm.Create(self);
  calculatorForm.Show;
end;

procedure TMainForm.OpenDiagram();
var diagramForm : TDiagramForm;
var diagramOptionsForm : TDiagramOptionsForm;
begin
  diagramOptionsForm := TDiagramOptionsForm.Create(self);
  diagramOptionsForm.ShowModal;
  if diagramOptionsForm.IsOptionsSelected() then begin
    diagramForm := TDiagramForm.Create(self);
    diagramForm.SetArgs(self.CreateDiagram(diagramOptionsForm));
    diagramForm.ShowModal;
    diagramForm.Release;
  end;
  diagramOptionsForm.Release;
end;

function TMainForm.CreateDiagram(options : TDiagramOptionsForm) : TPieGroup;
var header : string;
var pies : TPies;
var itemIndex : integer;
var size : integer;
var gen  : integer;
var data : integer;
begin
  gen  := options.OptionsGenComboBox.ItemIndex;
  size := options.OptionsSizeComboBox.ItemIndex;
  case size of
    0 : size := 0;
    1 : size := 3;
    2 : size := 6;
  end;
  itemIndex := size + gen;

  SetLength(pies, 3);
  header := self.DataListView.Items[itemIndex].Caption;
  case options.OptionsDataComboBox.ItemIndex of
    0: begin
        pies[0] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[0]), 'Shake Sort', clGreen);
        pies[1] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[2]), 'SSelection Sort', clBlue);
        pies[2] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[4]), 'Quick Sort', clPurple);
        header := header + ' (Perms)';
    end;
    1: begin
        pies[0] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[1]), 'Shake Sort', clGreen);
        pies[1] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[3]), 'SSelection Sort', clBlue);
        pies[2] := CreatePie(StrToInt(self.DataListView.Items[itemIndex].SubItems[5]), 'Quick Sort', clPurple);
        header := header + ' (Comps)';
    end;
  end;
  exit(CreatePieGroup(pies, CreateLocation(20, 20, 460, 440), header));
end;

procedure TMainForm.OpenDiagramsButtonClick(Sender: TObject);
begin
  self.OpenDiagram;
end;

procedure TMainForm.RegenerateButtonClick(Sender: TObject);
begin
  self.FillListView;
end;

procedure TMainForm.ChangeFont(bySize : integer);
begin
  if bySize > 0 then begin
    if self.DataListView.Font.Size + bySize <= 25 then
      self.DataListView.Font.Size := self.DataListView.Font.Size + bySize;
  end
  else
    if self.DataListView.Font.Size + bySize >= 5 then
      self.DataListView.Font.Size := self.DataListView.Font.Size + bySize;
end;

procedure TMainForm.DataListViewKeyPress(Sender: TObject; var Key: Char);
begin
  case Key of
    '+' : self.ChangeFont(2);
    '-' : self.ChangeFont(-2);
    'c' : self.OpenCalculator;
  end;
  Key := #10;
end;

procedure TMainForm.FormCreate(Sender: TObject);
begin
  self.FillListView;
end;

procedure TMainForm.FormResize(Sender: TObject);
var i : integer;
begin
  for i := 0 to self.DataListView.Columns.Count-1 do
    self.DataListView.Columns[i].Width := round(self.Width / 7);
end;

end.
