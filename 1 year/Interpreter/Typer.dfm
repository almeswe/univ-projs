object TypeForm: TTypeForm
  Left = 0
  Top = 0
  ActiveControl = TypeEdit
  Caption = 'Typer'
  ClientHeight = 228
  ClientWidth = 491
  Color = clWhite
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  DesignSize = (
    491
    228)
  PixelsPerInch = 96
  TextHeight = 13
  object EnterExprLabel: TLabel
    Left = 8
    Top = 3
    Width = 133
    Height = 14
    Caption = 'Enter Expression : '
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Consolas'
    Font.Style = []
    ParentFont = False
  end
  object InfoLabel: TLabel
    Left = 8
    Top = 58
    Width = 98
    Height = 14
    Caption = 'Information : '
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Consolas'
    Font.Style = []
    ParentFont = False
  end
  object InfoListBox: TListBox
    Left = 8
    Top = 74
    Width = 475
    Height = 146
    Anchors = [akLeft, akTop, akRight, akBottom]
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Consolas'
    Font.Style = []
    ItemHeight = 14
    ParentFont = False
    TabOrder = 0
    OnKeyPress = InfoListBoxKeyPress
  end
  object TypeEdit: TEdit
    Left = 8
    Top = 21
    Width = 475
    Height = 23
    Anchors = [akLeft, akTop, akRight]
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    OnChange = TypeEditChange
  end
  object SaveButton: TButton
    Left = 318
    Top = 47
    Width = 83
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'SAVE'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 2
    OnClick = SaveButtonClick
  end
  object ShowButton: TButton
    Left = 401
    Top = 47
    Width = 83
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'SHOW'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = ShowButtonClick
  end
  object OpenDialog: TOpenDialog
    Left = 280
    Top = 48
  end
end
