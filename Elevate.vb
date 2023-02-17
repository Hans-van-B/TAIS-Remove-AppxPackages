Module Elevate
    Sub ElevateMe()
        xtrace_subs("ElevateMe")

        Dim Exe As String = Application.ExecutablePath
        Dim Exe2 = Exe.Replace("""", "")
        xtrace_i("Exe2 = " & Exe2)

        Dim Param As String
        Param = "--install --seq=inst"

        xtrace_i("ReStart Me Elevated")
        Try
            Dim Proc As New Process()
            Dim ProcStartInfo As New ProcessStartInfo(Exe2)
            ProcStartInfo.Verb = "runas"
            ProcStartInfo.Arguments = Param
            Proc.StartInfo = ProcStartInfo
            Proc.Start()

        Catch ex As Exception
            xtrace_err(ex.Message)
        End Try

        xtrace_sube("ElevateMe")
    End Sub


End Module
