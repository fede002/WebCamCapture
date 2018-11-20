Public Class Inicio


    Public Shared Sub Main(ByVal arg() As String)
        Application.EnableVisualStyles()

        'Dim fAcceso As New FormAcceso
        'If fAcceso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

        'fAcceso.Close()
        Dim param As String = ""
        For Each tmpstr In arg
            If tmpstr <> "" Then
                param += tmpstr.Trim
            End If
        Next
        param = param.ToLower().Replace("fotouom:", "")
        If param <> "" Then
            'MsgBox("Estos son los parametros: " + param)
        End If

        ''PRUEBA DE PARAMETROS
        param = "TIT,50128,FOTO"
        'param = "16,35,58,LMORALES,19"


        Try
            Dim reg As New RegEdit
            If reg.EjecucionValida Then
                Application.Run(New Login(param))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        'End If
    End Sub

End Class
