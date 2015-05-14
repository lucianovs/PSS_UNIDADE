Imports System.Data.OleDb

Public Class frmRelUnidades
    Dim cParteSelect As String
    Dim cParteWhere As String
    Dim cParteOrder As String

    Private Sub frmRelUnidades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dtLoad As DataTable = New DataTable("EUN000")
        Dim cQuery As String
        'Dim nCodUsuario As Integer

        'Conectar com o Banco
        If Not ConectarBanco() Then
            Me.Close()
        End If

        ''Filtrar as Unidades para exibir só as liberadas para o usuário
        ''-- Opção Desativada
        'nCodUsuario = getCodUsuario(ClassCrypt.Decrypt(g_Login))
        ''MsgBox(LCase(ClassCrypt.Decrypt(g_Login)))
        'cParteSelect = "EUN000.UN000_CODRED, EUN000.UN000_NIVUNI, EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI,"
        'If Trim(LCase(ClassCrypt.Decrypt(g_Login))) = "admin" Then
        'cParteSelect += " 3 AS UN013_PERACE FROM EUN000"
        'cParteWhere = ""
        'Else
        'cParteSelect += " EUN013.UN013_PERACE FROM (EUN000 inner join EUN013 on EUN013.UN013_CODUNI=EUN000.UN000_CODRED)"
        'cParteWhere = "where EUN013.UN013_CODUSU=" & nCodUsuario.ToString & _
        '    "AND UN013_PERACE > 0 "
        'End If
        'cParteOrder = " order by EUN000.UN000_CLAUNI"

        'If Treeview_GerUnidades.Nodes.Count > 0 Then Exit Sub
        'Ler as Unidades
        'cQuery = "Select " & cParteSelect & cParteWhere & cParteOrder
        cQuery = "Select EUN000.UN000_CODRED, EUN000.UN000_NIVUNI, EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI from EUN000 ORDER BY EUN000.UN000_CLAUNI"
        'MsgBox(cQuery)
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtLoad)
            Application.DoEvents()
            cbUnidades.Items.Add("todos")
            Application.DoEvents()
            For i = 0 To dtLoad.Rows.Count() - 1
                cbUnidades.Items.Add(dtLoad.Rows(i).Item("UN000_CLAUNI") & " - " & dtLoad.Rows(i).Item("UN000_NOMUNI"))
            Next
            dtLoad.Clear()
        End Using

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Me.Cursor = Cursors.WaitCursor

        Dim Rpt_Ds As New DataSet
        Dim RptPath As String

        Dim StrQry As String
        'MsgBox(SecondDT)
        StrQry = "Select * from EUN000 " '& cParteWhere
        'StrQry = "Select " & cParteSelect & cParteWhere
        If Not cbUnidades.Text = "todos" Then 'Todas as Unidades 
            StrQry += " where "

            If Microsoft.VisualBasic.Mid(cbUnidades.Text, 4, 2) = "00" Then 'Todas as Unidades do CM
                StrQry += " EUN000.UN000_CLAUNI like '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 2) & "%'"
            ElseIf Microsoft.VisualBasic.Mid(cbUnidades.Text, 7, 2) = "00" Then 'Todas as Unidades do CC
                StrQry += " EUN000.UN000_CLAUNI like '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 5) & "%'"
            ElseIf Microsoft.VisualBasic.Mid(cbUnidades.Text, 10, 2) = "00" Then 'Todas as Unidades do CC
                StrQry += " EUN000.UN000_CLAUNI like '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 8) & "%'"
            Else
                StrQry += " EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 11) & "'"
                StrQry += " OR EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 8) & ".00'"
                StrQry += " OR EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 5) & ".00.00'"
                StrQry += " OR EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(cbUnidades.Text, 2) & ".00.00.00'"
            End If
        End If
        StrQry += " ORDER BY EUN000.UN000_CLAUNI"
        'MsgBox(StrQry)
        Application.DoEvents()
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(StrQry, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(Rpt_Ds)

            RptPath = LerDadosINI(nomeArquivoINI(), "PATH", "Reports", "C:\Fontes\SSVP_Projeto\Report\")
            'RptPath = "C:\Fontes\SSVP_Projeto\Report\"
            frmReportViewer.ShowReport(Rpt_Ds, "RelacaoUnidades.rpt", RptPath)
            Me.Cursor = Cursors.Default

        End Using



    End Sub
End Class