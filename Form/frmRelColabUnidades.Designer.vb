﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelColabUnidades
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRelColabUnidades))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtConselho = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnLocUnidade = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(510, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Escolha um C.Particular para Imprimir suas conferências e seus Colaboradores:"
        '
        'txtConselho
        '
        Me.txtConselho.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtConselho.Enabled = False
        Me.txtConselho.Location = New System.Drawing.Point(15, 46)
        Me.txtConselho.Name = "txtConselho"
        Me.txtConselho.Size = New System.Drawing.Size(491, 22)
        Me.txtConselho.TabIndex = 4
        Me.txtConselho.Text = "Clique ao lado para selecionar um Conselho -->"
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
        'btnImprimir
        '
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.Location = New System.Drawing.Point(139, 111)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(82, 69)
        Me.btnImprimir.TabIndex = 2
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnLocUnidade
        '
        Me.btnLocUnidade.BackgroundImage = Global.UNIDADES.My.Resources.Resources.AllDay_ru_Search
        Me.btnLocUnidade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLocUnidade.Location = New System.Drawing.Point(509, 45)
        Me.btnLocUnidade.Name = "btnLocUnidade"
        Me.btnLocUnidade.Size = New System.Drawing.Size(23, 24)
        Me.btnLocUnidade.TabIndex = 21
        Me.btnLocUnidade.UseVisualStyleBackColor = True
        '
        'frmRelColabUnidades
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 220)
        Me.Controls.Add(Me.btnLocUnidade)
        Me.Controls.Add(Me.txtConselho)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRelColabUnidades"
        Me.Text = "frmRelUnidades"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents txtConselho As System.Windows.Forms.TextBox
    Friend WithEvents btnLocUnidade As System.Windows.Forms.Button
End Class
