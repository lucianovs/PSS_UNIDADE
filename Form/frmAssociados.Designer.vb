<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssociados
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssociados))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnIncluir = New System.Windows.Forms.ToolStripButton()
        Me.btnAlterar = New System.Windows.Forms.ToolStripButton()
        Me.btnExcluir = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGravar = New System.Windows.Forms.ToolStripButton()
        Me.btnCancelar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAnterior = New System.Windows.Forms.ToolStripButton()
        Me.btnProximo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnLocalizar = New System.Windows.Forms.ToolStripButton()
        Me.btnImprimir = New System.Windows.Forms.ToolStripButton()
        Me.lblDesSit = New System.Windows.Forms.Label()
        Me.txtDesSit = New System.Windows.Forms.TextBox()
        Me.cbSitCol = New System.Windows.Forms.ComboBox()
        Me.lblSitCol = New System.Windows.Forms.Label()
        Me.txtUnidade = New System.Windows.Forms.TextBox()
        Me.btnLocUnidade = New System.Windows.Forms.Button()
        Me.txtCPF = New System.Windows.Forms.TextBox()
        Me.lblCPF = New System.Windows.Forms.Label()
        Me.lblUnidades = New System.Windows.Forms.Label()
        Me.lblNmColaborador = New System.Windows.Forms.Label()
        Me.txtNmColaborador = New System.Windows.Forms.TextBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabCadastro = New System.Windows.Forms.TabPage()
        Me.dtpDatPro = New System.Windows.Forms.DateTimePicker()
        Me.lblDatPro = New System.Windows.Forms.Label()
        Me.dtpDatInc = New System.Windows.Forms.DateTimePicker()
        Me.lblDatInc = New System.Windows.Forms.Label()
        Me.txtUsuarioAlt = New System.Windows.Forms.TextBox()
        Me.dtpDatAlt = New System.Windows.Forms.DateTimePicker()
        Me.lblDatAlt = New System.Windows.Forms.Label()
        Me.cbSexoCo = New System.Windows.Forms.ComboBox()
        Me.lblSexoCo = New System.Windows.Forms.Label()
        Me.dtpDtNasc = New System.Windows.Forms.DateTimePicker()
        Me.lblDtNasc = New System.Windows.Forms.Label()
        Me.lblNumTel = New System.Windows.Forms.Label()
        Me.txtNumTel = New System.Windows.Forms.TextBox()
        Me.lblEMail1 = New System.Windows.Forms.Label()
        Me.txtEMail1 = New System.Windows.Forms.TextBox()
        Me.cbSigEst = New System.Windows.Forms.ComboBox()
        Me.txtNmPais = New System.Windows.Forms.TextBox()
        Me.lblNmPais = New System.Windows.Forms.Label()
        Me.txtCodCEP = New System.Windows.Forms.TextBox()
        Me.lblCodCEP = New System.Windows.Forms.Label()
        Me.lblSigEst = New System.Windows.Forms.Label()
        Me.lblCidade = New System.Windows.Forms.Label()
        Me.txtCidade = New System.Windows.Forms.TextBox()
        Me.lblBairro = New System.Windows.Forms.Label()
        Me.txtBairro = New System.Windows.Forms.TextBox()
        Me.lblComple = New System.Windows.Forms.Label()
        Me.txtComple = New System.Windows.Forms.TextBox()
        Me.lblENDCOL = New System.Windows.Forms.Label()
        Me.txtEndCol = New System.Windows.Forms.TextBox()
        Me.tabOutrosDados = New System.Windows.Forms.TabPage()
        Me.txtUsuarioCad = New System.Windows.Forms.TextBox()
        Me.txtObser4 = New System.Windows.Forms.TextBox()
        Me.dtpDatCad = New System.Windows.Forms.DateTimePicker()
        Me.txtObser3 = New System.Windows.Forms.TextBox()
        Me.lblDatCad = New System.Windows.Forms.Label()
        Me.txtObser2 = New System.Windows.Forms.TextBox()
        Me.lblObser = New System.Windows.Forms.Label()
        Me.txtObser1 = New System.Windows.Forms.TextBox()
        Me.tabEncargos = New System.Windows.Forms.TabPage()
        Me.dtgMandato = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tssContReg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabCadastro.SuspendLayout()
        Me.tabOutrosDados.SuspendLayout()
        Me.tabEncargos.SuspendLayout()
        CType(Me.dtgMandato, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnIncluir, Me.btnAlterar, Me.btnExcluir, Me.ToolStripSeparator1, Me.btnGravar, Me.btnCancelar, Me.ToolStripSeparator3, Me.btnAnterior, Me.btnProximo, Me.ToolStripSeparator2, Me.btnLocalizar, Me.btnImprimir})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(974, 39)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnIncluir
        '
        Me.btnIncluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnIncluir.Image = CType(resources.GetObject("btnIncluir.Image"), System.Drawing.Image)
        Me.btnIncluir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnIncluir.Name = "btnIncluir"
        Me.btnIncluir.Size = New System.Drawing.Size(36, 36)
        Me.btnIncluir.Text = "Incluir"
        '
        'btnAlterar
        '
        Me.btnAlterar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAlterar.Image = CType(resources.GetObject("btnAlterar.Image"), System.Drawing.Image)
        Me.btnAlterar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAlterar.Name = "btnAlterar"
        Me.btnAlterar.Size = New System.Drawing.Size(36, 36)
        Me.btnAlterar.Text = "Alterar"
        '
        'btnExcluir
        '
        Me.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(36, 36)
        Me.btnExcluir.Text = "Excluir"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'btnGravar
        '
        Me.btnGravar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnGravar.Image = CType(resources.GetObject("btnGravar.Image"), System.Drawing.Image)
        Me.btnGravar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGravar.Name = "btnGravar"
        Me.btnGravar.Size = New System.Drawing.Size(36, 36)
        Me.btnGravar.Text = "Gravar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(36, 36)
        Me.btnCancelar.Text = "Cancelar"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'btnAnterior
        '
        Me.btnAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(36, 36)
        Me.btnAnterior.Text = "Anterior"
        '
        'btnProximo
        '
        Me.btnProximo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnProximo.Image = CType(resources.GetObject("btnProximo.Image"), System.Drawing.Image)
        Me.btnProximo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Size = New System.Drawing.Size(36, 36)
        Me.btnProximo.Text = "Próximo"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'btnLocalizar
        '
        Me.btnLocalizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalizar.Image = CType(resources.GetObject("btnLocalizar.Image"), System.Drawing.Image)
        Me.btnLocalizar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalizar.Name = "btnLocalizar"
        Me.btnLocalizar.Size = New System.Drawing.Size(36, 36)
        Me.btnLocalizar.Text = "Localizar"
        '
        'btnImprimir
        '
        Me.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(36, 36)
        Me.btnImprimir.Text = "Imprimir"
        '
        'lblDesSit
        '
        Me.lblDesSit.AutoSize = True
        Me.lblDesSit.ForeColor = System.Drawing.Color.Black
        Me.lblDesSit.Location = New System.Drawing.Point(741, 106)
        Me.lblDesSit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDesSit.Name = "lblDesSit"
        Me.lblDesSit.Size = New System.Drawing.Size(49, 17)
        Me.lblDesSit.TabIndex = 47
        Me.lblDesSit.Text = "Motivo"
        '
        'txtDesSit
        '
        Me.txtDesSit.Location = New System.Drawing.Point(744, 124)
        Me.txtDesSit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDesSit.MaxLength = 30
        Me.txtDesSit.Name = "txtDesSit"
        Me.txtDesSit.Size = New System.Drawing.Size(217, 22)
        Me.txtDesSit.TabIndex = 48
        '
        'cbSitCol
        '
        Me.cbSitCol.FormattingEnabled = True
        Me.cbSitCol.Items.AddRange(New Object() {"NOVO (ASPIRANTE)", "VINCENTINO", "ASSOCIADO", "INATIVO", "EXCLUIDO"})
        Me.cbSitCol.Location = New System.Drawing.Point(788, 69)
        Me.cbSitCol.Name = "cbSitCol"
        Me.cbSitCol.Size = New System.Drawing.Size(174, 24)
        Me.cbSitCol.TabIndex = 46
        '
        'lblSitCol
        '
        Me.lblSitCol.AutoSize = True
        Me.lblSitCol.Location = New System.Drawing.Point(792, 51)
        Me.lblSitCol.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSitCol.Name = "lblSitCol"
        Me.lblSitCol.Size = New System.Drawing.Size(148, 17)
        Me.lblSitCol.TabIndex = 45
        Me.lblSitCol.Text = "Situação do Cadastro:"
        '
        'txtUnidade
        '
        Me.txtUnidade.Location = New System.Drawing.Point(298, 72)
        Me.txtUnidade.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidade.MaxLength = 60
        Me.txtUnidade.Name = "txtUnidade"
        Me.txtUnidade.Size = New System.Drawing.Size(457, 22)
        Me.txtUnidade.TabIndex = 44
        '
        'btnLocUnidade
        '
        Me.btnLocUnidade.BackgroundImage = Global.UNIDADES.My.Resources.Resources.AllDay_ru_Search
        Me.btnLocUnidade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLocUnidade.Location = New System.Drawing.Point(754, 72)
        Me.btnLocUnidade.Name = "btnLocUnidade"
        Me.btnLocUnidade.Size = New System.Drawing.Size(28, 22)
        Me.btnLocUnidade.TabIndex = 43
        Me.btnLocUnidade.UseVisualStyleBackColor = True
        '
        'txtCPF
        '
        Me.txtCPF.Location = New System.Drawing.Point(541, 124)
        Me.txtCPF.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCPF.MaxLength = 14
        Me.txtCPF.Name = "txtCPF"
        Me.txtCPF.Size = New System.Drawing.Size(143, 22)
        Me.txtCPF.TabIndex = 42
        '
        'lblCPF
        '
        Me.lblCPF.AutoSize = True
        Me.lblCPF.Location = New System.Drawing.Point(541, 106)
        Me.lblCPF.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPF.Name = "lblCPF"
        Me.lblCPF.Size = New System.Drawing.Size(38, 17)
        Me.lblCPF.TabIndex = 41
        Me.lblCPF.Text = "CPF:"
        '
        'lblUnidades
        '
        Me.lblUnidades.AutoSize = True
        Me.lblUnidades.ForeColor = System.Drawing.Color.Black
        Me.lblUnidades.Location = New System.Drawing.Point(163, 75)
        Me.lblUnidades.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnidades.Name = "lblUnidades"
        Me.lblUnidades.Size = New System.Drawing.Size(135, 17)
        Me.lblUnidades.TabIndex = 40
        Me.lblUnidades.Text = "Unidade de Vínculo:"
        '
        'lblNmColaborador
        '
        Me.lblNmColaborador.AutoSize = True
        Me.lblNmColaborador.ForeColor = System.Drawing.Color.Red
        Me.lblNmColaborador.Location = New System.Drawing.Point(13, 105)
        Me.lblNmColaborador.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNmColaborador.Name = "lblNmColaborador"
        Me.lblNmColaborador.Size = New System.Drawing.Size(151, 17)
        Me.lblNmColaborador.TabIndex = 38
        Me.lblNmColaborador.Text = "Nome do Colaborador:"
        '
        'txtNmColaborador
        '
        Me.txtNmColaborador.Location = New System.Drawing.Point(16, 125)
        Me.txtNmColaborador.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNmColaborador.MaxLength = 100
        Me.txtNmColaborador.Name = "txtNmColaborador"
        Me.txtNmColaborador.Size = New System.Drawing.Size(517, 22)
        Me.txtNmColaborador.TabIndex = 39
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(13, 71)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(56, 17)
        Me.lblCodigo.TabIndex = 36
        Me.lblCodigo.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCodigo.Location = New System.Drawing.Point(70, 70)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.MaxLength = 15
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(65, 22)
        Me.txtCodigo.TabIndex = 37
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabCadastro)
        Me.TabControl1.Controls.Add(Me.tabOutrosDados)
        Me.TabControl1.Controls.Add(Me.tabEncargos)
        Me.TabControl1.Location = New System.Drawing.Point(15, 161)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(946, 330)
        Me.TabControl1.TabIndex = 49
        '
        'tabCadastro
        '
        Me.tabCadastro.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabCadastro.Controls.Add(Me.dtpDatPro)
        Me.tabCadastro.Controls.Add(Me.lblDatPro)
        Me.tabCadastro.Controls.Add(Me.dtpDatInc)
        Me.tabCadastro.Controls.Add(Me.lblDatInc)
        Me.tabCadastro.Controls.Add(Me.txtUsuarioAlt)
        Me.tabCadastro.Controls.Add(Me.dtpDatAlt)
        Me.tabCadastro.Controls.Add(Me.lblDatAlt)
        Me.tabCadastro.Controls.Add(Me.cbSexoCo)
        Me.tabCadastro.Controls.Add(Me.lblSexoCo)
        Me.tabCadastro.Controls.Add(Me.dtpDtNasc)
        Me.tabCadastro.Controls.Add(Me.lblDtNasc)
        Me.tabCadastro.Controls.Add(Me.lblNumTel)
        Me.tabCadastro.Controls.Add(Me.txtNumTel)
        Me.tabCadastro.Controls.Add(Me.lblEMail1)
        Me.tabCadastro.Controls.Add(Me.txtEMail1)
        Me.tabCadastro.Controls.Add(Me.cbSigEst)
        Me.tabCadastro.Controls.Add(Me.txtNmPais)
        Me.tabCadastro.Controls.Add(Me.lblNmPais)
        Me.tabCadastro.Controls.Add(Me.txtCodCEP)
        Me.tabCadastro.Controls.Add(Me.lblCodCEP)
        Me.tabCadastro.Controls.Add(Me.lblSigEst)
        Me.tabCadastro.Controls.Add(Me.lblCidade)
        Me.tabCadastro.Controls.Add(Me.txtCidade)
        Me.tabCadastro.Controls.Add(Me.lblBairro)
        Me.tabCadastro.Controls.Add(Me.txtBairro)
        Me.tabCadastro.Controls.Add(Me.lblComple)
        Me.tabCadastro.Controls.Add(Me.txtComple)
        Me.tabCadastro.Controls.Add(Me.lblENDCOL)
        Me.tabCadastro.Controls.Add(Me.txtEndCol)
        Me.tabCadastro.Location = New System.Drawing.Point(4, 25)
        Me.tabCadastro.Name = "tabCadastro"
        Me.tabCadastro.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCadastro.Size = New System.Drawing.Size(938, 301)
        Me.tabCadastro.TabIndex = 0
        Me.tabCadastro.Text = "Dados Cadastrais"
        '
        'dtpDatPro
        '
        Me.dtpDatPro.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDatPro.Location = New System.Drawing.Point(478, 211)
        Me.dtpDatPro.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDatPro.MinDate = New Date(1800, 1, 1, 0, 0, 0, 0)
        Me.dtpDatPro.Name = "dtpDatPro"
        Me.dtpDatPro.Size = New System.Drawing.Size(108, 22)
        Me.dtpDatPro.TabIndex = 34
        '
        'lblDatPro
        '
        Me.lblDatPro.AutoSize = True
        Me.lblDatPro.Location = New System.Drawing.Point(475, 193)
        Me.lblDatPro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatPro.Name = "lblDatPro"
        Me.lblDatPro.Size = New System.Drawing.Size(144, 17)
        Me.lblDatPro.TabIndex = 34
        Me.lblDatPro.Text = "Data da Proclamação"
        '
        'dtpDatInc
        '
        Me.dtpDatInc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDatInc.Location = New System.Drawing.Point(295, 211)
        Me.dtpDatInc.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDatInc.MinDate = New Date(1800, 1, 1, 0, 0, 0, 0)
        Me.dtpDatInc.Name = "dtpDatInc"
        Me.dtpDatInc.Size = New System.Drawing.Size(108, 22)
        Me.dtpDatInc.TabIndex = 33
        '
        'lblDatInc
        '
        Me.lblDatInc.AutoSize = True
        Me.lblDatInc.Location = New System.Drawing.Point(292, 193)
        Me.lblDatInc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatInc.Name = "lblDatInc"
        Me.lblDatInc.Size = New System.Drawing.Size(165, 17)
        Me.lblDatInc.TabIndex = 33
        Me.lblDatInc.Text = "Inclusão na Organização"
        '
        'txtUsuarioAlt
        '
        Me.txtUsuarioAlt.Location = New System.Drawing.Point(724, 265)
        Me.txtUsuarioAlt.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsuarioAlt.MaxLength = 100
        Me.txtUsuarioAlt.Name = "txtUsuarioAlt"
        Me.txtUsuarioAlt.Size = New System.Drawing.Size(196, 22)
        Me.txtUsuarioAlt.TabIndex = 33
        '
        'dtpDatAlt
        '
        Me.dtpDatAlt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDatAlt.Location = New System.Drawing.Point(608, 267)
        Me.dtpDatAlt.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDatAlt.MinDate = New Date(1800, 1, 1, 0, 0, 0, 0)
        Me.dtpDatAlt.Name = "dtpDatAlt"
        Me.dtpDatAlt.Size = New System.Drawing.Size(108, 22)
        Me.dtpDatAlt.TabIndex = 32
        '
        'lblDatAlt
        '
        Me.lblDatAlt.AutoSize = True
        Me.lblDatAlt.Location = New System.Drawing.Point(605, 249)
        Me.lblDatAlt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatAlt.Name = "lblDatAlt"
        Me.lblDatAlt.Size = New System.Drawing.Size(111, 17)
        Me.lblDatAlt.TabIndex = 32
        Me.lblDatAlt.Text = "Última Alteração"
        '
        'cbSexoCo
        '
        Me.cbSexoCo.FormattingEnabled = True
        Me.cbSexoCo.Items.AddRange(New Object() {"M", "F"})
        Me.cbSexoCo.Location = New System.Drawing.Point(166, 209)
        Me.cbSexoCo.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSexoCo.Name = "cbSexoCo"
        Me.cbSexoCo.Size = New System.Drawing.Size(62, 24)
        Me.cbSexoCo.TabIndex = 31
        '
        'lblSexoCo
        '
        Me.lblSexoCo.AutoSize = True
        Me.lblSexoCo.Location = New System.Drawing.Point(163, 193)
        Me.lblSexoCo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSexoCo.Name = "lblSexoCo"
        Me.lblSexoCo.Size = New System.Drawing.Size(39, 17)
        Me.lblSexoCo.TabIndex = 31
        Me.lblSexoCo.Text = "Sexo"
        '
        'dtpDtNasc
        '
        Me.dtpDtNasc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDtNasc.Location = New System.Drawing.Point(12, 211)
        Me.dtpDtNasc.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDtNasc.MinDate = New Date(1800, 1, 1, 0, 0, 0, 0)
        Me.dtpDtNasc.Name = "dtpDtNasc"
        Me.dtpDtNasc.Size = New System.Drawing.Size(108, 22)
        Me.dtpDtNasc.TabIndex = 30
        '
        'lblDtNasc
        '
        Me.lblDtNasc.AutoSize = True
        Me.lblDtNasc.Location = New System.Drawing.Point(9, 193)
        Me.lblDtNasc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDtNasc.Name = "lblDtNasc"
        Me.lblDtNasc.Size = New System.Drawing.Size(136, 17)
        Me.lblDtNasc.TabIndex = 30
        Me.lblDtNasc.Text = "Data de Nascimento"
        '
        'lblNumTel
        '
        Me.lblNumTel.AutoSize = True
        Me.lblNumTel.ForeColor = System.Drawing.Color.Black
        Me.lblNumTel.Location = New System.Drawing.Point(289, 119)
        Me.lblNumTel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNumTel.Name = "lblNumTel"
        Me.lblNumTel.Size = New System.Drawing.Size(64, 17)
        Me.lblNumTel.TabIndex = 29
        Me.lblNumTel.Text = "Telefone"
        '
        'txtNumTel
        '
        Me.txtNumTel.Location = New System.Drawing.Point(292, 137)
        Me.txtNumTel.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNumTel.MaxLength = 100
        Me.txtNumTel.Name = "txtNumTel"
        Me.txtNumTel.Size = New System.Drawing.Size(235, 22)
        Me.txtNumTel.TabIndex = 29
        '
        'lblEMail1
        '
        Me.lblEMail1.AutoSize = True
        Me.lblEMail1.ForeColor = System.Drawing.Color.Black
        Me.lblEMail1.Location = New System.Drawing.Point(7, 119)
        Me.lblEMail1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEMail1.Name = "lblEMail1"
        Me.lblEMail1.Size = New System.Drawing.Size(46, 17)
        Me.lblEMail1.TabIndex = 28
        Me.lblEMail1.Text = "e-Mail"
        '
        'txtEMail1
        '
        Me.txtEMail1.Location = New System.Drawing.Point(10, 137)
        Me.txtEMail1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEMail1.MaxLength = 100
        Me.txtEMail1.Name = "txtEMail1"
        Me.txtEMail1.Size = New System.Drawing.Size(277, 22)
        Me.txtEMail1.TabIndex = 28
        '
        'cbSigEst
        '
        Me.cbSigEst.FormattingEnabled = True
        Me.cbSigEst.Items.AddRange(New Object() {"AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO", "EX"})
        Me.cbSigEst.Location = New System.Drawing.Point(535, 88)
        Me.cbSigEst.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSigEst.Name = "cbSigEst"
        Me.cbSigEst.Size = New System.Drawing.Size(62, 24)
        Me.cbSigEst.TabIndex = 24
        '
        'txtNmPais
        '
        Me.txtNmPais.Location = New System.Drawing.Point(698, 89)
        Me.txtNmPais.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNmPais.MaxLength = 60
        Me.txtNmPais.Name = "txtNmPais"
        Me.txtNmPais.Size = New System.Drawing.Size(132, 22)
        Me.txtNmPais.TabIndex = 27
        '
        'lblNmPais
        '
        Me.lblNmPais.AutoSize = True
        Me.lblNmPais.Location = New System.Drawing.Point(699, 72)
        Me.lblNmPais.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNmPais.Name = "lblNmPais"
        Me.lblNmPais.Size = New System.Drawing.Size(35, 17)
        Me.lblNmPais.TabIndex = 27
        Me.lblNmPais.Text = "País"
        '
        'txtCodCEP
        '
        Me.txtCodCEP.Location = New System.Drawing.Point(605, 89)
        Me.txtCodCEP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodCEP.MaxLength = 9
        Me.txtCodCEP.Name = "txtCodCEP"
        Me.txtCodCEP.Size = New System.Drawing.Size(87, 22)
        Me.txtCodCEP.TabIndex = 26
        '
        'lblCodCEP
        '
        Me.lblCodCEP.AutoSize = True
        Me.lblCodCEP.Location = New System.Drawing.Point(605, 72)
        Me.lblCodCEP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodCEP.Name = "lblCodCEP"
        Me.lblCodCEP.Size = New System.Drawing.Size(35, 17)
        Me.lblCodCEP.TabIndex = 26
        Me.lblCodCEP.Text = "CEP"
        '
        'lblSigEst
        '
        Me.lblSigEst.AutoSize = True
        Me.lblSigEst.Location = New System.Drawing.Point(532, 72)
        Me.lblSigEst.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSigEst.Name = "lblSigEst"
        Me.lblSigEst.Size = New System.Drawing.Size(52, 17)
        Me.lblSigEst.TabIndex = 24
        Me.lblSigEst.Text = "Estado"
        '
        'lblCidade
        '
        Me.lblCidade.AutoSize = True
        Me.lblCidade.ForeColor = System.Drawing.Color.Black
        Me.lblCidade.Location = New System.Drawing.Point(7, 72)
        Me.lblCidade.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCidade.Name = "lblCidade"
        Me.lblCidade.Size = New System.Drawing.Size(52, 17)
        Me.lblCidade.TabIndex = 21
        Me.lblCidade.Text = "Cidade"
        '
        'txtCidade
        '
        Me.txtCidade.Location = New System.Drawing.Point(10, 90)
        Me.txtCidade.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCidade.MaxLength = 100
        Me.txtCidade.Name = "txtCidade"
        Me.txtCidade.Size = New System.Drawing.Size(277, 22)
        Me.txtCidade.TabIndex = 21
        '
        'lblBairro
        '
        Me.lblBairro.AutoSize = True
        Me.lblBairro.ForeColor = System.Drawing.Color.Black
        Me.lblBairro.Location = New System.Drawing.Point(292, 72)
        Me.lblBairro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBairro.Name = "lblBairro"
        Me.lblBairro.Size = New System.Drawing.Size(46, 17)
        Me.lblBairro.TabIndex = 22
        Me.lblBairro.Text = "Bairro"
        '
        'txtBairro
        '
        Me.txtBairro.Location = New System.Drawing.Point(292, 90)
        Me.txtBairro.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBairro.MaxLength = 100
        Me.txtBairro.Name = "txtBairro"
        Me.txtBairro.Size = New System.Drawing.Size(235, 22)
        Me.txtBairro.TabIndex = 22
        '
        'lblComple
        '
        Me.lblComple.AutoSize = True
        Me.lblComple.ForeColor = System.Drawing.Color.Black
        Me.lblComple.Location = New System.Drawing.Point(534, 20)
        Me.lblComple.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComple.Name = "lblComple"
        Me.lblComple.Size = New System.Drawing.Size(94, 17)
        Me.lblComple.TabIndex = 17
        Me.lblComple.Text = "Complemento"
        '
        'txtComple
        '
        Me.txtComple.Location = New System.Drawing.Point(534, 37)
        Me.txtComple.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComple.MaxLength = 60
        Me.txtComple.Name = "txtComple"
        Me.txtComple.Size = New System.Drawing.Size(281, 22)
        Me.txtComple.TabIndex = 18
        '
        'lblENDCOL
        '
        Me.lblENDCOL.AutoSize = True
        Me.lblENDCOL.ForeColor = System.Drawing.Color.Red
        Me.lblENDCOL.Location = New System.Drawing.Point(7, 20)
        Me.lblENDCOL.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblENDCOL.Name = "lblENDCOL"
        Me.lblENDCOL.Size = New System.Drawing.Size(69, 17)
        Me.lblENDCOL.TabIndex = 15
        Me.lblENDCOL.Text = "Endereço"
        '
        'txtEndCol
        '
        Me.txtEndCol.Location = New System.Drawing.Point(10, 37)
        Me.txtEndCol.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEndCol.MaxLength = 100
        Me.txtEndCol.Name = "txtEndCol"
        Me.txtEndCol.Size = New System.Drawing.Size(517, 22)
        Me.txtEndCol.TabIndex = 16
        '
        'tabOutrosDados
        '
        Me.tabOutrosDados.Controls.Add(Me.txtUsuarioCad)
        Me.tabOutrosDados.Controls.Add(Me.txtObser4)
        Me.tabOutrosDados.Controls.Add(Me.dtpDatCad)
        Me.tabOutrosDados.Controls.Add(Me.txtObser3)
        Me.tabOutrosDados.Controls.Add(Me.lblDatCad)
        Me.tabOutrosDados.Controls.Add(Me.txtObser2)
        Me.tabOutrosDados.Controls.Add(Me.lblObser)
        Me.tabOutrosDados.Controls.Add(Me.txtObser1)
        Me.tabOutrosDados.Location = New System.Drawing.Point(4, 25)
        Me.tabOutrosDados.Name = "tabOutrosDados"
        Me.tabOutrosDados.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutrosDados.Size = New System.Drawing.Size(938, 301)
        Me.tabOutrosDados.TabIndex = 1
        Me.tabOutrosDados.Text = "Outros dados"
        Me.tabOutrosDados.UseVisualStyleBackColor = True
        '
        'txtUsuarioCad
        '
        Me.txtUsuarioCad.Location = New System.Drawing.Point(126, 45)
        Me.txtUsuarioCad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsuarioCad.MaxLength = 100
        Me.txtUsuarioCad.Name = "txtUsuarioCad"
        Me.txtUsuarioCad.Size = New System.Drawing.Size(196, 22)
        Me.txtUsuarioCad.TabIndex = 37
        '
        'txtObser4
        '
        Me.txtObser4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObser4.Location = New System.Drawing.Point(7, 209)
        Me.txtObser4.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObser4.MaxLength = 255
        Me.txtObser4.Name = "txtObser4"
        Me.txtObser4.Size = New System.Drawing.Size(823, 22)
        Me.txtObser4.TabIndex = 41
        '
        'dtpDatCad
        '
        Me.dtpDatCad.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDatCad.Location = New System.Drawing.Point(10, 45)
        Me.dtpDatCad.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDatCad.MinDate = New Date(1800, 1, 1, 0, 0, 0, 0)
        Me.dtpDatCad.Name = "dtpDatCad"
        Me.dtpDatCad.Size = New System.Drawing.Size(108, 22)
        Me.dtpDatCad.TabIndex = 36
        '
        'txtObser3
        '
        Me.txtObser3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObser3.Location = New System.Drawing.Point(7, 179)
        Me.txtObser3.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObser3.MaxLength = 255
        Me.txtObser3.Name = "txtObser3"
        Me.txtObser3.Size = New System.Drawing.Size(823, 22)
        Me.txtObser3.TabIndex = 40
        '
        'lblDatCad
        '
        Me.lblDatCad.AutoSize = True
        Me.lblDatCad.Location = New System.Drawing.Point(7, 27)
        Me.lblDatCad.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatCad.Name = "lblDatCad"
        Me.lblDatCad.Size = New System.Drawing.Size(119, 17)
        Me.lblDatCad.TabIndex = 36
        Me.lblDatCad.Text = "Data do Cadastro"
        '
        'txtObser2
        '
        Me.txtObser2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObser2.Location = New System.Drawing.Point(7, 149)
        Me.txtObser2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObser2.MaxLength = 255
        Me.txtObser2.Name = "txtObser2"
        Me.txtObser2.Size = New System.Drawing.Size(823, 22)
        Me.txtObser2.TabIndex = 39
        '
        'lblObser
        '
        Me.lblObser.AutoSize = True
        Me.lblObser.ForeColor = System.Drawing.Color.Black
        Me.lblObser.Location = New System.Drawing.Point(7, 98)
        Me.lblObser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblObser.Name = "lblObser"
        Me.lblObser.Size = New System.Drawing.Size(89, 17)
        Me.lblObser.TabIndex = 38
        Me.lblObser.Text = "Observação:"
        '
        'txtObser1
        '
        Me.txtObser1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObser1.Location = New System.Drawing.Point(7, 119)
        Me.txtObser1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObser1.MaxLength = 255
        Me.txtObser1.Name = "txtObser1"
        Me.txtObser1.Size = New System.Drawing.Size(823, 22)
        Me.txtObser1.TabIndex = 38
        '
        'tabEncargos
        '
        Me.tabEncargos.Controls.Add(Me.dtgMandato)
        Me.tabEncargos.Location = New System.Drawing.Point(4, 25)
        Me.tabEncargos.Name = "tabEncargos"
        Me.tabEncargos.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEncargos.Size = New System.Drawing.Size(938, 301)
        Me.tabEncargos.TabIndex = 2
        Me.tabEncargos.Text = "Encargos"
        Me.tabEncargos.UseVisualStyleBackColor = True
        '
        'dtgMandato
        '
        Me.dtgMandato.AllowUserToAddRows = False
        Me.dtgMandato.AllowUserToDeleteRows = False
        Me.dtgMandato.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgMandato.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dtgMandato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgMandato.Location = New System.Drawing.Point(6, 21)
        Me.dtgMandato.Name = "dtgMandato"
        Me.dtgMandato.ReadOnly = True
        Me.dtgMandato.RowTemplate.Height = 24
        Me.dtgMandato.Size = New System.Drawing.Size(926, 277)
        Me.dtgMandato.TabIndex = 13
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssContReg})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 486)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(974, 25)
        Me.StatusStrip1.TabIndex = 50
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tssContReg
        '
        Me.tssContReg.Name = "tssContReg"
        Me.tssContReg.Size = New System.Drawing.Size(98, 20)
        Me.tssContReg.Text = "Registro n / n"
        '
        'frmAssociados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 511)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblDesSit)
        Me.Controls.Add(Me.txtDesSit)
        Me.Controls.Add(Me.cbSitCol)
        Me.Controls.Add(Me.lblSitCol)
        Me.Controls.Add(Me.txtUnidade)
        Me.Controls.Add(Me.btnLocUnidade)
        Me.Controls.Add(Me.txtCPF)
        Me.Controls.Add(Me.lblCPF)
        Me.Controls.Add(Me.lblUnidades)
        Me.Controls.Add(Me.lblNmColaborador)
        Me.Controls.Add(Me.txtNmColaborador)
        Me.Controls.Add(Me.lblCodigo)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmAssociados"
        Me.Text = "frmAssociados"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabCadastro.ResumeLayout(False)
        Me.tabCadastro.PerformLayout()
        Me.tabOutrosDados.ResumeLayout(False)
        Me.tabOutrosDados.PerformLayout()
        Me.tabEncargos.ResumeLayout(False)
        CType(Me.dtgMandato, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnIncluir As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAlterar As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExcluir As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGravar As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAnterior As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnProximo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnLocalizar As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnImprimir As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblDesSit As System.Windows.Forms.Label
    Friend WithEvents txtDesSit As System.Windows.Forms.TextBox
    Friend WithEvents cbSitCol As System.Windows.Forms.ComboBox
    Friend WithEvents lblSitCol As System.Windows.Forms.Label
    Friend WithEvents txtUnidade As System.Windows.Forms.TextBox
    Friend WithEvents btnLocUnidade As System.Windows.Forms.Button
    Friend WithEvents txtCPF As System.Windows.Forms.TextBox
    Friend WithEvents lblCPF As System.Windows.Forms.Label
    Friend WithEvents lblUnidades As System.Windows.Forms.Label
    Friend WithEvents lblNmColaborador As System.Windows.Forms.Label
    Friend WithEvents txtNmColaborador As System.Windows.Forms.TextBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabCadastro As System.Windows.Forms.TabPage
    Friend WithEvents dtpDatPro As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatPro As System.Windows.Forms.Label
    Friend WithEvents dtpDatInc As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatInc As System.Windows.Forms.Label
    Friend WithEvents txtUsuarioAlt As System.Windows.Forms.TextBox
    Friend WithEvents dtpDatAlt As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatAlt As System.Windows.Forms.Label
    Friend WithEvents cbSexoCo As System.Windows.Forms.ComboBox
    Friend WithEvents lblSexoCo As System.Windows.Forms.Label
    Friend WithEvents dtpDtNasc As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDtNasc As System.Windows.Forms.Label
    Friend WithEvents lblNumTel As System.Windows.Forms.Label
    Friend WithEvents txtNumTel As System.Windows.Forms.TextBox
    Friend WithEvents lblEMail1 As System.Windows.Forms.Label
    Friend WithEvents txtEMail1 As System.Windows.Forms.TextBox
    Friend WithEvents cbSigEst As System.Windows.Forms.ComboBox
    Friend WithEvents txtNmPais As System.Windows.Forms.TextBox
    Friend WithEvents lblNmPais As System.Windows.Forms.Label
    Friend WithEvents txtCodCEP As System.Windows.Forms.TextBox
    Friend WithEvents lblCodCEP As System.Windows.Forms.Label
    Friend WithEvents lblSigEst As System.Windows.Forms.Label
    Friend WithEvents lblCidade As System.Windows.Forms.Label
    Friend WithEvents txtCidade As System.Windows.Forms.TextBox
    Friend WithEvents lblBairro As System.Windows.Forms.Label
    Friend WithEvents txtBairro As System.Windows.Forms.TextBox
    Friend WithEvents lblComple As System.Windows.Forms.Label
    Friend WithEvents txtComple As System.Windows.Forms.TextBox
    Friend WithEvents lblENDCOL As System.Windows.Forms.Label
    Friend WithEvents txtEndCol As System.Windows.Forms.TextBox
    Friend WithEvents tabOutrosDados As System.Windows.Forms.TabPage
    Friend WithEvents txtUsuarioCad As System.Windows.Forms.TextBox
    Friend WithEvents txtObser4 As System.Windows.Forms.TextBox
    Friend WithEvents dtpDatCad As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtObser3 As System.Windows.Forms.TextBox
    Friend WithEvents lblDatCad As System.Windows.Forms.Label
    Friend WithEvents txtObser2 As System.Windows.Forms.TextBox
    Friend WithEvents lblObser As System.Windows.Forms.Label
    Friend WithEvents txtObser1 As System.Windows.Forms.TextBox
    Friend WithEvents tabEncargos As System.Windows.Forms.TabPage
    Friend WithEvents dtgMandato As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tssContReg As System.Windows.Forms.ToolStripStatusLabel
End Class
