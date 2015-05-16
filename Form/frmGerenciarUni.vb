Imports System.Data.OleDb

Public Class frmGerenciarUni
    Dim dtCM As DataTable = New DataTable("EUN000")
    Dim dtUnidade As DataTable = New DataTable("EUN000")
    Dim nCodUsuario As Integer

    Private Sub frmGerenciarUni_Activated(sender As Object, e As EventArgs) Handles Me.Activated

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

        toolTipText.SetToolTip(Me.btnAlterar, "Alterar")
        toolTipText.SetToolTip(Me.btnMudarEstru, "Mudar de Estrutura")
        toolTipText.SetToolTip(Me.btnExcluir, "Excluir")

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
                If ClassCrypt.Decrypt(g_Login) = "Admin" Then
                    nNivelAcesso = 3
                Else
                    nNivelAcesso = dtLoad.Rows(i).Item("UN013_PERACE") 'Permissao_Unidade(dtLoad.Rows(i).Item("UN000_CLAUNI"), nCodUsuario)
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
        Call CarregarDados()
    End Sub

    Private Sub CarregarDados()
        Dim nPos, nPos_temp As Integer
        Dim sClaUni As String
        Dim cQuery As String

        If IsNothing(Treeview_GerUnidades.SelectedNode) Or txtUnidadeMudar.Text <> "" Then Exit Sub

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

        btnAlterar.Enabled = Treeview_GerUnidades.SelectedNode.ImageIndex > 1 Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"
        'btnMudarEstru.Enabled = (Treeview_GerUnidades.SelectedNode.ImageIndex = 3) And (Microsoft.VisualBasic.Right(sClaUni, 2) <> "00")
        btnMudarEstru.Enabled = False '(Treeview_GerUnidades.SelectedNode.ImageIndex = 3 And Microsoft.VisualBasic.Right(sClaUni, 2) <> "00") Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"
        btnExcluir.Enabled = (Treeview_GerUnidades.SelectedNode.ImageIndex = 3 And Microsoft.VisualBasic.Right(sClaUni, 2) <> "00") Or Trim(UCase(ClassCrypt.Decrypt(g_Login))) = "ADMIN"

    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click, btnExcluir.Click, btnMudarEstru.Click
        Dim fBotao As Button

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
            If txtUnidadeMudar.Text = "" Then
                grpBox_MudarEstru.Visible = True
                txtUnidadeMudar.Text = txtEstruturaUnidade.Text
                txtCodUnidadeMudar.Text = txtCodigo.Text
                txtEstruDestino.Enabled = True
            Else
                txtEstruDestino.Text = txtEstruturaUnidade.Text
                'Verificar se a Estrutura é válida
                If Microsoft.VisualBasic.Right(txtUnidadeMudar.Text, 2) <> "00" Then
                    'Conferência
                    If Microsoft.VisualBasic.Right(txtEstruturaUnidade.Text, 2) = "00" And _
                            Microsoft.VisualBasic.Mid(txtEstruturaUnidade.Text, 7, 2) <> "00" Then
                        'Foi escolhido um CP
                        btnConfirmar.Enabled = True
                    Else
                        MsgBox("A Estrutura destino é inválida")
                        txtEstruDestino.Text = ""
                        btnConfirmar.Enabled = False
                    End If
                    'ElseIf Microsoft.VisualBasic.Right(txtUnidadeMudar.Text, 9) = ".00.00.00" Then
                    'CM
                End If
            End If
        ElseIf fBotao.Name = "btnExcluir" Then
            g_Comando = "excluir"
        ElseIf fBotao.Name = "btnAlterar" Then
            g_Comando = "alterar"
        Else
            g_Comando = "ver"
        End If

        If g_Comando <> "incluir" Then
            'Carregar o campos Chave
            g_Param(1) = txtCodigo.Text
        Else
            g_Param(1) = "INSERT"
            g_Unidade = txtEstruturaUnidade.Text
        End If

        If g_Comando <> "" Then Call CarregarFormCadastro()

        Exit Sub

erro_comandos:
            'Qdo houver erro, não executar nenhuma ação

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
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        grpBox_MudarEstru.Visible = False
        txtUnidadeMudar.Text = ""
        txtEstruDestino.Text = ""
        btnConfirmar.Enabled = False
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Timer1.Enabled = False
        Treeview_GerUnidades.Enabled = False
        Call carregarComponentes()
        Treeview_GerUnidades.Enabled = True

        lblMensagem.Visible = False

    End Sub

    Private Sub Treeview_GerUnidades_Click(sender As Object, e As EventArgs) Handles Treeview_GerUnidades.Click
        CarregarDados()
    End Sub

    Private Sub txtEstruDestino_TextChanged(sender As Object, e As EventArgs) Handles txtEstruDestino.TextChanged
        If Len(txtEstruDestino.Text) = 11 Then btnConfirmar.Enabled = True
    End Sub

    Private Sub frmGerenciarUni_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Treeview_GerUnidades.Size = New Size(589, Me.Size.Height - 60) '112

        'pnlDadosUni.Size = New Size(Me.Size.Width - 60, Me.Size.Height - 60) '112

        'btnAnterior.Location = New System.Drawing.Point(ListView_Browse.Size.Width - 218, ListView_Browse.Size.Height + 58)
        'lblRegistros.Location = New System.Drawing.Point(btnAnterior.Location.X + 64, btnAnterior.Location.Y + 3)
        'btnProximo.Location = New System.Drawing.Point(btnAnterior.Location.X + 161, btnAnterior.Location.Y)
    End Sub
End Class