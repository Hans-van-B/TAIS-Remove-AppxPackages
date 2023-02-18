' https://www.vbforums.com/showthread.php?586974-Create-a-shortcut-using-IWshRuntimeLibrary
'
' Add Scripting host:
' - Go to Form Design
' - From the Add Reference dialog, click the .COM tab
' - Select the 'Windows Scripting Host Object Model', make sure you check the box
' - and click the 'Add' button.

Imports System.IO
Imports IWshRuntimeLibrary

Module Install
    Sub InstallMe()
        xtrace_subs("InstallMe")

        '---- Copy myself to program files
        Dim InstTarget = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\TAIS\Util"
        Dim MyCopy = InstTarget & "\" & AppName & ".exe"
        xtrace_i("MyCopy = " & MyCopy)

        My.Computer.FileSystem.CopyFile(Application.ExecutablePath, MyCopy, True)
        My.Computer.FileSystem.CopyFile(IniFile, InstTarget & "\" & AppName & ".ini", True)

        '---- Create Icon
        xtrace_i("CreateStartIcon")
        Dim Sm As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu)
        xtrace_i("Start Menu = " & Sm)
        Dim TargetDir As String = Sm & "\TAIS\Util"
        If Not My.Computer.FileSystem.DirectoryExists(TargetDir) Then
            My.Computer.FileSystem.CreateDirectory(TargetDir)
        End If

        Dim ShortcutPath As String = TargetDir & "\" & AppName.Replace("-", " ") & ".lnk"
        xtrace_i("ShortcutPath = " & ShortcutPath)

        Dim MyWshShell As WshShell = New WshShell
        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
        MyShortcut = CType(MyWshShell.CreateShortcut(ShortcutPath), IWshRuntimeLibrary.IWshShortcut)
        With MyShortcut
            .TargetPath = MyCopy     'Specify target executable full path
            .Description = "Technical Application Installation Service Utility: " & AppName
            '.IconLocation = InstTarget & "\Installer.ico"
            .IconLocation = MyCopy
            .Save()
        End With

        xtrace_sube("InstallMe")
    End Sub
End Module
