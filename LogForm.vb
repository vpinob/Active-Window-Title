Imports System
Imports System.IO
Imports System.Text

Public Class LogForm
    
    Private Declare Function GetForegroundWindow Lib "user32.dll" () As Int32
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
            
            'Get the active window title
            Private Function GetActiveWindowTitle() As String
                Dim StringTitle As String

                StringTitle = New String(Chr(0), 100)
                GetWindowText(GetForegroundWindow, StringTitle, 100)
                StringTitle = StringTitle.Substring(0, InStr(StringTitle, Chr(0)) - 1)

                Return StringTitle

            End Function
            
            'Set the window title into TextBox1 each 5 seconds.
            
            Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

                TextBox1.Text = TextBox1.Text + vbNewLine & My.Computer.Clock.LocalTime + " - " & GetActiveWindowTitle()

            End Sub
            
            
            
            Private Sub LogForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

                Dim path As String = "C:\Temp\File.txt"
                
                'Ones the form Closes, create a new directory "C:\Temp" if it does not exist.
                If Not File.Exists(path) Then
                    My.Computer.FileSystem.CreateDirectory("C:\Temp")
                    
                    'Writes all lines from TextBox1 into the file "File.txt".
                    Using sw As StreamWriter = File.CreateText(path)
                        sw.WriteLine(TextBox1.Text)
                    End Using
                Else
            'Append the new lines into the the "File.txt"
                    Using sw As StreamWriter = File.AppendText(path)
                        sw.WriteLine(TextBox1.Text)
                    End Using
                End If
            End Sub
End Class
