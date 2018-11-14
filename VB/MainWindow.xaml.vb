Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.Xpf.PdfViewer

Namespace DXSample
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CustomPdfCommandProvider


        Inherits PdfCommandProvider

        Private ReadOnly factors As New List(Of Double) From {0.15, 0.3, 0.45, 1, 1.25, 1.5, 2, 5}
        Public ReadOnly Property Control() As PdfViewerControl
            Get
                Return TryCast(DocumentViewer, PdfViewerControl)
            End Get
        End Property


        Private zoomInCommandInternal_Renamed As ICommand
        Protected Overrides ReadOnly Property ZoomInCommandInternal() As ICommand
            Get
                If zoomInCommandInternal_Renamed IsNot Nothing Then
                    Return zoomInCommandInternal_Renamed
                Else
                    zoomInCommandInternal_Renamed = New DelegateCommand(AddressOf ZoomIn, AddressOf CanZoomIn)
                    Return zoomInCommandInternal_Renamed
                End If
            End Get
        End Property


        Private zoomOutCommandInternal_Renamed As ICommand
        Protected Overrides ReadOnly Property ZoomOutCommandInternal() As ICommand
            Get
                If zoomOutCommandInternal_Renamed IsNot Nothing Then
                    Return zoomOutCommandInternal_Renamed
                Else
                    zoomOutCommandInternal_Renamed = New DelegateCommand(AddressOf ZoomOut, AddressOf CanZoomOut)
                    Return zoomOutCommandInternal_Renamed
                End If
            End Get
        End Property

        Private Sub ZoomIn()
            If Control Is Nothing Then
                Return
            End If

            For Each zoomFactor In factors
                If Control.ZoomFactor < zoomFactor Then
                    Control.ZoomFactor = zoomFactor
                    Exit For
                End If
            Next zoomFactor
        End Sub

        Private Function CanZoomIn() As Boolean
            Return Control IsNot Nothing AndAlso Control.Document IsNot Nothing AndAlso Control.ZoomFactor < factors(factors.Count - 1)
        End Function

        Private Sub ZoomOut()
            If Control Is Nothing Then
                Return
            End If

            For i As Integer = factors.Count - 1 To 0 Step -1
                If factors(i) < Control.ZoomFactor Then
                    Control.ZoomFactor = factors(i)
                    Exit For
                End If
            Next i
        End Sub

        Private Function CanZoomOut() As Boolean
            Return Control IsNot Nothing AndAlso Control.Document IsNot Nothing AndAlso Control.ZoomFactor > factors(0)
        End Function

        Protected Overrides Function CreateZoomModeAndZoomFactorItem(ByVal dllName As String) As ICommand

            Dim items = CreateZoomModeAndFactorsItems()
            Dim setZoomModeAndFactor As CommandCheckItems = New CommandCheckItems With {.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomCaption),
                .Hint = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomDescription),
                .Group = DocumentViewerLocalizer.GetString(DocumentViewerStringId.ZoomRibbonGroupCaption),
                .Command = New DelegateCommand(Function()
                                               End Function, Function() items.Any(Function(x) x.CanExecute(Nothing))), .Items = items,
            .SmallGlyph = UriHelper.GetUri(dllName, "\Images\Zoom_16x16.png"), .LargeGlyph = UriHelper.GetUri(dllName, "\Images\Zoom_32x32.png")}
            Return setZoomModeAndFactor
        End Function

        Private Function CreateZoomModeAndFactorsItems() As ObservableCollection(Of CommandToggleButton)

            Dim zoomModeAndFactorsItems As New ObservableCollection(Of CommandToggleButton)()

            Dim setZoomFactorCommand As DelegateCommand(Of Double) = New DelegateCommand(Of Double)(Function(x)
                                                                                                        SetZoomFactorCommandInternal.Execute(x)
                                                                                                        UpdateZoomCommand()
                                                                                                    End Function, Function(x) SetZoomFactorCommandInternal.CanExecute(x))

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "15%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 0.15,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "30%",
            .Command = New CommandWrapper(Function() (setZoomFactorCommand)),
            .ZoomFactor = 0.3,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "45%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 0.45,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "100%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 1,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "125%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 1.25,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "150%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 1.5,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "200%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 2,
            .GroupIndex = 1
        })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With {
            .Caption = "500%",
            .Command = New CommandWrapper(Function() setZoomFactorCommand),
            .ZoomFactor = 5,
            .GroupIndex = 1
        })
            Return zoomModeAndFactorsItems
        End Function

        Protected Overrides Function UpdateZoomFactorCheckState(item As CommandToggleButton) As Boolean
            Return MyBase.UpdateZoomFactorCheckState(item)
        End Function

        Private Sub UpdateZoomCommand()
            Dim zoomCommand As CommandCheckItems = TryCast(zoomCommand, CommandCheckItems)
            If zoomCommand Is Nothing Then
            End If
            Return
        End Sub
    End Class
End Namespace






