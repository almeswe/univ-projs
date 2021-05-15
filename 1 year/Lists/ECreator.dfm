object EConstructorForm: TEConstructorForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'EConstructorForm'
  ClientHeight = 386
  ClientWidth = 300
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
  object AddNewGroupBox: TGroupBox
    Left = 0
    Top = 0
    Width = 300
    Height = 386
    Align = alClient
    Caption = 'ADD NEW EMPLOYEE'
    TabOrder = 0
    object InfoGroupBox: TGroupBox
      Left = 15
      Top = 25
      Width = 281
      Height = 121
      Caption = 'INFO'
      TabOrder = 0
      object NameLabel: TLabel
        Left = 40
        Top = 26
        Width = 28
        Height = 13
        Caption = 'NAME'
      end
      object MiddlenameLabel: TLabel
        Left = 3
        Top = 80
        Width = 65
        Height = 13
        Caption = 'MIDDLENAME'
      end
      object SuurnameLabel: TLabel
        Left = 20
        Top = 53
        Width = 48
        Height = 13
        Caption = 'SURNAME'
      end
      object NameEdit: TEdit
        Left = 74
        Top = 24
        Width = 185
        Height = 21
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clMenuHighlight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        ParentFont = False
        TabOrder = 0
        Text = 'Alexey'
      end
      object MiddlenameEdit: TEdit
        Left = 74
        Top = 78
        Width = 185
        Height = 21
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clMenuHighlight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        Text = 'Sergeevich'
      end
      object SurnameEdit: TEdit
        Left = 74
        Top = 51
        Width = 185
        Height = 21
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clMenuHighlight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        ParentFont = False
        TabOrder = 2
        Text = 'Meleshko'
      end
    end
    object ProjectGroupBox: TGroupBox
      Left = 15
      Top = 152
      Width = 281
      Height = 122
      Caption = 'PROJECT'
      TabOrder = 1
      object ProjectNameLabel: TLabel
        Left = 40
        Top = 26
        Width = 28
        Height = 13
        Caption = 'NAME'
      end
      object TaskLabel: TLabel
        Left = 40
        Top = 55
        Width = 25
        Height = 13
        Caption = 'TASK'
      end
      object DeadlineLabel: TLabel
        Left = 19
        Top = 82
        Width = 49
        Height = 13
        Caption = 'DEADLINE'
      end
      object ProjectNameEdit: TEdit
        Left = 74
        Top = 24
        Width = 185
        Height = 21
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clMenuHighlight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        ParentFont = False
        TabOrder = 0
        Text = 'Arctic code'
      end
      object ProjectTaskComboBox: TComboBox
        Left = 74
        Top = 51
        Width = 185
        Height = 21
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clMenuHighlight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        Text = 'SE'
        Items.Strings = (
          'UX Design'
          'UI Design'
          'Management'
          'Front-end'
          'Back-end'
          'Full-stack'
          'SE')
      end
      object ProjectDeadlineMaskEdit: TMaskEdit
        Left = 74
        Top = 78
        Width = 183
        Height = 21
        EditMask = '!99/99/00;1;_'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        MaxLength = 8
        ParentFont = False
        TabOrder = 2
        Text = '12.12.21'
      end
    end
    object SheduleGroupBox: TGroupBox
      Left = 15
      Top = 280
      Width = 281
      Height = 74
      Caption = 'SHEDULE'
      TabOrder = 2
      object FromLabel: TLabel
        Left = 40
        Top = 18
        Width = 29
        Height = 13
        Caption = 'FROM'
      end
      object ToLabel: TLabel
        Left = 54
        Top = 45
        Width = 14
        Height = 13
        Caption = 'TO'
      end
      object SheduleEndMaskEdit: TMaskEdit
        Left = 74
        Top = 40
        Width = 174
        Height = 21
        EditMask = '!90:00;1;_'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        MaxLength = 5
        ParentFont = False
        TabOrder = 0
        Text = '22:22'
      end
      object SheduleStartMaskEdit: TMaskEdit
        Left = 75
        Top = 13
        Width = 173
        Height = 21
        EditMask = '!90:00;1;_'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -11
        Font.Name = 'Consolas'
        Font.Style = []
        MaxLength = 5
        ParentFont = False
        TabOrder = 1
        Text = '12:12'
      end
    end
    object SubmitButton: TButton
      Left = 34
      Top = 357
      Width = 262
      Height = 25
      Caption = 'SUBMIT'
      TabOrder = 3
      OnClick = SubmitButtonClick
    end
    object RandomButton: TButton
      Left = 3
      Top = 357
      Width = 28
      Height = 25
      Caption = 'R'
      TabOrder = 4
      OnClick = RandomButtonClick
    end
  end
end
