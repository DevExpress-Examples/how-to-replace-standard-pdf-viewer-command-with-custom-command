Imports System
Imports System.Windows.Input

Namespace DXSample
  Public Class CustomPreviousPageCommand
      Implements ICommand

        Private baseCommand As ICommand
        Public Sub New(ByVal baseCommand As ICommand)
            Me.baseCommand = baseCommand
        End Sub

        Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                AddHandler baseCommand.CanExecuteChanged, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler baseCommand.CanExecuteChanged, value
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
            End RaiseEvent
        End Event

        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return baseCommand.CanExecute(parameter)
        End Function

        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            baseCommand.Execute(parameter)
        End Sub
  End Class
End Namespace