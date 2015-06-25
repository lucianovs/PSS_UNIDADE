Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmColaboradores

    '?? Alterar para a Entidade Principal ??
    Dim dtCadastro As DataTable = New DataTable("EUN003")
    Dim dtUnidade As DataTable = New DataTable("EUN000")

    Dim i As Integer
    Dim bAlterar As Boolean = False
    Dim bIncluir As Boolean = False
    Dim cQueryCadastro As String
    Dim sClasseUnidade As String = ""
    Dim sNomeUnidade As String = ""

    Private Sub frmColaboradores_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        g_Param(1) = txtCodigo.Text 'Voltar com a Chave do registro do formulário
        g_AtuBrowse = True
        g_Comando = "REFRESH" 'Forçar a atualização do browser pelo timer
    End Sub

    Private Sub frmColaboradores_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i_point As Integer

        ToolStrip1.ShowItemToolTips = True

        'Criar um adaptador que vai fazer o download de dados da base de dados
        '?? Alterar o Código para a Entidade Principal ??
        If Me.Tag = 4 Or g_Param(1) = "INSERT" Then
            cQueryCadastro = "SELECT * FROM EUN003"
        Else
            cQueryCadastro = "SELECT * FROM EUN003 where UN003_CODCOL = " & g_Param(1)
        End If

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQueryCadastro, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtCadastro)
        End Using

        If g_Param(1) <> "INSERT" Then
            'Posicionar no registro selecionado
            '?? Alterar para localizar a chave da tabela ??
            For i_point = 0 To dtCadastro.Rows.Count() - 1
                If dtCadastro.Rows(i_point).Item("UN003_CODCOL").ToString = g_Param(1) Then
                    Exit For
                End If
            Next
            i = i_point

            'Iniciar com o comando passado
            If g_Comando = "incluir" Then
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

        TratarObjetos()

    End Sub

    Private Sub TratarObjetos()
        tssContReg.Text = "Registro " & (i + 1).ToString & "/" & dtCadastro.Rows.Count().ToString

        'Botoes da Barra de comandos
        btnIncluir.Enabled = Not bAlterar And Me.Tag = 4 'And Me.Tag > 1
        btnAlterar.Enabled = Not bAlterar 'And Me.Tag > 1
        btnExcluir.Enabled = Not bAlterar And Me.Tag = 4
        btnGravar.Enabled = bAlterar
        btnCancelar.Enabled = bAlterar
        btnAnterior.Enabled = Not bAlterar
        btnProximo.Enabled = Not bAlterar
        btnLocalizar.Enabled = Not bAlterar
        btnImprimir.Enabled = Not bAlterar

        'Campos
        '?? Alterar para os seus objetos da Tela ??
        lblCodigo.Enabled = bIncluir
        txtCodigo.Enabled = bIncluir
        lblNmColaborador.Enabled = bAlterar And Me.Tag > 1
        txtNmColaborador.Enabled = bAlterar And Me.Tag > 1
        lblCPF.Enabled = bAlterar And Me.Tag > 1
        txtCPF.Enabled = bAlterar And Me.Tag > 1
        lblSitCol.Enabled = bAlterar And Me.Tag > 1
        cbSitCol.Enabled = bAlterar And Me.Tag > 1
        txtDesSit.Enabled = bAlterar And Me.Tag > 1
        lblDesSit.Enabled = bAlterar And Me.Tag > 1
        lblUnidades.Enabled = bAlterar And Me.Tag > 1
        txtUnidade.Enabled = bAlterar And Me.Tag > 1
        lblENDCOL.Enabled = bAlterar And Me.Tag > 1
        txtEndCol.Enabled = bAlterar And Me.Tag > 1
        lblComple.Enabled = bAlterar And Me.Tag > 1
        txtComple.Enabled = bAlterar And Me.Tag > 1
        lblBairro.Enabled = bAlterar And Me.Tag > 1
        txtBairro.Enabled = bAlterar And Me.Tag > 1
        lblCidade.Enabled = bAlterar And Me.Tag > 1
        txtCidade.Enabled = bAlterar And Me.Tag > 1
        lblCodCEP.Enabled = bAlterar And Me.Tag > 1
        txtCodCEP.Enabled = bAlterar And Me.Tag > 1
        lblSigEst.Enabled = bAlterar And Me.Tag > 1
        cbSigEst.Enabled = bAlterar And Me.Tag > 1
        lblNmPais.Enabled = bAlterar And Me.Tag > 1
        txtNmPais.Enabled = bAlterar And Me.Tag > 1
        lblEMail1.Enabled = bAlterar And Me.Tag > 1
        txtEMail1.Enabled = bAlterar And Me.Tag > 1
        lblNumTel.Enabled = bAlterar And Me.Tag > 1
        txtNumTel.Enabled = bAlterar And Me.Tag > 1
        lblDtNasc.Enabled = bAlterar And Me.Tag > 1
        dtpDtNasc.Enabled = bAlterar And Me.Tag > 1
        lblSexoCo.Enabled = bAlterar And Me.Tag > 1
        cbSexoCo.Enabled = bAlterar And Me.Tag > 1
        lblObser.Enabled = bAlterar And Me.Tag > 1
        txtObser1.Enabled = bAlterar And Me.Tag > 1
        txtObser2.Enabled = bAlterar And Me.Tag > 1
        txtObser3.Enabled = bAlterar And Me.Tag > 1
        txtObser4.Enabled = bAlterar And Me.Tag > 1
        lblDatAlt.Enabled = False
        dtpDatAlt.Enabled = False
        txtUsuarioAlt.Enabled = False
        lblDatCad.Enabled = False
        dtpDatCad.Enabled = False
        txtUsuarioCad.Enabled = False

        'Outros Controles
        '*****************
        dtgMandato.Enabled = False

        'Preencher Campos
        If i > -1 And Not bIncluir Then
            txtCodigo.Text = dtCadastro.Rows(i).Item("UN003_CODCOL")
            txtNmColaborador.Text = dtCadastro.Rows(i).Item("UN003_NOMCOL")
            txtEndCol.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_ENDCOL")), "", dtCadastro.Rows(i).Item("UN003_ENDCOL"))
            txtComple.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_COMPLE")), "", dtCadastro.Rows(i).Item("UN003_COMPLE"))
            txtBairro.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_BAIRRO")), "", dtCadastro.Rows(i).Item("UN003_BAIRRO"))
            txtCidade.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_CIDADE")), "", dtCadastro.Rows(i).Item("UN003_CIDADE"))
            txtCodCEP.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_CODCEP")), "", dtCadastro.Rows(i).Item("UN003_CODCEP"))
            txtCPF.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_CPFCOL")), "0", dtCadastro.Rows(i).Item("UN003_CPFCOL"))
            If IsDBNull(dtCadastro.Rows(i).Item("UN003_SITCOL")) Then
                cbSitCol.Text = "ATIVO"
            ElseIf dtCadastro.Rows(i).Item("UN003_SITCOL") = "I" Then
                cbSitCol.Text = "INATIVO"
            ElseIf dtCadastro.Rows(i).Item("UN003_SITCOL") = "E" Then
                cbSitCol.Text = "EXCLUIDO"
            Else
                cbSitCol.Text = "ATIVO"
            End If
            txtDesSit.Enabled = cbSitCol.Text <> "ATIVO" And bAlterar
            lblDesSit.Enabled = cbSitCol.Text <> "ATIVO" And bAlterar
            txtDesSit.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_DESSIT")), "", dtCadastro.Rows(i).Item("UN003_DESSIT"))
            cbSigEst.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_SIGEST")), "", dtCadastro.Rows(i).Item("UN003_SIGEST"))
            txtNmPais.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_NMPAIS")), "BRASIL", dtCadastro.Rows(i).Item("UN003_NMPAIS"))
            txtEMail1.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_EMAIL1")), "", dtCadastro.Rows(i).Item("UN003_EMAIL1"))
            txtNumTel.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_NUMTEL")), "", dtCadastro.Rows(i).Item("UN003_NUMTEL"))
            dtpDtNasc.Value = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_DTNASC")), "01/01/1900", dtCadastro.Rows(i).Item("UN003_DTNASC"))
            cbSexoCo.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_SEXOCO")), "", dtCadastro.Rows(i).Item("UN003_SEXOCO"))
            txtObser1.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_OBSER1")), "", dtCadastro.Rows(i).Item("UN003_OBSER1"))
            txtObser2.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_OBSER2")), "", dtCadastro.Rows(i).Item("UN003_OBSER2"))
            txtObser3.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_OBSER3")), "", dtCadastro.Rows(i).Item("UN003_OBSER3"))
            txtObser4.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_OBSER4")), "", dtCadastro.Rows(i).Item("UN003_OBSER4"))
            dtpDatAlt.Value = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_DATALT")), "01/01/1900", dtCadastro.Rows(i).Item("UN003_DATALT"))
            dtpDatCad.Value = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_DATCAD")), "01/01/1900", dtCadastro.Rows(i).Item("UN003_DATCAD"))
            If IsDBNull(dtCadastro.Rows(i).Item("UN003_USUALT")) Then
                txtUsuarioAlt.Text = ""
            Else
                txtUsuarioAlt.Text = getLoginUsuario(dtCadastro.Rows(i).Item("UN003_USUALT"))
            End If
            'txtUsuarioAlt.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_USUALT")), "", getLoginUsuario(dtCadastro.Rows(i).Item("UN003_USUALT")))
            If IsDBNull(dtCadastro.Rows(i).Item("UN003_USUCAD")) Then
                txtUsuarioCad.Text = ""
            Else
                getLoginUsuario(dtCadastro.Rows(i).Item("UN003_USUCAD"))
            End If
            'txtUsuarioCad.Text = IIf(IsDBNull(dtCadastro.Rows(i).Item("UN003_USUCAD")), "", getLoginUsuario(dtCadastro.Rows(i).Item("UN003_USUCAD")))

            'Outros Controles
            If Not IsDBNull(dtCadastro.Rows(i).Item("UN003_CODUNI")) Then
                sClasseUnidade = LerClasse_Unidade(dtCadastro.Rows(i).Item("UN003_CODUNI"), sNomeUnidade)
            Else
                sClasseUnidade = ""
                sNomeUnidade = ""
            End If
            txtUnidade.Text = sClasseUnidade & IIf(sClasseUnidade = "", "Sem vínculo", " - ") & sNomeUnidade

            'Carregar os Mandatos do Colaborador
            Call CarregarGrid()

        End If

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

        If g_Comando = "incluir" Or g_Comando = "alterar" Then
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
        txtNmColaborador.Text = ""
        txtUnidade.Text = ""
        txtEndCol.Text = ""
        txtComple.Text = ""
        txtCidade.Text = ""
        txtBairro.Text = ""
        cbSigEst.Text = ""
        txtCodCEP.Text = "_____-___"
        txtCPF.Text = "0"
        cbSitCol.Text = "ATIVO"
        txtNmPais.Text = "BRASIL"
        txtEMail1.Text = ""
        txtNumTel.Text = ""
        dtpDtNasc.Value = "01/01/1900"
        dtpDatCad.Value = "01/01/1900"
        dtpDatAlt.Value = "01/01/1900"
        cbSexoCo.Text = ""
        txtObser1.Text = ""
        txtObser2.Text = ""
        txtObser3.Text = ""
        txtObser4.Text = ""
        txtDesSit.Text = ""
        cbSitCol.Text = "ATIVO"

        Call TratarObjetos()

    End Sub

    Private Sub btnGravar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGravar.Click
        Dim cSql As String = ""
        Dim cMensagem As String = ""
        Dim cmd As OleDbCommand
        Dim nProxCod As Integer

        If ConectarBanco() Then
            '?? Colocar o Comando SQL para Gravar (Update e Insert)
            If Validardados(cMensagem) Then
                If bIncluir Then
                    nProxCod = ProxCodChave("EUN003", "UN003_CODCOL")
                    cSql = "INSERT INTO EUN003 (UN003_CODCOL, UN003_NOMCOL, UN003_CODUNI"
                    cSql += ",UN003_ENDCOL, UN003_COMPLE, UN003_BAIRRO, UN003_CIDADE"
                    cSql += ",UN003_CODCEP, UN003_SIGEST, UN003_NMPAIS"
                    cSql += ",UN003_EMAIL1, UN003_NUMTEL"
                    cSql += ",UN003_DTNASC, UN003_SEXOCO"
                    cSql += ",UN003_OBSER1, UN003_OBSER2"
                    cSql += ",UN003_OBSER3, UN003_OBSER4"
                    cSql += ",UN003_DATCAD, UN003_USUCAD"
                    cSql += ",UN003_DATALT, UN003_USUALT"
                    cSql += ",UN003_SITCOL, UN003_CPFCOL"
                    cSql += ",UN003_DESSIT"
                    cSql += ")"
                    cSql += " values (" & nProxCod.ToString & ",'" & txtNmColaborador.Text & "', " & LerCod_Unidade(Microsoft.VisualBasic.Left(txtUnidade.Text, 11)).ToString
                    cSql += ",'" & Trim(txtEndCol.Text) & "','" & Trim(txtComple.Text) & "'"
                    cSql += ",'" & Trim(txtBairro.Text) & "','" & Trim(txtCidade.Text) & "'"
                    cSql += ",'" & Trim(txtCodCEP.Text) & "','" & Trim(cbSigEst.Text) & "','" & Trim(txtNmPais.Text) & "'"
                    cSql += ",'" & Trim(txtEMail1.Text) & "','" & Trim(txtNumTel.Text) & "'"
                    cSql += "," & FormatarData(dtpDtNasc.Text) & ",'" & cbSexoCo.Text & "'"
                    cSql += ",'" & txtObser1.Text & "','" & txtObser2.Text & "'"
                    cSql += ",'" & txtObser3.Text & "','" & txtObser4.Text & "'"
                    cSql += "," & FormatarData(Today()) & "," & getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString
                    cSql += "," & FormatarData(Today()) & "," & getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString
                    cSql += ",'" & Microsoft.VisualBasic.Left(cbSitCol.Text, 1) & "'"
                    cSql += ",'" & txtCPF.Text & "','A'"
                    cSql += ")"

                ElseIf bAlterar Then
                    cSql = "UPDATE EUN003 set UN003_NOMCOL='" & Trim(txtNmColaborador.Text) & _
                            "', UN003_CODUNI=" & LerCod_Unidade(Microsoft.VisualBasic.Left(txtUnidade.Text, 11)).ToString
                    cSql += ",UN003_ENDCOL='" & txtEndCol.Text & "'"
                    cSql += ",UN003_COMPLE='" & txtComple.Text & "'"
                    cSql += ",UN003_BAIRRO='" & txtBairro.Text & "'"
                    cSql += ",UN003_CIDADE='" & txtCidade.Text & "'"
                    cSql += ",UN003_CODCEP='" & txtCodCEP.Text & "'"
                    cSql += ",UN003_SIGEST='" & cbSigEst.Text & "'"
                    cSql += ",UN003_NMPAIS='" & txtNmPais.Text & "'"
                    cSql += ",UN003_EMAIL1='" & txtEMail1.Text & "'"
                    cSql += ",UN003_NUMTEL='" & txtNumTel.Text & "'"
                    cSql += ",UN003_DTNASC=" & FormatarData(dtpDtNasc.Text) & ""
                    cSql += ",UN003_SEXOCO='" & cbSexoCo.Text & "'"
                    cSql += ",UN003_OBSER1='" & txtObser1.Text & "'"
                    cSql += ",UN003_OBSER2='" & txtObser2.Text & "'"
                    cSql += ",UN003_OBSER3='" & txtObser3.Text & "'"
                    cSql += ",UN003_OBSER4='" & txtObser4.Text & "'"
                    cSql += ",UN003_DATALT=" & FormatarData(Today) & ""
                    cSql += ",UN003_USUALT=" & getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString
                    cSql += ",UN003_SITCOL='" & Microsoft.VisualBasic.Left(cbSitCol.Text, 1) & "'"
                    cSql += ",UN003_CPFCOL='" & txtCPF.Text & "'"
                    If txtUsuarioCad.Text = "" Then
                        cSql += ",UN003_USUCAD=" & getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString
                    End If
                    If IsDate(dtpDatCad.Text) Then
                        If Year(dtpDatCad.Text) = 1900 Then
                            cSql += ",UN003_DATCAD=" & FormatarData(Today) & ""
                        End If
                    Else
                        cSql += ",UN003_DATCAD=" & FormatarData(Today) & ""
                    End If
                    cSql += ",UN003_DESSIT='" & txtDesSit.Text & "'"
                    cSql += " where UN003_CODCOL = " & txtCodigo.Text

                    'acessoWEB=" & If(chkSIM.Checked = 0, False, True)
                    End If
                    cmd = New OleDbCommand(cSql, g_ConnectBanco)

                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
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
                            If g_Comando = "incluir" Or g_Comando = "alterar" Then
                            dtCadastro.Clear()
                                Me.Close()
                            Else
                                TratarObjetos()
                            End If

                        End If
                    End Try
            Else
                MsgBox(cMensagem)
            End If
        End If

    End Sub

    Private Function Validardados(ByRef cMensagem As String) As Boolean
        Dim bRetorno As Boolean = True

        '?? Acrescentar as validações da Tela ??
        If Trim(txtNmColaborador.Text) = "" Then
            cMensagem += " <O Nome do Colaborador não pode estar vazio> "
            bRetorno = False
        End If

        If Trim(txtEndCol.Text) = "" Then
            cMensagem += " <O Endereço do Colaborador não pode estar vazio> "
            bRetorno = False
        End If

        Return (bRetorno)

    End Function

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        Call Excluir_Registro()

    End Sub

    Private Sub Excluir_Registro()
        Dim cSql As String
        Dim cMensagem As String = ""
        Dim cmd As OleDbCommand

        Application.DoEvents()

        If MsgBox("Deseja excluir este registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "cadastro de Colaboradores") = MsgBoxResult.Yes Then

            'cSql = "DELETE FROM EUN003 where UN003_CODCOL = " & txtCodigo.Text
            cSql = "UPDATE EUN003 SET UN003_SITCOL='E', UN003_DESSIT='EXCLUIDO PELO SISTEMA' "
            If txtUsuarioCad.Text = "" Then
                cSql += ",UN003_USUALT=" & getCodUsuario(ClassCrypt.Decrypt(g_Login)).ToString
            End If
            cSql += ",UN003_DATALT=" & FormatarData(Today)
            cSql += "where UN003_CODCOL = " & txtCodigo.Text

            cmd = New OleDbCommand(cSql, g_ConnectBanco)

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally

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

        Else
            If Not Trim(cMensagem) = "" Then MsgBox(cMensagem)
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        'cria um novo documento para impressão
        Dim pd As PrintDocument = New PrintDocument()
        Dim PrinterSetup As PrintDialog = New PrintDialog

        'Mostrar o PrinterSetup
        PrinterSetup.Document = pd
        PrinterSetup.ShowDialog()
        '**

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
        Dim y As Integer

        Dim margemEsq As Single = Relatorio.MarginBounds.Left
        Dim margemSup As Single = Relatorio.MarginBounds.Top
        Dim margemDir As Single = Relatorio.MarginBounds.Right
        Dim margemInf As Single = Relatorio.MarginBounds.Bottom

        Dim fonteTitulo As Font
        Dim fonteRodape As Font
        Dim fonteNormal As Font

        fonteTitulo = New Font("Verdana", 15, FontStyle.Bold)
        fonteRodape = New Font("Verdana", 8)
        fonteNormal = New Font("Verdana", 10)

        LinhasPorPagina = Relatorio.MarginBounds.Height / fonteNormal.GetHeight(Relatorio.Graphics) - 10

        margemEsq = 10
        'Imprimir o Cabeçalho
        Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 40, margemDir, 40)
        Relatorio.Graphics.DrawImage(Image.FromFile("logo.png"), 10, 48)
        Relatorio.Graphics.DrawString(Me.Text, fonteTitulo, Brushes.Blue, margemEsq + 275, 80, New StringFormat())
        Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 145, margemDir, 145)
        LinhaAdic = 2

        For Each FormControl In Me.Controls
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
        Next

        'imprime o rodape no relatorio
        Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, margemInf, margemDir, margemInf)
        Relatorio.Graphics.DrawString(System.DateTime.Now, fonteRodape, Brushes.Black, margemEsq, margemInf, New StringFormat())
        Relatorio.Graphics.DrawString("Formulário", fonteRodape, Brushes.Black, margemDir - 50, margemInf, New StringFormat())

        Relatorio.HasMorePages = False

    End Sub

    Private Sub btnLocalizar_Click(sender As Object, e As EventArgs) Handles btnLocalizar.Click
        dtCadastro.Clear() 'Limpar o DataTable

        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'Usado o Timer para poder carregar o formulário antes de excluir
        'Verificar se é para excluir o registro comandado pelo browse
        Timer1.Enabled = False
        If g_Comando = "excluir" Then
            Call Excluir_Registro()
        End If

    End Sub

    Private Sub txtCPF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCPF.KeyPress
        If IsNumeric(e.KeyChar) AndAlso txtCPF.TextLength < txtCPF.MaxLength Then
            txtCPF.Text &= e.KeyChar
            txtCPF.SelectionStart = txtCPF.TextLength
            Call formatacpf(txtCPF)
        End If
        e.Handled = True
    End Sub

    Private Sub txtCPF_LostFocus(sender As Object, e As EventArgs) Handles txtCPF.LostFocus
        Dim nCPF As New classValidarCPF_CNPJ

        If Trim(txtCPF.Text) <> "" And txtCPF.Text <> "999.999.999-99" Then
            nCPF.cpf = txtCPF.Text

            If nCPF.cpf = "000.000.000-00" Then 'Not nCPF.isCpfValido() Then
                MsgBox("CPF Inválido!!")
                txtCPF.Focus()
                txtCPF.SelectionStart = 0
                txtCPF.SelectionLength = Len(txtCPF.Text)
            End If
        End If

    End Sub

    Private Sub txtCPF_TextChanged(sender As Object, e As EventArgs) Handles txtCPF.TextChanged

    End Sub

    Private Sub formatacpf(ByVal txtTexto As Object)
        If Len(txtTexto.Text) = 3 Then
            txtTexto.Text = txtTexto.Text & "."
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        ElseIf Len(txtTexto.Text) = 7 Then
            txtTexto.Text = txtTexto.Text & "."
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        ElseIf Len(txtTexto.Text) = 11 Then
            txtTexto.Text = txtTexto.Text & "-"
            txtTexto.SelectionStart = Len(txtTexto.Text) + 1
        End If
    End Sub

    Private Sub txtCPF_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCPF.Validating
        'Me.txtCPF.Text = String.Empty Or
        If Me.txtCPF.Text.Length <> txtCPF.MaxLength And txtCPF.MaxLength > 0 Then
            MsgBox("Informe um valor válido para CPF.(Formato: 999.999.999-99", MsgBoxStyle.Critical)
            e.Cancel = True
        End If
    End Sub

    'metodo OnClosing permite sair do formulário sem preencher os campos texto
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        e.Cancel = False
    End Sub

    Private Sub txtNmColaborador_Leave(sender As Object, e As EventArgs) Handles txtNmColaborador.Leave
        Dim sParteNome As String() = txtNmColaborador.Text.Split(New Char() {" "c})
        Dim sSql, sWhere, sMensagem As String
        Dim nCont As Integer
        Dim dtNomCol As DataTable = New DataTable("EUN003")
        Dim i As Integer

        'Corrigir o Nome
        Dim cont As Integer = 1
        Dim sNome As String = ""
        Dim bMaiuscula As Boolean = False

        txtNmColaborador.Text = LCase(txtNmColaborador.Text)
        For cont = 1 To Len(txtNmColaborador.Text)
            If cont = 1 Then
                sNome += UCase(Microsoft.VisualBasic.Mid(txtNmColaborador.Text, 1, 1))
            Else
                If Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont, 1) = " " Then
                    If Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont + 1, 3) <> "de " And
                            Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont + 1, 3) <> "do " And
                            Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont + 1, 3) <> "da " Then
                        bMaiuscula = True
                    End If
                End If

                If bMaiuscula And Not Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont, 1) = " " Then
                    sNome += UCase(Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont, 1))
                    bMaiuscula = False
                Else
                    sNome += Microsoft.VisualBasic.Mid(txtNmColaborador.Text, cont, 1)
                End If
            End If
        Next
        txtNmColaborador.Text = sNome



        If bIncluir Then
            sWhere = ""
            sSql = ""
            For nCont = 0 To sParteNome.Length - 1
                sWhere += " and UN003_NOMCOL like '%" & sParteNome(nCont) & "%'"
                If nCont > 5 Then Exit For
            Next
            If sWhere <> "" Then
                sSql = "select UN003_CODCOL, UN003_NOMCOL, UN003_CIDADE FROM EUN003 where UN003_SITCOL <> 'I' " & sWhere

                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(sSql, g_ConnectBanco)

                    ' Preencher o DataTable 
                    da.Fill(dtNomCol)
                    If dtNomCol.Rows.Count() > 0 Then
                        sMensagem = "** Encontrado Semelhanças **"
                        For i = 0 To dtNomCol.Rows.Count() - 1
                            sMensagem += Chr(13) & dtNomCol.Rows(i).Item("UN003_CODCOL").ToString & "-" & _
                                dtNomCol.Rows(i).Item("UN003_NOMCOL") & " - " & dtNomCol.Rows(i).Item("UN003_CIDADE")
                            If i > 10 Then
                                Exit For
                            End If
                        Next
                        MsgBox(sMensagem)
                    End If
                    dtNomCol.Clear()
                End Using
            End If
        End If

    End Sub

    Private Sub CarregarGrid()
        Dim dtGrid As DataTable = New DataTable("EUN012")
        Dim sQueryMandato As String

        dtgMandato.DataSource = Nothing

        sQueryMandato = "Select EUN000.UN000_CLAUNI as Classe, EUN000.UN000_NOMUNI as Unidade, " & _
            "EUN016.UN016_DESMDT as Mandato, EUN011.UN011_DESOCP as Descricao, " & _
            "EUN016.UN016_DATINI as Data_Inicio, EUN016.UN016_DATFIN as Data_Final " & _
            "from ((EUN012 INNER JOIN EUN011 ON EUN011.UN011_CODOCP=EUN012.UN012_CODOCP) " & _
            "INNER JOIN EUN016 ON EUN016.UN016_CODMDT=EUN012.UN012_CODMDT) " & _
            "INNER JOIN EUN000 ON EUN000.UN000_CODRED=EUN016.UN016_CODRED " & _
            "WHERE EUN012.UN012_CODCOL=" & txtCodigo.Text & _
            " ORDER BY EUN016.UN016_DATINI DESC"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQueryMandato, g_ConnectBanco)

            ' Preencher o DataTable  
            da.Fill(dtGrid)
        End Using

        dtgMandato.DataSource = dtGrid
        dtgMandato.Columns(0).Width = 85
        dtgMandato.Columns(1).Width = 210
        dtgMandato.Columns(2).Width = 200
        dtgMandato.Columns(3).Width = 230
        dtgMandato.Columns(4).Width = 90
        dtgMandato.Columns(5).Width = 90

    End Sub

    Private Sub btnLocUnidade_Click(sender As Object, e As EventArgs) Handles btnLocUnidade.Click
        'txtColaborador.Text = dlgColaborador.ShowDialog()
        Dim options = New dlgConferencia

        ' Did the user click Save?
        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Yes, so grab the values you want from the dialog here
            sClasseUnidade = LerClasse_Unidade(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6), sNomeUnidade)
            txtUnidade.Text = sClasseUnidade & " - " & sNomeUnidade
        End If
    End Sub

    Private Sub cbSitCol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSitCol.SelectedIndexChanged
        txtDesSit.Enabled = cbSitCol.Text <> "ATIVO"
        lblDesSit.Enabled = cbSitCol.Text <> "ATIVO"
    End Sub

    Private Sub txtNmColaborador_TextChanged(sender As Object, e As EventArgs) Handles txtNmColaborador.TextChanged

    End Sub

End Class