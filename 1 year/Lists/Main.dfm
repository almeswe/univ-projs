object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'MainForm'
  ClientHeight = 624
  ClientWidth = 791
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnResize = FormResize
  DesignSize = (
    791
    624)
  PixelsPerInch = 96
  TextHeight = 13
  object DataListView: TListView
    Left = 0
    Top = 72
    Width = 791
    Height = 552
    Anchors = [akLeft, akTop, akRight, akBottom]
    Columns = <
      item
        Caption = 'EMPLOYEE'
        Width = 150
      end
      item
        Caption = 'PROJECT'
        Width = 170
      end
      item
        Caption = 'TASK'
        Width = 150
      end
      item
        Caption = 'DEADLINE'
        Width = 150
      end
      item
        Caption = 'WORK SHEDULE'
        Width = 170
      end>
    TabOrder = 0
    ViewStyle = vsReport
  end
  object AddNewButton: TButton
    Left = 0
    Top = 39
    Width = 75
    Height = 25
    Caption = 'ADD'
    TabOrder = 1
    OnClick = AddNewButtonClick
  end
  object LoadButton: TButton
    Left = 0
    Top = 8
    Width = 75
    Height = 25
    Caption = 'LOAD'
    TabOrder = 2
    OnClick = LoadButtonClick
  end
  object SaveButton: TButton
    Left = 81
    Top = 8
    Width = 75
    Height = 25
    Caption = 'SAVE'
    TabOrder = 3
    OnClick = SaveButtonClick
  end
  object SortButton: TButton
    Left = 410
    Top = 8
    Width = 75
    Height = 25
    Caption = 'SORT'
    TabOrder = 4
  end
  object DeleteButton: TButton
    Left = 81
    Top = 39
    Width = 75
    Height = 25
    Caption = 'DELETE'
    TabOrder = 5
    OnClick = DeleteButtonClick
  end
  object OpenDialog: TOpenDialog
    Left = 736
    Top = 80
  end
end
