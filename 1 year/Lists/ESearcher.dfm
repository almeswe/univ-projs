object ESearchForm: TESearchForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'ESearcher'
  ClientHeight = 91
  ClientWidth = 279
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
  object SearchSubmitButton: TButton
    Left = 8
    Top = 62
    Width = 267
    Height = 25
    Caption = 'SUBMIT'
    TabOrder = 0
    OnClick = SearchSubmitButtonClick
  end
  object SearchNameEdit: TEdit
    Left = 8
    Top = 8
    Width = 267
    Height = 23
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 1
  end
  object SearchOptionsComboBox: TComboBox
    Left = 8
    Top = 35
    Width = 267
    Height = 21
    Style = csDropDownList
    TabOrder = 2
    Items.Strings = (
      'NSM'
      'Project Name'
      'Project Task'
      'Deadline'
      'Shedule')
  end
end
