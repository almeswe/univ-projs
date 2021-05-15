object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'MainForm'
  ClientHeight = 624
  ClientWidth = 959
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
    959
    624)
  PixelsPerInch = 96
  TextHeight = 13
  object DataListView: TListView
    Left = 0
    Top = 0
    Width = 766
    Height = 623
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
    SortType = stText
    TabOrder = 0
    ViewStyle = vsReport
    OnKeyPress = DataListViewKeyPress
  end
  object AddNewButton: TButton
    Left = 792
    Top = 48
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'ADD'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 1
    OnClick = AddNewButtonClick
  end
  object LoadButton: TButton
    Left = 792
    Top = 24
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'LOAD'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 2
    OnClick = LoadButtonClick
  end
  object SaveButton: TButton
    Left = 866
    Top = 24
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'SAVE'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = SaveButtonClick
  end
  object DeleteButton: TButton
    Left = 866
    Top = 48
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'DELETE'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 4
    OnClick = DeleteButtonClick
  end
  object CorrectButton: TButton
    Left = 832
    Top = 72
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'CORRECT'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 5
    OnClick = CorrectButtonClick
  end
  object ClearButton: TButton
    Left = 832
    Top = 96
    Width = 75
    Height = 25
    Anchors = [akTop, akRight]
    Caption = 'CLEAR'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Consolas'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 6
    OnClick = ClearButtonClick
  end
  object SETasksRadioButton: TRadioButton
    Left = 775
    Top = 232
    Width = 114
    Height = 17
    Anchors = [akTop, akRight]
    Caption = 'Employee Tasks'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 7
    OnClick = SETasksRadioButtonClick
  end
  object NoneRadioButton: TRadioButton
    Left = 775
    Top = 209
    Width = 97
    Height = 17
    Anchors = [akTop, akRight]
    Caption = 'None'
    Checked = True
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 8
    TabStop = True
    OnClick = NoneRadioButtonClick
  end
  object SENTasksRadioButton: TRadioButton
    Left = 775
    Top = 255
    Width = 176
    Height = 17
    Anchors = [akTop, akRight]
    Caption = 'Employee Tasks (Nowadays)'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 9
    OnClick = SENTasksRadioButtonClick
  end
  object OpenDialog: TOpenDialog
    Left = 40
    Top = 520
  end
end
