unit Calc;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, Vcl.StdCtrls, Vcl.Grids, Defines, Sorts;

type TCalcForm = class(TForm)
    CalcPermsCountLabel: TLabel;
    CalcCompsCountLabel: TLabel;
    CalcSizeEdit: TEdit;
    CalcRunButton: TButton;
    CalcSortComboBox: TComboBox;
    CalcGenComboBox: TComboBox;
    CalcCompsLabel: TLabel;
    CalcPermsLabel: TLabel;
    CalcArrayListBox: TListBox;
    CalcSortedArrayListBox: TListBox;
    procedure FormCreate(Sender: TObject);
    procedure CalcRunButtonClick(Sender: TObject);

  private
    type TGenOption  = (RandomGen, SortedGen, ReversedGen, AllGens);
    type TSortOption = (ShakeSort, QuickSort, StraightSelectionSort, AllSorts);

    type TOptions = record
      Gen  : TGenOption;
      Sort : TSortOption;
    end;

    var MainOptions : TOptions;

    procedure ShowArray(arr : TIntArray; listbox : TListBox);
    procedure Execute(sort : TSortOption; size : integer; gen : TGenOption);

  public

  end;

var CalcForm : TCalcForm;

implementation

{$R *.dfm}

procedure TCalcForm.CalcRunButtonClick(Sender: TObject);
begin
  Execute(TSortOption(self.CalcSortComboBox.ItemIndex), StrToInt(self.CalcSizeEdit.Text), TGenOption(self.CalcGenComboBox.ItemIndex));
end;

procedure TCalcForm.ShowArray(arr : TIntArray; listbox : TListBox);
var i : int;
begin
  listbox.Clear;
  for i := 0 to length(arr)-1 do begin
    listbox.AddItem(IntToStr(arr[i]), nil);
  end;
end;

procedure TCalcForm.Execute(sort : TSortOption; size : integer; gen : TGenOption);
var arr : TIntArray;
var comps, perms : uint;
begin
  arr := Sorts.CreateArray(size, TGenKind(gen));
  self.ShowArray(arr, self.CalcArrayListBox);
  case sort of
    TSortOption.QuickSort:
       arr := Sorts.QuickSort(arr, comps, perms);

    TSortOption.ShakeSort:
       arr := Sorts.ShakeSort(arr, comps, perms);

    TSortOption.StraightSelectionSort:
       arr := Sorts.StraightSelectionSort(arr, comps, perms);
  end;
  self.ShowArray(arr, self.CalcSortedArrayListBox);
  self.CalcPermsCountLabel.Caption := IntToStr(perms);
  self.CalcCompsCountLabel.Caption := IntToStr(comps);
end;

procedure TCalcForm.FormCreate(Sender: TObject);
begin
  self.Position := TPosition.poScreenCenter;
  self.MainOptions.Gen := TGenOption(self.CalcGenComboBox.ItemIndex);
  self.MainOptions.Sort := TSortOption(self.CalcSortComboBox.ItemIndex);
end;

end.
