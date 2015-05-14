Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmAprovAgr
    Dim nCodUnidade As Double
    '?? Alterar para a Entidade Principal ??
    Dim dt As DataTable = New DataTable("EUN015")

    Dim i As Integer
    Dim bAlterar As Boolean = False
    Dim bIncluir As Boolean = False
    Dim cQuery As String = ""
    Dim cQueryTemp As String = ""
    Dim cCampos As String
    Dim cValorCampos As String
    Dim nCodUsuario As Integer
    Dim nPermissao As Integer
    Dim nGerAgregacao As Integer = Usuario_GerAgregacao(g_Login)
    Dim nAprovaAgreg As Integer
    Dim sClasseUni_Usuario As String
    Dim bAprovCM As Boolean = False
    Dim bAprovCC As Boolean = False
    Dim bAprovCP As Boolean = False

    Private Sub btnApCPCo_Click(sender As Object, e As EventArgs) Handles btnApCPCo.Click
        Dim options = New dlgColaborador

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtApCPCo.Text = Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6) & " - " & LerNome_Colaborador(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            txtApCPEn.Text = LerCargo_Colaborador(CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6)), nCodUnidade)
        End If

    End Sub

    Private Sub btnApCCCo_Click(sender As Object, e As EventArgs) Handles btnApCCCo.Click
        Dim options = New dlgColaborador

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtApCCCo.Text = Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6) & " - " & LerNome_Colaborador(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            txtApCCEn.Text = LerCargo_Colaborador(CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6)), nCodUnidade)
        End If

    End Sub

    Private Sub btnApCMCo_Click(sender As Object, e As EventArgs) Handles btnApCMCo.Click
        Dim options = New dlgColaborador

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtApCMCo.Text = Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6) & " - " & LerNome_Colaborador(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            txtApCMEn.Text = LerCargo_Colaborador(CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6)), nCodUnidade)
        End If

    End Sub

    Private Sub btnApCNCo_Click(sender As Object, e As EventArgs) Handles btnApCNCo.Click
        Dim options = New dlgColaborador

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtApCNCo.Text = Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6) & " - " & LerNome_Colaborador(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            txtApCNEn.Text = LerCargo_Colaborador(CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6)), nCodUnidade)
        End If

    End Sub

    Private Sub btnApCGCo_Click(sender As Object, e As EventArgs) Handles btnApCGCo.Click
        Dim options = New dlgColaborador

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtApCGCo.Text = Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6) & " - " & LerNome_Colaborador(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            txtApCGEn.Text = LerCargo_Colaborador(CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6)), nCodUnidade)
        End If

    End Sub

    Private Sub frmAprovAgr_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        g_Param(1) = txtNumAgr.Text 'Voltar com a Chave do registro do formulário
    End Sub

    Private Sub TratarObjetos()
        Dim bEmDigitacao As Boolean

        tssContReg.Text = "Registro " & (i + 1).ToString & "/" & dt.Rows.Count().ToString

        'Desabilitar os objetos se a conferência for diferente de Digitada
        bEmDigitacao = False
        If i > -1 And Not bIncluir Then
            If dt.Rows(i).Item("UN015_STAAGR") = "D" Then
                bEmDigitacao = True
            End If
        End If

        If UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN" Then bEmDigitacao = True

        'Botoes da Barra de comandos
        btnIncluir.Enabled = Not bAlterar And Me.Tag > 2 'And Me.Tag > 1
        btnAlterar.Enabled = Not bAlterar And Me.Tag > 2 And bEmDigitacao
        btnExcluir.Enabled = Not bAlterar And Me.Tag = 4 And bEmDigitacao
        btnGravar.Enabled = bAlterar
        btnCancelar.Enabled = bAlterar
        btnAnterior.Enabled = Not bAlterar
        btnProximo.Enabled = Not bAlterar
        btnLocalizar.Enabled = Not bAlterar
        btnImprimir.Enabled = Not bAlterar

        'Campos
        '?? Alterar para os seus objetos da Tela ??
        lblNumAgregacao.Enabled = False 'bAlterar
        txtNumAgr.Enabled = False
        lblClaUni.Enabled = False
        txtClaUni.Enabled = False
        lblID_CF.Enabled = False
        txtID_CF.Enabled = False
        lblStatus.Enabled = nGerAgregacao = 1
        txtStatus.Enabled = nGerAgregacao = 1

        lblNomCfr.Enabled = False
        txtNomCfr.Enabled = False

        'Aprovacoes
        lblEnvFic.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        cbEnvFic.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblDatAgr.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDatAgr.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1

        lblDAutCP.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        dtpDAutCP.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        lblApCPCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        txtApCPCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        btnApCPCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        lblApCPEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)
        txtApCPEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCP)

        lblDAutCC.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        dtpDAutCC.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        lblApCCCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        txtApCCCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        btnApCCCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        lblApCCEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)
        txtApCCEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCC)

        lblDAutCM.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        dtpDAutCM.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        lblApCMCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        txtApCMCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        btnApCMCo.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        lblApCMEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)
        txtApCMEn.Enabled = bAlterar And bEmDigitacao And (nGerAgregacao = 1 Or bAprovCM)

        lblDAutCN.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDAutCN.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        btnApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCNEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCNEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1

        lblDAutCG.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDAutCG.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCGCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCGCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        btnApCGCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCGEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCGEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1

        lblDtChCN.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDtChCN.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        btnApCNCo.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblApCNEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtApCNEn.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1

        lblDtEnvi.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDtEnvi.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblDtReci.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDtReci.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblDtEnvC.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        dtpDtEnvC.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblSitAgr.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        txtSitAgr.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblAcomp.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1
        lblAprov.Enabled = bAlterar And bEmDigitacao And nGerAgregacao = 1

        'Preencher Campos e Armazenar os dados do formulário para gravar o log
        If i > -1 And Not bIncluir Then
            txtNumAgr.Text = dt.Rows(i).Item("UN015_NUMAGR").ToString()
            txtID_CF.Text = dt.Rows(i).Item("UN015_CODCF").ToString()
            If dt.Rows(i).Item("UN015_CODCF") > 0 Then
                txtClaUni.Text = LerClasse_Unidade(CInt(txtID_CF.Text))
            Else
                txtClaUni.Text = dt.Rows(i).Item("UN015_CLAUNI").ToString()
            End If
            txtNomCfr.Text = dt.Rows(i).Item("UN015_NOMCFR")
            Select Case dt.Rows(i).Item("UN015_STAAGR")
                Case "D"
                    txtStatus.Text = "Em Digitação"
                Case "P"
                    txtStatus.Text = "Em Processo"
                Case "A"
                    txtStatus.Text = "Agregada"
                Case "I"
                    txtStatus.Text = "Inativa"
                Case Else
                    txtStatus.Text = "Sem status"
            End Select

            'Aprovacoes
            cbEnvFic.Text = IIf(dt.Rows(i).Item("UN015_ENVFIC") = 1, "Sim", "Não")
            If IsDBNull(dt.Rows(i).Item("UN015_DATAGR")) Then
                dtpDatAgr.Value = "01/01/1900"
                dtpDatAgr.Text = ""
            Else
                dtpDatAgr.Text = dt.Rows(i).Item("UN015_DATAGR")
            End If

            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCP")) Then
                dtpDAutCP.Value = "01/01/1900"
                dtpDAutCP.Text = ""
            Else
                dtpDAutCP.Text = dt.Rows(i).Item("UN015_DAUTCP")
            End If
            If Not IsDBNull(dt.Rows(i).Item("UN015_APCPCO")) Then
                txtApCPCo.Text = Format(dt.Rows(i).Item("UN015_APCPCO"), "000000") & " - " & LerNome_Colaborador(dt.Rows(i).Item("UN015_APCPCO"))
            Else
                txtApCPCo.Text = ""
            End If
            txtApCPEn.Text = dt.Rows(i).Item("UN015_APCPEN")

            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCC")) Then
                dtpDAutCC.Value = "01/01/1900"
                dtpDAutCC.Text = ""
            Else
                dtpDAutCC.Text = dt.Rows(i).Item("UN015_DAUTCC")
            End If
            If Not IsDBNull(dt.Rows(i).Item("UN015_APCCCO")) Then
                txtApCCCo.Text = Format(dt.Rows(i).Item("UN015_APCCCO"), "000000") & " - " & LerNome_Colaborador(dt.Rows(i).Item("UN015_APCCCO"))
            Else
                txtApCCCo.Text = ""
            End If
            txtApCCEn.Text = dt.Rows(i).Item("UN015_APCCEN")

            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCM")) Then
                dtpDAutCM.Value = "01/01/1900"
                dtpDAutCM.Text = ""
            Else
                dtpDAutCM.Text = dt.Rows(i).Item("UN015_DAUTCM")
            End If
            If Not IsDBNull(dt.Rows(i).Item("UN015_APCMCO")) Then
                txtApCMCo.Text = Format(dt.Rows(i).Item("UN015_APCMCO"), "000000") & " - " & LerNome_Colaborador(dt.Rows(i).Item("UN015_APCMCO"))
            Else
                txtApCMCo.Text = ""
            End If
            txtApCMEn.Text = dt.Rows(i).Item("UN015_APCMEN")

            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCN")) Then
                dtpDAutCN.Value = "01/01/1900"
                dtpDAutCN.Text = ""
            Else
                dtpDAutCN.Text = dt.Rows(i).Item("UN015_DAUTCN")
            End If
            If Not IsDBNull(dt.Rows(i).Item("UN015_APCNCO")) Then
                txtApCNCo.Text = Format(dt.Rows(i).Item("UN015_APCNCO"), "000000") & " - " & LerNome_Colaborador(dt.Rows(i).Item("UN015_APCNCO"))
            Else
                txtApCNCo.Text = ""
            End If
            txtApCNEn.Text = dt.Rows(i).Item("UN015_APCMEN")

            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCG")) Then
                dtpDAutCG.Value = "01/01/1900"
                dtpDAutCG.Text = ""
            Else
                dtpDAutCG.Text = dt.Rows(i).Item("UN015_DAUTCG")
            End If
            If Not IsDBNull(dt.Rows(i).Item("UN015_APCGCO")) Then
                txtApCGCo.Text = Format(dt.Rows(i).Item("UN015_APCGCO"), "000000") & " - " & LerNome_Colaborador(dt.Rows(i).Item("UN015_APCGCO"))
            Else
                txtApCGCo.Text = ""
            End If
            txtApCGEn.Text = dt.Rows(i).Item("UN015_APCGEN")

            If IsDBNull(dt.Rows(i).Item("UN015_DTCHCN")) Then
                dtpDtChCN.Value = "01/01/1900"
                dtpDtChCN.Text = ""
            Else
                dtpDtChCN.Text = dt.Rows(i).Item("UN015_DTCHCN")
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DTENVI")) Then
                dtpDtEnvi.Value = "01/01/1900"
                dtpDtEnvi.Text = ""
            Else
                dtpDtEnvi.Text = dt.Rows(i).Item("UN015_DTENVI")
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DTRECI")) Then
                dtpDtReci.Value = "01/01/1900"
                dtpDtReci.Text = ""
            Else
                If Year(dt.Rows(i).Item("UN015_DTRECI")) < 1900 Then
                    dtpDtReci.Value = "01/01/1900"
                    dtpDtReci.Text = ""
                Else
                    dtpDtReci.Text = dt.Rows(i).Item("UN015_DTRECI")
                End If
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DTENVC")) Then
                dtpDtEnvC.Value = "01/01/1900"
                dtpDtEnvC.Text = ""
            Else
                dtpDtEnvC.Text = dt.Rows(i).Item("UN015_DTENVC")
            End If
            txtSitAgr.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_SITAGR")), "", dt.Rows(i).Item("UN015_SITAGR"))
        End If

        'Verificar se é para excluir o registro comandado pelo browse
        If g_Comando = "excluir" Then
            'Call Excluir_Registro()
        End If

    End Sub

    Private Sub btnProximo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProximo.Click

        i += 1
        If Not i = dt.Rows.Count() Then
            Call TratarObjetos()
        Else
            i = dt.Rows.Count() - 1
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
            dt.Clear()
            Me.Close()
        Else
            bAlterar = False
            bIncluir = False
            TratarObjetos()
        End If
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        Call Excluir_Registro()

    End Sub

    Private Sub Excluir_Registro()
        'Formulário não permite exclusão
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

        margemEsq = 5
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
        dt.Clear() 'Limpar o DataTable

        Me.Close()
    End Sub

    Private Sub frmAgregacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i_point As Integer

        nCodUsuario = getCodUsuario(ClassCrypt.Decrypt(g_Login))
        nPermissao = 3

        'Criar um adaptador que vai fazer o download de dados da base de dados
        '?? Alterar o Código para a Entidade Principal ??
        If Me.Tag = 4 Then
            cQuery = "SELECT * FROM EUN015 where UN015_STAAGR <> 'I'"
        Else
            cQuery = "SELECT * FROM EUN015 where UN015_STAAGR <> 'I' and UN015_NUMAGR = " & g_Param(1)
        End If

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dt)
        End Using

        If g_Param(1) <> "INSERT" Then
            'Posicionar no registro selecionado
            '?? Alterar para localizar a chave da tabela ??
            For i_point = 0 To dt.Rows.Count() - 1

                If dt.Rows(i_point).Item("UN015_NUMAGR").ToString = g_Param(1) Then
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

        cCampos = "EUN015.UN015_ENVFIC, EUN015.UN015_DATAGR, EUN015.UN015_DAUTCP, EUN015.UN015_DAUTCC, " & _
            "EUN015.UN015_DAUTCM, EUN015.UN015_DAUTCN, EUN015.UN015_DAUTCG, EUN015.UN015_DTCHCN, " & _
            "EUN015.UN015_DTENVI, EUN015.UN015_DTRECI, EUN015.UN015_DTENVC, EUN015.UN015_SITAGR, " & _
            "EUN015.UN015_APCGCO, EUN015.UN015_APCGEN, EUN015.UN015_APCNCO, EUN015.UN015_APCNEN, " & _
            "EUN015.UN015_APCMCO, EUN015.UN015_APCMEN, EUN015.UN015_APCCCO, EUN015.UN015_APCCEN, " & _
            "EUN015.UN015_APCPCO, EUN015.UN015_APCPEN"

        'Verificar se o usuário exerce algum cargo
        If nGerAgregacao = 0 Then
            'Não gerencia Agregação
            Dim cQuery As String
            Dim dtLerCargo As DataTable = New DataTable("EUN011")

            cQuery = "SELECT EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN011.UN011_DESOCP, EUN011.UN011_APRAGR, " & _
                "EUN016.UN016_CODRED FROM ((EUN003 INNER JOIN EUN012 ON EUN003.UN003_CODCOL = EUN012.UN012_CODCOL) " & _
                "INNER JOIN EUN011 ON EUN012.UN012_CODOCP = EUN011.UN011_CODOCP) INNER JOIN EUN016 ON " & _
                "(EUN012.UN012_CODRED = EUN016.UN016_CODRED) AND (EUN012.UN012_CODMDT = EUN016.UN016_CODMDT) " & _
                "where EUN016.UN016_DATINI <= DATE() AND EUN016.UN016_DATFIN >= DATE() AND " & _
                "UN003_CODCOL = " & LerCodColaborador(ClassCrypt.Decrypt(g_Login)).ToString

            Using daTabela As New OleDbDataAdapter()
                daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                daTabela.Fill(dtLerCargo)
                If dtLerCargo.Rows.Count > 0 Then
                    nAprovaAgreg = IIf(IsDBNull(dtLerCargo.Rows(0).Item("UN011_APRAGR")), 0, dtLerCargo.Rows(0).Item("UN011_APRAGR"))
                    sClasseUni_Usuario = LerClasse_Unidade(dtLerCargo.Rows(0).Item("UN016_CODRED"))
                    If nAprovaAgreg = 1 Then
                        If Microsoft.VisualBasic.Right(sClasseUni_Usuario, 9) = ".00.00.00" Then 'CM
                            If Microsoft.VisualBasic.Left(sClasseUni_Usuario, 2) = Microsoft.VisualBasic.Left(txtClaUni.Text, 2) Then
                                bAprovCM = True
                                bAprovCC = True
                                bAprovCP = True
                            End If
                        ElseIf Microsoft.VisualBasic.Right(sClasseUni_Usuario, 6) = ".00.00" Then 'CC
                            If Microsoft.VisualBasic.Left(sClasseUni_Usuario, 5) = Microsoft.VisualBasic.Left(txtClaUni.Text, 5) Then
                                bAprovCC = True
                                bAprovCP = True
                            End If
                        ElseIf Microsoft.VisualBasic.Right(sClasseUni_Usuario, 3) = ".00" Then 'CP
                            If Microsoft.VisualBasic.Left(sClasseUni_Usuario, 8) = Microsoft.VisualBasic.Left(txtClaUni.Text, 8) Then
                                bAprovCP = True
                            End If
                        End If
                    End If
                End If
            End Using
            dtLerCargo.Clear()
        End If

        TratarObjetos()

    End Sub

End Class