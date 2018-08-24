Imports System.Windows
Imports System.Windows.Input
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

        Protected Overrides ReadOnly Property NextPageCommandInternal() As ICommand
            Get
                Return New CustomNextPageCommand(MyBase.NextPageCommandInternal)
            End Get
        End Property

        Protected Overrides ReadOnly Property PreviousPageCommandInternal() As ICommand
            Get
                Return New CustomPreviousPageCommand(MyBase.PreviousPageCommandInternal)
            End Get
        End Property
    End Class
End Namespace
