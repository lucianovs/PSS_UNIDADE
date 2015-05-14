<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnidadeUsu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnidadeUsu))
        Me.cbUsuario = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCM = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstCC = New System.Windows.Forms.ListBox()
        Me.lblCC = New System.Windows.Forms.Label()
        Me.lstCP = New System.Windows.Forms.ListBox()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lstCF = New System.Windows.Forms.ListBox()
        Me.lblCF = New System.Windows.Forms.Label()
        Me.btnAtualizar_CM = New System.Windows.Forms.Button()
        Me.lblMensagem = New System.Windows.Forms.Label()
        Me.btnAtualizar_CC = New System.Windows.Forms.Button()
        Me.btnAtualizar_CP = New System.Windows.Forms.Button()
        Me.btnAtualizar_CF = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cbUsuario
        '
        Me.cbUsuario.FormattingEnabled = True
        Me.cbUsuario.Location = New System.Drawing.Point(60, 14)
        Me.cbUsuario.Margin = New System.Windows.Forms.Padding(2)
        Me.cbUsuario.Name = "cbUsuario"
        Me.cbUsuario.Size = New System.Drawing.Size(195, 21)
        Me.cbUsuario.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Usuário:"
        '
        'lstCM
        '
        Me.lstCM.FormattingEnabled = True
        Me.lstCM.Location = New System.Drawing.Point(9, 67)
        Me.lstCM.Margin = New System.Windows.Forms.Padding(2)
        Me.lstCM.Name = "lstCM"
        Me.lstCM.Size = New System.Drawing.Size(178, 225)
        Me.lstCM.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 51)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "CM"
        '
        'lstCC
        '
        Me.lstCC.FormattingEnabled = True
        Me.lstCC.Location = New System.Drawing.Point(190, 67)
        Me.lstCC.Margin = New System.Windows.Forms.Padding(2)
        Me.lstCC.Name = "lstCC"
        Me.lstCC.Size = New System.Drawing.Size(178, 225)
        Me.lstCC.TabIndex = 4
        '
        'lblCC
        '
        Me.lblCC.AutoSize = True
        Me.lblCC.Location = New System.Drawing.Point(188, 51)
        Me.lblCC.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCC.Name = "lblCC"
        Me.lblCC.Size = New System.Drawing.Size(21, 13)
        Me.lblCC.TabIndex = 5
        Me.lblCC.Text = "CC"
        '
        'lstCP
        '
        Me.lstCP.FormattingEnabled = True
        Me.lstCP.Location = New System.Drawing.Point(372, 67)
        Me.lstCP.Margin = New System.Windows.Forms.Padding(2)
        Me.lstCP.Name = "lstCP"
        Me.lstCP.Size = New System.Drawing.Size(178, 225)
        Me.lstCP.TabIndex = 6
        '
        'lblCP
        '
        Me.lblCP.AutoSize = True
        Me.lblCP.Location = New System.Drawing.Point(370, 51)
        Me.lblCP.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(21, 13)
        Me.lblCP.TabIndex = 7
        Me.lblCP.Text = "CP"
        '
        'lstCF
        '
        Me.lstCF.FormattingEnabled = True
        Me.lstCF.Location = New System.Drawing.Point(554, 67)
        Me.lstCF.Margin = New System.Windows.Forms.Padding(2)
        Me.lstCF.Name = "lstCF"
        Me.lstCF.Size = New System.Drawing.Size(178, 225)
        Me.lstCF.TabIndex = 8
        '
        'lblCF
        '
        Me.lblCF.AutoSize = True
        Me.lblCF.Location = New System.Drawing.Point(551, 51)
        Me.lblCF.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCF.Name = "lblCF"
        Me.lblCF.Size = New System.Drawing.Size(20, 13)
        Me.lblCF.TabIndex = 9
        Me.lblCF.Text = "CF"
        '
        'btnAtualizar_CM
        '
        Me.btnAtualizar_CM.Location = New System.Drawing.Point(40, 296)
        Me.btnAtualizar_CM.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAtualizar_CM.Name = "btnAtualizar_CM"
        Me.btnAtualizar_CM.Size = New System.Drawing.Size(110, 24)
        Me.btnAtualizar_CM.TabIndex = 10
        Me.btnAtualizar_CM.Text = "Atualizar CMs"
        Me.btnAtualizar_CM.UseVisualStyleBackColor = True
        '
        'lblMensagem
        '
        Me.lblMensagem.AutoSize = True
        Me.lblMensagem.Location = New System.Drawing.Point(40, 340)
        Me.lblMensagem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblMensagem.Name = "lblMensagem"
        Me.lblMensagem.Size = New System.Drawing.Size(16, 13)
        Me.lblMensagem.TabIndex = 11
        Me.lblMensagem.Text = "..."
        Me.lblMensagem.Visible = False
        '
        'btnAtualizar_CC
        '
        Me.btnAtualizar_CC.Location = New System.Drawing.Point(227, 296)
        Me.btnAtualizar_CC.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAtualizar_CC.Name = "btnAtualizar_CC"
        Me.btnAtualizar_CC.Size = New System.Drawing.Size(110, 24)
        Me.btnAtualizar_CC.TabIndex = 12
        Me.btnAtualizar_CC.Text = "Atualizar CCs"
        Me.btnAtualizar_CC.UseVisualStyleBackColor = True
        '
        'btnAtualizar_CP
        '
        Me.btnAtualizar_CP.Location = New System.Drawing.Point(406, 296)
        Me.btnAtualizar_CP.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAtualizar_CP.Name = "btnAtualizar_CP"
        Me.btnAtualizar_CP.Size = New System.Drawing.Size(110, 24)
        Me.btnAtualizar_CP.TabIndex = 13
        Me.btnAtualizar_CP.Text = "Atualizar CPs"
        Me.btnAtualizar_CP.UseVisualStyleBackColor = True
        '
        'btnAtualizar_CF
        '
        Me.btnAtualizar_CF.Location = New System.Drawing.Point(590, 296)
        Me.btnAtualizar_CF.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAtualizar_CF.Name = "btnAtualizar_CF"
        Me.btnAtualizar_CF.Size = New System.Drawing.Size(110, 24)
        Me.btnAtualizar_CF.TabIndex = 14
        Me.btnAtualizar_CF.Text = "Atualizar CFs"
        Me.btnAtualizar_CF.UseVisualStyleBackColor = True
        '
        'frmUnidadeUsu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 362)
        Me.Controls.Add(Me.btnAtualizar_CF)
        Me.Controls.Add(Me.btnAtualizar_CP)
        Me.Controls.Add(Me.btnAtualizar_CC)
        Me.Controls.Add(Me.lblMensagem)
        Me.Controls.Add(Me.btnAtualizar_CM)
        Me.Controls.Add(Me.lblCF)
        Me.Controls.Add(Me.lstCF)
        Me.Controls.Add(Me.lblCP)
        Me.Controls.Add(Me.lstCP)
        Me.Controls.Add(Me.lblCC)
        Me.Controls.Add(Me.lstCC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstCM)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbUsuario)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmUnidadeUsu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vínculo de Usuários  e Unidades"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbUsuario As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstCM As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstCC As System.Windows.Forms.ListBox
    Friend WithEvents lblCC As System.Windows.Forms.Label
    Friend WithEvents lstCP As System.Windows.Forms.ListBox
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lstCF As System.Windows.Forms.ListBox
    Friend WithEvents lblCF As System.Windows.Forms.Label
    Friend WithEvents btnAtualizar_CM As System.Windows.Forms.Button
    Friend WithEvents lblMensagem As System.Windows.Forms.Label
    Friend WithEvents btnAtualizar_CC As System.Windows.Forms.Button
    Friend WithEvents btnAtualizar_CP As System.Windows.Forms.Button
    Friend WithEvents btnAtualizar_CF As System.Windows.Forms.Button
End Class
