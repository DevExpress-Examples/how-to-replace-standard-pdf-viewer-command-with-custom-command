Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
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
            Dim setZoomModeAndFactor As CommandCheckItems = New CommandCheckItems With {.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomCaption), .Hint = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomDescription), .Group = DocumentViewerLocalizer.GetString(DocumentViewerStringId.ZoomRibbonGroupCaption), .Command = New DelegateCommand(Function()
                
            End Function, Function() items.Any(Function(x) x.CanExecute(Nothing))), .Items = items, .SmallGlyph = UriHelper.GetUri(dllName, "\Images\Zoom_16x16.png"), .LargeGlyph = UriHelper.GetUri(dllName, "\Images\Zoom_32x32.png")}
            Return setZoomModeAndFactor
        End Function

        Private Function CreateZoomModeAndFactorsItems() As ObservableCollection(Of CommandToggleButton)

            Dim zoomModeAndFactorsItems As New ObservableCollection(Of CommandToggleButton)()

            Dim setZoomFactorCommand As DelegateCommand(Of Double) = New DelegateCommand(Of Double)(Sub(x)
                SetZoomFactorCommandInternal.Execute(x)
                UpdateZoomCommand()
            End Sub, x{get{Return SetZoomFactorCommandInternal.CanExecute(x))
        End Function
    End Class


            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "15%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 0.15, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "30%", _
                .Command = New CommandWrapper(Function() (setZoomFactorCommand)), _
                .ZoomFactor = 0.3, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "45%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 0.45, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "100%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 1, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "125%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 1.25, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "150%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 1.5, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "200%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 2, _
                .GroupIndex = 1 _
            })

            zoomModeAndFactorsItems.Add(New CommandSetZoomFactorAndModeItem With { _
                .Caption = "500%", _
                .Command = New CommandWrapper(Function() setZoomFactorCommand), _
                .ZoomFactor = 5, _
                .GroupIndex = 1 _
            })
            Return zoomModeAndFactorsItems
End Namespace

'INSTANT VB TODO TASK: Local functions are not converted by Instant VB:
'        void UpdateZoomCommand()
'        {
'            CommandCheckItems zoomCommand = TryCast(ZoomCommand, CommandCheckItems);
'            if (zoomCommand == Nothing)
'                Return;
'            zoomCommand.UpdateCheckState(UpdateZoomFactorCheckState);
'        }
    }
}


