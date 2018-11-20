<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer


    Sub New(ByVal param As String)
        parametros = param
        InitializeComponent()
    End Sub

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbMensaje = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbMensaje
        '
        Me.lbMensaje.AutoSize = True
        Me.lbMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMensaje.ForeColor = System.Drawing.Color.Blue
        Me.lbMensaje.Location = New System.Drawing.Point(62, 55)
        Me.lbMensaje.Name = "lbMensaje"
        Me.lbMensaje.Size = New System.Drawing.Size(301, 55)
        Me.lbMensaje.TabIndex = 14
        Me.lbMensaje.Text = "Cargando ..."
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 143)
        Me.Controls.Add(Me.lbMensaje)
        Me.Name = "Login"
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbMensaje As Label
End Class
