Imports System.Data.OleDb

Public Class frmRelUnidades
    Dim cParteSelect As String
    Dim cParteWhere As String
    Dim cParteOrder As String

    Private Sub frmRelUnidades_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Me.Cursor = Cursors.WaitCursor

        'Dim Rpt_Ds As New DataSet
        Dim RptPath As String
        Dim rptSelection As String = ""

        If Not txtConselho.Text = "" Then 'Todas as Unidades 
            rptSelection = "{EUN000.UN000_CLAUNI} startswith '"
            If Microsoft.VisualBasic.Mid(txtConselho.Text, 4, 2) = "00" Then 'Todas as Unidades do CM
                rptSelection += Microsoft.VisualBasic.Left(txtConselho.Text, 2) & "'"
            ElseIf Microsoft.VisualBasic.Mid(txtConselho.Text, 7, 2) = "00" Then 'Todas as Unidades do CC
                rptSelection += Microsoft.VisualBasic.Left(txtConselho.Text, 5) & "'"
            ElseIf Microsoft.VisualBasic.Mid(txtConselho.Text, 10, 2) = "00" Then 'Todas as Unidades do CP
                rptSelection += Microsoft.VisualBasic.Left(txtConselho.Text, 8) & "'"
            End If
        End If

        RptPath = Application.StartupPath & LerDadosINI(nomeArquivoINI(), "PATH", "Reports", "\Reports\")
        'RptPath = "C:\Fontes\SSVP_Projeto\Report\"
        'MsgBox(RptPath & "Unidades_Relacao.rpt")
        frmReportViewer.ShowReport("Unidades_Relacao.rpt", RptPath, rptSelection)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnLocUnidade_Click(sender As Object, e As EventArgs) Handles btnLocUnidade.Click
        'txtColaborador.Text = dlgColaborador.ShowDialog()
        Dim options = New dlgUnidade
        Dim sClasseUnidade, sNomeUnidade As String

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            sNomeUnidade = ""
            sClasseUnidade = LerClasse_Unidade(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6), sNomeUnidade)
            txtConselho.Text = sClasseUnidade & " - " & sNomeUnidade
        End If

    End Sub

End Class