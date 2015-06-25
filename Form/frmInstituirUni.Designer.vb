<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInstituirUni
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstituirUni))
        Me.Treeview_GerUnidades = New System.Windows.Forms.TreeView()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.txtEstruturaUnidade = New System.Windows.Forms.TextBox()
        Me.lblEstruturaUnidade = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.pnlDadosUni = New System.Windows.Forms.Panel()
        Me.txtDatInst = New System.Windows.Forms.TextBox()
        Me.lblDatInst = New System.Windows.Forms.Label()
        Me.txtEstUni = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCidUni = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBaiUni = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEndUni = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDatFun = New System.Windows.Forms.TextBox()
        Me.lblDatFund = New System.Windows.Forms.Label()
        Me.grpBox_MudarEstru = New System.Windows.Forms.GroupBox()
        Me.txtCodUnidadeMudar = New System.Windows.Forms.TextBox()
        Me.txtEstruDestino = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUnidadeMudar = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnConfirmar = New System.Windows.Forms.Button()
        Me.btnAlterar = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnMudarEstru = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblMensagem = New System.Windows.Forms.Label()
        Me.btnIncluir = New System.Windows.Forms.Button()
        Me.pnlDadosUni.SuspendLayout()
        Me.grpBox_MudarEstru.SuspendLayout()
        Me.SuspendLayout()
        '
        'Treeview_GerUnidades
        '
        Me.Treeview_GerUnidades.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Treeview_GerUnidades.ImageIndex = 0
        Me.Treeview_GerUnidades.ImageList = Me.ImageList
        Me.Treeview_GerUnidades.Location = New System.Drawing.Point(19, 7)
        Me.Treeview_GerUnidades.Margin = New System.Windows.Forms.Padding(4)
        Me.Treeview_GerUnidades.Name = "Treeview_GerUnidades"
        Me.Treeview_GerUnidades.SelectedImageIndex = 0
        Me.Treeview_GerUnidades.Size = New System.Drawing.Size(360, 560)
        Me.Treeview_GerUnidades.TabIndex = 0
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "Logo SSVP.png")
        Me.ImageList.Images.SetKeyName(1, "Locker.png")
        Me.ImageList.Images.SetKeyName(2, "AllDay.ru_New Floppy Drive.png")
        Me.ImageList.Images.SetKeyName(3, "login2.jpg")
        '
        'txtEstruturaUnidade
        '
        Me.txtEstruturaUnidade.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEstruturaUnidade.Enabled = False
        Me.txtEstruturaUnidade.Location = New System.Drawing.Point(668, 63)
        Me.txtEstruturaUnidade.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEstruturaUnidade.MaxLength = 11
        Me.txtEstruturaUnidade.Name = "txtEstruturaUnidade"
        Me.txtEstruturaUnidade.Size = New System.Drawing.Size(283, 22)
        Me.txtEstruturaUnidade.TabIndex = 5
        '
        'lblEstruturaUnidade
        '
        Me.lblEstruturaUnidade.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEstruturaUnidade.AutoSize = True
        Me.lblEstruturaUnidade.Enabled = False
        Me.lblEstruturaUnidade.Location = New System.Drawing.Point(560, 66)
        Me.lblEstruturaUnidade.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEstruturaUnidade.Name = "lblEstruturaUnidade"
        Me.lblEstruturaUnidade.Size = New System.Drawing.Size(106, 17)
        Me.lblEstruturaUnidade.TabIndex = 6
        Me.lblEstruturaUnidade.Text = "Estrut.Unidade:"
        '
        'txtCodigo
        '
        Me.txtCodigo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodigo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(449, 63)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.MaxLength = 8
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(76, 22)
        Me.txtCodigo.TabIndex = 3
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Enabled = False
        Me.lblCodigo.Location = New System.Drawing.Point(384, 66)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(56, 17)
        Me.lblCodigo.TabIndex = 4
        Me.lblCodigo.Text = "Código:"
        '
        'pnlDadosUni
        '
        Me.pnlDadosUni.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDadosUni.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.pnlDadosUni.Controls.Add(Me.txtDatInst)
        Me.pnlDadosUni.Controls.Add(Me.lblDatInst)
        Me.pnlDadosUni.Controls.Add(Me.txtEstUni)
        Me.pnlDadosUni.Controls.Add(Me.Label6)
        Me.pnlDadosUni.Controls.Add(Me.txtCidUni)
        Me.pnlDadosUni.Controls.Add(Me.Label5)
        Me.pnlDadosUni.Controls.Add(Me.txtBaiUni)
        Me.pnlDadosUni.Controls.Add(Me.Label4)
        Me.pnlDadosUni.Controls.Add(Me.txtEndUni)
        Me.pnlDadosUni.Controls.Add(Me.Label3)
        Me.pnlDadosUni.Controls.Add(Me.txtDatFun)
        Me.pnlDadosUni.Controls.Add(Me.lblDatFund)
        Me.pnlDadosUni.Controls.Add(Me.grpBox_MudarEstru)
        Me.pnlDadosUni.Location = New System.Drawing.Point(396, 95)
        Me.pnlDadosUni.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlDadosUni.Name = "pnlDadosUni"
        Me.pnlDadosUni.Size = New System.Drawing.Size(543, 471)
        Me.pnlDadosUni.TabIndex = 7
        '
        'txtDatInst
        '
        Me.txtDatInst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatInst.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDatInst.Enabled = False
        Me.txtDatInst.Location = New System.Drawing.Point(188, 28)
        Me.txtDatInst.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDatInst.MaxLength = 8
        Me.txtDatInst.Name = "txtDatInst"
        Me.txtDatInst.Size = New System.Drawing.Size(95, 22)
        Me.txtDatInst.TabIndex = 15
        Me.txtDatInst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDatInst
        '
        Me.lblDatInst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDatInst.AutoSize = True
        Me.lblDatInst.Enabled = False
        Me.lblDatInst.Location = New System.Drawing.Point(184, 9)
        Me.lblDatInst.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatInst.Name = "lblDatInst"
        Me.lblDatInst.Size = New System.Drawing.Size(156, 17)
        Me.lblDatInst.TabIndex = 16
        Me.lblDatInst.Text = "Instituição / Agregação:"
        '
        'txtEstUni
        '
        Me.txtEstUni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEstUni.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEstUni.Enabled = False
        Me.txtEstUni.Location = New System.Drawing.Point(386, 121)
        Me.txtEstUni.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEstUni.MaxLength = 8
        Me.txtEstUni.Name = "txtEstUni"
        Me.txtEstUni.Size = New System.Drawing.Size(40, 22)
        Me.txtEstUni.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Enabled = False
        Me.Label6.Location = New System.Drawing.Point(348, 123)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 17)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "UF:"
        '
        'txtCidUni
        '
        Me.txtCidUni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCidUni.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCidUni.Enabled = False
        Me.txtCidUni.Location = New System.Drawing.Point(77, 121)
        Me.txtCidUni.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCidUni.MaxLength = 8
        Me.txtCidUni.Name = "txtCidUni"
        Me.txtCidUni.Size = New System.Drawing.Size(263, 22)
        Me.txtCidUni.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(1, 123)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Cidade:"
        '
        'txtBaiUni
        '
        Me.txtBaiUni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBaiUni.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBaiUni.Enabled = False
        Me.txtBaiUni.Location = New System.Drawing.Point(76, 90)
        Me.txtBaiUni.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBaiUni.MaxLength = 8
        Me.txtBaiUni.Name = "txtBaiUni"
        Me.txtBaiUni.Size = New System.Drawing.Size(443, 22)
        Me.txtBaiUni.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Enabled = False
        Me.Label4.Location = New System.Drawing.Point(1, 94)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Bairro:"
        '
        'txtEndUni
        '
        Me.txtEndUni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEndUni.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEndUni.Enabled = False
        Me.txtEndUni.Location = New System.Drawing.Point(76, 60)
        Me.txtEndUni.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEndUni.MaxLength = 8
        Me.txtEndUni.Name = "txtEndUni"
        Me.txtEndUni.Size = New System.Drawing.Size(443, 22)
        Me.txtEndUni.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Enabled = False
        Me.Label3.Location = New System.Drawing.Point(0, 63)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Endereço:"
        '
        'txtDatFun
        '
        Me.txtDatFun.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatFun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDatFun.Enabled = False
        Me.txtDatFun.Location = New System.Drawing.Point(5, 28)
        Me.txtDatFun.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDatFun.MaxLength = 8
        Me.txtDatFun.Name = "txtDatFun"
        Me.txtDatFun.Size = New System.Drawing.Size(95, 22)
        Me.txtDatFun.TabIndex = 5
        Me.txtDatFun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDatFund
        '
        Me.lblDatFund.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDatFund.AutoSize = True
        Me.lblDatFund.Enabled = False
        Me.lblDatFund.Location = New System.Drawing.Point(1, 9)
        Me.lblDatFund.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatFund.Name = "lblDatFund"
        Me.lblDatFund.Size = New System.Drawing.Size(75, 17)
        Me.lblDatFund.TabIndex = 6
        Me.lblDatFund.Text = "Fundação:"
        '
        'grpBox_MudarEstru
        '
        Me.grpBox_MudarEstru.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpBox_MudarEstru.Controls.Add(Me.txtCodUnidadeMudar)
        Me.grpBox_MudarEstru.Controls.Add(Me.lblMensagem)
        Me.grpBox_MudarEstru.Controls.Add(Me.txtEstruDestino)
        Me.grpBox_MudarEstru.Controls.Add(Me.Label2)
        Me.grpBox_MudarEstru.Controls.Add(Me.Label1)
        Me.grpBox_MudarEstru.Controls.Add(Me.txtUnidadeMudar)
        Me.grpBox_MudarEstru.Controls.Add(Me.btnCancelar)
        Me.grpBox_MudarEstru.Controls.Add(Me.btnConfirmar)
        Me.grpBox_MudarEstru.Location = New System.Drawing.Point(5, 148)
        Me.grpBox_MudarEstru.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grpBox_MudarEstru.Name = "grpBox_MudarEstru"
        Me.grpBox_MudarEstru.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grpBox_MudarEstru.Size = New System.Drawing.Size(531, 260)
        Me.grpBox_MudarEstru.TabIndex = 0
        Me.grpBox_MudarEstru.TabStop = False
        Me.grpBox_MudarEstru.Text = "Mudar Unidade de Estrutura"
        Me.grpBox_MudarEstru.Visible = False
        '
        'txtCodUnidadeMudar
        '
        Me.txtCodUnidadeMudar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCodUnidadeMudar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodUnidadeMudar.Location = New System.Drawing.Point(115, 20)
        Me.txtCodUnidadeMudar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodUnidadeMudar.MaxLength = 11
        Me.txtCodUnidadeMudar.Name = "txtCodUnidadeMudar"
        Me.txtCodUnidadeMudar.Size = New System.Drawing.Size(99, 30)
        Me.txtCodUnidadeMudar.TabIndex = 10
        '
        'txtEstruDestino
        '
        Me.txtEstruDestino.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEstruDestino.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEstruDestino.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstruDestino.Location = New System.Drawing.Point(223, 63)
        Me.txtEstruDestino.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEstruDestino.MaxLength = 11
        Me.txtEstruDestino.Name = "txtEstruDestino"
        Me.txtEstruDestino.Size = New System.Drawing.Size(291, 30)
        Me.txtEstruDestino.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 70)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(185, 25)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Estrutura Destino:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 25)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Unidade:"
        '
        'txtUnidadeMudar
        '
        Me.txtUnidadeMudar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUnidadeMudar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUnidadeMudar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnidadeMudar.Location = New System.Drawing.Point(223, 20)
        Me.txtUnidadeMudar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidadeMudar.MaxLength = 11
        Me.txtUnidadeMudar.Name = "txtUnidadeMudar"
        Me.txtUnidadeMudar.Size = New System.Drawing.Size(291, 30)
        Me.txtUnidadeMudar.TabIndex = 6
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(227, 116)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(113, 28)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "CANCELAR"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnConfirmar.Enabled = False
        Me.btnConfirmar.Location = New System.Drawing.Point(85, 116)
        Me.btnConfirmar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(113, 28)
        Me.btnConfirmar.TabIndex = 0
        Me.btnConfirmar.Text = "CONFIRMAR"
        Me.btnConfirmar.UseVisualStyleBackColor = False
        '
        'btnAlterar
        '
        Me.btnAlterar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAlterar.Enabled = False
        Me.btnAlterar.Image = CType(resources.GetObject("btnAlterar.Image"), System.Drawing.Image)
        Me.btnAlterar.Location = New System.Drawing.Point(387, 6)
        Me.btnAlterar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAlterar.Name = "btnAlterar"
        Me.btnAlterar.Size = New System.Drawing.Size(53, 48)
        Me.btnAlterar.TabIndex = 8
        Me.btnAlterar.UseVisualStyleBackColor = True
        '
        'btnExcluir
        '
        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcluir.Enabled = False
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(674, 7)
        Me.btnExcluir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(53, 48)
        Me.btnExcluir.TabIndex = 9
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnMudarEstru
        '
        Me.btnMudarEstru.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMudarEstru.Enabled = False
        Me.btnMudarEstru.Image = CType(resources.GetObject("btnMudarEstru.Image"), System.Drawing.Image)
        Me.btnMudarEstru.Location = New System.Drawing.Point(448, 6)
        Me.btnMudarEstru.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMudarEstru.Name = "btnMudarEstru"
        Me.btnMudarEstru.Size = New System.Drawing.Size(53, 48)
        Me.btnMudarEstru.TabIndex = 10
        Me.btnMudarEstru.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'lblMensagem
        '
        Me.lblMensagem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMensagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMensagem.Enabled = False
        Me.lblMensagem.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensagem.ForeColor = System.Drawing.Color.Blue
        Me.lblMensagem.Location = New System.Drawing.Point(-339, -1)
        Me.lblMensagem.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMensagem.Name = "lblMensagem"
        Me.lblMensagem.Size = New System.Drawing.Size(715, 56)
        Me.lblMensagem.TabIndex = 11
        Me.lblMensagem.Text = "Código:"
        '
        'btnIncluir
        '
        Me.btnIncluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnIncluir.Enabled = False
        Me.btnIncluir.Image = CType(resources.GetObject("btnIncluir.Image"), System.Drawing.Image)
        Me.btnIncluir.Location = New System.Drawing.Point(613, 7)
        Me.btnIncluir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnIncluir.Name = "btnIncluir"
        Me.btnIncluir.Size = New System.Drawing.Size(53, 48)
        Me.btnIncluir.TabIndex = 13
        Me.btnIncluir.UseVisualStyleBackColor = True
        '
        'frmInstituirUni
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 570)
        Me.Controls.Add(Me.btnIncluir)
        Me.Controls.Add(Me.btnMudarEstru)
        Me.Controls.Add(Me.btnExcluir)
        Me.Controls.Add(Me.btnAlterar)
        Me.Controls.Add(Me.pnlDadosUni)
        Me.Controls.Add(Me.txtEstruturaUnidade)
        Me.Controls.Add(Me.lblEstruturaUnidade)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.lblCodigo)
        Me.Controls.Add(Me.Treeview_GerUnidades)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInstituirUni"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Gerenciar o Cadastro de Unidades"
        Me.pnlDadosUni.ResumeLayout(False)
        Me.pnlDadosUni.PerformLayout()
        Me.grpBox_MudarEstru.ResumeLayout(False)
        Me.grpBox_MudarEstru.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Treeview_GerUnidades As System.Windows.Forms.TreeView
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents txtEstruturaUnidade As System.Windows.Forms.TextBox
    Friend WithEvents lblEstruturaUnidade As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents pnlDadosUni As System.Windows.Forms.Panel
    Friend WithEvents btnAlterar As System.Windows.Forms.Button
    Friend WithEvents btnExcluir As System.Windows.Forms.Button
    Friend WithEvents btnMudarEstru As System.Windows.Forms.Button
    Friend WithEvents grpBox_MudarEstru As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUnidadeMudar As System.Windows.Forms.TextBox
    Friend WithEvents txtEstruDestino As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCodUnidadeMudar As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtDatFun As System.Windows.Forms.TextBox
    Friend WithEvents lblDatFund As System.Windows.Forms.Label
    Friend WithEvents txtEndUni As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEstUni As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCidUni As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBaiUni As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblMensagem As System.Windows.Forms.Label
    Friend WithEvents txtDatInst As System.Windows.Forms.TextBox
    Friend WithEvents lblDatInst As System.Windows.Forms.Label
    Friend WithEvents btnIncluir As System.Windows.Forms.Button
End Class
