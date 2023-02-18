Module HelpMod

    '==== GUI Help ============================================================

    Dim HelpPage As String = Temp & "\help.html"
    Dim HelpPageHtm As String = AppRoot & "\" & AppName & ".html"
    Dim HelpPagePdf As String = AppRoot & "\" & AppName & ".pdf"

    Sub ShowHelp()
        xtrace("Show Help")

        If (System.IO.File.Exists(HelpPageHtm)) Then
            HelpPage = HelpPageHtm
        ElseIf (System.IO.File.Exists(HelpPagePdf)) Then
            HelpPage = HelpPagePdf
        Else
            xtrace("Did not find " & HelpPageHtm)
            xtrace("Did not find " & HelpPagePdf)

            xtrace("Create page")
            My.Computer.FileSystem.WriteAllText(HelpPage, "<html>" & vbNewLine, False)
            WriteHelp("<head>")
            WriteHelp("")
            WriteHelp("</head>")
            WriteHelp("<H2>" & AppName & " V" & AppVer & " Help Page</H2>")
            WriteHelp("<H2>WOW</H2>")
            WriteHelp("<ul>")
            WriteHelp(" <li>Press [Start] to read the existing AppxPackages. </li><br>")
            WriteHelp(" <li>Press any of the buttons on the right to remove that AppxPackage. </li><br>")
            WriteHelp("</ul>")
            WriteHelp("That't it. <br>")
            WriteHelp("<i>You are strongly encoraged to check what can be removed, and what should better be left alone.<i><br>")
            WriteHelp("<br>")
            WriteHelp("</body>")
            WriteHelp("</html")
        End If

        Process.Start(HelpPage, "")
    End Sub

    Sub ShowHelpAbout()
        xtrace("Show Help, about")

        xtrace("Create page")
        My.Computer.FileSystem.WriteAllText(HelpPage, "<html>" & vbNewLine, False)
        WriteHelp("<head>")
        WriteHelp("")
        WriteHelp("</head>")
        WriteHelp("<H2>" & AppName & " V" & AppVer & " Help About</H2>")
        WriteHelp("<br>")
        WriteHelp("Wiki: <a href=""https://github.com/Hans-van-B/TAIS-Remove-AppxPackages/wiki"">https://github.com/Hans-van-B/TAIS-Remove-AppxPackages/wiki</a> <br>")
        WriteHelp("<br>")
        WriteHelp("<br>")
        WriteHelp("<font size=""-1"">")
        WriteHelp(" The " & AppName & " log can be found here: " & LogFile & "<br>")
        WriteHelp(" Dev. : C:\Cloud1\VB.Net-Dev\TAIS-Util\TAIS-Remove-AppxPackages")
        WriteHelp(" Maint: C:\Cloud1\Dev\TAIS\TAIS-Remove-AppxPackages<br>")
        WriteHelp(" GitHub: <a href=""https://github.com/Hans-van-B/TAIS-Remove-AppxPackages"">https://github.com/Hans-van-B/TAIS-Remove-AppxPackages</a> <br>")
        WriteHelp(" Inst.: " & AppRoot & "<br>")
        WriteHelp(" The " & AppName & " log can be found here: " & Log.LogFile & "<br>")
        WriteHelp("</font>")
        WriteHelp("</body>")
        WriteHelp("</html")

        Process.Start(HelpPage, "")
    End Sub

    Sub WriteHelp(Line As String)
        My.Computer.FileSystem.WriteAllText(HelpPage, Line & vbNewLine, True)
    End Sub

End Module
