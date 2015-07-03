Imports System.Data.OleDb

Public Class frmRelMandato
    Dim cParteSelect As String
    Dim cParteWhere As String
    Dim cParteOrder As String
    Dim nCodigoUnidade As Double
    Dim dt As DataTable = New DataTable("EUN016")

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Me.Cursor = Cursors.WaitCursor

        Dim RptPath As String
        Dim rptSelection As String = ""
        Dim sSqlMandato As String

        'Carregar a data do último mandato
        sSqlMandato = "Select Max(UN016_DATINI) as DataMandato from EUN016 where UN016_CODRED=" & nCodigoUnidade.ToString
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sSqlMandato, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dt)
        End Using

        If IsDBNull(dt.Rows(0).Item("DataMandato")) Then
            MsgBox("Nenhum mandato encontrado para este conselho !!")
            dt.Clear()
            Exit Sub
        End If

        rptSelection = "{EUN016.UN016_DATINI} = DateTime (" & _
            Format(dt.Rows(0).Item("DataMandato"), "yyyy, MM, dd") & _
            ", 0, 0, 0)"
        dt.Clear()

        If Not txtConselho.Text = "" Then
            rptSelection += " and {EUN000.UN000_CODRED} = " & nCodigoUnidade.ToString
        Else
            MsgBox("Favor selecionar um Conselho.")
            Exit Sub
        End If
        
        RptPath = Application.StartupPath & LerDadosINI(nomeArquivoINI(), "PATH", "Reports", "\Reports\")
        'RptPath = "C:\Fontes\SSVP_Projeto\Report\"
        frmReportViewer.Text = Me.Text
        frmReportViewer.ShowReport("Unidades_RelMandato.rpt", RptPath, rptSelection)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnLocUnidade_Click(sender As Object, e As EventArgs) Handles btnLocUnidade.Click
        Dim options = New dlgUnidade
        Dim sClasseUnidade, sNomeUnidade As String

        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            nCodigoUnidade = CDbl(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6))
            sNomeUnidade = ""
            sClasseUnidade = LerClasse_Unidade(Microsoft.VisualBasic.Left(options.txtPesquisa.Text, 6), sNomeUnidade)
            txtConselho.Text = sClasseUnidade & " - " & sNomeUnidade
        End If

    End Sub

End Class