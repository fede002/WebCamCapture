Imports System.Drawing
Imports WIA
Imports System.IO
Imports System.Net

Public Class Form1

    Dim wiaFormatJPEG As String = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"
    Dim wiaFormatGIF As String = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}"
    Dim pagina As String
    Public locform As Form
    Public imagen As String = ""
    Public archivoaSubir As String = ""
    Public archivoaBajar As String = ""
    Dim archivoFinal As String = ""
    Public bajaCalidad As Boolean = False
    Dim tmpEsImagen As Boolean = False
    Dim _escaenPixiles As Double = 37.795275591
    Dim WithEvents webClient As WebClient
    Dim locseCancelo As Boolean = False
    Public locEsPedido As String = ""

    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        locform.Enabled = True
    End Sub

    Public Function procesarUpload(ByVal esimagen) As Boolean
        Dim resul As Boolean = False
        pagina = System.Configuration.ConfigurationManager.AppSettings("pagina").ToString()
        If esimagen Then
            Dim scan As Boolean = scanner()
            If scan Then

                Dim f As New IO.FileInfo(System.IO.Path.GetTempPath() + imagen)
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
                    tmpEsImagen = True
                    SuboImagen()
                    resul = True
                End If
            Else
                MsgBox("Error al momento de Tomar la imagen" + vbCrLf + "Verifique que el scanner funcione correctamente !!!", MsgBoxStyle.Critical)
                resul = False
            End If
        Else

            Dim f As New IO.FileInfo(System.IO.Path.GetTempPath() + imagen)
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
                tmpEsImagen = False
                SuboImagen()
                resul = True
            End If
        End If
        Return resul
    End Function

    Private Sub SuboImagen()
        Me.Show()
        Me.Refresh()
        WinAPI.SiempreEncima(Me.Handle.ToInt32)
        webClient = New WebClient()
        MsgBox("Hace un post a página: " + pagina + vbCrLf + "con la imagen: " + System.IO.Path.GetTempPath() + imagen)

        'Dim f As IO.FileStream = IO.File.OpenRead(System.IO.Path.GetTempPath() + imagen)        
        Try
            Dim dir As New System.Uri(pagina)
            'fede: si no es async si da la respuesta
            'responseArray = webClient.UploadFile(dir.AbsoluteUri, "POST", f.Name)
            'fede: async no da respues si esta ok o no
            webClient.UploadFileAsync(dir, "POST", System.IO.Path.GetTempPath() + imagen)
        Catch ex As System.Net.WebException
            MsgBox("Error al subir Imagen !!!" + vbCrLf + ex.Message + vbCrLf, MsgBoxStyle.Critical)
            Exit Sub
        End Try


    End Sub

    Public Sub procesarDownload()
        Me.Show()
        WinAPI.SiempreEncima(Me.Handle.ToInt32)
        webClient = New WebClient()
        'Dim f As IO.FileStream = IO.File.OpenRead(System.IO.Path.GetTempPath() + imagen)        
        Try
            Dim dir As New System.Uri(archivoaBajar)

            Dim inicioEXT As Integer = InStrRev(archivoaBajar, ".")
            Dim largoEXT As Integer = Len(archivoaBajar) - inicioEXT + 1
            Dim TomarExtension As String = Mid(archivoaBajar, inicioEXT, largoEXT).Replace(".", "")

            'responseArray = myWebClient.UploadFile(dir.AbsoluteUri, "POST", f.Name)
            Dim savef As New SaveFileDialog()
            savef.Filter = TomarExtension + "|*." + TomarExtension
            savef.Title = "Guardar documento"
            savef.ShowDialog()
            If savef.FileName <> "" Then
                webClient.DownloadFileAsync(dir, savef.FileName)
                archivoFinal = savef.FileName
            End If


        Catch ex As System.Net.WebException
            MsgBox("Error al descargar el archivo !!!" + vbCrLf + ex.Message + vbCrLf, MsgBoxStyle.Critical)
            Exit Sub
        End Try


    End Sub

    Private Function scanner() As Boolean
        'Return True
        Dim resul As Boolean = False
        Do
            Try
                Dim wiaDiag As CommonDialogClass = New WIA.CommonDialogClass()

                Dim wiaImage As WIA.ImageFile

                Application.DoEvents()
                Me.Show()

                'MsgBox("Antes de Tomar la imagen")
                'Dim obj As WIA.Device = wiaDiag.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, True, False)
                'wiaImage = wiaDiag.ShowAcquisitionWizard(obj)
                wiaImage = wiaDiag.ShowAcquireImage( _
                    WiaDeviceType.ScannerDeviceType, _
                    WiaImageIntent.GrayscaleIntent, _
                    WiaImageBias.MinimizeSize, _
                    wiaFormatJPEG, False, True, False)

                Dim vector As WIA.Vector = wiaImage.FileData

                Dim i As System.Drawing.Bitmap = Image.FromStream(New MemoryStream(DirectCast(vector.BinaryData(), Byte())))
                'MsgBox("Despues de Tomar la imagen")
                If IO.File.Exists(System.IO.Path.GetTempPath + imagen) Then
                    'MsgBox("Antes de Borrar el archivo")
                    IO.File.Delete(System.IO.Path.GetTempPath + imagen)
                End If
                'i.Save(System.IO.Path.GetTempPath + imagen + "tmp")
                'MsgBox(System.IO.Path.GetTempPath + imagen)
                'If bajaCalidad Then
                '    i = PasarImagenaCM(21, 29.9, i)
                Dim compre As Integer = System.Configuration.ConfigurationManager.AppSettings("compresion").ToString()
                Try
                    ImageManipulation.SaveJPGWithCompressionSetting(i, Application.StartupPath & "\" & "temp_comp.jpg", compre)
                Catch exc As Exception
                    MessageBox.Show(exc, " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'End If
                Dim img As Image = Image.FromFile(Application.StartupPath & "\" & "temp_comp.jpg")
                'MsgBox("Toma el archivo comprimido dentro de la capeta del app." + vbCrLf + Application.StartupPath & "\" & "temp_comp.jpg" + vbCrLf + _
                '      "Y lo pasa al temp:" + vbCrLf + System.IO.Path.GetTempPath + imagen)
                img.Save(System.IO.Path.GetTempPath + imagen)
                'i.Save(System.IO.Path.GetTempPath + imagen)
                resul = True
                Exit Do
            Catch ex As Exception
                If ex.Message = "Excepción de HRESULT: 0x80210003" Then
                    MsgBox("Verifique que la hoja del scanner este correctamente cargada!!!", MsgBoxStyle.Critical)
                    resul = False
                ElseIf ex.Message = "Referencia a objeto no establecida como instancia de un objeto." Then
                    MsgBox("Se canceló la operación o no se pudo tomar correctamente la imagen de la escaner" + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Error General")
                    Me.locform.Enabled = True
                    Exit Do
                Else
                    MsgBox("Verifique que el scanner este correctamente instalado" + vbCrLf + ex.Message, MsgBoxStyle.Critical, "Error General")
                    Me.locform.Enabled = True
                    Exit Do
                End If
            End Try
        Loop While resul = False

        Return resul
    End Function

    Private Function PasarImagenaCM(ByVal ancho As Double, ByVal alto As Double, ByVal imagen As System.Drawing.Bitmap) As System.Drawing.Bitmap

        Dim NeedsHorizontalCrop As Boolean = True
        Dim NeedsVerticalCrop As Boolean = False

        'Determina si la imagen es Landscape o Portrait
        If imagen.Height > imagen.Width Then
            NeedsHorizontalCrop = False
            NeedsVerticalCrop = True
        End If

        Dim tmpancho As Double = ancho * _escaenPixiles
        Dim tmpalto As Double = alto * _escaenPixiles

        'Determina si la imagen excede el ancho del PictureBox
        If imagen.Width < tmpancho Then
            NeedsHorizontalCrop = False
            If imagen.Height > tmpalto Then
                NeedsVerticalCrop = True
            End If
        End If
        '1 centimeter = 37.795275591 pixel (X)
        'Calcula el Factor de Ajuste
        Dim scale_factor As Single = 1
        If imagen.Width > 0 Then
            If NeedsHorizontalCrop = True Then
                ' Obtiene el Factor de Ajuste
                scale_factor = tmpancho / imagen.Width
            End If
        End If

        If imagen.Height > 0 Then
            If NeedsVerticalCrop = True Then
                ' Obtiene el Factor de Ajuste
                scale_factor = tmpalto / imagen.Height
            End If
        End If

        ' Generar un bitmap tmp para el resultado. Ajuste Proporcional
        Dim DestTmpImage As New Bitmap( _
            CInt(imagen.Width * scale_factor), _
            CInt(imagen.Height * scale_factor))

        ' Generar un objeto Gráfico para el bitmap tmp resultante
        Dim gr_desttmp As Graphics = Graphics.FromImage(DestTmpImage)

        ' Copiar la imagen origen al bitmap tmp destino
        gr_desttmp.DrawImage(imagen, 0, 0, _
            DestTmpImage.Width, _
            DestTmpImage.Height)

        DestTmpImage.Save(Application.StartupPath & "\" & "temp_1.jpg")

        Dim DPIX, DPIY, PPCMX, PPCMY As Double

        DPIX = gr_desttmp.DpiX
        DPIY = gr_desttmp.DpiY
        PPCMX = (gr_desttmp.DpiX / 2.54)
        PPCMY = (gr_desttmp.DpiY / 2.54)

        'Comprime y Guarda un Archivo Temporal para calcular su peso en Kb
        Try
            ImageManipulation.SaveJPGWithCompressionSetting(DestTmpImage, Application.StartupPath & "\" & "temp.jpg", 100)
        Catch exc As Exception
            MessageBox.Show(exc, " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage

        'La lectura del nuevo archivo no se puede hacer en forma directa y repetitiva
        'ya que está bloqueado por GDI+ la 1era vez que se lo utiliza,
        'por lo tanto resulta necesario resolver en varios pasos. 
        'Al efectuar el Dispose() se libera el recurso

        Dim DestImage As Bitmap
        DestImage = New Bitmap(Application.StartupPath & "\" & "temp.jpg")

        ' Generar un bitmap para el resultado
        Dim DestToImage As New Bitmap(DestImage.Width, DestImage.Height)

        ' Generar un objeto Grafico para el bitmap resultante
        Dim gr_dest As Graphics = Graphics.FromImage(DestToImage)

        ' Copiar la imagen origen al bitmap destino
        gr_dest.DrawImage(DestImage, 0, 0, _
            DestToImage.Width, _
            DestToImage.Height)

        'Muestra imagen comprimida
        'PictureBox2.Image = CType(DestToImage, Image)

        'Liberar recursos
        gr_dest.Dispose()
        DestImage.Dispose()

        Dim theFile As New FileInfo(Application.StartupPath & "\" & "temp.jpg")

        Dim DetalleDestino As String = ""
        DetalleDestino = ""
        DetalleDestino = "Ancho=" & DestTmpImage.Width & " px"
        DetalleDestino = DetalleDestino & " - Alto=" & DestTmpImage.Height & " px"
        DetalleDestino = DetalleDestino & vbCrLf & "(" & theFile.Length & " bytes)"
        DetalleDestino = DetalleDestino & vbCrLf & "(" & Math.Round(theFile.Length / 1024, 5) & " Kb)"
        DetalleDestino = DetalleDestino & vbCrLf & "(" & Math.Round(DestToImage.Width / PPCMX, 2) & " x " & Math.Round(DestToImage.Height / PPCMY, 2) & " cm)"
        'Label4.Text = DetalleDestino

        gr_desttmp.Dispose()

        'BtnSave.Enabled = True
        Return DestTmpImage
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
        locform.Enabled = True
        locform.Close()
        Me.Close()
        If tmpEsImagen Then
            Shell("rundll32.exe C:\WINDOWS\system32\shimgvw.dll,ImageView_Fullscreen " + System.IO.Path.GetTempPath + imagen, vbMaximizedFocus)
        Else
            MsgBox("El Documento se cargo correctamente", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If
        'End If

    End Sub

    Private Sub webClient_UploadProgressChanged(sender As Object, e As UploadProgressChangedEventArgs) Handles webClient.UploadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub webClient_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles webClient.DownloadFileCompleted

        ProgressBar1.Value = 100
        'If locEsPedido = "P" Then
        '    Me.locform.cargarGrillaPedido()
        'ElseIf locEsPedido = "E" Then
        '    Me.locform.cargarGrilla()
        'ElseIf locEsPedido = "PRO" Then
        '    Me.locform.cargarGrillaProtesis()
        'End If

        ' Saves the Image via a FileStream created by the OpenFile method.
        'KpImageViewer1.OpenButton = False
        'Dim locimagen As Image = Image.FromStream(stfile)
        'KpImageViewer1.Image = locimagen
        'stfile.Close()
        'KpImageViewer1.Zoom = 100
        locform.Enabled = True
        Me.Close()
        If IO.File.Exists(archivoFinal) Then
            MsgBox("El archivo se guardo correctamente !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        Else
            MsgBox("Error al descargar el archivo !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End If

    End Sub

    Private Sub webClient_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles webClient.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        'Me.lbMsg.Text = "Cargando ...." + e.ProgressPercentage.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        webClient.CancelAsync()
        locform.Enabled = True
        Me.locseCancelo = True
        Me.Close()
    End Sub

End Class
