Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmUnidades
    '?? Alterar para a Entidade Principal ??
    Dim dtCadastro As DataTable = New DataTable("EUN000")
    Dim dtMandato As DataTable = New DataTable("EUN016")
    Dim dtFichaInst As DataTable = New DataTable("EUN015")

    Dim i As Integer
    Dim i_Mandato As Integer
    Dim bAlterar As Boolean = False
    Dim bIncluir As Boolean = False
    Dim cQueryCadastro As String
    Dim cCampos As String
    Dim cValorCampos As String
    Dim nCodUsuario As Integer
    Dim nPermissao As Integer

    Private Sub frmUnidades_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        g_Param(1) = txtCodigo.Text 'Voltar com a Chave do registro do formulário
        g_AtuBrowse = True
        g_Comando = "REFRESH" 'Forçar a atualização do browser pelo timer
    End Sub

    Private Sub frmUnidades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i_point As Integer

        If g_Comando = "incluir" Then 'Or g_Comando = "excluir" Then
            MsgBox("Comando não permitido !!")
            Me.Close()
            Exit Sub
        End If

        nCodUsuario = getCodUsuario(ClassCrypt.Decrypt(g_Login))

        'Carregar a permissão do usuário
        cQueryCadastro = "Select UN013_PERACE FROM EUN013 where EUN013.UN013_CODUSU=" & _
            getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString & " AND UN013_CODUNI = " & g_Param(1)
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQueryCadastro, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtCadastro)
        End Using
        If dtCadastro.Rows.Count() > 0 Then
            nPermissao = dtCadastro.Rows(0).Item("UN013_PERACE")
        Else
            If UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN" Then
                nPermissao = 3
            Else
                nPermissao = 0
            End If

        End If
        dtCadastro.Clear()

        'Criar um adaptador que vai fazer o download de dados da base de dados
        '?? Alterar o Código para a Entidade Principal ??

        'If Me.Tag = 4 Then
        'cQuerycadastro = "SELECT * FROM EUN000 where UN000_STAUNI <> 'I'"
        'Else
        cQueryCadastro = "SELECT * FROM EUN000 where UN000_STAUNI <> 'I' and UN000_CODRED = " & g_Param(1)
        'End If

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQueryCadastro, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtCadastro)
        End Using

        If g_Param(1) <> "INSERT" Then
            'Posicionar no registro selecionado
            '?? Alterar para localizar a chave da tabela ??
            For i_point = 0 To dtCadastro.Rows.Count() - 1

                If dtCadastro.Rows(i_point).Item("UN000_CODRED").ToString = g_Param(1) Then
                    Exit For
                End If
            Next
            i = i_point

            'Iniciar com o comando passado
            If g_Comando = "insert" Then
                bIncluir = True
                bAlterar = True
            ElseIf g_Comando = "alterar" Then
                bIncluir = False
                bAlterar = True
            Else
                bIncluir = False
                bAlterar = False
            End If
        Else
            bIncluir = True
            bAlterar = True
        End If

        cCampos = "UN000_CODRED,UN000_NUMREG,UN000_CLAUNI,UN000_NOMUNI,UN000_DATFUN,UN000_CNPUNI," & _
            "UN000_ENDUNI,UN000_BAIUNI,UN000_CEPUNI,UN000_CIDUNI,UN000_ESTUNI,UN000_NACUNI,UN000_DIOUNI," & _
            "UN000_BCOUNI,UN000_AGEUNI,UN000_CCOUNI,UN000_TITUNI,UN000_OBSCCO,UN000_FREREU,UN000_APROCP," & _
            "UN000_APROCC,UN000_APROCM,UN000_APROCN,UN000_APROCG,UN000_DATINS,UN000_DATENV"

        TratarObjetos()
    End Sub

    Private Sub TratarObjetos()
        Dim nCodigoMandato As Integer
        Dim sQuery As String

        tssContReg.Text = "Registro " & (i + 1).ToString & "/" & dtCadastro.Rows.Count().ToString

        'Botoes da Barra de comandos
        btnIncluir.Enabled = False 'Not bAlterar And Me.Tag = 4 'And Me.Tag > 1
        btnAlterar.Enabled = Not bAlterar And nPermissao > 1 'Me.Tag > 1
        btnExcluir.Enabled = Not bAlterar And nPermissao > 1 'Me.Tag = 4
        btnGravar.Enabled = bAlterar
        btnCancelar.Enabled = bAlterar
        btnAnterior.Enabled = False 'Not bAlterar
        btnProximo.Enabled = False 'Not bAlterar
        btnLocalizar.Enabled = Not bAlterar
        btnImprimir.Enabled = Not bAlterar

        'Campos
        '?? Alterar para os seus objetos da Tela ??
        lblCodigo.Enabled = bAlterar
        lblBairro.Enabled = bAlterar
        lblBanco.Enabled = bAlterar
        lblCEP.Enabled = bAlterar
        lblCidade.Enabled = bAlterar
        lblConta.Enabled = bAlterar
        lblDiocese.Enabled = bAlterar
        lblEndereco.Enabled = bAlterar
        lblEstado.Enabled = bAlterar
        lblEstruturaUnidade.Enabled = bIncluir
        lblFrequenciaReuniao.Enabled = bAlterar
        lblAgencia.Enabled = bAlterar
        lblTitular.Enabled = bAlterar
        lblNome.Enabled = bAlterar
        lblObs.Enabled = bAlterar
        lblPais.Enabled = bAlterar
        lblRegistro.Enabled = bAlterar
        lblCnpj.Enabled = bAlterar
        txtAgencia.Enabled = bAlterar
        txtBairro.Enabled = bAlterar
        txtBanco.Enabled = bAlterar
        txtCEP.Enabled = bAlterar
        txtCidade.Enabled = bAlterar
        txtCodigo.Enabled = False
        txtConta.Enabled = bAlterar
        txtDiocese.Enabled = bAlterar
        txtEndereco.Enabled = bAlterar
        txtEstruturaUnidade.Enabled = bIncluir
        btnEstrutura.Enabled = bIncluir
        txtNome.Enabled = bAlterar
        txtPais.Enabled = bAlterar
        txtRegistro.Enabled = bAlterar
        txtTitular.Enabled = bAlterar
        txtCnpj.Enabled = bAlterar
        cbEstado.Enabled = bAlterar

        dtpDataFundacao.Enabled = bAlterar And nPermissao > 2
        lblDataFundacao.Enabled = bAlterar And nPermissao > 2
        dtpDataAprovacaoCG.Enabled = bAlterar And nPermissao > 2 'False 'bAlterar
        lblDataAprovacaoCG.Enabled = bAlterar And nPermissao > 2
        dtpDataAprovacaoCN.Enabled = bAlterar And nPermissao > 2 'False 'bAlterar
        lblDataAprovacaoCN.Enabled = bAlterar And nPermissao > 2
        dtpDataAprovacaoCM.Enabled = bAlterar And nPermissao > 2 'False 'bAlterar
        lblDataAprovacaoCM.Enabled = bAlterar And nPermissao > 2
        dtpDataAprovacaoCC.Enabled = bAlterar And nPermissao > 2 'False 'bAlterar
        lblDataAprovacaoCC.Enabled = bAlterar And nPermissao > 2 'bAlterar
        dtpDataAprovacaoCP.Enabled = bAlterar And nPermissao > 2
        lblDataAprovacaoCP.Enabled = bAlterar And nPermissao > 2

        dtpDataInstituicaoUnidade.Enabled = False 'bAlterar And nPermissao > 2
        lblDataInstituicaoUnidade.Enabled = False 'bAlterar And nPermissao > 2
        dtpDataAgregacaoUnidade.Enabled = False 'bAlterar And nPermissao > 2
        lblDataAgregacaoUnidade.Enabled = False 'bAlterar And nPermissao > 2

        dtpDataEnvio.Enabled = bAlterar And nPermissao > 2
        lblDataEnvio.Enabled = bAlterar And nPermissao > 2

        txtFrequenciaReuniao.Enabled = bAlterar And nPermissao > 1 'False
        txtCFAtv1.Enabled = bAlterar And nPermissao > 1 'false
        txtCFAtv2.Enabled = bAlterar And nPermissao > 1 'False
        txtCFAtv3.Enabled = bAlterar And nPermissao > 1 'False
        txtCFAtv4.Enabled = bAlterar And nPermissao > 1 'False
        txtCFAtv5.Enabled = bAlterar And nPermissao > 1 'False
        txtCFAtv6.Enabled = bAlterar And nPermissao > 1 'False

        txtObs.Enabled = bAlterar

        'Dados dos Mandatos
        btnProx_Mandato.Enabled = bAlterar And nPermissao > 1
        btnAnt_Mandato.Enabled = False
        lblCodMdt.Enabled = False
        txtUN016_CodMdt.Enabled = False
        txtUN016_DesMdt.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        lblUN016_DatIni.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        dtpUN016_DatIni.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        lblUN016_DatFin.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        dtpUN016_DatFin.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        dtgMandato.Enabled = bAlterar And Not bIncluir And nPermissao > 1

        'Membros Ativos
        txtPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        lstPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        dtgMembroAtivo.Enabled = bAlterar And Not bIncluir And nPermissao > 1
        btnPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1

        'Preencher Campos e Armazenar os dados do formulário para gravar o log
        If i > -1 And Not bIncluir Then
            txtCodigo.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CODRED")), "", dtCadastro.Rows(i).Item("UN000_CODRED").ToString())
            txtRegistro.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_NUMREG")), "", dtCadastro.Rows(i).Item("UN000_NUMREG").ToString())
            txtEstruturaUnidade.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CLAUNI")), "", dtCadastro.Rows(i).Item("UN000_CLAUNI"))
            txtNome.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_NOMUNI")), "", dtCadastro.Rows(i).Item("UN000_NOMUNI"))
            dtpDataFundacao.Value = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_DATFUN")), "01/01/1900", dtCadastro.Rows(i).Item("UN000_DATFUN"))

            txtCnpj.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CNPUNI")), "", dtCadastro.Rows(i).Item("UN000_CNPUNI"))
            txtEndereco.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_ENDUNI")), "", dtCadastro.Rows(i).Item("UN000_ENDUNI"))
            txtBairro.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_BAIUNI")), "", dtCadastro.Rows(i).Item("UN000_BAIUNI"))
            txtCEP.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CEPUNI")), "", dtCadastro.Rows(i).Item("UN000_CEPUNI"))
            txtCidade.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CIDUNI")), "", dtCadastro.Rows(i).Item("UN000_CIDUNI"))
            cbEstado.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_ESTUNI")), "", dtCadastro.Rows(i).Item("UN000_ESTUNI"))
            txtPais.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_NACUNI")), "", dtCadastro.Rows(i).Item("UN000_NACUNI"))
            txtDiocese.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_DIOUNI")), "", dtCadastro.Rows(i).Item("UN000_DIOUNI"))
            txtBanco.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_BCOUNI")), "", dtCadastro.Rows(i).Item("UN000_BCOUNI"))
            txtAgencia.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_AGEUNI")), "", dtCadastro.Rows(i).Item("UN000_AGEUNI"))
            txtConta.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_CCOUNI")), "", dtCadastro.Rows(i).Item("UN000_CCOUNI"))
            txtTitular.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_TITUNI")), "", dtCadastro.Rows(i).Item("UN000_TITUNI"))
            txtObs.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN000_OBSCCO")), "", dtCadastro.Rows(i).Item("UN000_OBSCCO"))

            'Mostrar as datas conforme a Estrutura
            dtpDataAprovacaoCM.Visible = Mid(txtEstruturaUnidade.Text, 4, 2) <> "00"
            lblDataAprovacaoCM.Visible = Mid(txtEstruturaUnidade.Text, 4, 2) <> "00"
            dtpDataAprovacaoCC.Visible = Mid(txtEstruturaUnidade.Text, 7, 2) <> "00"
            lblDataAprovacaoCC.Visible = Mid(txtEstruturaUnidade.Text, 7, 2) <> "00"
            dtpDataAprovacaoCP.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) <> "00"
            lblDataAprovacaoCP.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) <> "00"
            dtpDataInstituicaoUnidade.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) = "00"
            lblDataInstituicaoUnidade.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) = "00"
            dtpDataAgregacaoUnidade.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) <> "00"
            lblDataAgregacaoUnidade.Visible = Mid(txtEstruturaUnidade.Text, 10, 2) <> "00"

            cValorCampos = CarregarValoresTela()

            'Carregar os dados da Ficha de Instituição - Conferência
            If Mid(txtEstruturaUnidade.Text, 10, 2) <> "00" Then
                sQuery = "Select * from EUN015 where UN015_CODCF=" & txtCodigo.Text
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                    ' Preencher o DataTable 
                    da.Fill(dtFichaInst)
                End Using
                If dtFichaInst.Rows.Count > 0 Then
                    dtpDataAprovacaoCP.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DAUTCP")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DAUTCP"))
                    dtpDataAprovacaoCC.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DAUTCC")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DAUTCC"))
                    dtpDataAprovacaoCM.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DAUTCM")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DAUTCM"))
                    dtpDataAprovacaoCN.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DAUTCN")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DAUTCN"))
                    dtpDataAprovacaoCG.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DAUTCG")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DAUTCG"))
                    dtpDataInstituicaoUnidade.Value = dtpDataAprovacaoCG.Value 'IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DATAGR")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DATAGR"))
                    dtpDataAgregacaoUnidade.Value = dtpDataAprovacaoCG.Value
                    dtpDataEnvio.Value = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_DTENVI")), "01/01/1900", dtFichaInst.Rows(0).Item("UN015_DTENVI"))

                    txtFrequenciaReuniao.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_REUDIA")), "", dtFichaInst.Rows(0).Item("UN015_REUDIA"))
                    txtFrequenciaReuniao.Text += " AS " + IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_REUHOR")), "", dtFichaInst.Rows(0).Item("UN015_REUHOR").ToString)
                    txtFrequenciaReuniao.Text += " - " + IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_REUEND")), "", dtFichaInst.Rows(0).Item("UN015_REUEND"))

                    txtCFAtv1.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV1")), "", dtFichaInst.Rows(0).Item("UN015_CFATV1"))
                    txtCFAtv2.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV2")), "", dtFichaInst.Rows(0).Item("UN015_CFATV2"))
                    txtCFAtv3.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV3")), "", dtFichaInst.Rows(0).Item("UN015_CFATV3"))
                    txtCFAtv4.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV4")), "", dtFichaInst.Rows(0).Item("UN015_CFATV4"))
                    txtCFAtv5.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV5")), "", dtFichaInst.Rows(0).Item("UN015_CFATV5"))
                    txtCFAtv6.Text = IIf(IsDBNull(dtFichaInst.Rows(0).Item("UN015_CFATV6")), "", dtFichaInst.Rows(0).Item("UN015_CFATV6"))
                Else
                    dtpDataAprovacaoCP.Value = "01/01/1900"
                    dtpDataAprovacaoCC.Value = "01/01/1900"
                    dtpDataAprovacaoCM.Value = "01/01/1900"
                    dtpDataAprovacaoCN.Value = "01/01/1900"
                    dtpDataAprovacaoCG.Value = "01/01/1900"
                    dtpDataInstituicaoUnidade.Value = "01/01/1900"
                    dtpDataAgregacaoUnidade.Value = "01/01/1900"
                    dtpDataEnvio.Value = "01/01/1900"
                    txtCFAtv1.Text = ""
                    txtCFAtv2.Text = ""
                    txtCFAtv3.Text = ""
                    txtCFAtv4.Text = ""
                    txtCFAtv5.Text = ""
                    txtCFAtv6.Text = ""
                End If
                dtFichaInst.Clear()
            Else
                dtpDataAgregacaoUnidade.Value = "01/01/1900" 'Criar uma funcao para resgatar a informação 
            End If

            'Carregar os Dados do Mandato
            dtMandato.Clear()
            'Ler o último mandato
            sQuery = "Select * from EUN016 where UN016_CODRED=" & txtCodigo.Text & _
                " order by UN016_DATFIN DESC"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)
                da.Fill(dtMandato)
            End Using
            If dtMandato.Rows.Count = 0 Then
                'Não existe nenhum mandato, incluir um novo
                nCodigoMandato = InserirMandato(CDbl(txtCodigo.Text))
                If nCodigoMandato > 0 Then
                    dtMandato.Clear()
                    sQuery = "Select * from EUN016 where UN016_CODRED=" & txtCodigo.Text & _
                        " and UN016_CODMDT=" & nCodigoMandato.ToString
                    Using da As New OleDbDataAdapter()
                        da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                        ' Preencher o DataTable 
                        da.Fill(dtMandato)
                    End Using
                End If
            End If
            If dtMandato.Rows.Count > 0 Then
                i_Mandato = 0
                Call Carregardados_Mandato()
            End If

            'Carregar os Membros Ativos
            txtPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1 And Microsoft.VisualBasic.Right(Trim(txtEstruturaUnidade.Text), 2) <> "00"
            lstPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1 And Microsoft.VisualBasic.Right(Trim(txtEstruturaUnidade.Text), 2) <> "00"
            dtgMembroAtivo.Enabled = bAlterar And Not bIncluir And nPermissao > 1 And Microsoft.VisualBasic.Right(Trim(txtEstruturaUnidade.Text), 2) <> "00"
            btnPesqMembro.Enabled = bAlterar And Not bIncluir And nPermissao > 1 And Microsoft.VisualBasic.Right(Trim(txtEstruturaUnidade.Text), 2) <> "00"

            If Microsoft.VisualBasic.Right(Trim(txtEstruturaUnidade.Text), 2) <> "00" Then
                CarregarGridMembro()
            End If
        End If

        'Verificar se é para excluir o registro - comandado pelo browse
        If g_Comando = "excluir" Then
            Call Excluir_Registro()
        End If

    End Sub

    Private Sub Carregardados_Mandato()

        txtUN016_CodMdt.Text = dtMandato.Rows(i_Mandato).Item("UN016_CODMDT")
        txtUN016_DesMdt.Text = dtMandato.Rows(i_Mandato).Item("UN016_DESMDT")
        dtpUN016_DatIni.Value = dtMandato.Rows(i_Mandato).Item("UN016_DATINI")
        dtpUN016_DatFin.Value = dtMandato.Rows(i_Mandato).Item("UN016_DATFIN")

        'Montar o Grid com os encargos
        Call CarregarGrid(dtMandato.Rows(0).Item("UN016_CODMDT"))

        btnGravar_Mandato.Enabled = True

    End Sub

    Private Sub btnProximo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProximo.Click

        i += 1
        If Not i = dtCadastro.Rows.Count() Then
            Call TratarObjetos()
        Else
            i = dtCadastro.Rows.Count() - 1
        End If

    End Sub

    Private Sub btnAnterior_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnterior.Click

        i -= 1
        If Not i < 0 Then
            Call TratarObjetos()
        Else
            i = 0
        End If

    End Sub

    Private Sub btnAlterar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlterar.Click

        bAlterar = True
        Call TratarObjetos()

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If g_Comando = "inserir" Or g_Comando = "alterar" Then
            dtCadastro.Clear()
            Me.Close()
        Else
            bAlterar = False
            bIncluir = False
            TratarObjetos()
        End If
    End Sub

    Private Sub btnIncluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        bAlterar = True
        bIncluir = True

        'Inicializar os seus Componentes de Entrada de Dados
        txtCodigo.Text = ""
        txtRegistro.Text = ""
        txtEstruturaUnidade.Text = ""
        txtNome.Text = ""
        dtpDataFundacao.Text = ""
        txtCnpj.Text = ""
        txtEndereco.Text = ""
        txtBairro.Text = ""
        txtCEP.Text = ""
        txtCidade.Text = ""
        cbEstado.Text = ""
        txtPais.Text = ""
        txtDiocese.Text = ""
        txtBanco.Text = ""
        txtAgencia.Text = ""
        txtConta.Text = ""
        txtTitular.Text = ""
        txtObs.Text = ""
        txtFrequenciaReuniao.Text = ""
        dtpDataAprovacaoCP.Text = ""
        dtpDataAprovacaoCC.Text = ""
        dtpDataAprovacaoCM.Text = ""
        dtpDataAprovacaoCN.Text = ""
        dtpDataAprovacaoCG.Text = ""
        dtpDataInstituicaoUnidade.Text = ""
        dtpDataEnvio.Text = ""

        TabControl1.SelectedTab = tbpCadastro

        Call TratarObjetos()

    End Sub

    Private Sub btnGravar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGravar.Click
        Dim cSql As String = ""
        Dim cMensagem As String = ""
        Dim cmd As OleDbCommand

        If Validardados(cMensagem) Then
            If ConectarBanco() Then
                '?? Colocar o Comando SQL para Gravar (Update e Insert)
                If bIncluir Then
                    cSql = "INSERT INTO EUN000(UN000_CODRED, UN000_NUMREG, UN000_CLAUNI, UN000_NOMUNI, UN000_DATFUN, UN000_CNPUNI, UN000_ENDUNI, UN000_BAIUNI, UN000_CEPUNI, "
                    cSql += "UN000_CIDUNI, UN000_ESTUNI, UN000_NACUNI, UN000_DIOUNI, UN000_BCOUNI, UN000_AGEUNI, UN000_CCOUNI, UN000_TITUNI, UN000_OBSCCO, UN000_FREREU, "
                    cSql += "UN000_APROCP, UN000_APROCC, UN000_APROCM, UN000_APROCN, UN000_APROCG, UN000_DATINS, UN000_DATENV)"
                    cSql += " values (" & Integer.Parse(ProxCodChave("EUN000", "UN000_CODRED")) & ", " & Integer.Parse(txtRegistro.Text) & ", '" & txtEstruturaUnidade.Text & "'"
                    cSql += ", '" & txtNome.Text & "', " & FormatarData(dtpDataFundacao.Text) & ", '" & txtCnpj.Text & "'"
                    cSql += ", '" & txtEndereco.Text & "', '" & txtBairro.Text & "', '" & txtCEP.Text & "', '" & txtCidade.Text & "', '" & cbEstado.Text & "'"
                    cSql += ", '" & txtPais.Text & "', '" & txtDiocese.Text & "', '" & txtBanco.Text & "', '" & txtAgencia.Text & "', '" & txtConta.Text & "'"
                    cSql += ", '" & txtTitular.Text & "', '" & txtObs.Text & "', '" & txtFrequenciaReuniao.Text & "'"
                    cSql += ", " & FormatarData(dtpDataAprovacaoCP.Text) & ", " & FormatarData(dtpDataAprovacaoCC.Text) & ""
                    cSql += ", " & FormatarData(dtpDataAprovacaoCM.Text) & ", " & FormatarData(dtpDataAprovacaoCN.Text) & ""
                    cSql += ", " & FormatarData(dtpDataAprovacaoCG.Text) & ", " & FormatarData(dtpDataInstituicaoUnidade.Text) & ", " & FormatarData(dtpDataEnvio.Text) & " )"

                ElseIf bAlterar Then
                    cSql = "UPDATE EUN000 set UN000_NUMREG = " & Integer.Parse(txtRegistro.Text) & ", UN000_CLAUNI = '" & txtEstruturaUnidade.Text & "', "
                    cSql += "UN000_NOMUNI = '" & txtNome.Text & "', UN000_DATFUN = " & FormatarData(dtpDataFundacao.Text) & ", "
                    cSql += "UN000_CNPUNI = '" & txtCnpj.Text & "', UN000_ENDUNI = '" & txtEndereco.Text & "', UN000_BAIUNI = '" & txtBairro.Text & "', "
                    cSql += "UN000_CEPUNI = '" & txtCEP.Text & "', UN000_CIDUNI = '" & txtCidade.Text & "', UN000_ESTUNI = '" & cbEstado.Text & "', "
                    cSql += "UN000_NACUNI = '" & txtPais.Text & "', UN000_DIOUNI = '" & txtDiocese.Text & "', UN000_BCOUNI = '" & txtBanco.Text & "', "
                    cSql += "UN000_AGEUNI = '" & txtAgencia.Text & "', UN000_CCOUNI = '" & txtConta.Text & "', UN000_TITUNI = '" & txtTitular.Text & "', "
                    cSql += "UN000_OBSCCO = '" & txtObs.Text & "', UN000_FREREU = '" & txtFrequenciaReuniao.Text & "', "
                    cSql += "UN000_APROCP = " & FormatarData(dtpDataAprovacaoCP.Value) & ", UN000_APROCC = " & FormatarData(dtpDataAprovacaoCC.Value) & ", "
                    cSql += "UN000_APROCM = " & FormatarData(dtpDataAprovacaoCM.Value) & ", UN000_APROCN = " & FormatarData(dtpDataAprovacaoCN.Value) & ", "
                    cSql += "UN000_APROCG = " & FormatarData(dtpDataAprovacaoCG.Value) & ", UN000_DATINS = " & FormatarData(dtpDataInstituicaoUnidade.Value) & ", "
                    cSql += "UN000_DATENV = " & FormatarData(dtpDataEnvio.Value) & ""
                    cSql += " where UN000_CODRED = " & Integer.Parse(txtCodigo.Text)
                End If

                cmd = New OleDbCommand(cSql, g_ConnectBanco)

                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.ToString())
                Finally
                    'Gravar o Log
                    Call RegistrarLog_Alteracao()
                    '************

                    bIncluir = False
                    bAlterar = False

                    If g_Param(1) = "INSERT" Then
                        dtCadastro.Clear()
                        'fechar o form de cadastro
                        Me.Close()
                    Else
                        dtCadastro.Reset()
                        Using da As New OleDbDataAdapter()
                            da.SelectCommand = New OleDbCommand(cQueryCadastro, g_ConnectBanco)

                            ' Preencher o DataTable 
                            da.Fill(dtCadastro)
                        End Using
                        'Verificar se o comando veio do browse
                        If g_Comando = "inserir" Or g_Comando = "alterar" Then
                            dtCadastro.Clear()
                            Me.Close()
                        Else
                            TratarObjetos()
                        End If
                    End If
                End Try
            Else
                MsgBox("Erro ao conectar com o banco de Dados!!")
            End If
        Else
            MsgBox(cMensagem)
        End If

    End Sub

    Private Function Validardados(ByRef cMensagem As String) As Boolean
        Dim bRetorno As Boolean = True

        '?? Acrescentar as validações da Tela ??
        If Trim(txtNome.Text) = "" Then
            cMensagem += " <O Nome da Unidade não pode estar vazio> "
            bRetorno = False
        End If
        If Trim(txtEndereco.Text) = "" Then
            cMensagem += " <O Endereço da Unidade não pode estar vazio> "
            bRetorno = False
        End If
        If Trim(txtBairro.Text) = "" Then
            cMensagem += " <O Bairro da Unidade não pode estar vazio> "
            bRetorno = False
        End If
        If Trim(txtCidade.Text) = "" Then
            cMensagem += " <A Cidade da Unidade não pode estar vazia> "
            bRetorno = False
        End If

        Return (bRetorno)

    End Function

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        Call Excluir_Registro()

    End Sub

    Private Sub Excluir_Registro()
        Dim cSql As String
        Dim sMensagem As String = ""
        Dim cmd As OleDbCommand
        Dim bPodeExcluir As Boolean

        bPodeExcluir = GetPermissaoExcluirUnidade(Double.Parse(txtCodigo.Text), sMensagem)

        If bPodeExcluir Then
            If MsgBox("Deseja excluir este registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "cadastro de Usuarios") = MsgBoxResult.Yes Then
                '?? Alterar para a Tabela a ser Excluída ??
                'cSql = "DELETE FROM EUN000 where UN000_CODRED = " & Integer.Parse(txtCodigo.Text)
                cSql = "UPDATE EUN000 set UN000_STAUNI='I', UN000_CODUSU=" & nCodUsuario.ToString & ",UN000_DATINA=" & FormatarData(Today()) & " " & _
                    "where UN000_CODRED = " & Integer.Parse(txtCodigo.Text)

                cmd = New OleDbCommand(cSql, g_ConnectBanco)

                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.ToString())
                Finally
                    'Gravar o Log da Exclusão
                    If Not Gravar_LogUnidade(nCodUsuario.ToString, CInt(txtCodigo.Text), "UN000_STAUNI", "A", "I", sMensagem) Then
                        MsgBox(sMensagem)
                    End If
                    '************************

                    dtCadastro.Reset()
                    Using da As New OleDbDataAdapter()
                        da.SelectCommand = New OleDbCommand(cQueryCadastro, g_ConnectBanco)

                        'Preencher o DataTable 
                        da.Fill(dtCadastro)
                    End Using

                    If i > dtCadastro.Rows.Count() - 1 Then
                        i = dtCadastro.Rows.Count() - 1
                    End If
                    'Verificar se o comando veio do browse
                    If g_Comando = "excluir" Then
                        dtCadastro.Clear() 'Limpar o DataTable
                        Me.Close()
                    Else
                        TratarObjetos()
                    End If
                End Try
            End If
        Else
            MsgBox(sMensagem)
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        'cria um novo documento para impressão
        Dim pd As PrintDocument = New PrintDocument()

        'relaciona o objeto pd ao procedimento rptProdutos
        AddHandler pd.PrintPage, AddressOf Me.rptFormulario

        'cria uma nova instância do objeto PrintPreviewDialog()
        Dim objPrintPreview = New PrintPreviewDialog()

        'define algumas propriedades do obejto
        With objPrintPreview
            'indica qual o documento vai ser visualizado
            .Document = pd
            .WindowState = FormWindowState.Maximized
            .PrintPreviewControl.Zoom = 1   'maxima a visualização
            .Text = Me.Text
            'exibe a janela de visualização para o usuário
            .ShowDialog()
        End With

    End Sub

    Private Sub rptFormulario(ByVal sender As Object, ByVal Relatorio As System.Drawing.Printing.PrintPageEventArgs)
        Dim FormControl As Control
        Dim FormListBox As ListBox
        Dim pLinhaList As String

        Dim LinhasPorPagina As Integer
        Dim LinhaAdic As Integer
        Dim posicaoDaLinha As Integer
        Dim y As Integer, x As Integer

        Dim margemEsq As Single = Relatorio.MarginBounds.Left
        Dim margemSup As Single = Relatorio.MarginBounds.Top - 70
        Dim margemDir As Single = Relatorio.MarginBounds.Right
        Dim margemInf As Single = Relatorio.MarginBounds.Bottom

        Dim fonteTitulo As Font
        Dim fonteRodape As Font
        Dim fonteNormal As Font

        fonteTitulo = New Font("Verdana", 15, FontStyle.Bold)
        fonteRodape = New Font("Verdana", 8)
        fonteNormal = New Font("Verdana", 10)

        LinhasPorPagina = Relatorio.MarginBounds.Height / fonteNormal.GetHeight(Relatorio.Graphics) - 10

        margemEsq = 5
        'Imprimir o Cabeçalho
        'Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 40, margemDir, 40)
        Relatorio.Graphics.DrawImage(Image.FromFile("logo.png"), 10, 10)
        Relatorio.Graphics.DrawString(Me.Text, fonteTitulo, Brushes.Blue, margemEsq + 275, 40, New StringFormat())
        'Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 80, margemDir, 80) '145->100
        LinhaAdic = 2

        For x = 0 To Me.TabControl1.TabCount - 1
            Me.TabControl1.SelectedIndex = x
            Relatorio.Graphics.DrawString(TabControl1.SelectedTab.Text, fonteNormal, Brushes.Black, margemEsq, 40, New StringFormat())
            Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 80, margemDir, 80)
            LinhaAdic = 2
            For Each FormControl In Me.TabControl1.SelectedTab.Controls
                If FormControl.TabIndex > 0 Then
                    If (TypeOf FormControl Is Label) Then
                        posicaoDaLinha = margemSup + (((FormControl.TabIndex * 2) + LinhaAdic) * fonteNormal.GetHeight(Relatorio.Graphics))
                        Relatorio.Graphics.DrawString(FormControl.Text, fonteNormal, Brushes.Black, margemEsq, posicaoDaLinha, New StringFormat())
                    ElseIf (TypeOf FormControl Is TextBox) Or (TypeOf FormControl Is ComboBox) Then
                        posicaoDaLinha = margemSup + (((FormControl.TabIndex * 2) + LinhaAdic) * fonteNormal.GetHeight(Relatorio.Graphics))
                        Relatorio.Graphics.DrawString(FormControl.Text, fonteNormal, Brushes.Black, margemEsq + 150, posicaoDaLinha, New StringFormat())
                    ElseIf (TypeOf FormControl Is DateTimePicker) Then
                        posicaoDaLinha = margemSup + (((FormControl.TabIndex * 2) + LinhaAdic) * fonteNormal.GetHeight(Relatorio.Graphics))
                        Relatorio.Graphics.DrawString(FormControl.Text, fonteNormal, Brushes.Black, margemEsq + 150, posicaoDaLinha, New StringFormat())
                    ElseIf (TypeOf FormControl Is ListBox) Then
                        FormListBox = FormControl
                        posicaoDaLinha = margemSup + (((FormListBox.TabIndex * 2) + LinhaAdic) * fonteNormal.GetHeight(Relatorio.Graphics))
                        pLinhaList = ""
                        For y = 0 To FormListBox.Items.Count - 1
                            If Trim(FormListBox.Items(y).ToString) = "" Then Exit For
                            pLinhaList += "(" & FormListBox.Items(y).ToString & ") "
                        Next
                        Relatorio.Graphics.DrawString(pLinhaList, fonteNormal, Brushes.Black, margemEsq + 150, posicaoDaLinha, New StringFormat())
                    End If
                End If
            Next
        Next x
        'imprime o rodape no relatorio
        Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, margemInf + 70, margemDir, margemInf + 70)
        Relatorio.Graphics.DrawString(System.DateTime.Now, fonteRodape, Brushes.Black, margemEsq, margemInf + 70, New StringFormat())
        Relatorio.Graphics.DrawString("Formulário", fonteRodape, Brushes.Black, margemDir - 50, margemInf + 70, New StringFormat())

        Relatorio.HasMorePages = False

    End Sub

    Private Sub btnLocalizar_Click(sender As Object, e As EventArgs) Handles btnLocalizar.Click
        dtCadastro.Clear() 'Limpar o DataTable

        Me.Close()
    End Sub

    Private Function CarregarValoresTela() As String
        CarregarValoresTela = txtCodigo.Text & ","
        CarregarValoresTela += txtRegistro.Text & ","
        CarregarValoresTela += txtEstruturaUnidade.Text & ","
        CarregarValoresTela += txtNome.Text & ","
        CarregarValoresTela += dtpDataFundacao.Value.ToString & ","
        CarregarValoresTela += txtCnpj.Text & ","
        CarregarValoresTela += txtEndereco.Text & ","
        CarregarValoresTela += txtBairro.Text & ","
        CarregarValoresTela += txtCEP.Text & ","
        CarregarValoresTela += txtCidade.Text & ","
        CarregarValoresTela += cbEstado.Text & ","
        CarregarValoresTela += txtPais.Text & ","
        CarregarValoresTela += txtDiocese.Text & ","
        CarregarValoresTela += txtBanco.Text & ","
        CarregarValoresTela += txtAgencia.Text & ","
        CarregarValoresTela += txtConta.Text & ","
        CarregarValoresTela += txtTitular.Text & ","
        CarregarValoresTela += txtObs.Text & ","
        CarregarValoresTela += txtFrequenciaReuniao.Text & ","
        CarregarValoresTela += dtpDataAprovacaoCP.Value.ToString & ","
        CarregarValoresTela += dtpDataAprovacaoCC.Value.ToString & ","
        CarregarValoresTela += dtpDataAprovacaoCM.Value.ToString & ","
        CarregarValoresTela += dtpDataAprovacaoCN.Value.ToString & ","
        CarregarValoresTela += dtpDataAprovacaoCG.Value.ToString & ","
        CarregarValoresTela += dtpDataInstituicaoUnidade.Value.ToString & ","
        CarregarValoresTela += dtpDataEnvio.Value.ToString '& ","

    End Function

    Private Sub RegistrarLog_Alteracao()
        Dim cListaCampos() As String = cCampos.Split(",")
        Dim cListaValores() As String = cValorCampos.Split(",")
        Dim cNewsValores As String
        Dim cListaValoresNews() As String
        Dim cMensagem As String = ""

        cNewsValores = CarregarValoresTela()
        cListaValoresNews = cNewsValores.Split(",")

        'Gravar o Log da Exclusão
        For x = 0 To cListaCampos.Length - 1
            If cListaValores(x) <> cListaValoresNews(x) Then
                If Not Gravar_LogUnidade(nCodUsuario.ToString, CInt(txtCodigo.Text), cListaCampos(x), cListaValores(x), cListaValoresNews(x), cMensagem) Then
                    MsgBox("Erro no Campo: " & cListaCampos(x) & Chr(13) & cMensagem)
                End If
            End If
        Next
        '************************
    End Sub

    Private Sub txtCnpj_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCnpj.KeyPress
        If IsNumeric(e.KeyChar) And txtCnpj.TextLength < txtCnpj.MaxLength Then
            txtCnpj.Text &= e.KeyChar
            txtCnpj.SelectionStart = txtCnpj.TextLength
            Call formatacnpj(txtCnpj)
        End If
        e.Handled = True
    End Sub

    Private Sub formatacnpj(ByVal txtTexto As Object)
        If Len(txtTexto.Text) = 2 Or Len(txtTexto.Text) = 6 Then
            txtTexto.Text = txtTexto.Text & "."
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        End If

        If Len(txtTexto.Text) = 10 Then
            txtTexto.Text = txtTexto.Text & "/"
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        End If
        If Len(txtTexto.Text) = 15 Then
            txtTexto.Text = txtTexto.Text & "-"
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        End If
    End Sub

    Private Sub txtCnpj_LostFocus(sender As Object, e As EventArgs) Handles txtCnpj.LostFocus
        Dim nCNPJ As New classValidarCPF_CNPJ

        nCNPJ.cnpj = txtCnpj.Text

        'If Not nCNPJ.isCnpjValido() Then
        If nCNPJ.cnpj = "00.000.000/0000-00" Then
            MsgBox("CNPJ Inválido!!")
            txtCnpj.Focus()
        End If
    End Sub

    Private Sub txtCnpj_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCnpj.Validating
        'Me.txtCnpj.Text = String.Empty OrElse 
        If Me.txtCnpj.Text.Length <> txtCnpj.MaxLength And txtCnpj.TextLength > 0 Then
            MsgBox("Informe um valor válido para CNPJ.(Formato: 99.999.999/9999-99", MsgBoxStyle.Critical)
            e.Cancel = True
        End If
    End Sub

    Private Sub CarregarGrid(fCodMandato As Integer)
        Dim dtGrid As DataTable = New DataTable("EUN012")
        Dim cmdEncargos As OleDbCommand
        Dim sNivelUn As String
        Dim nCodigoMandato As Integer
        Dim sQuery As String

        dtgMandato.DataSource = Nothing

        If txtEstruturaUnidade.Text = "00.00.00.00" Then
            sNivelUn = "CNB"
        ElseIf Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 8) = "00.00.00" Then
            sNivelUn = "CM"
        ElseIf Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 5) = "00.00" Then
            sNivelUn = "CC"
        ElseIf Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 2) = "00" Then
            sNivelUn = "CP"
        Else
            sNivelUn = "CF"
        End If

        sQuery = "Select EUN012.UN012_CODOCP as enc, EUN011.UN011_DESOCP as Encargo, " & _
            "EUN012.UN012_CODCOL as cfd, EUN003.UN003_NOMCOL as Confrade " & _
            "from ((EUN012 INNER JOIN EUN011 ON EUN011.UN011_CODOCP=EUN012.UN012_CODOCP) " & _
            "LEFT JOIN EUN003 ON EUN003.UN003_CODCOL=EUN012.UN012_CODCOL) " & _
            "INNER JOIN EUN016 ON EUN016.UN016_CODMDT=EUN012.UN012_CODMDT AND EUN016.UN016_CODRED=EUN012.UN012_CODRED " & _
            "WHERE EUN016.UN016_CODRED=" & txtCodigo.Text & " AND EUN011.UN011_NIVOCP='" & sNivelUn & "'" & _
            " ORDER BY EUN011.UN011_DESOCP"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

            ' Preencher o DataTable  
            da.Fill(dtGrid)
        End Using

        If dtGrid.Rows.Count = 0 Then
            'Não existe Mandato associado a esta unidade
            'Criar um novo mandato
            ncodigomandato = InserirMandato(CDbl(txtCodigo.Text))

            If nCodigoMandato > 0 Then
                dtGrid.Clear()

                'Ler os Encargos e atribuir a este mandato na Unidade
                sQuery = " select * from EUN011 where EUN011.UN011_NIVOCP='" & sNivelUn & "'"
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                    ' Preencher o DataTable  
                    da.Fill(dtGrid)
                End Using
                If dtGrid.Rows.Count > 0 Then
                    For x = 0 To dtGrid.Rows.Count - 1
                        'INSERIR O MANDATO
                        sQuery = "INSERT INTO EUN012 (UN012_CODMDT, UN012_CODOCP, UN012_CODCOL, UN012_CODRED) VALUES " & _
                            "(" & nCodigoMandato & "," & dtGrid.Rows(x).Item("UN011_CODOCP") & ",0," & txtCodigo.Text & ")"
                        cmdEncargos = New OleDbCommand(sQuery, g_ConnectBanco)

                        Try
                            cmdEncargos.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox(ex.ToString())
                        Finally
                        End Try
                    Next

                    'REFAZER A LEITURA DOS ENCARGOS DO MANDATO
                    dtGrid.Clear()
                    sQuery = "Select EUN012.UN012_CODOCP as enc, EUN011.UN011_DESOCP as Encargo, " & _
                        "EUN012.UN012_CODCOL as cfd, EUN003.UN003_NOMCOL as Confrade " & _
                        "from ((EUN012 INNER JOIN EUN011 ON EUN011.UN011_CODOCP=EUN012.UN012_CODOCP) " & _
                        "LEFT JOIN EUN003 ON EUN003.UN003_CODCOL=EUN012.UN012_CODCOL) " & _
                        "INNER JOIN EUN016 ON EUN016.UN016_CODMDT=EUN012.UN012_CODMDT AND EUN016.UN016_CODRED=EUN012.UN012_CODRED" & _
                        "WHERE EUN016.UN016_CODRED=" & txtCodigo.Text & " AND EUN011.UN011_NIVOCP='" & sNivelUn & "'" & _
                        " ORDER BY EUN011.UN011_DESOCP"
                    Using da As New OleDbDataAdapter()
                        da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                        ' Preencher o DataTable  
                        da.Fill(dtGrid)
                    End Using
                Else
                    Exit Sub
                End If
            Else
                MsgBox("Não foi possível criar um novo mandato para esta unidade. Verificar com o TI.")
            End If
        End If

        dtgMandato.DataSource = dtGrid
        dtgMandato.Columns(0).Width = 50
        dtgMandato.Columns(1).Width = 300
        dtgMandato.Columns(2).Width = 50
        dtgMandato.Columns(3).Width = 300

    End Sub

    Private Sub txtUN016_DesMdt_GotFocus(sender As Object, e As EventArgs) Handles txtUN016_DesMdt.GotFocus, dtpUN016_DatIni.GotFocus, dtpUN016_DatFin.GotFocus
        btnAnt_Mandato.Enabled = True
    End Sub

    Private Sub btnProx_Mandato_Click(sender As Object, e As EventArgs) Handles btnProx_Mandato.Click
        Dim nCodigoMandato As Integer
        Dim sQuery As String

        If i_Mandato = dtMandato.Rows.Count - 1 And Not dtpUN016_DatFin.Value = dtpUN016_DatIni.Value Then
            'Último registro, forçar a inclusão de um mandato novo
            nCodigomandato = InserirMandato(CDbl(txtCodigo.Text))
            If nCodigomandato > 0 Then
                dtMandato.Clear()
                sQuery = "Select * from EUN016 where UN016_CODRED=" & txtCodigo.Text & _
                    " and UN016_CODMDT=" & nCodigoMandato.ToString
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                    ' Preencher o DataTable 
                    da.Fill(dtMandato)
                End Using

                i_Mandato = 0
                Call Carregardados_Mandato()

            End If
        Else
            i_Mandato += 1
        End If

    End Sub

    Private Sub btnAnt_Mandato_Click(sender As Object, e As EventArgs) Handles btnAnt_Mandato.Click

        If i_Mandato > 0 Then
            i_Mandato -= 1
        End If

    End Sub

    Private Sub btnGravar_Mandato_Click(sender As Object, e As EventArgs) Handles btnGravar_Mandato.Click
        Dim cmdMandato As OleDbCommand
        Dim sQuery As String

        sQuery = "UPDATE EUN016 SET UN016_DESMDT='" & txtUN016_DesMdt.Text & "' " & _
            ", UN016_DATINI=" & FormatarData(dtpUN016_DatIni.Value) & " " & _
            ", UN016_DATFIN=" & FormatarData(dtpUN016_DatFin.Value) & " "
        sQuery += "WHERE UN016_CODMDT=" & txtUN016_CodMdt.Text & " AND UN016_CODRED=" & txtCodigo.Text
        cmdMandato = New OleDbCommand(sQuery, g_ConnectBanco)

        Try
            cmdMandato.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
        End Try

    End Sub

    Private Sub frmUnidades_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim nWidth As Integer
        Dim nHeight As Integer

        If Me.Size.Width > 31 Then
            nWidth = Me.Size.Width - 31
        Else
            nWidth = TabControl1.Size.Width
        End If
        If Me.Size.Height > 140 Then
            nHeight = Me.Size.Height - 140
        Else
            nHeight = TabControl1.Size.Height
        End If
        TabControl1.Size = New Size(nWidth, nHeight)

        If Me.Size.Width > 464 Then
            nWidth = Me.Size.Width - 464
        Else
            nWidth = dtgMandato.Size.Width
        End If
        If Me.Size.Height > 187 Then
            nHeight = Me.Size.Height - 187
        Else
            nHeight = dtgMandato.Size.Height
        End If
        dtgMandato.Size = New Size(nWidth, nHeight)
        'btnAnterior.Location = New System.Drawing.Point(ListView_Browse.Size.Width - 218, ListView_Browse.Size.Height + 58)
    End Sub

    Private Sub ComandoPesquisar_Click(sender As Object, e As EventArgs) Handles ComandoPesquisar.Click
        Dim nPos As Integer = 99
        Dim nSeq As Integer = 0
        Dim nStart As Integer = 1
        Dim sNome(10) As String
        Dim sItemLista As String
        Dim dtPesquisar As DataTable = New DataTable("EUN003")
        Dim sQuery As String

        If Not Trim(NomePesquisar.Text) = "" Then
            sQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_BAIRRO " & _
                ", EUN003.UN003_CIDADE, EUN003.UN003_SIGEST, EUN003.UN003_DTNASC from EUN003 " & _
                "where EUN003.UN003_SITCOL<>'I'"
            nStart = 1

            ListaPesquisa.Items.Clear()

            Do Until nPos = 0
                nPos = InStr(nStart, NomePesquisar.Text, " ")
                If nPos > 0 Then
                    sNome(nSeq) = Mid(NomePesquisar.Text, nStart, nPos - nStart)
                    nStart = nPos + 1
                    nSeq += 1
                Else
                    sNome(nSeq) = Mid(NomePesquisar.Text, nStart, Len(NomePesquisar.Text) - nStart + 1)
                End If
            Loop
            'Montar a Condição
            For nPos = 0 To nSeq
                sQuery += " and EUN003.UN003_NOMCOL LIKE '%" & sNome(nPos) & "%'"
            Next nPos
            sQuery += " order by EUN003.UN003_NOMCOL"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtPesquisar)
            End Using

            For nSeq = 0 To dtPesquisar.Rows.Count - 1
                sItemLista = Format(dtPesquisar.Rows(nSeq).Item("UN003_CODCOL"), "000000") & " | "
                sItemLista += IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL")), "", dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL"))
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC")) Then
                    sItemLista += " | " & Format(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC"), "dd/MM/yy")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")
                End If

                ListaPesquisa.Items.Add(sItemLista)
            Next nSeq

            dtPesquisar.Clear()
        Else
            MsgBox("Digite um nome para pesquisar. Para melhor performance, digitar nome e sobrenome.'")
        End If

    End Sub

    Private Sub ListaPesquisa_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListaPesquisa.MouseDoubleClick
        Dim sQuery As String

        If dtgMandato.SelectedRows.Count > 0 Then
            'Gravar o Ocupante do Cargo
            Dim cmdMandato As OleDbCommand

            sQuery = "UPDATE EUN012 SET UN012_CODCOL=" & Microsoft.VisualBasic.Left(ListaPesquisa.SelectedItem, 6)
            sQuery += " WHERE UN012_CODMDT=" & txtUN016_CodMdt.Text
            sQuery += " AND UN012_CODOCP=" & dtgMandato.SelectedRows.Item(0).Cells(0).Value
            sQuery += " AND UN012_CODRED=" & txtCodigo.Text
            cmdMandato = New OleDbCommand(sQuery, g_ConnectBanco)

            Try
                cmdMandato.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGrid(txtUN016_CodMdt.Text)
            End Try

            'MsgBox(ListaPesquisa.SelectedItem & " grid: " & dtgMandato.SelectedRows.Item(0).Cells(0).Value)
        End If

    End Sub

    Private Sub dtgMandato_MouseClick(sender As Object, e As MouseEventArgs) Handles dtgMandato.MouseClick

        If dtgMandato.SelectedRows.Count > 0 Then
            NomePesquisar.Enabled = True
            ComandoPesquisar.Enabled = True
            labelEncargo.Text = dtgMandato.SelectedRows.Item(0).Cells(1).Value
            If IsDBNull(dtgMandato.SelectedRows.Item(0).Cells(3).Value) Then
                NomePesquisar.Text = ""
            Else
                NomePesquisar.Text = dtgMandato.SelectedRows.Item(0).Cells(3).Value
            End If
        Else
            NomePesquisar.Enabled = False
            ComandoPesquisar.Enabled = False
            NomePesquisar.Text = ""
            ListaPesquisa.Items.Clear()
        End If

    End Sub

    Private Sub btnPesqMembro_Click(sender As Object, e As EventArgs) Handles btnPesqMembro.Click
        Dim nPos As Integer = 99
        Dim nSeq As Integer = 0
        Dim nStart As Integer = 1
        Dim sNome(10) As String
        Dim sItemLista As String
        Dim dtPesquisar As DataTable = New DataTable("EUN003")
        Dim sQuery As String

        If Not Trim(txtPesqMembro.Text) = "" Then
            sQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_BAIRRO, " & _
                "EUN003.UN003_CIDADE, EUN003.UN003_SIGEST, EUN003.UN003_DTNASC from EUN003 " & _
                "where EUN003.UN003_SITCOL<>'I' AND EUN003.UN003_CODUNI=0"
            nStart = 1

            lstPesqMembro.Items.Clear()

            Do Until nPos = 0
                nPos = InStr(nStart, txtPesqMembro.Text, " ")
                If nPos > 0 Then
                    sNome(nSeq) = Mid(txtPesqMembro.Text, nStart, nPos - nStart)
                    nStart = nPos + 1
                    nSeq += 1
                Else
                    sNome(nSeq) = Mid(txtPesqMembro.Text, nStart, Len(txtPesqMembro.Text) - nStart + 1)
                End If
            Loop
            'Montar a Condição
            For nPos = 0 To nSeq
                sQuery += " and EUN003.UN003_NOMCOL LIKE '%" & sNome(nPos) & "%'"
            Next nPos
            sQuery += " order by EUN003.UN003_NOMCOL"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtPesquisar)
            End Using

            For nSeq = 0 To dtPesquisar.Rows.Count - 1
                sItemLista = Format(dtPesquisar.Rows(nSeq).Item("UN003_CODCOL"), "000000") & " | "
                sItemLista += IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL")), "", dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL"))
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC")) Then
                    sItemLista += " | " & Format(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC"), "dd/MM/yy")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")
                End If

                lstPesqMembro.Items.Add(sItemLista)
            Next nSeq

            dtPesquisar.Clear()
        Else
            MsgBox("Digite um nome para pesquisar. Para melhor performance, digitar nome e sobrenome.'")
        End If

    End Sub

    Private Sub lstPesqMembro_DoubleClick(sender As Object, e As EventArgs) Handles lstPesqMembro.DoubleClick

        If lstPesqMembro.SelectedItem.ToString <> "" Then
            'Incluir o Colaborador na Conferência
            Dim cmdMandato As OleDbCommand
            Dim sQuery As String

            sQuery = "UPDATE EUN003 SET UN003_CODUNI=" & txtCodigo.Text
            sQuery += " WHERE UN003_CODCOL=" & Microsoft.VisualBasic.Left(lstPesqMembro.SelectedItem, 6)
            cmdMandato = New OleDbCommand(sQuery, g_ConnectBanco)

            Try
                cmdMandato.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGridMembro()
            End Try

            'MsgBox(ListaPesquisa.SelectedItem & " grid: " & dtgMandato.SelectedRows.Item(0).Cells(0).Value)
        End If

    End Sub

    Private Sub CarregarGridMembro()
        Dim dtGridMembro As DataTable = New DataTable("EUN003")
        Dim sQuery As String

        dtgMembroAtivo.DataSource = Nothing

        sQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_DTNASC " & _
            "from EUN003 WHERE EUN003.UN003_CODUNI=" & txtCodigo.Text & _
            " ORDER BY EUN003.UN003_NOMCOL"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

            ' Preencher o DataTable  
            da.Fill(dtGridMembro)
        End Using

        lblContadorMembro.Text = "Número de Membros Ativos: " & Format(dtGridMembro.Rows.Count, "###0")
        dtgMembroAtivo.DataSource = dtGridMembro
        dtgMembroAtivo.Columns(0).HeaderText = "Cod."
        dtgMembroAtivo.Columns(0).Width = 50
        dtgMembroAtivo.Columns(1).HeaderText = "Nome"
        dtgMembroAtivo.Columns(1).Width = 300
        dtgMembroAtivo.Columns(2).HeaderText = "Dt.Nasc."
        dtgMembroAtivo.Columns(2).Width = 100

    End Sub

    Private Sub dtgMembroAtivo_DoubleClick(sender As Object, e As EventArgs) Handles dtgMembroAtivo.DoubleClick
        Dim sQuery As String

        If dtgMembroAtivo.SelectedRows.Count > 0 Then
            'Preparar o comando para Exluir o Colaborador
            'txtPesqMembro.Text = dtgMembroAtivo.SelectedRows.Item(0).Cells(2).Value
            If IsDBNull(dtgMembroAtivo.SelectedRows.Item(0).Cells(1).Value) Then
                txtPesqMembro.Text = ""
            Else
                txtPesqMembro.Text = dtgMembroAtivo.SelectedRows.Item(0).Cells(1).Value
            End If

            'Comando para retirar o associado
            Dim cmdRetirarMembro As OleDbCommand

            sQuery = "UPDATE EUN003 SET UN003_CODUNI=0"
            sQuery += " WHERE UN003_CODCOL=" & dtgMembroAtivo.SelectedRows.Item(0).Cells(0).Value
            cmdRetirarMembro = New OleDbCommand(sQuery, g_ConnectBanco)

            Try
                cmdRetirarMembro.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGridMembro()
            End Try

            txtPesqMembro.Text = ""

        End If

    End Sub


End Class