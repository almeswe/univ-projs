object DiagramOptionsForm: TDiagramOptionsForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'Options'
  ClientHeight = 122
  ClientWidth = 199
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object OptionsSizeComboBox: TComboBox
    Left = 8
    Top = 8
    Width = 183
    Height = 21
    Style = csDropDownList
    ItemIndex = 0
    TabOrder = 0
    Text = '10'
    Items.Strings = (
      '10'
      '100'
      '2000')
  end
  object OptionsGenComboBox: TComboBox
    Left = 8
    Top = 35
    Width = 183
    Height = 21
    Style = csDropDownList
    ItemIndex = 0
    TabOrder = 1
    Text = 'Random'
    Items.Strings = (
      'Random'
      'Sorted'
      'Reversed')
  end
  object OptionsDataComboBox: TComboBox
    Left = 8
    Top = 62
    Width = 183
    Height = 21
    Style = csDropDownList
    ItemIndex = 0
    TabOrder = 2
    Text = 'Perms'
    Items.Strings = (
      'Perms'
      'Comps')
  end
  object OptionsSubmitButton: TButton
    Left = 8
    Top = 89
    Width = 183
    Height = 25
    Caption = 'Submit'
    TabOrder = 3
    OnClick = OptionsSubmitButtonClick
  end
end
