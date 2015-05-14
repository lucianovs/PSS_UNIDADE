<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelUnidades
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRelUnidades))
        Me.cbUnidades = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cbUnidades
        '
        Me.cbUnidades.FormattingEnabled = True
        Me.cbUnidades.Location = New System.Drawing.Point(15, 45)
        Me.cbUnidades.Name = "cbUnidades"
        Me.cbUnidades.Size = New System.Drawing.Size(520, 24)
        Me.cbUnidades.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(295, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Escolha a Unidade para Imprimir sua relação:"
        '
        'btnImprimir
        '
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.Location = New System.Drawing.Point(139, 111)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(82, 69)
        Me.btnImprimir.TabIndex = 2
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(242, 111)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(82, 69)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'frmRelUnidades
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 220)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbUnidades)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRelUnidades"
        Me.Text = "frmRelUnidades"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbUnidades As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class
