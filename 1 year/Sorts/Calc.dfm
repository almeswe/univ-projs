object CalcForm: TCalcForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'Calculator'
  ClientHeight = 121
  ClientWidth = 260
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Visible = True
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object CalcPermsCountLabel: TLabel
    Left = 207
    Top = 10
    Width = 21
    Height = 15
    Caption = '...'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = []
    ParentFont = False
  end
  object CalcCompsCountLabel: TLabel
    Left = 207
    Top = 37
    Width = 21
    Height = 15
    Caption = '...'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = []
    ParentFont = False
  end
  object CalcCompsLabel: TLabel
    Left = 159
    Top = 38
    Width = 32
    Height = 13
    Caption = 'Comps'
  end
  object CalcPermsLabel: TLabel
    Left = 159
    Top = 11
    Width = 29
    Height = 13
    Caption = 'Perms'
  end
  object CalcSizeEdit: TEdit
    Left = 8
    Top = 62
    Width = 145
    Height = 21
    TabOrder = 0
    Text = '20'
  end
  object CalcRunButton: TButton
    Left = 8
    Top = 89
    Width = 145
    Height = 25
    Caption = 'Run'
    TabOrder = 1
    OnClick = CalcRunButtonClick
  end
  object CalcSortComboBox: TComboBox
    Left = 8
    Top = 8
    Width = 145
    Height = 21
    Style = csDropDownList
    ItemIndex = 1
    TabOrder = 2
    Text = 'Quick Sort'
    Items.Strings = (
      'Shake Sort'
      'Quick Sort'
      'Strict Selection Sort')
  end
  object CalcGenComboBox: TComboBox
    Left = 8
    Top = 35
    Width = 145
    Height = 21
    Style = csDropDownList
    ItemIndex = 2
    TabOrder = 3
    Text = 'Reversed'
    Items.Strings = (
      'Random'
      'Sorted'
      'Reversed')
  end
  object CalcArrayListBox: TListBox
    Left = 159
    Top = 62
    Width = 42
    Height = 51
    BorderStyle = bsNone
    ItemHeight = 13
    TabOrder = 4
  end
  object CalcSortedArrayListBox: TListBox
    Left = 207
    Top = 62
    Width = 45
    Height = 51
    BorderStyle = bsNone
    ItemHeight = 13
    TabOrder = 5
  end
end
