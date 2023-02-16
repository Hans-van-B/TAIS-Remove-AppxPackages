Module Glob
    ' Template Windows Forms
    Public AppName As String = "TAIS-Remove-AppxPackages"
    Public AppVer As String = "0.01"

    Public AppRoot As String = Application.StartupPath
    Public CD As String = My.Computer.FileSystem.CurrentDirectory

    ' Log File
    Public Temp As String = Environment.GetEnvironmentVariable("TEMP")
    Public G_TL_FR As Integer = 2   ' Trace level File Reads
    Public ErrorCount As Integer = 0
    Public WarningCount As Integer = 0
    Public ExitProgram As Boolean = False

    ' Defaults
    Public IniFile1 As String = AppRoot & "\" & AppName & ".ini"
    Public IniFile2 As String = AppRoot & "\Data\" & AppName & ".ini"


    Public EndDelay As Integer = 2
    Public EndPause As Boolean = False
    Public ShowGui As Boolean = False

    Sub InitTemp()
        Temp = Temp & "\TAIS"
        If Not My.Computer.FileSystem.DirectoryExists(Temp) Then
            My.Computer.FileSystem.CreateDirectory(Temp)
        End If

        LogFile = Temp & "\" & AppName & ".log"
    End Sub
End Module
