Imports System.Net

Public Class Uploading

    Public locform As Form
    Dim WithEvents webClient As WebClient
    Dim locseCancelo As Boolean = False
    Dim pagina As String
    Public locimagen As String = ""

    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        locform.Enabled = True
    End Sub


    Public Function procesarUpload() As Boolean
        Dim resul As Boolean = False
        pagina = System.Configuration.ConfigurationManager.AppSettings("pagina").ToString()


        Dim f As New IO.FileInfo(locimagen)
        If f.Exists = False Then
            Return False
        Else

            If f.Length > 15728640 Then
                MsgBox("El archivo supera los valores permitidos para su publicación" + vbCrLf + "Redusca la calidad de la Imagen e intente subirla nuevamente", MsgBoxStyle.Critical, "Error: " + Me.Text)
                Return False
            ElseIf f.Length > 10485760 Then
                MsgBox("El archivo supera los 10 Megabystes" + vbCrLf + "Esto puede producir errores al momento de publicar el documento. " + vbCrLf, MsgBoxStyle.Exclamation, "Cuidado: " + Me.Text)
                Return False
            End If

            '**************************************

            Me.Show()
            Me.Refresh()
            WinAPI.SiempreEncima(Me.Handle.ToInt32)
            webClient = New WebClient()
            'Dim f As IO.FileStream = IO.File.OpenRead( locimagen)        
            Try
                Dim dir As New System.Uri(pagina)
                webClient.UploadFile(dir.AbsoluteUri, "POST", f.FullName)
                'webClient.UploadFileAsync(dir, "POST", locimagen)
            Catch ex As System.Net.WebException
                MsgBox("Error al subir Imagen !!!" + vbCrLf + ex.Message + vbCrLf, MsgBoxStyle.Critical)
                Return False
            End Try

            '**************************************

            resul = True
        End If


        Return resul
    End Function




    Private Sub webClient_UploadFileCompleted(sender As Object, e As UploadFileCompletedEventArgs) Handles webClient.UploadFileCompleted
        'Me.TextBox1.Text = System.Text.Encoding.ASCII.GetString(responseArray)
        'If Me.TextBox1.Text.Trim = "Se guardo el archivo" Then
        'MsgBox("La imagen se guardó correctamente" + vbCrLf + "Refresque el listado de imágenes!", MsgBoxStyle.Information)
        ProgressBar1.Value = 100
        'If locEsPedido = "P" Then
        '    Me.locform.cargarGrillaPedido()
        'ElseIf locEsPedido = "E" Then
        '    Me.locform.cargarGrilla()
        'ElseIf locEsPedido = "PRO" Then
        '    Me.locform.cargarGrillaProtesis()
        'End If


        'If locseCancelo = False Then            
        MsgBox("El Documento se cargo correctamente", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        locform.Enabled = True
        'locform.Close()
        Me.Close()



        'End If

    End Sub

    Private Sub webClient_UploadProgressChanged(sender As Object, e As UploadProgressChangedEventArgs) Handles webClient.UploadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        webClient.CancelAsync()
        locform.Enabled = True
        Me.locseCancelo = True
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class