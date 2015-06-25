<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReorgUnidade
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReorgUnidade))
        Me.chkCM = New System.Windows.Forms.CheckBox()
        Me.chkCC = New System.Windows.Forms.CheckBox()
        Me.chkCP = New System.Windows.Forms.CheckBox()
        Me.chkCF = New System.Windows.Forms.CheckBox()
        Me.lblStatusCM = New System.Windows.Forms.Label()
        Me.lblStatusCC = New System.Windows.Forms.Label()
        Me.lblStatusCP = New System.Windows.Forms.Label()
        Me.lblStatusCF = New System.Windows.Forms.Label()
        Me.btnProcessar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.pbarOrganizacao = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'chkCM
        '
        Me.chkCM.AutoSize = True
        Me.chkCM.Location = New System.Drawing.Point(12, 34)
        Me.chkCM.Name = "chkCM"
        Me.chkCM.Size = New System.Drawing.Size(228, 21)
        Me.chkCM.TabIndex = 0
        Me.chkCM.Text = "Reorganizar Estrutura dos CMs"
        Me.chkCM.UseVisualStyleBackColor = True
        '
        'chkCC
        '
        Me.chkCC.AutoSize = True
        Me.chkCC.Location = New System.Drawing.Point(12, 72)
        Me.chkCC.Name = "chkCC"
        Me.chkCC.Size = New System.Drawing.Size(226, 21)
        Me.chkCC.TabIndex = 1
        Me.chkCC.Text = "Reorganizar Estrutura dos CCs"
        Me.chkCC.UseVisualStyleBackColor = True
        '
        'chkCP
        '
        Me.chkCP.AutoSize = True
        Me.chkCP.Location = New System.Drawing.Point(12, 110)
        Me.chkCP.Name = "chkCP"
        Me.chkCP.Size = New System.Drawing.Size(226, 21)
        Me.chkCP.TabIndex = 2
        Me.chkCP.Text = "Reorganizar Estrutura dos CPs"
        Me.chkCP.UseVisualStyleBackColor = True
        '
        'chkCF
        '
        Me.chkCF.AutoSize = True
        Me.chkCF.Location = New System.Drawing.Point(12, 151)
        Me.chkCF.Name = "chkCF"
        Me.chkCF.Size = New System.Drawing.Size(284, 21)
        Me.chkCF.TabIndex = 3
        Me.chkCF.Text = "Reorganizar Estrutura das Conferências"
        Me.chkCF.UseVisualStyleBackColor = True
        '
        'lblStatusCM
        '
        Me.lblStatusCM.AutoSize = True
        Me.lblStatusCM.Location = New System.Drawing.Point(312, 34)
        Me.lblStatusCM.Name = "lblStatusCM"
        Me.lblStatusCM.Size = New System.Drawing.Size(48, 17)
        Me.lblStatusCM.TabIndex = 4
        Me.lblStatusCM.Text = "Status"
        '
        'lblStatusCC
        '
        Me.lblStatusCC.AutoSize = True
        Me.lblStatusCC.Location = New System.Drawing.Point(312, 75)
        Me.lblStatusCC.Name = "lblStatusCC"
        Me.lblStatusCC.Size = New System.Drawing.Size(48, 17)
        Me.lblStatusCC.TabIndex = 5
        Me.lblStatusCC.Text = "Status"
        '
        'lblStatusCP
        '
        Me.lblStatusCP.AutoSize = True
        Me.lblStatusCP.Location = New System.Drawing.Point(312, 110)
        Me.lblStatusCP.Name = "lblStatusCP"
        Me.lblStatusCP.Size = New System.Drawing.Size(48, 17)
        Me.lblStatusCP.TabIndex = 6
        Me.lblStatusCP.Text = "Status"
        '
        'lblStatusCF
        '
        Me.lblStatusCF.AutoSize = True
        Me.lblStatusCF.Location = New System.Drawing.Point(312, 152)
        Me.lblStatusCF.Name = "lblStatusCF"
        Me.lblStatusCF.Size = New System.Drawing.Size(48, 17)
        Me.lblStatusCF.TabIndex = 7
        Me.lblStatusCF.Text = "Status"
        '
        'btnProcessar
        '
        Me.btnProcessar.Location = New System.Drawing.Point(29, 210)
        Me.btnProcessar.Name = "btnProcessar"
        Me.btnProcessar.Size = New System.Drawing.Size(90, 41)
        Me.btnProcessar.TabIndex = 8
        Me.btnProcessar.Text = "Processar"
        Me.btnProcessar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(190, 210)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(90, 41)
        Me.btnCancelar.TabIndex = 9
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'pbarOrganizacao
        '
        Me.pbarOrganizacao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbarOrganizacao.Location = New System.Drawing.Point(12, 264)
        Me.pbarOrganizacao.Name = "pbarOrganizacao"
        Me.pbarOrganizacao.Size = New System.Drawing.Size(606, 23)
        Me.pbarOrganizacao.TabIndex = 10
        '
        'frmReorgUnidade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 299)
        Me.Controls.Add(Me.pbarOrganizacao)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnProcessar)
        Me.Controls.Add(Me.lblStatusCF)
        Me.Controls.Add(Me.lblStatusCP)
        Me.Controls.Add(Me.lblStatusCC)
        Me.Controls.Add(Me.lblStatusCM)
        Me.Controls.Add(Me.chkCF)
        Me.Controls.Add(Me.chkCP)
        Me.Controls.Add(Me.chkCC)
        Me.Controls.Add(Me.chkCM)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReorgUnidade"
        Me.Text = "Reorganização de Unidades"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkCM As System.Windows.Forms.CheckBox
    Friend WithEvents chkCC As System.Windows.Forms.CheckBox
    Friend WithEvents chkCP As System.Windows.Forms.CheckBox
    Friend WithEvents chkCF As System.Windows.Forms.CheckBox
    Friend WithEvents lblStatusCM As System.Windows.Forms.Label
    Friend WithEvents lblStatusCC As System.Windows.Forms.Label
    Friend WithEvents lblStatusCP As System.Windows.Forms.Label
    Friend WithEvents lblStatusCF As System.Windows.Forms.Label
    Friend WithEvents btnProcessar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents pbarOrganizacao As System.Windows.Forms.ProgressBar
End Class
