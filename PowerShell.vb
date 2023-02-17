Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window

Module PowerShell
    ' ToDo: Completely revise towards the existing procedures
    Public PS_Script_Target As String = Temp
    Public ScriptPath As String
    Public PS_Output As String
    Public AppxPackage_list(10) As String

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
        Dim AppxPackage_list_tmp
        AppxPackage_list_tmp = Split(reader, vbNewLine)
        Dim LineCnt = AppxPackage_list_tmp.Length.ToString
        xtrace_i(LineCnt & " Lines read")

        xtrace_i("Filter")
        Dim Hdr As Boolean = True
        Dim Nr As Integer = 0
        ReDim AppxPackage_list(LineCnt + 1)

        For Each Title As String In AppxPackage_list_tmp
            If Title.Length < 2 Then Continue For
            If Title.Contains("----") Then
                Hdr = False
                Continue For
            End If
            If Hdr Then Continue For

            xtrace_i("Add: " & Title)
            AppxPackage_list(Nr) = Title.Trim
            Nr += 1
        Next
        xtrace_i(Nr & " Lines Added")

        xtrace_i("Sort")
        Array.Sort(AppxPackage_list)

        xtrace_sube("PS_Get_AppxPackage")
    End Sub

    ' https://presearch.com/search?q=What+Is+AppxPackage+Microsoft.BioEnrollment

    Sub DeleteAppxPackageConfirm(PackageName As String)
        xtrace_subs("DeleteAppxPackageConfirm")

        Dim NoWarning = {
            "Microsoft.BioEnrollment",
            "Microsoft.UI.Xaml.CBS",
            "Microsoft.MicrosoftStickyNotes",
            "Microsoft.MicrosoftSolitaireCollection",
            "Microsoft.YourPhone",
            "Microsoft.ZuneVideo"
            }

        Dim DontRemove = {
            "Microsoft.DesktopAppInstaller"
            }

        Dim Rsp As MsgBoxResult

        If NoWarning.Contains(PackageName) Then
            xtrace_i("No Warning")
            DeleteAppxPackage(PackageName)

        ElseIf DontRemove.Contains(PackageName) Then
            xtrace_i("Don't Remove")
            'MessageBox.Show("It is better not to remove this package", "Warning not to remove", MessageBoxButtons.OK)
            MsgBox("It is better not to remove this package", vbInformation, "Warning not to remove")

        Else
            xtrace_i("Print Warning")
            Rsp = MsgBox("Are you sure you want to remove package" & vbNewLine & PackageName, vbYesNo Or vbCritical, "Confirm")
            If Rsp = MsgBoxResult.Yes Then
                xtrace_i("Accept")
                DeleteAppxPackage(PackageName)
            Else
                xtrace_i("Decline")
            End If
        End If

        xtrace_sube("DeleteAppxPackageConfirm")
    End Sub

    Sub DeleteAppxPackage(PackageName As String)
        xtrace_subs("DeleteAppxPackage")

        CreatePSScript("Get-AppxPackage " & PackageName & " | Remove-AppxPackage")
        RunPS()

        xtrace_i("Disable button")
        For Nr As Integer = 0 To Form1.MaxButton
            If Form1.BTitles(Nr) = PackageName Then
                Form1.Buttons(Nr).Enabled = False
            End If
        Next

        xtrace_sube("DeleteAppxPackage")
    End Sub
End Module
