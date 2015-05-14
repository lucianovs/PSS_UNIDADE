Imports System.Data.OleDb

Public Class frmInstituirUni
    Dim dtCM As DataTable = New DataTable("EUN000")
    Dim dtUnidade As DataTable = New DataTable("EUN000")
    Dim nCodUsuario As Integer
    Dim bAtualizar As Boolean = False

    Private Sub frmInstituirUni_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        If bAtualizar Then
            Call carregarComponentes()
            bAtualizar = False
        End If

    End Sub

    Private Sub frmGerenciarUni_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        If Treeview_GerUnidades.Nodes.Count > 0 Then Call CarregarDados()
    End Sub

    Private Sub frmGerenciarUni_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Application.DoEvents()
        'Call carregarComponentes()
    End Sub

    Private Sub carregarComponentes()
        Dim dtLoad As DataTable = New DataTable("EUN000")

        lblMensagem.Text = "Iniciando a leitura das unidades ..."
        lblMensagem.Refresh()

        nCodUsuario = getCodUsuario(ClassCrypt.Decrypt(g_Login))

        Dim i_node, i As Integer
        Dim cQuery As String
        Dim cParteSelect As String
        Dim cParteWhere As String
        Dim nNivelAcesso As Integer

        Dim nodo_Level1 As TreeNode
        Dim nodoCP As TreeNode

        Dim toolTipText As New ToolTip()

        'Colocar os Helps nos Botões
        toolTipText.AutoPopDelay = 5000
        toolTipText.InitialDelay = 1000
        toolTipText.AutomaticDelay = 300
        toolTipText.ShowAlways = True

        toolTipText.SetToolTip(Me.btnAlterar, "Alterar a Unidade")
        toolTipText.SetToolTip(Me.btnMudarEstru, "Mudar de Estrutura")
        toolTipText.SetToolTip(Me.btnExcluir, "Excluir a Unidade")
        toolTipText.SetToolTip(Me.btnIncluir, "Incluir unidade")

        'Conectar com o Banco
        If Not ConectarBanco() Then
            Me.Close()
        End If

        cParteSelect = "EUN000.UN000_CODRED, EUN000.UN000_NIVUNI, EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI,"
        cParteWhere = " where EUN000.UN000_STAUNI<>'I'"
        If Trim(LCase(ClassCrypt.Decrypt(g_Login))) = "admin" Then
            cParteSelect += " 3 AS UN013_PERACE FROM EUN000"
        Else
            cParteSelect += " EUN013.UN013_PERACE FROM (EUN000 inner join EUN013 on EUN013.UN013_CODUNI=EUN000.UN000_CODRED)"
            cParteWhere += " AND EUN013.UN013_CODUSU=" & nCodUsuario.ToString & _
                " AND UN013_PERACE > 0 "
        End If

        Treeview_GerUnidades.Nodes.Clear()

        'If Treeview_GerUnidades.Nodes.Count > 0 Then Exit Sub
        'Ler as Unidades
        cQuery = "Select " & cParteSelect & cParteWhere & " order by EUN000.UN000_CLAUNI"
        'MsgBox(cQuery)
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtLoad)
            i_node = -1
            For i = 0 To dtLoad.Rows.Count() - 1
                'Verificar Nivel de Acesso
                If LCase(ClassCrypt.Decrypt(g_Login)) = "admin" Then
                    nNivelAcesso = 3
                Else
                    nNivelAcesso = dtLoad.Rows(i).Item("UN013_PERACE")
                    'nNivelAcesso = Permissao_Unidade(dtLoad.Rows(i).Item("UN000_CLAUNI"), nCodUsuario)
                End If

                lblMensagem.Text = "Aguarde, carregando a unidade " & dtLoad.Rows(i).Item("UN000_CLAUNI")
                lblMensagem.Refresh()

                'Verificar se é CM
                If Microsoft.VisualBasic.Mid(dtLoad.Rows(i).Item("UN000_CLAUNI"), 4, 8) = "00.00.00" Then
                    nodo_Level1 = Treeview_GerUnidades.Nodes.Add(dtLoad.Rows(i).Item("UN000_CODRED"), dtLoad.Rows(i).Item("UN000_CLAUNI") & " - " & dtLoad.Rows(i).Item("UN000_NOMUNI"), dtLoad.Rows(i).Item("UN013_PERACE"), 0)
                    i_node = -1
                    Treeview_GerUnidades.Refresh()
                    Application.DoEvents()
                    'Verificar se é CC
                ElseIf Microsoft.VisualBasic.Mid(dtLoad.Rows(i).Item("UN000_CLAUNI"), 7, 5) = "00.00" Then
                    nodo_Level1.Nodes.Add(dtLoad.Rows(i).Item("UN000_CODRED").ToString, dtLoad.Rows(i).Item("UN000_CLAUNI") & " - " & dtLoad.Rows(i).Item("UN000_NOMUNI"), dtLoad.Rows(i).Item("UN013_PERACE"), 0)
                    i_node += 1
                    'Verificar se é CP
                ElseIf Microsoft.VisualBasic.Mid(dtLoad.Rows(i).Item("UN000_CLAUNI"), 10, 2) = "00" Then
                    nodoCP = nodo_Level1.Nodes(i_node).Nodes.Add(dtLoad.Rows(i).Item("UN000_CODRED").ToString, dtLoad.Rows(i).Item("UN000_CLAUNI") & " - " & dtLoad.Rows(i).Item("UN000_NOMUNI"), dtLoad.Rows(i).Item("UN013_PERACE"), 0)
                Else
                    nodoCP.Nodes.Add(dtLoad.Rows(i).Item("UN000_CODRED").ToString, dtLoad.Rows(i).Item("UN000_CLAUNI") & " - " & dtLoad.Rows(i).Item("UN000_NOMUNI"), dtLoad.Rows(i).Item("UN013_PERACE"), 0)
                End If

            Next i
        End Using
        dtLoad.Clear()

    End Sub

    Private Sub Treeview_GerUnidades_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Treeview_GerUnidades.AfterSelect
        If bAtualizar Then
            MsgBox("Foi detectado alterações na estrutura. As unidades precisam ser atualizadas!")
            Call carregarComponentes()
            bAtualizar = False
        End If
        Call CarregarDados()
    End Sub

    Private Sub CarregarDados()
        Dim nPos, nPos_temp As Integer
        Dim sClaUni As String
        Dim cQuery As String

        'If IsNothing(Treeview_GerUnidades.SelectedNode) Or txtUnidadeMudar.Text <> "" Then Exit Sub
        If IsNothing(Treeview_GerUnidades.SelectedNode) Then Exit Sub

        nPos = InStr(1, Treeview_GerUnidades.SelectedNode.FullPath, "\", CompareMethod.Text)

        If nPos > 0 Then 'CM
            nPos_temp = InStr(nPos + 1, Treeview_GerUnidades.SelectedNode.FullPath, "\", CompareMethod.Text)
            If nPos_temp > 0 Then 'CC
                nPos = nPos_temp
                nPos_temp = InStr(nPos + 1, Treeview_GerUnidades.SelectedNode.FullPath, "\", CompareMethod.Text)
                If nPos_temp > 0 Then 'CP
                    nPos = nPos_temp
                End If
            End If
        End If
        sClaUni = Microsoft.VisualBasic.Mid(Treeview_GerUnidades.SelectedNode.FullPath, nPos + 1, 11)

        'Ler os CMs
        cQuery = "Select * from EUN000 where UN000_CLAUNI='" & sClaUni & "' and UN000_STAUNI<>'I' " & _
                 "order by UN000_CLAUNI"
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtCM)
            If dtCM.Rows.Count() > 0 Then
                txtCodigo.Text = dtCM.Rows(0).Item("UN000_CODRED").ToString
                txtEstruturaUnidade.Text = sClaUni
                If IsDBNull(dtCM.Rows(0).Item("UN000_DATFUN")) Then
                    txtDatFun.Text = ""
                Else
                    txtDatFun.Text = Format(dtCM.Rows(0).Item("UN000_DATFUN"), "dd/MM/yyyy")
                End If
                If IsDBNull(dtCM.Rows(0).Item("UN000_DATINS")) Then
                    txtDatInst.Text = ""
                Else
                    txtDatInst.Text = Format(dtCM.Rows(0).Item("UN000_DATINS"), "dd/MM/yyyy")
                End If
                txtBaiUni.Text = If(IsDBNull(dtCM.Rows(0).Item("UN000_BAIUNI")), "", dtCM.Rows(0).Item("UN000_BAIUNI"))
                txtCidUni.Text = If(IsDBNull(dtCM.Rows(0).Item("UN000_CIDUNI")), "", dtCM.Rows(0).Item("UN000_CIDUNI"))
                'txtEndUni.Text = IsDBNull(dtCM.Rows(0).Item("UN000_ENDUNI"), "", dtCM.Rows(0).Item("UN000_ENDUNI"))
                txtEndUni.Text = If(IsDBNull(dtCM.Rows(0).Item("UN000_ENDUNI")), "", dtCM.Rows(0).Item("UN000_ENDUNI"))
                txtEstUni.Text = If(IsDBNull(dtCM.Rows(0).Item("UN000_ESTUNI")), "", dtCM.Rows(0).Item("UN000_ESTUNI"))
            End If
        End Using
        dtCM.Clear()

        If Trim(txtUnidadeMudar.Text) = "" Then
            btnMudarEstru.Enabled = (Treeview_GerUnidades.SelectedNode.ImageIndex > 1 And Microsoft.VisualBasic.Right(sClaUni, 8) <> "00.00.00") Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"
            btnExcluir.Enabled = (Treeview_GerUnidades.SelectedNode.ImageIndex > 1 And Microsoft.VisualBasic.Right(sClaUni, 2) <> "00") Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"
            btnIncluir.Enabled = ((Treeview_GerUnidades.SelectedNode.ImageIndex > 1) Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN") And _
                (getTipoUnidade(txtEstruturaUnidade.Text) = "CM" Or getTipoUnidade(txtEstruturaUnidade.Text) = "CC" Or _
                getTipoUnidade(txtEstruturaUnidade.Text) = "CP")
            btnAlterar.Enabled = Treeview_GerUnidades.SelectedNode.ImageIndex > 1 Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"
            'btnMudarEstru.Enabled = (Treeview_GerUnidades.SelectedNode.ImageIndex = 3) And (Microsoft.VisualBasic.Right(sClaUni, 2) <> "00")
        End If
    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click, btnExcluir.Click, btnMudarEstru.Click
        Dim fBotao As Button
        Dim bOk As Boolean = True
        fBotao = sender

        'Limpar os dados da tela
        'txtDatFun.Text = ""
        'txtDatInst.Text = ""
        'txtEndUni.Text = ""
        'txtBaiUni.Text = ""
        'txtCidUni.Text = ""
        'txtEstUni.Text = ""

        'Inicializar os Parâmetros da Chave (até 3 chaves)
        g_Param(1) = ""
        g_Param(2) = ""
        g_Param(3) = ""

        If fBotao.Name = "btnMudarEstru" Then
            g_Comando = ""
            btnExcluir.Enabled = False
            btnAlterar.Enabled = False
            btnIncluir.Enabled = False
            btnExcluir.Enabled = False
            If txtUnidadeMudar.Text = "" Then
                grpBox_MudarEstru.Visible = True
                txtUnidadeMudar.Text = txtEstruturaUnidade.Text
                txtCodUnidadeMudar.Text = txtCodigo.Text
                txtEstruDestino.Enabled = True
            Else
                If Not (Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 2) = "00") Then
                    bOk = False
                ElseIf (getTipoUnidade(txtUnidadeMudar.Text) = "CF" And getTipoUnidade(txtEstruturaUnidade.Text) <> "CP") Then
                    bOk = False
                ElseIf (getTipoUnidade(txtUnidadeMudar.Text) = "CP" And getTipoUnidade(txtEstruturaUnidade.Text) <> "CC") Then
                    bOk = False
                ElseIf (getTipoUnidade(txtUnidadeMudar.Text) = "CC" And getTipoUnidade(txtEstruturaUnidade.Text) <> "CM") Then
                    bOk = False
                ElseIf (Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 2) = "00" And _
                        Microsoft.VisualBasic.Left(txtEstruturaUnidade.Text, 8) = Microsoft.VisualBasic.Left(txtUnidadeMudar.Text, 8)) Then
                    bOk = False
                ElseIf (Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 5) = "00.00" And _
                        Microsoft.VisualBasic.Left(txtEstruturaUnidade.Text, 5) = Microsoft.VisualBasic.Left(txtUnidadeMudar.Text, 5)) Then
                    bOk = False
                ElseIf (Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 8) = "00.00.00" And _
                        Microsoft.VisualBasic.Left(txtEstruturaUnidade.Text, 2) <> Microsoft.VisualBasic.Left(txtUnidadeMudar.Text, 2)) Then
                    bOk = False
                End If
                If bok Then
                    txtEstruDestino.Text = txtEstruturaUnidade.Text
                    btnConfirmar.Enabled = True
                Else
                    MsgBox("A Estrutura destino é inválida")
                    txtEstruDestino.Text = ""
                    btnConfirmar.Enabled = False
                End If
            End If
        ElseIf fBotao.Name = "btnExcluir" Then
            g_Comando = "excluir"
        ElseIf fBotao.Name = "btnAlterar" Then
            g_Comando = "alterar"
        Else
            g_Comando = "ver"
        End If

        'Carregar o campos Chave
        g_Param(1) = txtCodigo.Text

        If g_Comando <> "" Then Call CarregarFormCadastro()

        Exit Sub

