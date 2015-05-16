Imports System.Data.OleDb

Public Class frmUnidadeUsu
    Dim dtUsuario As DataTable = New DataTable("ESI000")
    Dim dtUnidade As DataTable = New DataTable("EUN000")
    Dim dtUsuUni As DataTable = New DataTable("EUN013")

    Dim nCodUsu As Integer

    Private Sub frmUnidadeUsu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cQuery_Usuario As String

        'Carregar o Combo de Grupos
        cQuery_Usuario = "SELECT * FROM ESI000 WHERE SI000_CODUSU <> 1 order by SI000_LGIUSU" 'Não carregar o administrador
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery_Usuario, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)
            If dtUsuario.Rows.Count() > 0 Then
                For x = 0 To dtUsuario.Rows.Count() - 1
                    cbUsuario.Items.Add(dtUsuario.Rows(x).Item("SI000_LGIUSU"))
                Next
            Else
                cbUsuario.Items.Clear()
            End If
        End Using
        dtUsuario.Clear()
        '**************

    End Sub

    Private Sub cbUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUsuario.SelectedIndexChanged
        Call CarregarLst_CM()
    End Sub

    Private Sub CarregarLst_CM()
        Dim cQuery_Unidade As String
        Dim sItemLista As String

        btnAtualizar_CM.Enabled = cbUsuario.Text <> ""
        lstCM.Enabled = cbUsuario.Text <> ""

        lstCC.Items.Clear()
        lstCP.Items.Clear()
        lstCF.Items.Clear()
        lstCC.Enabled = False
        lstCP.Enabled = False
        lstCF.Enabled = False

        lblCC.Text = "CM"
        lblCP.Text = "CC"
        lblCF.Text = "CP"

        If cbUsuario.Text <> "" Then
            nCodUsu = getCodUsuario(cbUsuario.Text)
            lstCM.Items.Clear()

            If nCodUsu = 0 Then Exit Sub

            'Carregar a lista dos CMs
            cQuery_Unidade = "SELECT EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI, " & _
                    "(SELECT EUN013.UN013_PERACE FROM EUN013 WHERE EUN013.UN013_CODUNI=EUN000.UN000_CODRED " & _
                    "AND EUN013.UN013_CODUSU=" & nCodUsu.ToString & ") AS PERACE " & _
                "FROM EUN000 WHERE EUN000.UN000_NIVUNI=0 order by EUN000.UN000_CLAUNI"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery_Unidade, g_ConnectBanco)
                dtUnidade.Clear()
                ' Preencher o DataTable 
                da.Fill(dtUnidade)
                If dtUnidade.Rows.Count() > 0 Then
                    For x = 0 To dtUnidade.Rows.Count() - 1
                        sItemLista = IIf(IsDBNull(dtUnidade.Rows(x).Item("UN000_CLAUNI")), "", dtUnidade.Rows(x).Item("UN000_CLAUNI"))
                        sItemLista += CarregarPermissao(IIf(IsDBNull(dtUnidade.Rows(x).Item("PERACE")), -1, dtUnidade.Rows(x).Item("PERACE")), "")
                        sItemLista += dtUnidade.Rows(x).Item("UN000_NOMUNI")
                        lstCM.Items.Add(sItemLista)
                    Next
                Else
                    dtUnidade.Clear()
                End If
            End Using
            dtUnidade.Clear()
            '**************
        End If

    End Sub

    Private Sub lstCM_DoubleClick(sender As Object, e As EventArgs) Handles lstCM.DoubleClick
        Dim sPermissao As String
        Dim sLinha As String

        sPermissao = Microsoft.VisualBasic.Mid(lstCM.SelectedItem.ToString, 13, 3)

        sLinha = Microsoft.VisualBasic.Left(lstCM.SelectedItem.ToString, 11)
        If sPermissao = "---" Then
            sLinha += "[VER]"
        ElseIf sPermissao = "VER" Then
            sLinha += "[ALT]"
        ElseIf sPermissao = "ALT" Then
            sLinha += "[GER]"
        ElseIf sPermissao = "GER" Then
            sLinha += "[---]"
        End If

        sLinha += Microsoft.VisualBasic.Mid(lstCM.SelectedItem.ToString, 17, Len(lstCM.SelectedItem.ToString) - 16)
        'lstCM.SelectedItem.text = sLinha

        'Dim curItem As String = lstCM.SelectedItem.ToString()
        'Dim index As Integer = lstCM.FindString(curItem)

        lstCM.Items(lstCM.SelectedIndex) = sLinha

        lstCM.SelectedItem = sLinha

        btnAtualizar_CM.Enabled = True
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub lstCM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCM.SelectedIndexChanged

        CarregarLst_CC()
        btnAtualizar_CM.Enabled = True
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub CarregarLst_CC()
        Dim cQuery_Unidade As String
        Dim sItemLista As String
        Dim sPermissao As String

        If IsNothing(lstCM.SelectedItem) Then Exit Sub
        lblCC.Text = "CM: " & Microsoft.VisualBasic.Left(lstCM.SelectedItem.ToString, 11)
        sPermissao = Microsoft.VisualBasic.Mid(lstCM.SelectedItem.ToString, 13, 3)

        nCodUsu = getCodUsuario(cbUsuario.Text)

        lstCC.Items.Clear()

        'Carregar o ListView dos CCs
        cQuery_Unidade = "SELECT EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI, " & _
                "(SELECT EUN013.UN013_PERACE FROM EUN013 WHERE EUN013.UN013_CODUNI=EUN000.UN000_CODRED " & _
                "AND EUN013.UN013_CODUSU=" & nCodUsu.ToString & ") AS PERACE " & _
            "FROM EUN000 WHERE EUN000.UN000_NIVUNI=1 AND LEFT(EUN000.UN000_CLAUNI,2) = '" & _
            Microsoft.VisualBasic.Left(lstCM.SelectedItem.ToString, 2) & "' order by EUN000.UN000_CLAUNI"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery_Unidade, g_ConnectBanco)

            dtUnidade.Clear()
            ' Preencher o DataTable 
            da.Fill(dtUnidade)
            If dtUnidade.Rows.Count() > 0 Then
                For x = 0 To dtUnidade.Rows.Count() - 1
                    sItemLista = IIf(IsDBNull(dtUnidade.Rows(x).Item("UN000_CLAUNI")), "", dtUnidade.Rows(x).Item("UN000_CLAUNI"))
                    sItemLista += CarregarPermissao(IIf(IsDBNull(dtUnidade.Rows(x).Item("PERACE")), -1, dtUnidade.Rows(x).Item("PERACE")), "")
                    sItemLista += dtUnidade.Rows(x).Item("UN000_NOMUNI")
                    lstCC.Items.Add(sItemLista)
                Next
            Else
                dtUnidade.Clear()
            End If
        End Using
        dtUnidade.Clear()
        '**************

        lstCC.Enabled = sPermissao <> "---"
        lblCC.Enabled = lstCC.Enabled
        lstCP.Items.Clear()
        lstCF.Items.Clear()

    End Sub

    Private Sub lstCC_DoubleClick(sender As Object, e As EventArgs) Handles lstCC.DoubleClick
        Dim sPermissao As String
        Dim sLinha As String

        sPermissao = Microsoft.VisualBasic.Mid(lstCC.SelectedItem.ToString, 13, 3)

        sLinha = Microsoft.VisualBasic.Left(lstCC.SelectedItem.ToString, 11)
        If sPermissao = "---" Then
            sLinha += "[VER]"
        ElseIf sPermissao = "VER" Then
            sLinha += "[ALT]"
        ElseIf sPermissao = "ALT" Then
            sLinha += "[GER]"
        ElseIf sPermissao = "GER" Then
            sLinha += "[---]"
        End If

        sLinha += Microsoft.VisualBasic.Mid(lstCc.SelectedItem.ToString, 17, Len(lstCc.SelectedItem.ToString) - 16)

        lstCC.Items(lstCC.SelectedIndex) = sLinha

        lstCC.SelectedItem = sLinha

        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = True
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub lstCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCC.SelectedIndexChanged
        Call CarregarLst_CP()
        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = True
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub CarregarLst_CP()
        Dim cQuery_Unidade As String
        Dim sItemLista As String
        Dim sPermissao As String

        nCodUsu = getCodUsuario(cbUsuario.Text)

        If IsNothing(lstCC.SelectedItem) Then Exit Sub
        lblCP.Text = "CC: " & Microsoft.VisualBasic.Left(lstCC.SelectedItem.ToString, 11)
        sPermissao = Microsoft.VisualBasic.Mid(lstCC.SelectedItem.ToString, 13, 3)

        lstCP.Items.Clear()

        'Carregar o Combo de Grupos
        cQuery_Unidade = "SELECT EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI, " & _
                "(SELECT EUN013.UN013_PERACE FROM EUN013 WHERE EUN013.UN013_CODUNI=EUN000.UN000_CODRED " & _
                "AND EUN013.UN013_CODUSU=" & nCodUsu.ToString & ") AS PERACE " & _
            "FROM EUN000 WHERE EUN000.UN000_NIVUNI=2 AND LEFT(EUN000.UN000_CLAUNI,5) = '" & _
            Microsoft.VisualBasic.Left(lstCC.SelectedItem.ToString, 5) & "' order by EUN000.UN000_CLAUNI"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery_Unidade, g_ConnectBanco)

            dtUnidade.Clear()
            ' Preencher o DataTable 
            da.Fill(dtUnidade)
            If dtUnidade.Rows.Count() > 0 Then
                For x = 0 To dtUnidade.Rows.Count() - 1
                    sItemLista = dtUnidade.Rows(x).Item("UN000_CLAUNI")
                    sItemLista += CarregarPermissao(IIf(IsDBNull(dtUnidade.Rows(x).Item("PERACE")), -1, dtUnidade.Rows(x).Item("PERACE")), "")
                    sItemLista += dtUnidade.Rows(x).Item("UN000_NOMUNI")
                    lstCP.Items.Add(sItemLista)
                Next
            Else
                dtUnidade.Clear()
            End If
        End Using
        dtUnidade.Clear()
        '**************

        lstCP.Enabled = sPermissao <> "---"
        lblCP.Enabled = lstCP.Enabled

        lstCF.Items.Clear()

    End Sub

    Private Sub lstCP_DoubleClick(sender As Object, e As EventArgs) Handles lstCP.DoubleClick
        Dim sPermissao As String
        Dim sLinha As String

        sPermissao = Microsoft.VisualBasic.Mid(lstCP.SelectedItem.ToString, 13, 3)

        sLinha = Microsoft.VisualBasic.Left(lstCP.SelectedItem.ToString, 11)
        If sPermissao = "---" Then
            sLinha += "[VER]"
        ElseIf sPermissao = "VER" Then
            sLinha += "[ALT]"
        ElseIf sPermissao = "ALT" Then
            sLinha += "[GER]"
        ElseIf sPermissao = "GER" Then
            sLinha += "[---]"
        End If

        sLinha += Microsoft.VisualBasic.Mid(lstCP.SelectedItem.ToString, 17, Len(lstCP.SelectedItem.ToString) - 16)

        lstCP.Items(lstCP.SelectedIndex) = sLinha

        lstCP.SelectedItem = sLinha

        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = True
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub lstCP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCP.SelectedIndexChanged
        Call CarregarLst_CF()
        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = True
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = False

    End Sub

    Private Sub CarregarLst_CF()
        Dim cQuery_Unidade As String
        Dim sItemLista As String
        Dim sPermissao As String

        nCodUsu = getCodUsuario(cbUsuario.Text)

        If IsNothing(lstCP.SelectedItem) Then Exit Sub
        lblCF.Text = "CP: " & Microsoft.VisualBasic.Left(lstCP.SelectedItem.ToString, 11)
        sPermissao = Microsoft.VisualBasic.Mid(lstCP.SelectedItem.ToString, 13, 3)

        lstCF.Items.Clear()

        'Carregar o Combo de Grupos
        cQuery_Unidade = "SELECT EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI, " & _
                "(SELECT EUN013.UN013_PERACE FROM EUN013 WHERE EUN013.UN013_CODUNI=EUN000.UN000_CODRED " & _
                "AND EUN013.UN013_CODUSU=" & nCodUsu.ToString & ") AS PERACE " & _
            "FROM EUN000 WHERE EUN000.UN000_NIVUNI=3 AND LEFT(EUN000.UN000_CLAUNI,8) = '" & _
            Microsoft.VisualBasic.Left(lstCP.SelectedItem.ToString, 8) & "' order by EUN000.UN000_CLAUNI"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery_Unidade, g_ConnectBanco)

            dtUnidade.Clear()
            ' Preencher o DataTable 
            da.Fill(dtUnidade)
            If dtUnidade.Rows.Count() > 0 Then
                For x = 0 To dtUnidade.Rows.Count() - 1
                    sItemLista = dtUnidade.Rows(x).Item("UN000_CLAUNI")
                    sItemLista += CarregarPermissao(IIf(IsDBNull(dtUnidade.Rows(x).Item("PERACE")), -1, dtUnidade.Rows(x).Item("PERACE")), "")
                    sItemLista += dtUnidade.Rows(x).Item("UN000_NOMUNI")
                    lstCF.Items.Add(sItemLista)
                Next
            Else
                dtUnidade.Clear()
            End If
        End Using
        dtUnidade.Clear()
        '**************

        'lstCF.Enabled = sPermissao = "VER" Or sPermissao = "ALT"
        'lblCF.Enabled = sPermissao = "VER" Or sPermissao = "ALT"
        lstCF.Enabled = sPermissao <> "---"
        lblCF.Enabled = lstCF.Enabled

    End Sub

    Private Sub lstCF_Click(sender As Object, e As EventArgs) Handles lstCF.Click
        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = True
    End Sub

    Private Sub lstCF_DoubleClick(sender As Object, e As EventArgs) Handles lstCF.DoubleClick
        Dim sPermissao As String
        Dim sLinha As String

        sPermissao = Microsoft.VisualBasic.Mid(lstCF.SelectedItem.ToString, 13, 3)

        sLinha = Microsoft.VisualBasic.Left(lstCF.SelectedItem.ToString, 11)
        If sPermissao = "---" Then
            sLinha += "[VER]"
        ElseIf sPermissao = "VER" Then
            sLinha += "[ALT]"
        ElseIf sPermissao = "ALT" Then
            sLinha += "[GER]"
        ElseIf sPermissao = "GER" Then
            sLinha += "[---]"
        End If

        sLinha += Microsoft.VisualBasic.Mid(lstCF.SelectedItem.ToString, 17, Len(lstCF.SelectedItem.ToString) - 16)

        lstCF.Items(lstCF.SelectedIndex) = sLinha

        lstCF.SelectedItem = sLinha

        btnAtualizar_CM.Enabled = False
        btnAtualizar_CP.Enabled = False
        btnAtualizar_CC.Enabled = False
        btnAtualizar_CF.Enabled = True

    End Sub

    Private Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar_CM.Click, btnAtualizar_CC.Click, btnAtualizar_CP.Click, btnAtualizar_CF.Click
        Dim IndexCM As Integer
        Dim IndexCC As Integer
        Dim IndexCP As Integer
        Dim IndexCF As Integer
        Dim sPermissao As String
        Dim btnComando As Button

        btnComando = sender

        lblMensagem.Text = "Processando ..."
        lblMensagem.Refresh()
        Application.DoEvents()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If btnComando.Name = "btnAtualizar_CM" Then
            'Ler CMs
            For IndexCM = 0 To lstCM.Items.Count - 1
                'Identificar se houve alteração
                sPermissao = Microsoft.VisualBasic.Mid(lstCM.Items(IndexCM).ToString, 12, 5)
                If Microsoft.VisualBasic.Left(sPermissao, 1) = "[" Then
                    Call TratarPermissao(Microsoft.VisualBasic.Left(lstCM.Items(IndexCM).ToString, 11), sPermissao)
                End If
            Next
            Call CarregarLst_CM()
        ElseIf btnComando.Name = "btnAtualizar_CC" Then
            'Ler CCs
            If lstCC.Items.Count > 0 Then
                For IndexCC = 0 To lstCC.Items.Count - 1
                    'Identificar se houve alteração
                    sPermissao = Microsoft.VisualBasic.Mid(lstCC.Items(IndexCC).ToString, 12, 5)
                    If Microsoft.VisualBasic.Left(sPermissao, 1) = "[" Then
                        Call TratarPermissao(Microsoft.VisualBasic.Left(lstCC.Items(IndexCC).ToString, 11), sPermissao)
                    End If
                Next
            End If
            Call CarregarLst_CC()
        ElseIf btnComando.Name = "btnAtualizar_CP" Then
            'Ler CPs
            If lstCP.Items.Count > 0 Then
                For IndexCP = 0 To lstCP.Items.Count - 1
                    'Identificar se houve alteração
                    sPermissao = Microsoft.VisualBasic.Mid(lstCP.Items(IndexCP).ToString, 12, 5)
                    If Microsoft.VisualBasic.Left(sPermissao, 1) = "[" Then
                        Call TratarPermissao(Microsoft.VisualBasic.Left(lstCP.Items(IndexCP).ToString, 11), sPermissao)
                    End If
                Next
            End If
            Call CarregarLst_CP()
        ElseIf btnComando.Name = "btnAtualizar_CF" Then
            'Ler CFs
            If lstCF.Items.Count > 0 Then
                For IndexCF = 0 To lstCF.Items.Count - 1
                    'Identificar se houve alteração
                    sPermissao = Microsoft.VisualBasic.Mid(lstCF.Items(IndexCF).ToString, 12, 5)
                    If Microsoft.VisualBasic.Left(sPermissao, 1) = "[" Then
                        Call TratarPermissao(Microsoft.VisualBasic.Left(lstCF.Items(IndexCF).ToString, 11), sPermissao)
                    End If
                Next
            End If
            Call CarregarLst_CF()
        End If

        'cbUsuario.Text = ""
        lblMensagem.Text = ""
        lblMensagem.Refresh()

        Me.Cursor = System.Windows.Forms.Cursors.Default
        MsgBox("Atualização Concluída !!")

    End Sub

    Private Sub TratarPermissao(pUnidade As String, pPerGrv As String)
        Dim nCodUnidade As Double
        Dim nNivelPer As Integer
        Dim nNivelUni As Integer
        Dim y As Integer
        Dim cQuery_Uni As String

        'Ler o código da Unidade
        nCodUnidade = LerCod_Unidade(pUnidade)
        'Pegar o Nivel de Permissao
        nNivelPer = CarregarNivel(Microsoft.VisualBasic.Mid(pPerGrv, 2, 3))

        'Gravar o Primeiro item
        If GravarPermissao(nCodUnidade, nNivelPer) = 1 Then Exit Sub

        If (nNivelPer = 3 Or nNivelPer = 0) And Microsoft.VisualBasic.Right(pUnidade, 2) = "00" Then
            'Gravar nas demais árvores da Estrutura
            If Microsoft.VisualBasic.Right(pUnidade, 8) = "00.00.00" Then
                nNivelUni = 0
            ElseIf Microsoft.VisualBasic.Right(pUnidade, 5) = "00.00" Then
                nNivelUni = 1
            ElseIf Microsoft.VisualBasic.Right(pUnidade, 2) = "00" Then
                nNivelUni = 2
            End If


            dtUnidade.Clear()
            cQuery_Uni = "Select UN000_CODRED From EUN000 where "
            If nNivelUni = 0 Then
                cQuery_Uni += "left(UN000_CLAUNI,2)='" & Microsoft.VisualBasic.Left(pUnidade, 2) & "' "
            ElseIf nNivelUni = 1 Then
                cQuery_Uni += "left(UN000_CLAUNI,5)='" & Microsoft.VisualBasic.Left(pUnidade, 5) & "' "
            ElseIf nNivelUni = 2 Then
                cQuery_Uni += "left(UN000_CLAUNI,8)='" & Microsoft.VisualBasic.Left(pUnidade, 8) & "' "
            End If
            cQuery_Uni += "and UN000_CLAUNI <> '" & pUnidade & "'"
            'cQuery_Uni += " and UN000_NIVUNI=" & x.ToString

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery_Uni, g_ConnectBanco)

                dtUnidade.Clear()
                ' Preencher o DataTable 
                da.Fill(dtUnidade)
                If dtUnidade.Rows.Count() > 0 Then
                    For y = 0 To dtUnidade.Rows.Count() - 1
                        'If GravarPermissao(dtUnidade.Rows(y).Item("UN000_CODRED"), IIf(nNivelPer = 3, 2, 0)) = 1 Then 'Gravar ALT
                        If GravarPermissao(dtUnidade.Rows(y).Item("UN000_CODRED"), nNivelPer) = 1 Then 'Gravar a mesma permissão do OWNER
                            Exit For
                        End If
                    Next y
                End If
            End Using

        End If

    End Sub

    Private Function GravarPermissao(fUnidade As Double, fPerGrv As Integer) As Integer
        Dim cQuery_UsuUni As String
        Dim cmd As OleDbCommand

        lblMensagem.Text = "Processando ... " & fUnidade.ToString
        lblMensagem.Refresh()
        Application.DoEvents()

        'Verificar se o Vinculo já existe
        cQuery_UsuUni = "SELECT UN013_PERACE FROM EUN013 WHERE UN013_CODUSU=" & nCodUsu.ToString & _
                " AND UN013_CODUNI = " & fUnidade.ToString

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery_UsuUni, g_ConnectBanco)

            'Preencher o DataTable 
            da.Fill(dtUsuUni)
        End Using


        If dtUsuUni.Rows.Count() > 0 Then
            If fPerGrv <> dtUsuUni.Rows(0).Item("UN013_PERACE") Then
                cQuery_UsuUni = "UPDATE EUN013 SET UN013_PERACE = " & fPerGrv.ToString & _
                    " where UN013_CODUSU=" & nCodUsu.ToString & " and UN013_CODUNI = " & _
                    fUnidade.ToString
            Else
                cQuery_UsuUni = ""
            End If
        Else
            'Inserir o Vinculo
            cQuery_UsuUni = "INSERT INTO EUN013 (UN013_CODUSU, UN013_CODUNI, UN013_PERACE) " & _
                " values (" & nCodUsu.ToString & "," & fUnidade.ToString & "," & fPerGrv.ToString & ")"
        End If
        dtUsuUni.Clear()

        If cQuery_UsuUni <> "" Then
            'Gravar no banco
            cmd = New OleDbCommand(cQuery_UsuUni, g_ConnectBanco)

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
                Return 1
            Finally

            End Try
        End If

        Return 0

    End Function

    Private Function CarregarPermissao(fNivel As Integer, fPermissao As String) As String
        If fNivel = -1 Then
            If fPermissao = "GER" Then
                Return "[GER]"
            Else
                Return "(---)"
            End If
        ElseIf fNivel = 3 Then
            Return "(GER)"
        ElseIf fNivel = 2 Then
            Return "(ALT)"
        ElseIf fNivel = 1 Then
            Return "(VER)"
        Else
            If fPermissao = "GER" Then
                Return "[GER]"
            Else
                Return "(---)"
            End If
        End If
    End Function

    Private Function CarregarNivel(fPermissao As String) As Integer

        If fPermissao = "GER" Then
            Return 3
        ElseIf fPermissao = "ALT" Then
            Return 2
        ElseIf fPermissao = "VER" Then
            Return 1
        Else
            Return 0
        End If

    End Function

End Class