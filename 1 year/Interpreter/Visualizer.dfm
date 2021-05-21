object VisualForm: TVisualForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'Visualizer'
  ClientHeight = 575
  ClientWidth = 794
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object SurfaceImage: TImage
    Left = 0
    Top = 0
    Width = 794
    Height = 575
    Align = alClient
    ExplicitLeft = 376
    ExplicitTop = 304
    ExplicitWidth = 105
    ExplicitHeight = 105
  end
  object Button1: TButton
    Left = 464
    Top = 520
    Width = 75
    Height = 25
    Caption = 'Button1'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 545
    Top = 520
    Width = 75
    Height = 25
    Caption = 'Button2'
    TabOrder = 1
    OnClick = Button2Click
  end
end
