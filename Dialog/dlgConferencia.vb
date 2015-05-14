Imports System.Windows.Forms
Imports System.Data.OleDb

Public Class dlgConferencia

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click, lstPesquisa.DoubleClick

        If lstPesquisa.Items.Count > 0 Then
            If lstPesquisa.SelectedItem.ToString <> "" Then
                'Foi selecionado um Colaborador
                txtPesquisa.Text = lstPesquisa.SelectedItem
                'Microsoft.VisualBasic.Left(lstPesquisa.SelectedItem, 6)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MsgBox("Nenhuma unidade foi selecionada!!")
            End If
        Else
            Call btnPesquisa_Click(Nothing, New System.EventArgs())
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        Dim nPos As Integer = 99
        Dim nSeq As Integer = 0
        Dim nStart As Integer = 1
        Dim sNome(10) As String
        Dim sItemLista As String
        Dim dtPesquisar As DataTable = New DataTable("EUN000")
        Dim cQuery As String = ""
        Dim cQuery2 As String = "" 'Criada para utilizar o where em dois campos para o mesmo texto"

        If Not Trim(txtPesquisa.Text) = "" Then
            cQuery = "Select EUN000.UN000_CODRED, EUN000.UN000_CLAUNI, EUN000.UN000_NOMUNI " & _
                "FROM EUN000 where EUN000.UN000_STAUNI<>'I' " & _
                "AND right(UN000_CLAUNI,2)<>'00' " ' Conferências
            nStart = 1

            lstPesquisa.Items.Clear()

            Do Until nPos = 0
                nPos = InStr(nStart, txtPesquisa.Text, " ")
                If nPos > 0 Then
                    sNome(nSeq) = Mid(txtPesquisa.Text, nStart, nPos - nStart)
                    nStart = nPos + 1
                    nSeq += 1
                Else
                    sNome(nSeq) = Mid(txtPesquisa.Text, nStart, Len(txtPesquisa.Text) - nStart + 1)
                End If
            Loop
            'Montar a Condição
            For nPos = 0 To nSeq
                If nPos = 0 Then
                    cQuery += " and (("
                    cQuery2 += " or ("
                Else
                    cQuery += " and "
                    cQuery2 += " and "
                End If
                cQuery += " EUN000.UN000_NOMUNI LIKE '%" & sNome(nPos) & "%'"
                cQuery2 += " EUN000.UN000_CLAUNI LIKE '%" & sNome(nPos) & "%'"
            Next nPos
            cQuery += ")" & cQuery2 & "))"
            cQuery += " order by EUN000.UN000_CLAUNI"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtPesquisar)
            End Using

            For nSeq = 0 To dtPesquisar.Rows.Count - 1
                sItemLista = Format(dtPesquisar.Rows(nSeq).Item("UN000_CODRED"), "000000")
                sItemLista += " | " & IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN000_CLAUNI")), "", dtPesquisar.Rows(nSeq).Item("UN000_CLAUNI"))
                sItemLista += " | " & IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN000_NOMUNI")), "", dtPesquisar.Rows(nSeq).Item("UN000_NOMUNI"))

                lstPesquisa.Items.Add(sItemLista)
            Next nSeq

            dtPesquisar.Clear()
        Else
            MsgBox("Digite um valor para pesquisar. Para melhor performance, digitar mais de um valor.")
        End If

    End Sub

End Class
