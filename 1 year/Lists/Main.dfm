object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'MainForm'
  ClientHeight = 624
  ClientWidth = 939
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
    939
    624)
  PixelsPerInch = 96
  TextHeight = 13
  object DataListView: TListView
    Left = 0
    Top = 0
    Width = 741
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
    TabOrder = 0
    ViewStyle = vsReport
    OnColumnClick = DataListViewColumnClick
    OnKeyPress = DataListViewKeyPress
  end
  object AddNewButton: TButton
    Left = 764
    Top = 32
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
    Left = 764
    Top = 8
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
    Left = 838
    Top = 8
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
    Left = 838
    Top = 32
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
    Left = 804
    Top = 56
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
    Left = 804
    Top = 80
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
    Left = 747
    Top = 190
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
    Left = 747
    Top = 121
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
    Left = 747
    Top = 213
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
  object AEHRadioButton: TRadioButton
    Left = 747
    Top = 167
    Width = 176
    Height = 17
    Anchors = [akTop, akRight]
    Caption = 'Employees Hours'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 10
    OnClick = AEHRadioButtonClick
  end
  object SearchRadioButton: TRadioButton
    Left = 747
    Top = 144
    Width = 62
    Height = 17
    Anchors = [akTop, akRight]
    Caption = 'Search'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 11
    OnClick = SearchRadioButtonClick
  end
  object OpenDialog: TOpenDialog
    Left = 40
    Top = 520
  end
end
