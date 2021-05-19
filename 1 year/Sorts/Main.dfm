object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'MainForm'
  ClientHeight = 434
  ClientWidth = 730
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
    730
    434)
  PixelsPerInch = 96
  TextHeight = 13
  object DataListView: TListView
    Left = 0
    Top = 104
    Width = 730
    Height = 330
    Align = alBottom
    Anchors = [akLeft, akTop, akRight, akBottom]
    Columns = <
      item
        Caption = 'Size'
      end
      item
        Caption = 'Shake Sort (perms)'
      end
      item
        Caption = 'Shake Sort (comps)'
      end
      item
        Caption = 'SSelection Sort (perms)'
      end
      item
        Caption = 'SSelection Sort (comps)'
      end
      item
        Caption = 'Quick Sort (perms)'
      end
      item
        Caption = 'Quick Sort (comps)'
      end
      item
        Caption = 'Time (s)'
      end>
    TabOrder = 0
    ViewStyle = vsReport
    OnKeyPress = DataListViewKeyPress
    ExplicitLeft = 8
  end
  object OpenButtonsGroupBox: TGroupBox
    Left = 8
    Top = 8
    Width = 121
    Height = 90
    Caption = 'Other sources'
    TabOrder = 1
    object OpenCalcButton: TButton
      Left = 3
      Top = 47
      Width = 115
      Height = 25
      Caption = 'Open Calculator'
      TabOrder = 0
      OnClick = OpenCalcButtonClick
    end
    object OpenDiagramsButton: TButton
      Left = 3
      Top = 16
      Width = 115
      Height = 25
      Caption = 'Open Diagram'
      TabOrder = 1
      OnClick = OpenDiagramsButtonClick
    end
  end
  object RandomArraysGroupBox: TGroupBox
    Left = 135
    Top = 8
    Width = 587
    Height = 90
    Anchors = [akLeft, akTop, akRight]
    Caption = 'Arrays'
    TabOrder = 2
    DesignSize = (
      587
      90)
    object Label1: TLabel
      Left = 96
      Top = 16
      Width = 54
      Height = 13
      Caption = 'Random 10'
    end
    object Label2: TLabel
      Left = 96
      Top = 40
      Width = 60
      Height = 13
      Caption = 'Random 100'
    end
    object Label3: TLabel
      Left = 96
      Top = 67
      Width = 66
      Height = 13
      Caption = 'Random 2000'
    end
    object RegenerateButton: TButton
      Left = 0
      Top = 33
      Width = 90
      Height = 25
      Caption = 'Regen'
      TabOrder = 0
      OnClick = RegenerateButtonClick
    end
    object RandomArray10ListBox: TListBox
      Left = 168
      Top = 13
      Width = 409
      Height = 20
      Anchors = [akLeft, akTop, akRight]
      ItemHeight = 13
      TabOrder = 1
    end
    object RandomArray100ListBox: TListBox
      Left = 168
      Top = 38
      Width = 409
      Height = 20
      Anchors = [akLeft, akTop, akRight]
      ItemHeight = 13
      TabOrder = 2
    end
    object RandomArray2000ListBox: TListBox
      Left = 168
      Top = 64
      Width = 409
      Height = 20
      Anchors = [akLeft, akTop, akRight]
      ItemHeight = 13
      TabOrder = 3
    end
  end
end
