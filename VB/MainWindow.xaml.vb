Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.Xpf.PdfViewer

Namespace DXSample

    Public Partial Class MainWindow
        Inherits System.Windows.Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class CustomPdfCommandProvider
        Inherits DevExpress.Xpf.PdfViewer.PdfCommandProvider

        Private ReadOnly factors As System.Collections.Generic.List(Of Double) = New System.Collections.Generic.List(Of Double) From {0.15, 0.3, 0.45, 1, 1.25, 1.5, 2, 5}

        Public ReadOnly Property Control As PdfViewerControl
            Get
                Return TryCast(Me.DocumentViewer, DevExpress.Xpf.PdfViewer.PdfViewerControl)
            End Get
        End Property

        Private zoomInCommandInternalField As System.Windows.Input.ICommand

        Protected Overrides ReadOnly Property ZoomInCommandInternal As ICommand
            Get
                Return If(Me.zoomInCommandInternalField, Function()
                    Me.zoomInCommandInternalField = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.ZoomIn, AddressOf Me.CanZoomIn)
                    Return Me.zoomInCommandInternalField
                End Function())
            End Get
        End Property

        Private zoomOutCommandInternalField As System.Windows.Input.ICommand

        Protected Overrides ReadOnly Property ZoomOutCommandInternal As ICommand
            Get
                Return If(Me.zoomOutCommandInternalField, Function()
                    Me.zoomOutCommandInternalField = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.ZoomOut, AddressOf Me.CanZoomOut)
                    Return Me.zoomOutCommandInternalField
                End Function())
            End Get
        End Property

        Private Sub ZoomIn()
            If Me.Control Is Nothing Then Return
            For Each zoomFactor In Me.factors
                If Me.Control.ZoomFactor < zoomFactor Then
                    Me.Control.ZoomFactor = zoomFactor
                    Exit For
                End If
            Next
        End Sub

        Private Function CanZoomIn() As Boolean
            Return Me.Control IsNot Nothing AndAlso Me.Control.Document IsNot Nothing AndAlso Me.Control.ZoomFactor < Me.factors(Me.factors.Count - 1)
        End Function

        Private Sub ZoomOut()
            If Me.Control Is Nothing Then Return
            For i As Integer = Me.factors.Count - 1 To 0 Step -1
                If Me.factors(i) < Me.Control.ZoomFactor Then
                    Me.Control.ZoomFactor = Me.factors(i)
                    Exit For
                End If
            Next
        End Sub

        Private Function CanZoomOut() As Boolean
            Return Me.Control IsNot Nothing AndAlso Me.Control.Document IsNot Nothing AndAlso Me.Control.ZoomFactor > Me.factors(0)
        End Function

        Protected Overrides Function CreateZoomModeAndZoomFactorItem(ByVal dllName As String) As ICommand
            Dim items = Me.CreateZoomModeAndFactorsItems()
            Dim setZoomModeAndFactor As DevExpress.Xpf.DocumentViewer.CommandCheckItems = New DevExpress.Xpf.DocumentViewer.CommandCheckItems With {.Caption = DevExpress.Xpf.DocumentViewer.DocumentViewerLocalizer.GetString(DevExpress.Xpf.DocumentViewer.DocumentViewerStringId.CommandZoomCaption), .Hint = DevExpress.Xpf.DocumentViewer.DocumentViewerLocalizer.GetString(DevExpress.Xpf.DocumentViewer.DocumentViewerStringId.CommandZoomDescription), .Group = DevExpress.Xpf.DocumentViewer.DocumentViewerLocalizer.GetString(DevExpress.Xpf.DocumentViewer.DocumentViewerStringId.ZoomRibbonGroupCaption), .Command = New DevExpress.Mvvm.DelegateCommand(Sub()
            End Sub, Function() items.Any(Function(x) x.CanExecute(Nothing))), .Items = items, .SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "\Images\Zoom_16x16.png"), .LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "\Images\Zoom_32x32.png")}
            Return setZoomModeAndFactor
        End Function

        Private Function CreateZoomModeAndFactorsItems() As ObservableCollection(Of DevExpress.Xpf.DocumentViewer.CommandToggleButton)
            Dim zoomModeAndFactorsItems As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.DocumentViewer.CommandToggleButton) = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.DocumentViewer.CommandToggleButton)()
            Dim setZoomFactorCommand As DevExpress.Mvvm.DelegateCommand(Of Double) = New DevExpress.Mvvm.DelegateCommand(Of Double)(Sub(x)
                Me.SetZoomFactorCommandInternal.Execute(x)
                Me.UpdateZoomCommand()
            End Sub, Function(x) Me.SetZoomFactorCommandInternal.CanExecute(x))
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "15%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 0.15, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "30%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function()(setZoomFactorCommand)), .ZoomFactor = 0.3, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "45%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 0.45, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "100%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 1, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "125%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 1.25, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "150%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 1.5, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "200%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 2, .GroupIndex = 1})
            zoomModeAndFactorsItems.Add(New DevExpress.Xpf.DocumentViewer.CommandSetZoomFactorAndModeItem With {.Caption = "500%", .Command = New DevExpress.Xpf.DocumentViewer.CommandWrapper(Function() setZoomFactorCommand), .ZoomFactor = 5, .GroupIndex = 1})
            Return zoomModeAndFactorsItems
        End Function

        Private Sub UpdateZoomCommand()
            Dim zoomCommand As DevExpress.Xpf.DocumentViewer.CommandCheckItems = TryCast(Me.ZoomCommand, DevExpress.Xpf.DocumentViewer.CommandCheckItems)
            If zoomCommand Is Nothing Then Return
            zoomCommand.UpdateCheckState(New Global.System.Func(Of Global.DevExpress.Xpf.DocumentViewer.CommandToggleButton, System.Boolean)(AddressOf Me.UpdateZoomFactorCheckState))
        End Sub
    End Class
End Namespace
