
Public Class Login
    Public parametros As String
    Dim usuanterior As String = ""




    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim miMutex As System.Threading.Mutex
        Dim nuevaInstancia As Boolean
        miMutex = New System.Threading.Mutex(True, "EscanerAmb", nuevaInstancia)
        If nuevaInstancia = False Then
            MsgBox("La aplicacion ya se esta ejecutando !!!", MsgBoxStyle.Critical)
            System.Threading.Thread.Sleep(1000)
            Me.Close()
            Me.Dispose()
        Else
            'CompruebaVersion()
            EscanearDoc()
            Me.lbMensaje.Text = ""
            Me.StartPosition = FormStartPosition.CenterScreen
            'Me.Close()
        End If


    End Sub
    ''' <summary>
    ''' Verifica la version de la app por medio de un servicio web, no esta implementado para la foto
    ''' </summary>
    'Private Sub CompruebaVersion()
    '    Dim version As Integer = System.Configuration.ConfigurationManager.AppSettings("version").ToString()
    '    Dim svr As New ServiceReference1.svrAfiliadoSoapClient
    '    Try

    '        If Not svr.TestDelServicio Then
    '            MsgBox("El servicio no se encuentra disponible actualmente." + vbCrLf + "Contactar al servicio de soporte del Sistema", MsgBoxStyle.Critical, "Error: Fuera de Servicio")
    '            Me.Cursor = Cursors.Default
    '            Me.Close()
    '        End If

    '        Try

    '            If svr.VerificaVersionEscaner(version) = False Then
    '                'MsgBox("Debe descargar la ultima version del sistema para Escanear Documentos!." , MsgBoxStyle.Exclamation, "Alerta: Nueva version disponible")
    '                Dim pagina As String = System.Configuration.ConfigurationManager.AppSettings("pagina").ToString()
    '                'System.Diagnostics.Process.Start(pagina.ToUpper.Replace("SUBIRARCHIVO.ASPX", ""))
    '                'Dim down As String = pagina.ToUpper.Replace("SUBIRARCHIVO.ASPX", "download.xml")
    '                Dim down As String = pagina.ToUpper.Replace("SUBIRARCHIVO.ASPX", "EscanerAmbulatorio.exe")
    '                'AutoUpdater.LetUserSelectRemindLater = False
    '                'AutoUpdaterDotNET.
    '                Dim frmdown As New frmDownload(down)
    '                frmdown.ShowDialog()
    '                frmdown.Close()
    '                Me.Cursor = Cursors.Default
    '                'Me.Close()
    '            End If

    '        Catch ex As Exception
    '            MsgBox("El servicio no se encuentra disponible actualmente." + vbCrLf + "Contactar al servicio de soporte de Sistema" + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Error: Fuera de Servicio")
    '            Me.Cursor = Cursors.Default
    '        End Try

    '    Catch ex As Exception
    '        MsgBox("El servicio no se encuentra disponible actualmente." + vbCrLf + "Contactar al servicio de soporte de Sistema" + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Error: Fuera de Servicio")
    '        Me.Cursor = Cursors.Default
    '    End Try


    'End Sub

    Private Sub EscanearDoc()

        'Me.coBuscaAfil.Focus()
        Dim paramarr() As String
        If parametros <> "" Then
            parametros = parametros.Replace("escaner:", "")
            paramarr = parametros.Split(",")
        Else

            Dim sitioweb As String = System.Configuration.ConfigurationManager.AppSettings("sitioWeb").ToString()

            MsgBox("La aplicación se debe correr desde la pagina web:" + vbCrLf +
                   sitioweb)
            Try
                Process.Start(sitioweb)
            Catch ex As Exception
                'MsgBox("Debe establecer un explorador de internet por defecto. 
                '   Para que el sistema abra la siguiente página.  http://200.123.110.196/Escaner")

            End Try

            Me.Close()
            Me.Dispose()
            Exit Sub
        End If

        Dim imagen As String = ""
        imagen = paramarr(0)
        For i = 1 To paramarr.Length - 1
            imagen += "_" + paramarr(i)
        Next
        imagen += ".gif"

        If paramarr(2) = "FOTO" Then
            Dim f As New frmFotoDX
            f.imagen = imagen
            Me.Hide()
            f.ShowDialog()
            Application.DoEvents()
            f.Show()
            Me.Close()
        ElseIf paramarr(2) = "ESCAN" Then
            Dim f As New Form1
            f.imagen = imagen
            Me.Enabled = False
            f.locform = Me
            f.imagen = imagen
            f.bajaCalidad = False

            'Me.lbMensaje.Text = "CARGANDO INFO"
            Application.DoEvents()
            'f.Show()
            If f.procesarUpload(True) = False Then
                Me.Close()
                Me.Dispose()
                Exit Sub
            End If


        End If

    End Sub

End Class