erro_comandos:
        'Qdo houver erro, executar nenhuma ação

    End Sub

    Private Sub CarregarFormCadastro()
        'Carregar o Formulário do Cadastro
        Dim frmType As Type = Type.GetType("UNIDADES.frmUnidades")
        Dim frm As Object = Activator.CreateInstance(frmType)

        CType(frm, Form).Tag = Me.Tag 'nível de acesso
        CType(frm, Form).Text = "FORMULÁRIO - " & Me.Text
        frmType.InvokeMember("Show", Reflection.BindingFlags.InvokeMethod, Nothing, frm, Nothing)
        'CType(frm, Form).MdiParent = mdiDesktop
        'CType(frm, Form).Modal = True

        'Cadastro_Form.MdiParent = mdiPrincipal
        'Cadastro_Form.Tag = Me.Tag 'nível de acesso
        'Cadastro_Form.Show()

        bAtualizar = True

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        grpBox_MudarEstru.Visible = False
        txtUnidadeMudar.Text = ""
        txtEstruDestino.Text = ""
        btnConfirmar.Enabled = False

    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Dim sSql As String
        Dim sProxSeq, sMensagem As String
        Dim dtAlterCM As DataTable = New DataTable("EUN000")
        Dim cmd As OleDbCommand
        'Dim sCMseq, sCCseq, sCPseq As String
        Dim nPos As Integer

        'sCMseq = Microsoft.VisualBasic.Left(txtEstruDestino.Text, 2)
        'sCCseq = Microsoft.VisualBasic.Mid(txtEstruDestino.Text, 4, 2)
        'sCPseq = Microsoft.VisualBasic.Mid(txtEstruDestino.Text, 7, 2)

        'Gravar alterações 
        If Microsoft.VisualBasic.Right(txtUnidadeMudar.Text, 6) = ".00.00" And _
                Microsoft.VisualBasic.Right(txtEstruDestino.Text, 9) = ".00.00.00" Then
            'CC
            npos = 5
            sProxSeq = ProxSeq_Unidade("CC", txtEstruDestino.Text)
            'Alterar a classe do CC e toda a sua arvore
            sSql = "UPDATE EUN000 SET UN000_CLAUNI ='" & Microsoft.VisualBasic.Left(sProxSeq, 6) & "' + right(UN000_CLAUNI, 5)" & _
                " where left(UN000_CLAUNI,6) = '" & Microsoft.VisualBasic.Left(txtUnidadeMudar.Text, 6) & "'"
            cmd = New OleDbCommand(sSql, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
            End Try
        ElseIf Microsoft.VisualBasic.Right(txtUnidadeMudar.Text, 3) = ".00" And _
                Microsoft.VisualBasic.Right(txtEstruDestino.Text, 6) = ".00.00" Then
            'Alterar a classe do CP e toda a sua arvore
            nPos = 8
            sProxSeq = ProxSeq_Unidade("CP", txtEstruDestino.Text)

            sSql = "UPDATE EUN000 SET UN000_CLAUNI='" & Microsoft.VisualBasic.Left(sProxSeq, 9) & "' + right(UN000_CLAUNI, 2)" & _
                " where left(UN000_CLAUNI,9) = '" & Microsoft.VisualBasic.Left(txtUnidadeMudar.Text, 9) & "'"
            cmd = New OleDbCommand(sSql, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
            End Try
        ElseIf Microsoft.VisualBasic.Right(txtUnidadeMudar.Text, 3) <> ".00" And _
                Microsoft.VisualBasic.Right(txtEstruDestino.Text, 3) = ".00" Then
            'CF
            nPos = 11
            sProxSeq = ProxSeq_Unidade("CF", txtEstruDestino.Text)
            sSql = "UPDATE EUN000 SET UN000_CLAUNI='" & sProxSeq & "'" & _
                " where UN000_CLAUNI = '" & txtUnidadeMudar.Text & "'"
            cmd = New OleDbCommand(sSql, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
            End Try
        Else
            sProxSeq = ""
        End If

        If sProxSeq <> "" Then
            'Gravar o Log para registrar as alterações
            sSql = "Select * from EUN000 where left(UN000_CLAUNI," & nPos.ToString & ") = '" & Microsoft.VisualBasic.Left(sProxSeq, nPos) & "'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(sSql, g_ConnectBanco)
                da.Fill(dtAlterCM)
                For x = 0 To dtAlterCM.Rows.Count() - 1
                    'Gravar o Log da Mudança
                    sMensagem = ""
                    If Not Gravar_LogUnidade(nCodUsuario.ToString, dtAlterCM.Rows(x).Item("UN000_CODRED"), "UN000_CLAUNI", txtUnidadeMudar.Text, sProxSeq, sMensagem) Then
                        MsgBox(sMensagem)
                    End If
                    '************************
                Next
                dtAlterCM.Clear()
            End Using

            Call carregarComponentes()

        End If
        MsgBox("Processo Finalizado. !!!!")

        grpBox_MudarEstru.Visible = False
        txtUnidadeMudar.Text = ""
        txtEstruDestino.Text = ""
        btnConfirmar.Enabled = False
        btnMudarEstru.Enabled = False

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Timer1.Enabled = False
        Treeview_GerUnidades.Enabled = False
        Call carregarComponentes()
        Treeview_GerUnidades.Enabled = True

        lblMensagem.Visible = False

    End Sub

    Private Sub Treeview_GerUnidades_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles Treeview_GerUnidades.BeforeSelect
        'txtUnidadeMudar.Text = ""
    End Sub

    Private Sub Treeview_GerUnidades_Click(sender As Object, e As EventArgs) Handles Treeview_GerUnidades.Click
        CarregarDados()
    End Sub

    Private Sub txtEstruDestino_TextChanged(sender As Object, e As EventArgs) Handles txtEstruDestino.TextChanged
        If Len(txtEstruDestino.Text) = 11 Then btnConfirmar.Enabled = True
    End Sub

    Private Sub frmGerenciarUni_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Treeview_GerUnidades.Size = New Size(443, Me.Size.Height - 46) '112

        pnlDadosUni.Size = New Size(Me.Size.Width - 499, Me.Size.Height - 119) '112
        'btnAnterior.Location = New System.Drawing.Point(ListView_Browse.Size.Width - 218, ListView_Browse.Size.Height + 58)
        'lblRegistros.Location = New System.Drawing.Point(btnAnterior.Location.X + 64, btnAnterior.Location.Y + 3)
        'btnProximo.Location = New System.Drawing.Point(btnAnterior.Location.X + 161, btnAnterior.Location.Y)
    End Sub

    Private Sub btnIncluir_Click(sender As Object, e As EventArgs) Handles btnIncluir.Click
        Dim cmd As OleDbCommand
        Dim csql As String
        Dim sProxSeq_Unidade As String
        Dim nProxCod_Unidade As Double
        Dim sTipoUnidade As String
        Dim sMensagem As String

        sTipoUnidade = getTipoUnidade(txtEstruturaUnidade.Text)
        If sTipoUnidade = "CM" Then
            sTipoUnidade = "CC"
        ElseIf sTipoUnidade = "CC" Then
            sTipoUnidade = "CP"
        ElseIf sTipoUnidade = "CP" Then
            sTipoUnidade = "CF"
        End If

        sProxSeq_Unidade = ProxSeq_Unidade(sTipoUnidade, txtEstruturaUnidade.Text)
        nProxCod_Unidade = Double.Parse(ProxCodChave("EUN000", "UN000_CODRED"))

        'Incluir uma Unidade dentro da Estrutura 
        csql = "INSERT INTO EUN000(UN000_CODRED, UN000_NUMREG, UN000_CLAUNI, UN000_NOMUNI, UN000_DATFUN, UN000_CNPUNI, UN000_ENDUNI, UN000_BAIUNI, UN000_CEPUNI, "
        csql += "UN000_CIDUNI, UN000_ESTUNI, UN000_NACUNI, UN000_DIOUNI, UN000_BCOUNI, UN000_AGEUNI, UN000_CCOUNI, UN000_TITUNI, UN000_OBSCCO, UN000_FREREU, "
        csql += "UN000_APROCP, UN000_APROCC, UN000_APROCM, UN000_APROCN, UN000_APROCG, UN000_DATINS, UN000_DATENV, "
        csql += "UN000_STAUNI, UN000_NIVUNI)"
        csql += " values (" & nProxCod_Unidade.ToString & ",0,'" & sProxSeq_Unidade & "'"
        csql += ", 'NOVA', '" & FormatarData("01/01/1900") & "', ''"
        csql += ", '', '', '', '', ''"
        csql += ", '', '', '', '', ''"
        csql += ", '', '', ''"
        csql += ", '" & FormatarData("01/01/1900") & "', '" & FormatarData("01/01/1900") & "'"
        csql += ", '" & FormatarData("01/01/1900") & "', '" & FormatarData("01/01/1900") & "'"
        csql += ", '" & FormatarData("01/01/1900") & "', '" & FormatarData("01/01/1900") & "'"
        csql += ", '" & FormatarData("01/01/1900") & "','A'," & getNivelUnidade(sProxSeq_Unidade).ToString & ")"

        cmd = New OleDbCommand(csql, g_ConnectBanco)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            'Gravar o Log da Inclusão
            sMensagem = ""
            If Not Gravar_LogUnidade(nCodUsuario.ToString, nProxCod_Unidade, "UN000_CLAUNI", "INCLUSÃO", sProxSeq_Unidade, sMensagem) Then
                MsgBox(sMensagem)
            End If
            '************************
        End Try

        'Atualizar o Nivel de Acesso da Unidade
        Call GravarPermissao_Unidade(sProxSeq_Unidade)

        'Carregar a Estrutura
        Call carregarComponentes()

    End Sub
End Class