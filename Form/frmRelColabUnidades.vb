Imports System.Data.OleDb

Public Class frmRelColabUnidades
    Dim cParteSelect As String
    Dim cParteWhere As String
    Dim cParteOrder As String

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Me.Cursor = Cursors.WaitCursor

        Dim RptPath As String
        Dim rptSelection As String = ""

        If Not txtConselho.Text = "" Then 'Todas as Unidades 
            rptSelection = "{EUN000.UN000_CLAUNI} startswith '"
            rptSelection += Microsoft.VisualBasic.Left(txtConselho.Text, 8) & "' and "
        Else
            If MsgBox("Deseja listar todas as Unidades e seus colaboradores?", MsgBoxStyle.YesNo) = vbNo Then
                Exit Sub
            End If
            'MsgBox("Favor selecionar uma Conferência.")
        End If
        rptSelection += "{EUN003.UN003_SITCOL} <> 'E'"

        RptPath = Application.StartupPath & LerDadosINI(nomeArquivoINI(), "PATH", "Reports", "\Reports\")
        'RptPath = "C:\Fontes\SSVP_Projeto\Report\"
        frmReportViewer.Text = Me.Text
        frmReportViewer.ShowReport("Unidades_RelColaborador.rpt", RptPath, rptSelection)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnLocUnidade_Click(sender As Object, e As EventArgs) Handles btnLocUnidade.Click
        Dim options = New dlgCParticular
        Dim sClasseUnidade, sNomeUnidade As String

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            sNomeUnidade = ""
            sClasseUnidade = LerClasse_Unidade(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6), sNomeUnidade)
            txtConselho.Text = sClasseUnidade & " - " & sNomeUnidade
        End If

    End Sub

End Class