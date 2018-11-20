<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFotoDX
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFotoDX))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cbResolucion = New System.Windows.Forms.ComboBox()
        Me.cbDriver = New System.Windows.Forms.ComboBox()
        Me.txlog = New System.Windows.Forms.TextBox()
        Me.imgCapture = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.bntStop = New System.Windows.Forms.Button()
        Me.bntStart = New System.Windows.Forms.Button()
        Me.VideoSourcePlayer1 = New AForge.Controls.VideoSourcePlayer()
        Me.picFotofinal = New System.Windows.Forms.PictureBox()
        Me.bntCapture = New System.Windows.Forms.Button()
        Me.bntSave = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.imgCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFotofinal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cbResolucion)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cbDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txlog)
        Me.SplitContainer1.Panel1.Controls.Add(Me.imgCapture)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.bntStop)
        Me.SplitContainer1.Panel1.Controls.Add(Me.bntStart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.VideoSourcePlayer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.picFotofinal)
        Me.SplitContainer1.Panel2.Controls.Add(Me.bntCapture)
        Me.SplitContainer1.Panel2.Controls.Add(Me.bntSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(909, 391)
        Me.SplitContainer1.SplitterDistance = 566
        Me.SplitContainer1.SplitterIncrement = 3
        Me.SplitContainer1.TabIndex = 9
        '
        'cbResolucion
        '
        Me.cbResolucion.FormattingEnabled = True
        Me.cbResolucion.Location = New System.Drawing.Point(412, 224)
        Me.cbResolucion.Name = "cbResolucion"
        Me.cbResolucion.Size = New System.Drawing.Size(121, 21)
        Me.cbResolucion.TabIndex = 19
        '
        'cbDriver
        '
        Me.cbDriver.FormattingEnabled = True
        Me.cbDriver.Location = New System.Drawing.Point(412, 197)
        Me.cbDriver.Name = "cbDriver"
        Me.cbDriver.Size = New System.Drawing.Size(121, 21)
        Me.cbDriver.TabIndex = 17
        '
        'txlog
        '
        Me.txlog.Location = New System.Drawing.Point(14, 310)
        Me.txlog.Multiline = True
        Me.txlog.Name = "txlog"
        Me.txlog.Size = New System.Drawing.Size(378, 28)
        Me.txlog.TabIndex = 15
        '
        'imgCapture
        '
        Me.imgCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgCapture.Image = CType(resources.GetObject("imgCapture.Image"), System.Drawing.Image)
        Me.imgCapture.Location = New System.Drawing.Point(14, 12)
        Me.imgCapture.Name = "imgCapture"
        Me.imgCapture.Size = New System.Drawing.Size(378, 290)
        Me.imgCapture.TabIndex = 11
        Me.imgCapture.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(14, 199)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(378, 5)
        Me.Panel3.TabIndex = 15
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(294, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(5, 290)
        Me.Panel2.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Location = New System.Drawing.Point(113, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(5, 290)
        Me.Panel1.TabIndex = 13
        '
        'bntStop
        '
        Me.bntStop.Location = New System.Drawing.Point(412, 75)
        Me.bntStop.Name = "bntStop"
        Me.bntStop.Size = New System.Drawing.Size(70, 61)
        Me.bntStop.TabIndex = 9
        Me.bntStop.Text = "Parar Vdeo"
        Me.bntStop.UseVisualStyleBackColor = True
        '
        'bntStart
        '
        Me.bntStart.Location = New System.Drawing.Point(412, 12)
        Me.bntStart.Name = "bntStart"
        Me.bntStart.Size = New System.Drawing.Size(70, 61)
        Me.bntStart.TabIndex = 8
        Me.bntStart.Text = "Ver Video"
        Me.bntStart.UseVisualStyleBackColor = True
        '
        'VideoSourcePlayer1
        '
        Me.VideoSourcePlayer1.Location = New System.Drawing.Point(14, 12)
        Me.VideoSourcePlayer1.Name = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.Size = New System.Drawing.Size(378, 290)
        Me.VideoSourcePlayer1.TabIndex = 16
        Me.VideoSourcePlayer1.Text = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.VideoSource = Nothing
        '
        'picFotofinal
        '
        Me.picFotofinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFotofinal.Location = New System.Drawing.Point(43, 12)
        Me.picFotofinal.Name = "picFotofinal"
        Me.picFotofinal.Size = New System.Drawing.Size(250, 251)
        Me.picFotofinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picFotofinal.TabIndex = 12
        Me.picFotofinal.TabStop = False
        '
        'bntCapture
        '
        Me.bntCapture.Location = New System.Drawing.Point(179, 281)
        Me.bntCapture.Name = "bntCapture"
        Me.bntCapture.Size = New System.Drawing.Size(114, 57)
        Me.bntCapture.TabIndex = 10
        Me.bntCapture.Text = "Capture image"
        Me.bntCapture.UseVisualStyleBackColor = True
        '
        'bntSave
        '
        Me.bntSave.Location = New System.Drawing.Point(44, 281)
        Me.bntSave.Name = "bntSave"
        Me.bntSave.Size = New System.Drawing.Size(117, 57)
        Me.bntSave.TabIndex = 9
        Me.bntSave.Text = "Confirmar"
        Me.bntSave.UseVisualStyleBackColor = True
        '
        'frmFotoDX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 391)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmFotoDX"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sistema Fotografia UOM Sindical"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.imgCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFotofinal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents bntStop As Button
    Friend WithEvents bntStart As Button
    Friend WithEvents imgCapture As PictureBox
    Friend WithEvents bntCapture As Button
    Friend WithEvents bntSave As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents picFotofinal As PictureBox
    Friend WithEvents txlog As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents VideoSourcePlayer1 As AForge.Controls.VideoSourcePlayer
    Friend WithEvents cbResolucion As ComboBox
    Friend WithEvents cbDriver As ComboBox
End Class
