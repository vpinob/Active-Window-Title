Imports System
Imports System.IO
Imports System.Text

Public Class LogForm

    Private Declare Function GetForegroundWindow Lib "user32.dll" () As Int32
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32

    Private Function GetActiveWindowTitle() As String

        Dim MyStr As String

        MyStr = New String(Chr(0), 100)
        GetWindowText(GetForegroundWindow, MyStr, 100)
        MyStr = MyStr.Substring(0, InStr(MyStr, Chr(0)) - 1)

        Return MyStr

    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        TextBox1.Text = TextBox1.Text + vbNewLine & My.Computer.Clock.LocalTime + " - " & GetActiveWindowTitle()

    End Sub

    Private Sub LogForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        Dim path As String = "C:\Temp\File.txt"

        If Not File.Exists(path) Then
            My.Computer.FileSystem.CreateDirectory("C:\Temp")

            Using sw As StreamWriter = File.CreateText(path)
                sw.WriteLine(TextBox1.Text)
            End Using
        Else
            Using sw As StreamWriter = File.AppendText(path)
                sw.WriteLine(TextBox1.Text)
            End Using
        End If
    End Sub

    Private Sub LogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
