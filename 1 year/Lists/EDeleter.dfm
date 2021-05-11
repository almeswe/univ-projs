object EDeleterForm: TEDeleterForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'EDeleterForm'
  ClientHeight = 197
  ClientWidth = 244
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object DeleterIndexLabel: TLabel
    Left = 8
    Top = 9
    Width = 30
    Height = 13
    Caption = 'INDEX'
  end
  object DeleteInfoGroupBox: TGroupBox
    Left = 8
    Top = 55
    Width = 228
    Height = 106
    Caption = 'INFO'
    TabOrder = 0
    object DeleterNameLabel: TLabel
      Left = 16
      Top = 24
      Width = 28
      Height = 13
      Caption = 'NAME'
    end
    object DeleterSurnameLabel: TLabel
      Left = 16
      Top = 51
      Width = 48
      Height = 13
      Caption = 'SURNAME'
    end
    object DeleterMidnameLabel: TLabel
      Left = 16
      Top = 80
      Width = 47
      Height = 13
      Caption = 'MIDNAME'
    end
  end
  object DeleterIndexEdit: TEdit
    Left = 8
    Top = 28
    Width = 228
    Height = 21
    TabOrder = 1
    OnMouseLeave = DeleterIndexEditMouseLeave
  end
  object DeleterDeleteButton: TButton
    Left = 8
    Top = 167
    Width = 228
    Height = 25
    Caption = 'SUBMIT'
    TabOrder = 2
    OnClick = DeleterDeleteButtonClick
  end
end
