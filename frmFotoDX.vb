Imports System.ComponentModel
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.Drawing


Public Class frmFotoDX


    Private videoDevices As FilterInfoCollection
    Private videoDevice As VideoCaptureDevice
    Private videoCapabilities() As VideoCapabilities
    Private snapshotCapabilities() As VideoCapabilities
    Public locForm As Form
    Public imagen As String


    Private Sub mainWinForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargarDriver()
        bntStart_Click(sender, e)
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        bntStop_Click(sender, e)
    End Sub


    Private Sub cargarDriver()

        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        If videoDevices.Count > 0 Then
            For Each device In videoDevices
                Me.cbDriver.Items.Add(device.name)
            Next
        Else
            MsgBox("No se identificó ninguna Webcam")
        End If

        cbDriver.SelectedIndex = 0

    End Sub

    Private Sub bntStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntStart.Click
        desconectar()

        If Not videoDevice Is Nothing Then

            If Not videoCapabilities Is Nothing AndAlso videoCapabilities.Length <> 0 Then
                videoDevice.VideoResolution = videoCapabilities(cbResolucion.SelectedIndex)
            End If

            VideoSourcePlayer1.VideoSource = videoDevice
            VideoSourcePlayer1.Start()

        End If
        imgCapture.Visible = False
        VideoSourcePlayer1.Visible = True
    End Sub

    Private Sub bntStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntStop.Click

        VideoSourcePlayer1.Visible = False
        imgCapture.Visible = True

        desconectar()


    End Sub

    Private Sub desconectar()
        If Not VideoSourcePlayer1.VideoSource Is Nothing Then

            VideoSourcePlayer1.SignalToStop()
            VideoSourcePlayer1.WaitForStop()
            VideoSourcePlayer1.VideoSource = Nothing

        End If
    End Sub


    Private Sub EnumeratedSupportedFrameSizes(ByVal videoDevice As VideoCaptureDevice)
        Me.Cursor = Cursors.WaitCursor
        cbResolucion.Items.Clear()

        Try
            videoCapabilities = videoDevice.VideoCapabilities

            For Each capability In videoCapabilities
                Dim ancho As String = capability.FrameSize.Width.ToString
                Dim alto As String = capability.FrameSize.Height.ToString
                cbResolucion.Items.Add(" " + ancho + " x " + alto + ")")
            Next
            cbResolucion.SelectedIndex = 0

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub



    Private Sub bntCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntCapture.Click
        Dim imagentmp As Bitmap
        imagentmp = VideoSourcePlayer1.GetCurrentVideoFrame()
        imagentmp = ResizeImage(imagentmp, picFotofinal.Width * 1.5, picFotofinal.Height * 1.5, True)


        imgCapture.Image = imagentmp
        picFotofinal.Image = Nothing
        VideoSourcePlayer1.Visible = False
        imgCapture.Visible = True
        capturarFotoCarnet(60, 0)
    End Sub

    Private Sub bntSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSave.Click
        If picFotofinal.Image Is Nothing Then
            MsgBox("No capturo ninguna imagen")
            Exit Sub
        End If


        'Helper.SaveImageCapture(picFotofinal.Image)
        'guardo la imagen
        Dim nombreDelaImagenTemporal = System.IO.Path.GetTempPath() + imagen
        'MsgBox(nombreDelaImagenTemporal)
        Dim fstream As New IO.FileStream(nombreDelaImagenTemporal, IO.FileMode.Create)
        picFotofinal.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        fstream.Close()
        ' SUBO LA IMAGEN CON EL FORM DEL WEBCLIENT
        Dim frm As New Uploading
        frm.locform = Me
        frm.locimagen = nombreDelaImagenTemporal
        frm.procesarUpload()
        Me.Close()

    End Sub

    Private Sub imgCapture_MouseDown(sender As Object, e As MouseEventArgs) Handles imgCapture.MouseDown

        capturarFotoCarnet(e.X, e.Y)

    End Sub

    Private Sub capturarFotoCarnet(ByVal x As Integer, ByVal y As Integer)


        If imgCapture.Image Is Nothing Then
            Exit Sub
        End If

        Try

            imgCapture.Refresh()

            'imgCapture.Image = imgbackupcap.Image
            Dim bmp As Bitmap = New Bitmap(imgCapture.Image)
            'ScaleImage(picFotofinal, bmp)
            Me.txlog.Text = "Val x:  " + x.ToString + "   Val y: " + y.ToString
            Dim rec As Rectangle = New Rectangle() With {.X = x, .Y = y, .Width = picFotofinal.Width, .Height = picFotofinal.Height}
            Dim pen As New Pen(Color.Red)
            imgCapture.CreateGraphics.DrawRectangle(pen, rec)

            Dim newImg = bmp.Clone(rec, bmp.PixelFormat)
            'picFotofinal.Image = newImg




            picFotofinal.Image = newImg

        Catch ex As Exception
            Me.txlog.Text += vbCrLf + "Error: " + ex.Message + "    , num" + ex.Source

        End Try

    End Sub

    Private Sub cbDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDriver.SelectedIndexChanged
        If videoDevices.Count > 0 Then
            videoDevice = New VideoCaptureDevice(videoDevices(cbDriver.SelectedIndex).MonikerString)
            EnumeratedSupportedFrameSizes(videoDevice)
        End If
    End Sub

    Public Shared Function ResizeImage(ByVal image As Image,
                      ByVal sizeWidth As Int32, ByVal sizeHeight As Int32, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(sizeWidth) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(sizeHeight) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth,
                    percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = sizeWidth
            newHeight = sizeHeight
        End If
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

End Class
