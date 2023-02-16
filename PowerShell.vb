Module PowerShell
    ' ToDo: Completely revise towards the existing procedures
    Public PS_Script_Target As String = Temp
    Public ScriptPath As String
    Public PS_Output As String
    Public AppxPackage_list() As String

    '---- Create PowerShell Script ----
    Sub CreatePSScript(PSCmd As String)
        xtrace_subs("CreatePSScript")

        PS_Script_Target = Temp

        If Not My.Computer.FileSystem.DirectoryExists(PS_Script_Target) Then
            My.Computer.FileSystem.CreateDirectory(PS_Script_Target)
        End If
        ScriptPath = PS_Script_Target & "\" & AppName & ".ps1"
        xtrace_i("ScriptPath = " & ScriptPath)
        PS_Output = PS_Script_Target & "\" & AppName & "_out.txt"

        CS_WriteHeader()
        CS_WriteLine(PSCmd & " >" & PS_Output)
        CS_WriteLine("Exit")

        xtrace_sube("CreatePSScript")
    End Sub
    '---- Create Script, Write header ----
    Sub CS_WriteHeader()
        xtrace_i("Write header")
        My.Computer.FileSystem.WriteAllText(ScriptPath, "# " & ScriptPath & vbNewLine, False)
        CS_WriteLine("# Created: " & DateTime.Now)
    End Sub
    '---- Create Script, WriteLine ----
    Sub CS_WriteLine(Text As String)
        My.Computer.FileSystem.WriteAllText(ScriptPath, Text & vbNewLine, True)
    End Sub

    '==== Run PowerShell Script ========================================
    Sub RunPS()
        xtrace_subs("RunPS")

        Dim S32 As String = Environment.GetFolderPath(Environment.SpecialFolder.System)
        Dim PSExe As String = S32 & "\WindowsPowerShell\v1.0\PowerShell.exe"

        Dim Arguments = "-ExecutionPolicy Bypass -File """ & ScriptPath & """"

        xtrace_i("Start: " & PSExe & " " & Arguments)
        Dim ProcStartInfo As New ProcessStartInfo(PSExe)
        With ProcStartInfo
            .Arguments = Arguments
            .UseShellExecute = False         ' Redirect
            .CreateNoWindow = True
        End With

        Dim Proc As New Process()
        Proc.StartInfo = ProcStartInfo
        Proc.Start()
        Proc.WaitForExit()
        Proc.Close()

        xtrace_sube("RunPS")
    End Sub

    Sub PS_Get_AppxPackage()
        xtrace_subs("PS_Get_AppxPackage")

        CreatePSScript("Get-AppxPackage -ErrorAction SilentlyContinue | select name")
        RunPS()
        Dim reader As String = My.Computer.FileSystem.ReadAllText(PS_Output, System.Text.Encoding.ASCII)
        AppxPackage_list = Split(reader, vbNewLine)
        xtrace_i(AppxPackage_list.Length.ToString & " Lines read")


        xtrace_sube("PS_Get_AppxPackage")
    End Sub
End Module
