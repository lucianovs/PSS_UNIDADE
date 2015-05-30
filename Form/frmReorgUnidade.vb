Imports System.Data.OleDb

Public Class frmReorgUnidade

    Private Sub frmReorgUnidade_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblStatusCC.Text = ""
        lblStatusCF.Text = ""
        lblStatusCM.Text = ""
        lblStatusCP.Text = ""

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnProcessar_Click(sender As Object, e As EventArgs) Handles btnProcessar.Click
        Dim dtUnidade As DataTable = New DataTable("EUN000")
        Dim sSqlUnidade As String
        Dim sCM_ant As String = ""
        Dim sCC_ant As String = ""
        Dim sCP_ant As String = ""
        Dim cont As Double

        If Not chkCM.Checked And Not chkCC.Checked And Not chkCP.Checked And Not chkCF.Checked Then
            MsgBox("Defina quais níveis deseja processar ...")
            Exit Sub
        End If

        sSqlUnidade = "Select * from EUN000 where un000_stauni<>'I'"
        sSqlUnidade += " and un000_nivuni in ("
        If chkCM.Checked Then sSqlUnidade += "0"
        If chkCC.Checked Then
            If chkCM.Checked Then sSqlUnidade += ", "
            sSqlUnidade += "1"
        End If
        If chkCP.Checked Then
            If chkCM.Checked Or chkCC.Checked Then sSqlUnidade += ", "
            sSqlUnidade += "2"
        End If
        If chkCF.Checked Then
            If chkCM.Checked Or chkCC.Checked Or chkCP.Checked Then sSqlUnidade += ", "
            sSqlUnidade += "3"
        End If
        sSqlUnidade += ") order by un000_clauni"
        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(sSqlUnidade, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtUnidade)

            pbarOrganizacao.Minimum = 0
            pbarOrganizacao.Maximum = dtUnidade.Rows.Count

            For cont = 0 To dtUnidade.Rows.Count - 1
                pbarOrganizacao.Value = cont + 1
                pbarOrganizacao.Refresh()

                'MsgBox(dtUnidade.Rows(cont).Item("un000_clauni"))
                If sCM_ant = "" And chkCM.Checked And dtUnidade.Rows(cont).Item("un000_nivuni") = 0 Then  'Atualizar os CMs, uma única vez
                    Call ReorganizarEstrutura(dtUnidade.Rows(cont).Item("un000_clauni"))
                    sCM_ant = "00"
                End If

                If dtUnidade.Rows(cont).Item("un000_nivuni") = 0 Then
                    lblStatusCM.Text = "Lendo " & dtUnidade.Rows(cont).Item("un000_clauni")
                End If

                'Verificar se Mudou o CM e é um CC
                If sCM_ant <> Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 2) And _
                        dtUnidade.Rows(cont).Item("un000_nivuni") = 1 Then
                    If chkCC.Checked Then
                        'Atualizar os CCs
                        lblStatusCC.Text = "Lendo " & dtUnidade.Rows(cont).Item("un000_clauni")
                        Call ReorganizarEstrutura(dtUnidade.Rows(cont).Item("un000_clauni"))
                    End If

                    sCM_ant = Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 2)
                End If

                'Verificar se Mudou o CC e é um CP
                If sCC_ant <> Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 5) And _
                        dtUnidade.Rows(cont).Item("un000_nivuni") = 2 Then
                    If chkCP.Checked Then
                        'Atualizar os CPs
                        lblStatusCP.Text = "Lendo " & dtUnidade.Rows(cont).Item("un000_clauni")
                        Call ReorganizarEstrutura(dtUnidade.Rows(cont).Item("un000_clauni"))
                    End If

                    sCC_ant = Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 5)
                End If

                'Verificar se Mudou o CP e é uma CF
                If sCP_ant <> Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 8) And _
                        dtUnidade.Rows(cont).Item("un000_nivuni") = 3 Then
                    If chkCF.Checked Then
                        'Atualizar os CFs
                        lblStatusCF.Text = "Lendo " & dtUnidade.Rows(cont).Item("un000_clauni")
                        Call ReorganizarEstrutura(dtUnidade.Rows(cont).Item("un000_clauni"))
                    End If

                    sCP_ant = Microsoft.VisualBasic.Left(dtUnidade.Rows(cont).Item("un000_clauni"), 8)
                End If
                Application.DoEvents()
            Next
        End Using
        dtUnidade.Clear()

        MsgBox("Processo Concluído !!")

        Me.Close()

    End Sub
End Class