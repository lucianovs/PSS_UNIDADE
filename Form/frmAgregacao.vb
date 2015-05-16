Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmAgregacao

    '?? Alterar para a Entidade Principal ??
    Dim dt As DataTable = New DataTable("EUN015")
    Dim dtMandato As DataTable = New DataTable("EUN016")

    Dim i As Integer
    Dim i_Mandato As Integer
    Dim bAlterar As Boolean = False
    Dim bIncluir As Boolean = False
    Dim cQuery As String = ""
    Dim cQueryTemp As String = ""
    Dim cCampos As String
    Dim cValorCampos As String
    Dim nCodUsuario As Integer
    Dim nPermissao As Integer

    Dim dtUnidade As DataTable = New DataTable("EUN000")

    Private Sub frmUnidades_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        g_Param(1) = txtNumAgr.Text 'Voltar com a Chave do registro do formulário
    End Sub

    Private Sub TratarObjetos()
        Dim bEmDigitacao As Boolean
        Dim nCodigoMandato As Integer

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
        lblClaUni.Enabled = False Or (bAlterar And UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN") Or bIncluir
        txtClaUni.Enabled = False Or (bAlterar And UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN") Or bIncluir
        lblID_CF.Enabled = False Or (bAlterar And UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN") 'Or bIncluir
        txtID_CF.Enabled = False Or (bAlterar And UCase(ClassCrypt.Decrypt(g_Login)) = "ADMIN") 'Or bIncluir
        lblStatus.Enabled = False
        txtStatus.Enabled = False

        lblNomCfr.Enabled = bAlterar And bEmDigitacao
        txtNomCfr.Enabled = bAlterar And bEmDigitacao

        'Dados
        lblCidAgr.Enabled = bAlterar And bEmDigitacao
        txtCidAgr.Enabled = bAlterar And bEmDigitacao
        lblNomDio.Enabled = bAlterar And bEmDigitacao
        txtNomDio.Enabled = bAlterar And bEmDigitacao
        lblNomPar.Enabled = bAlterar And bEmDigitacao
        txtNomPar.Enabled = bAlterar And bEmDigitacao
        lblClaCfr.Enabled = bAlterar And bEmDigitacao
        cbClaCfr.Enabled = bAlterar And bEmDigitacao
        lblPaisCF.Enabled = bAlterar And bEmDigitacao
        txtPaiCFR.Enabled = bAlterar And bEmDigitacao
        lblEstCFR.Enabled = bAlterar And bEmDigitacao
        cbEstCFR.Enabled = bAlterar And bEmDigitacao
        lblDatFun.Enabled = bAlterar And bEmDigitacao
        dtpDatFun.Enabled = bAlterar And bEmDigitacao
        lblCP.Enabled = bAlterar And bEmDigitacao
        txtCP.Enabled = bAlterar And bEmDigitacao
        lblCC.Enabled = False 'bAlterar
        txtCC.Enabled = False 'bAlterar
        lblCM.Enabled = False 'bAlterar
        txtCM.Enabled = False 'bAlterar
        lblDatEle.Enabled = bAlterar And bEmDigitacao
        dtpDatEle.Enabled = bAlterar And bEmDigitacao

        'Atividades
        lblCFAtv.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCfAtv1.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv1.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCFAtv2.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv2.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCFAtv3.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv3.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCFAtv4.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv4.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCFAtv5.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv5.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblCFAtv6.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtCFAtv6.Enabled = bAlterar And bEmDigitacao And Not bIncluir

        'Reunioes
        lblReuDia.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtReuDia.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblReuHor.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        dtpReuHor.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblReuEnd.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtReuEnd.Enabled = bAlterar And bEmDigitacao And Not bIncluir

        'Membros da Mesa
        txtUN016_DesMdt.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        dtpUN016_DatIni.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        dtpUN016_DatFin.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        NomePesquisar.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        ListaPesquisa.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        dtgMandato.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        ComandoPesquisar.Enabled = bAlterar And bEmDigitacao And Not bIncluir

        'Membros Ativos
        txtPesqMembro.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lstPesqMembro.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        dtgMembroAtivo.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        btnPesqMembro.Enabled = bAlterar And bEmDigitacao And Not bIncluir

        'Outros Dados
        lblRelConf.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblNumSub.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtNumSub.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblNumBen.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtNumBen.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblParJur.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtParJur.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblVisSem.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtVisSem.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblOutPai.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtOutPai.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblMeiSoc.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtMeiSoc.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblNumAss.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtNumAss.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblRelCle.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtRelCle.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblRelPub.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtRelPub.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblRelSoc.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtRelSoc.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAplReg.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        cbAplReg.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblDifApl.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtDifApl.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblMis5In.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtMis5In.Enabled = bAlterar And bEmDigitacao And Not bIncluir

        'Recursos / Gastos
        lblPe1Ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtPe1ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAn1Ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtAn1Ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblPe2ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtPe2ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAn2Ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtAn2Ref.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblRecursos.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblColReu.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtColreu.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblOutRec.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtOutRec.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAjuEnt.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtAjuEnt.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAjuRec.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtAjuRec.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblAjuOut.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtAjuOut.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtTotalRec.Enabled = False
        lblTotalRec.Enabled = False

        lblGastos.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblGstPes.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtGstPes.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblGstCon.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtGstCon.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblGstDoa.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtGstDoa.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblGstEme.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtGstEme.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblGstAdm.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        txtGstAdm.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblTotalGastos.Enabled = False
        txtTotalGst.Enabled = False

        'Aprovacoes
        lblEnvFic.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        cbEnvFic.Enabled = bAlterar And bEmDigitacao And Not bIncluir
        lblDatAgr.Enabled = False
        dtpDatAgr.Enabled = False
        lblDAutCP.Enabled = False
        dtpDAutCP.Enabled = False
        lblDAutCC.Enabled = False
        dtpDAutCC.Enabled = False
        lblDAutCM.Enabled = False
        dtpDAutCM.Enabled = False
        lblDAutCN.Enabled = False
        dtpDAutCN.Enabled = False
        lblDAutCG.Enabled = False
        dtpDAutCG.Enabled = False
        lblDtChCN.Enabled = False
        dtpDtChCN.Enabled = False
        lblDtEnvi.Enabled = False
        dtpDtEnvi.Enabled = False
        lblDtReci.Enabled = False
        dtpDtReci.Enabled = False
        lblDtEnvC.Enabled = False
        dtpDtEnvC.Enabled = False
        lblSitAgr.Enabled = False
        txtSitAgr.Enabled = False
        lblAcomp.Enabled = False
        lblAprov.Enabled = False

        'Dados dos Mandatos
        btnProx_Mandato.Enabled = bAlterar And nPermissao = 3 And Not bIncluir
        btnAnt_Mandato.Enabled = False
        lblCodMdt.Enabled = False
        txtUN016_CodMdt.Enabled = False
        txtUN016_DesMdt.Enabled = bAlterar And Not bIncluir And nPermissao = 3
        lblUN016_DatIni.Enabled = bAlterar And Not bIncluir And nPermissao = 3
        dtpUN016_DatIni.Enabled = bAlterar And Not bIncluir And nPermissao = 3
        lblUN016_DatFin.Enabled = bAlterar And Not bIncluir And nPermissao = 3
        dtpUN016_DatFin.Enabled = bAlterar And Not bIncluir And nPermissao = 3
        dtgMandato.Enabled = bAlterar And Not bIncluir And nPermissao = 3

        'Preencher Campos e Armazenar os dados do formulário para gravar o log
        If i > -1 And Not bIncluir Then
            txtNumAgr.Text = dt.Rows(i).Item("UN015_NUMAGR").ToString()
            txtID_CF.Text = dt.Rows(i).Item("UN015_CODCF").ToString()
            If dt.Rows(i).Item("UN015_CODCF") > 0 Then
                txtClaUni.Text = LerClasse_Unidade(CInt(txtID_CF.Text))
            Else
                txtClaUni.Text = dt.Rows(i).Item("UN015_CLAUNI").ToString()
            End If
            'Carregar os dados das Unidades Owners
            Call CarregarUnidadesOwner_CF(txtClaUni.Text)

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

            txtCidAgr.Text = dt.Rows(i).Item("UN015_CIDAGR")
            txtNomDio.Text = dt.Rows(i).Item("UN015_NOMDIO")
            cbClaCfr.Text = dt.Rows(i).Item("UN015_CLACFR")
            txtPaiCFR.Text = dt.Rows(i).Item("UN015_PAICFR")
            cbEstCFR.Text = dt.Rows(i).Item("UN015_ESTCFR")
            If IsDBNull(dt.Rows(i).Item("UN015_DATFUN")) Then
                dtpDatFun.Value = "01/01/1800"
                dtpDatFun.Text = ""
            Else
                dtpDatFun.Text = dt.Rows(i).Item("UN015_DATFUN")
            End If

            If IsDBNull(dt.Rows(i).Item("UN015_DATELE")) Then
                dtpDatEle.Value = "01/01/1800"
                dtpDatEle.Text = ""
            Else
                dtpDatEle.Text = dt.Rows(i).Item("UN015_DATELE")
            End If

            'Atividades
            txtCFAtv1.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV1")), "", dt.Rows(i).Item("UN015_CFATV1"))
            txtCFAtv2.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV2")), "", dt.Rows(i).Item("UN015_CFATV2"))
            txtCFAtv3.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV3")), "", dt.Rows(i).Item("UN015_CFATV3"))
            txtCFAtv4.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV4")), "", dt.Rows(i).Item("UN015_CFATV4"))
            txtCFAtv5.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV5")), "", dt.Rows(i).Item("UN015_CFATV5"))
            txtCFAtv6.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_CFATV6")), "", dt.Rows(i).Item("UN015_CFATV6"))

            'Reuniões
            txtReuDia.Text = dt.Rows(i).Item("UN015_REUDIA")
            If IsDBNull(dt.Rows(i).Item("UN015_REUHOR")) Then
                dtpReuHor.Value = "00:00"
                dtpReuHor.Text = ""
            Else
                dtpReuHor.Value = dt.Rows(i).Item("UN015_REUHOR")
            End If
            txtReuEnd.Text = dt.Rows(i).Item("UN015_REUEND")

            'Outros Dados
            txtNumSub.Text = dt.Rows(i).Item("UN015_NUMSUB")
            txtNumBen.Text = dt.Rows(i).Item("UN015_NUMBEN")
            txtParJur.Text = dt.Rows(i).Item("UN015_PARJUR")
            txtVisSem.Text = dt.Rows(i).Item("UN015_VISSEM")
            txtOutPai.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_OUTPAI")), "", dt.Rows(i).Item("UN015_OUTPAI"))
            txtMeiSoc.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_MEISOC")), "", dt.Rows(i).Item("UN015_MEISOC"))
            txtNumAss.Text = dt.Rows(i).Item("UN015_NUMASS")
            txtRelCle.Text = dt.Rows(i).Item("UN015_RELCLE")
            txtRelPub.Text = dt.Rows(i).Item("UN015_RELPUB")
            txtRelSoc.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_RELSOC")), "", dt.Rows(i).Item("UN015_RELSOC"))
            cbAplReg.Text = dt.Rows(i).Item("UN015_APLREG")
            txtDifApl.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_DIFAPL")), "", dt.Rows(i).Item("UN015_DIFAPL"))
            txtMis5In.Text = IIf(IsDBNull(dt.Rows(i).Item("UN015_MIS5IN")), "", dt.Rows(i).Item("UN015_MIS5IN"))

            'Recursos / Gastos
            txtPe1ref.Text = dt.Rows(i).Item("UN015_PE1REF")
            txtAn1Ref.Text = dt.Rows(i).Item("UN015_AN1REF")
            txtPe2ref.Text = dt.Rows(i).Item("UN015_PE2REF")
            txtAn2Ref.Text = dt.Rows(i).Item("UN015_AN2REF")
            txtColreu.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_COLREU")), 0, _
                                        dt.Rows(i).Item("UN015_COLREU")), "###,###,##0.00")
            txtOutRec.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_OUTREC")), 0, _
                                        dt.Rows(i).Item("UN015_OUTREC")), "###,###,##0.00")
            txtAjuEnt.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_AJUENT")), 0, _
                                        dt.Rows(i).Item("UN015_AJUENT")), "###,###,##0.00")
            txtAjuRec.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_AJUREC")), 0, _
                                        dt.Rows(i).Item("UN015_AJUREC")), "###,###,##0.00")
            txtAjuOut.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_AJUOUT")), 0, _
                                        dt.Rows(i).Item("UN015_AJUOUT")), "###,###,##0.00")
            txtTotalRec.Text = Format(CDbl(txtColreu.Text) + CDbl(txtOutRec.Text) + _
                                    CDbl(txtAjuEnt.Text) + CDbl(txtAjuRec.Text) + _
                                    CDbl(txtAjuOut.Text), "###,###,##0.00")
            txtGstPes.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_GSTPES")), 0, _
                                        dt.Rows(i).Item("UN015_GSTPES")), "###,###,##0.00")
            txtGstCon.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_GSTCON")), 0, _
                                        dt.Rows(i).Item("UN015_GSTCON")), "###,###,##0.00")
            txtGstDoa.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_GSTDOA")), 0, _
                                        dt.Rows(i).Item("UN015_GSTDOA")), "###,###,##0.00")
            txtGstEme.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_GSTEME")), 0, _
                                        dt.Rows(i).Item("UN015_GSTEME")), "###,###,##0.00")
            txtGstAdm.Text = Format(IIf(IsDBNull(dt.Rows(i).Item("UN015_GSTADM")), 0, _
                                        dt.Rows(i).Item("UN015_GSTADM")), "###,###,##0.00")
            txtTotalGst.Text = Format(CDbl(txtGstPes.Text) + CDbl(txtGstCon.Text) + _
                                    CDbl(txtGstDoa.Text) + CDbl(txtGstEme.Text) + _
                                    CDbl(txtGstAdm.Text), "###,###,##0.00")

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
            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCC")) Then
                dtpDAutCC.Value = "01/01/1900"
                dtpDAutCC.Text = ""
            Else
                dtpDAutCC.Text = dt.Rows(i).Item("UN015_DAUTCC")
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCM")) Then
                dtpDAutCM.Value = "01/01/1900"
                dtpDAutCM.Text = ""
            Else
                dtpDAutCM.Text = dt.Rows(i).Item("UN015_DAUTCM")
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCN")) Then
                dtpDAutCN.Value = "01/01/1900"
                dtpDAutCN.Text = ""
            Else
                dtpDAutCN.Text = dt.Rows(i).Item("UN015_DAUTCN")
            End If
            If IsDBNull(dt.Rows(i).Item("UN015_DAUTCG")) Then
                dtpDAutCG.Value = "01/01/1900"
                dtpDAutCG.Text = ""
            Else
                dtpDAutCG.Text = dt.Rows(i).Item("UN015_DAUTCG")
            End If
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

            'If IsDBNull(dt.Rows(i).Item("UN000_DATFUN")) Then
            ' dtpDataFundacao.Value = "01/01/1800"
            'dtpDataFundacao.Text = ""
            'Else
            '   dtpDataFundacao.Value = dt.Rows(i).Item("UN000_DATFUN")
            'End If

            'Carregar os Dados do Mandato
            dtMandato.Clear()
            cQuery = "Select * from EUN016 where UN016_CODRED=" & txtID_CF.Text & _
                " order by UN016_DATFIN DESC"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtMandato)
            End Using
            If dtMandato.Rows.Count = 0 Then
                nCodigoMandato = InserirMandato(CDbl(txtID_CF.Text))
                If nCodigoMandato > 0 Then
                    dtMandato.Clear()
                    cQuery = "Select * from EUN016 where UN016_CODRED=" & txtID_CF.Text & _
                        " and UN016_CODMDT=" & nCodigoMandato.ToString
                    Using da As New OleDbDataAdapter()
                        da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                        ' Preencher o DataTable 
                        da.Fill(dtMandato)
                    End Using
                End If
            End If
            If dtMandato.Rows.Count > 0 Then
                i_Mandato = 0
                Call Carregardados_Mandato()
                ComandoPesquisar.Enabled = True
            Else
                ComandoPesquisar.Enabled = False
            End If

            'Carregar os Membros Ativos
            CarregarGridMembro()

        End If

        'Verificar se é para excluir o registro comandado pelo browse
        If g_Comando = "excluir" Then
            Call Excluir_Registro()
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

    Private Sub btnIncluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        bAlterar = True
        bIncluir = True

        'Inicializar os seus Componentes de Entrada de Dados
        txtClaUni.Text = ""
        txtNomCfr.Text = ""
        txtNumAgr.Text = ""

        'Dados
        txtPaiCFR.Text = "BRASIL"
        txtCidAgr.Text = ""
        txtNomDio.Text = ""
        txtNomPar.Text = ""
        cbClaCfr.Text = "Mista"
        cbEstCFR.Text = ""
        dtpDatFun.Value = "01/01/1800"
        dtpDatFun.Text = ""
        dtpDatEle.Value = "01/01/1800"
        dtpDatEle.Text = ""

        'Atividades
        txtCFAtv1.Text = ""
        txtCFAtv2.Text = ""
        txtCFAtv3.Text = ""
        txtCFAtv4.Text = ""
        txtCFAtv5.Text = ""
        txtCFAtv6.Text = ""

        'Reunioes
        txtReuDia.Text = ""
        dtpReuHor.Value = "00:00"
        dtpDatEle.Text = ""
        txtReuEnd.Text = ""


        'Outros Dados
        txtNumSub.Text = "0"
        txtNumBen.Text = "0"
        txtParJur.Text = ""
        txtVisSem.Text = "0"
        txtOutPai.Text = ""
        txtMeiSoc.Text = ""
        txtNumAss.Text = "0"
        txtRelCle.Text = ""
        txtRelPub.Text = ""
        txtRelSoc.Text = ""
        cbAplReg.Text = "Sim"
        txtDifApl.Text = ""
        txtMis5In.Text = ""

        'Recursos / gastos
        txtPe1ref.Text = ""
        txtPe2ref.Text = ""
        txtAn1Ref.Text = ""
        txtAn2Ref.Text = ""
        txtColreu.Text = "0"
        txtOutRec.Text = "0"
        txtAjuEnt.Text = "0"
        txtAjuRec.Text = "0"
        txtAjuOut.Text = "0"
        txtTotalRec.Text = "0"
        txtGstPes.Text = "0"
        txtGstCon.Text = "0"
        txtGstDoa.Text = "0"
        txtGstEme.Text = "0"
        txtGstAdm.Text = "0"
        txtTotalGst.Text = "0"

        Call TratarObjetos()

    End Sub

    Private Sub btnGravar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGravar.Click
        Dim cSql As String = ""
        Dim cMensagem As String = ""
        Dim cmd As OleDbCommand

        If ConectarBanco() Then
            '?? Colocar o Comando SQL para Gravar (Update e Insert)
            If bIncluir Then
                'EUN015.UN015_CIDAGR, EUN015.UN015_NOMDIO, EUN015.UN015_NOMPAR, EUN015.UN015_CLACFR, EUN015.UN015_PAICFR, EUN015.UN015_ESTCFR, EUN015.UN015_CODCM, EUN015.UN015_CODCC, EUN015.UN015_CODCP, 
                'EUN015.UN015_DATFUN, EUN015.UN015_ENVFIC, EUN015.UN015_DATELE, EUN015.UN015_DATAGR, EUN015.UN015_CFATV1, EUN015.UN015_CFATV2, EUN015.UN015_CFATV3, EUN015.UN015_CFATV4, EUN015.UN015_CFATV5, EUN015.UN015_CFATV6, EUN015.UN015_REUDIA, EUN015.UN015_REUHOR, 
                'EUN015.UN015_REUEND, EUN015.UN015_DAUTCP, EUN015.UN015_DAUTCC, EUN015.UN015_DAUTCM, EUN015.UN015_DAUTCN, EUN015.UN015_DAUTCG, EUN015.UN015_DTCHCN, EUN015.UN015_DTENVI, EUN015.UN015_DTRECI, EUN015.UN015_DTENVC, EUN015.UN015_SITAGR, EUN015.UN015_NUMSUB, 
                'EUN015.UN015_NUMBEN, EUN015.UN015_PARJUR, EUN015.UN015_VISSEM, EUN015.UN015_MEISOC, EUN015.UN015_NUMASS, EUN015.UN015_RELCLE, EUN015.UN015_RELPUB, EUN015.UN015_RELSOC, EUN015.UN015_APLREG, EUN015.UN015_DIFAPL, EUN015.UN015_MIS5IN, EUN015.UN015_OUTPAI, 
                'EUN015.UN015_COLREU, EUN015.UN015_OUTREC, EUN015.UN015_AJUENT, EUN015.UN015_AJUREC, EUN015.UN015_AJUOUT, EUN015.UN015_GSTPES, EUN015.UN015_GSTCON, EUN015.UN015_GSTDOA,  EUN015.UN015_GSTEME, EUN015.UN015_GSTADM, EUN015.UN015_PE1REF, EUN015.UN015_AN1REF, 
                'EUN015.UN015_PE2REF, EUN015.UN015_AN2REF

                cSql = "INSERT INTO EUN015 (UN015_STAAGR, UN015_CODUSU, UN015_DATINA, "
                cSql += "UN015_NUMAGR, UN015_CLAUNI, UN015_NOMCFR, UN015_CIDAGR, "
                cSql += "UN015_NOMDIO, UN015_NOMPAR, UN015_CLACFR, UN015_PAICFR, "
                cSql += "UN015_ESTCFR, UN015_CODCM,  UN015_CODCC,  UN015_CODCP,  "
                cSql += "UN015_CODCF,  UN015_DATFUN, UN015_DATELE, UN015_CFATV1, "
                cSql += "UN015_CFATV2, UN015_CFATV3, UN015_CFATV4, UN015_CFATV5, "
                cSql += "UN015_CFATV6, UN015_REUDIA, UN015_REUHOR, UN015_REUEND, "
                cSql += "UN015_NUMSUB, UN015_NUMBEN, UN015_PARJUR, UN015_VISSEM, "
                cSql += "UN015_OUTPAI, UN015_MEISOC, UN015_NUMASS, UN015_RELCLE, "
                cSql += "UN015_RELPUB, UN015_RELSOC, UN015_APLREG, UN015_DIFAPL, UN015_MIS5IN,"
                cSql += "UN015_PE1REF, UN015_AN1REF, UN015_PE2REF, UN015_AN2REF, "
                cSql += "UN015_COLREU, UN015_OUTREC, UN015_AJUENT, UN015_AJUREC, "
                cSql += "UN015_AJUOUT, UN015_GSTPES, UN015_GSTCON, UN015_GSTDOA, "
                cSql += "UN015_GSTEME, UN015_GSTADM, UN015_USUINC, UN015_DATINC,"
                cSql += "UN015_USUALT, UN015_DATALT"
                cSql += ")"
                cSql += " values ('A',0,null," & Integer.Parse(ProxCodChave("EUN015", "UN015_NUMAGR")) & ", '" & txtClaUni.Text & "'"
                cSql += ",'" & txtNomCfr.Text & "','" & txtCidAgr.Text & "','" & txtNomDio.Text & "','" & txtNomPar.Text & "'"
                cSql += ",'" & cbClaCfr.Text & "','" & txtPaiCFR.Text & "','" & cbEstCFR.Text & "'"
                cSql += "," & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 3) & "00.00.00").ToString()
                cSql += "," & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 6) & "00.00").ToString
                cSql += "," & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 9) & "00").ToString & _
                    "," & ProxSeq_Unidade("CF", Microsoft.VisualBasic.Left(txtCP.Text, 11)).ToString  'Prox. Codigo da Unidade
                cSql += ",'" & FormatarData(dtpDatFun.Value) & "'"
                cSql += ",'" & FormatarData(dtpDatEle.Value) & "','" & txtCFAtv1.Text & "','" & txtCFAtv2.Text & "'"
                cSql += ",'" & txtCFAtv3.Text & "','" & txtCFAtv4.Text & "','" & txtCFAtv5.Text & "','" & txtCFAtv6.Text & "'"
                cSql += ",'" & txtReuDia.Text & "','" & Format(dtpReuHor.Value, "hh:mm") & "','" & txtReuEnd.Text & "'"
                cSql += "," & txtNumSub.Text & "," & txtNumBen.Text & ",'" & txtParJur.Text & "'," & txtVisSem.Text
                cSql += ",'" & txtOutPai.Text & "','" & txtMeiSoc.Text & "'," & txtNumAss.Text & ",'" & txtRelCle.Text & "'"
                cSql += ",'" & txtRelPub.Text & "','" & txtRelSoc.Text & "'," & IIf(cbAplReg.Text = "Não", "0", "1")
                cSql += ",'" & txtDifApl.Text & "','" & txtMis5In.Text & "','" & txtPe1ref.Text & "','" & txtAn1Ref.Text & "'"
                cSql += ",'" & txtPe2ref.Text & "','" & txtAn2Ref.Text & "'," & FormatarValor_SQL(txtColreu.Text) & "," & FormatarValor_SQL(txtOutRec.Text)
                cSql += "," & FormatarValor_SQL(txtAjuEnt.Text) & "," & FormatarValor_SQL(txtAjuRec.Text) & "," & FormatarValor_SQL(txtAjuOut.Text)
                cSql += "," & FormatarValor_SQL(txtGstPes.Text) & "," & FormatarValor_SQL(txtGstCon.Text) & "," & FormatarValor_SQL(txtGstDoa.Text)
                cSql += "," & FormatarValor_SQL(txtGstEme.Text) & "," & FormatarValor_SQL(txtGstAdm.Text)
                cSql += "," & nCodUsuario.ToString & ",'" & FormatarData(Today()) & "'" 'Usu. Incluiu
                cSql += "," & nCodUsuario.ToString & ",'" & FormatarData(Today()) & "'" 'Usu Alterou (o mesmo)
                'cSql += ", '" & Convert.ToDateTime(dtpDataAprovacaoCP.Text) & "', '" & Convert.ToDateTime(dtpDataAprovacaoCC.Text) & "'"
                cSql += ")"
            ElseIf bAlterar Then
                cSql = "UPDATE EUN015 set UN015_CLAUNI = '" & txtClaUni.Text & "', UN015_NOMCFR = '" & txtNomCfr.Text & "'"
                cSql += ",UN015_CIDAGR='" & txtCidAgr.Text & "',UN015_NOMDIO='" & txtNomDio.Text & "',UN015_NOMPAR='" & txtNomPar.Text & "'"
                cSql += ",UN015_CLACFR='" & cbClaCfr.Text & "',UN015_PAICFR='" & txtPaiCFR.Text & "',UN015_ESTCFR='" & cbEstCFR.Text & "'"
                cSql += ",UN015_CODCM=" & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 3) & "00.00.00").ToString
                cSql += ",UN015_CODCC=" & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 6) & "00.00").ToString
                cSql += ",UN015_CODCP=" & LerCod_Unidade(Microsoft.VisualBasic.Left(txtClaUni.Text, 9) & "00").ToString
                cSql += ",UN015_CODCF=" & txtID_CF.Text
                cSql += ",UN015_DATFUN='" & FormatarData(dtpDatFun.Value) & "'"
                cSql += ",UN015_DATELE='" & FormatarData(dtpDatEle.Value) & "',UN015_CFATV1='" & txtCFAtv1.Text & "'"
                cSql += ",UN015_CFATV2='" & txtCFAtv2.Text & "',UN015_CFATV3='" & txtCFAtv3.Text & "',UN015_CFATV4='" & txtCFAtv4.Text & "'"
                cSql += ",UN015_CFATV5='" & txtCFAtv5.Text & "',UN015_CFATV6='" & txtCFAtv6.Text & "'"
                cSql += ",UN015_REUDIA='" & txtReuDia.Text & "',UN015_REUHOR='" & Format(dtpReuHor.Value, "hh:mm") & "',UN015_REUEND='" & txtReuEnd.Text & "'"
                cSql += ",UN015_NUMSUB=" & txtNumSub.Text & ",UN015_NUMBEN=" & txtNumBen.Text & ",UN015_PARJUR='" & txtParJur.Text & "',UN015_VISSEM=" & txtVisSem.Text
                cSql += ",UN015_OUTPAI='" & txtOutPai.Text & "',UN015_MEISOC='" & txtMeiSoc.Text & "',UN015_NUMASS=" & txtNumAss.Text & ",UN015_RELCLE='" & txtRelCle.Text & "'"
                cSql += ",UN015_RELPUB='" & txtRelPub.Text & "',UN015_RELSOC='" & txtRelSoc.Text & "',UN015_APLREG=" & IIf(cbAplReg.Text = "Não", "0", "1")
                cSql += ",UN015_DIFAPL='" & txtDifApl.Text & "',UN015_MIS5IN='" & txtMis5In.Text & "'"
                cSql += ",UN015_PE1REF='" & txtPe1ref.Text & "',UN015_AN1REF='" & txtAn1Ref.Text & "'"
                cSql += ",UN015_PE2REF='" & txtPe2ref.Text & "',UN015_AN2REF='" & txtAn2Ref.Text & "',UN015_COLREU=" & FormatarValor_SQL(txtColreu.Text) & ",UN015_OUTREC=" & FormatarValor_SQL(txtOutRec.Text)
                cSql += ",UN015_AJUENT=" & FormatarValor_SQL(txtAjuEnt.Text) & ",UN015_AJUREC=" & FormatarValor_SQL(txtAjuRec.Text) & ",UN015_AJUOUT=" & FormatarValor_SQL(txtAjuOut.Text)
                cSql += ",UN015_GSTPES=" & FormatarValor_SQL(txtGstPes.Text) & ",UN015_GSTCON=" & FormatarValor_SQL(txtGstCon.Text) & ",UN015_GSTDOA=" & FormatarValor_SQL(txtGstDoa.Text)
                cSql += ",UN015_GSTEME=" & FormatarValor_SQL(txtGstEme.Text) & ",UN015_GSTADM=" & FormatarValor_SQL(txtGstAdm.Text)
                cSql += ",UN015_USUALT=" & nCodUsuario.ToString & ", UN015_DATALT='" & FormatarData(Today()) & "'"
                'cSql += "UN000_APROCP = '" & Format(dtpDataAprovacaoCP.Value, "dd/MM/yyyy") & "', UN000_APROCC = '" & Format(dtpDataAprovacaoCC.Value, "dd/MM/yyyy") & "', "
                cSql += " where UN015_NUMAGR = " & Integer.Parse(txtNumAgr.Text)

            End If
            cmd = New OleDbCommand(cSql, g_ConnectBanco)

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally

                If bIncluir Then
                    Dim dtLerUsuGerAgr As DataTable = New DataTable("ESI000")
                    Dim nCodMural As Integer

                    'Criar a Mensagem para o Mural e registra o Usuário Owner
                    nCodMural = Gravar_Mural("Solicitação de Agregação para o CP " & Microsoft.VisualBasic.Left(txtClaUni.Text, 9) & "00", 3, nCodUsuario, 0)

                    If nCodMural > 0 Then 'NÃO HOUVE ERRO AO INSERIR A MENSAGEM NO MURAL
                        'Habilita o Usuário para Gerenciar a mensagem do mural
                        cSql = "select * from ESI000 where SI000_GERAGR=1"
                        Using daTabela As New OleDbDataAdapter()
                            daTabela.SelectCommand = New OleDbCommand(cSql, g_ConnectBanco)

                            'Preencher o DataTable 
                            daTabela.Fill(dtLerUsuGerAgr)
                            If dtLerUsuGerAgr.Rows.Count > 0 Then
                                For x = 0 To dtLerUsuGerAgr.Rows.Count - 1
                                    nCodMural = Gravar_Mural("", 2, dtLerUsuGerAgr.Rows(x).Item("SI000_CODUSU"), nCodMural)
                                Next
                            End If
                        End Using
                        dtLerUsuGerAgr.Clear()

                        'Habilita o Usuário para Visualizar a mensagem do mural
                        ' Todos os Usuários que tem permissão de alteração ou Gerenciamento
                        ' do Conselho Particular
                        cSql = "SELECT EUN013.UN013_CODUSU FROM (EUN000 inner join EUN013 on EUN013.UN013_CODUNI=EUN000.UN000_CODRED) "
                        cSql += "WHERE EUN000.UN000_STAUNI='A' "
                        cSql += "AND EUN000.UN000_CLAUNI='" & Microsoft.VisualBasic.Left(txtClaUni.Text, 9) & "00" & "' "
                        cSql += "AND UN013_PERACE > 1"
                        Using daTabela As New OleDbDataAdapter()
                            daTabela.SelectCommand = New OleDbCommand(cSql, g_ConnectBanco)

                            'Preencher o DataTable 
                            daTabela.Fill(dtLerUsuGerAgr)
                            If dtLerUsuGerAgr.Rows.Count > 0 Then
                                For x = 0 To dtLerUsuGerAgr.Rows.Count - 1
                                    nCodMural = Gravar_Mural("", 1, dtLerUsuGerAgr.Rows(x).Item("UN013_CODUSU"), nCodMural)
                                Next
                            End If
                        End Using
                        dtLerUsuGerAgr.Clear()
                    End If
                End If
                bIncluir = False
                bAlterar = False

                If g_Param(1) = "INSERT" Then
                    'Continuar no form em modo alteracao
                    g_Comando = "alterar"
                    bAlterar = True
                    TratarObjetos()

                    'dt.Clear()
                    'fechar o form de cadastro
                    'Me.Close()
                Else
                    dt.Reset()
                    Using da As New OleDbDataAdapter()
                        da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                        ' Preencher o DataTable 
                        da.Fill(dt)
                    End Using
                    'Verificar se o comando veio do browse
                    If g_Comando = "inserir" Or g_Comando = "alterar" Then
                        dt.Clear()
                        Me.Close()
                    Else
                        TratarObjetos()
                    End If
                End If

            End Try
        Else
            MsgBox(cMensagem)
        End If
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        Call Excluir_Registro()

    End Sub

    Private Sub Excluir_Registro()
        Dim cSql As String
        Dim cMensagem As String = ""
        Dim cmd As OleDbCommand

        If MsgBox("Deseja excluir este registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "cadastro de Usuarios") = MsgBoxResult.Yes Then
            '?? Alterar para a Tabela a ser Excluída ??
            'cSql = "DELETE FROM EUN000 where UN000_CODRED = " & Integer.Parse(txtCodigo.Text)
            cSql = "UPDATE EUN015 set UN015_STAAGR='I', UN015_CODUSU=" & nCodUsuario.ToString & ", UN015_DATINA='" & FormatarData(Today()) & "' " & _
                "where UN015_NUMAGR = " & Integer.Parse(txtNumAgr.Text)

            cmd = New OleDbCommand(cSql, g_ConnectBanco)

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally

                dt.Reset()
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                    'Preencher o DataTable 
                    da.Fill(dt)
                End Using

                If i > dt.Rows.Count() - 1 Then
                    i = dt.Rows.Count() - 1
                End If
                'Verificar se o comando veio do browse
                If g_Comando = "excluir" Then
                    dt.Clear() 'Limpar o DataTable
                    Me.Close()
                Else
                    TratarObjetos()
                End If
            End Try

        Else
            MsgBox(cMensagem)
        End If

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
        Dim y As Integer, x As Integer

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

        For x = 0 To Me.TabAgregacao.TabCount - 1
            Me.TabAgregacao.SelectedIndex = x
            Relatorio.Graphics.DrawString(TabAgregacao.SelectedTab.Text, fonteNormal, Brushes.Black, margemEsq, 40, New StringFormat())
            Relatorio.Graphics.DrawLine(Pens.Black, margemEsq, 145, margemDir, 145)
            LinhaAdic = 2
            For Each FormControl In Me.TabAgregacao.SelectedTab.Controls
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
        Next x
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

        If g_Param(1) <> "" Then
            'Posicionar no registro selecionado
            '?? Alterar para localizar a chave da tabela ??
            For i_point = 0 To dt.Rows.Count() - 1

                If dt.Rows(i_point).Item("UN015_NUMAGR").ToString = g_Param(1) Then
                    Exit For
                End If
            Next
            i = i_point

            'Iniciar com o comando passado
            If g_Comando = "alterar" Then
                bIncluir = False
                bAlterar = True
            Else
                bIncluir = False
                bAlterar = False
            End If
        ElseIf g_Comando = "incluir" Then
            bIncluir = True
            bAlterar = True
        Else
            bIncluir = False
            bAlterar = False
        End If

        cCampos = "EUN015.UN015_NUMAGR, EUN015.UN015_CLAUNI, EUN015.UN015_NOMCFR, EUN015.UN015_CIDAGR, " & _
            "EUN015.UN015_NOMDIO, EUN015.UN015_NOMPAR, EUN015.UN015_CLACFR, EUN015.UN015_PAICFR, " & _
            "EUN015.UN015_ESTCFR, EUN015.UN015_CODCM, EUN015.UN015_CODCC, EUN015.UN015_CODCP, " & _
            "EUN015.UN015_DATFUN, EUN015.UN015_ENVFIC, EUN015.UN015_DATELE, EUN015.UN015_DATAGR, " & _
            "EUN015.UN015_CFATV1, EUN015.UN015_CFATV2, EUN015.UN015_CFATV3, EUN015.UN015_CFATV4, " & _
            "EUN015.UN015_CFATV5, EUN015.UN015_CFATV6, EUN015.UN015_REUDIA, EUN015.UN015_REUHOR, " & _
            "EUN015.UN015_REUEND, EUN015.UN015_DAUTCP, EUN015.UN015_DAUTCC, EUN015.UN015_DAUTCM, " & _
            "EUN015.UN015_DAUTCN, EUN015.UN015_DAUTCG, EUN015.UN015_DTCHCN, EUN015.UN015_DTENVI, " & _
            "EUN015.UN015_DTRECI, EUN015.UN015_DTENVC, EUN015.UN015_SITAGR, EUN015.UN015_NUMSUB, " & _
            "EUN015.UN015_NUMBEN, EUN015.UN015_PARJUR, EUN015.UN015_VISSEM, EUN015.UN015_MEISOC, " & _
            "EUN015.UN015_NUMASS, EUN015.UN015_RELCLE, EUN015.UN015_RELPUB, EUN015.UN015_RELSOC, " &
            "EUN015.UN015_APLREG, EUN015.UN015_DIFAPL, EUN015.UN015_MIS5IN, EUN015.UN015_OUTPAI, " & _
            "EUN015.UN015_COLREU, EUN015.UN015_OUTREC, EUN015.UN015_AJUENT, EUN015.UN015_AJUREC, " & _
            "EUN015.UN015_AJUOUT, EUN015.UN015_GSTPES, EUN015.UN015_GSTCON, EUN015.UN015_GSTDOA, " & _
            "EUN015.UN015_GSTEME, EUN015.UN015_GSTADM, EUN015.UN015_PE1REF, EUN015.UN015_AN1REF, " & _
            "EUN015.UN015_PE2REF, EUN015.UN015_AN2REF"

        TratarObjetos()

    End Sub

    Private Sub Carregardados_Mandato()

        txtUN016_CodMdt.Text = dtMandato.Rows(i_Mandato).Item("UN016_CODMDT")
        txtUN016_DesMdt.Text = dtMandato.Rows(i_Mandato).Item("UN016_DESMDT")
        dtpUN016_DatIni.Value = dtMandato.Rows(i_Mandato).Item("UN016_DATINI")
        dtpUN016_DatFin.Value = dtMandato.Rows(i_Mandato).Item("UN016_DATFIN")

        'Montar o Grid com os encargos
        Call CarregarGrid(dtMandato.Rows(0).Item("UN016_CODMDT"))

        btnGravar_Mandato.Enabled = True

    End Sub

    Private Sub CarregarGrid(fCodMandato As Integer)
        Dim dtGrid As DataTable = New DataTable("EUN012")
        Dim cmdEncargos As OleDbCommand
        Dim sNivelUn As String
        Dim nCodigomandato As Integer

        dtgMandato.DataSource = Nothing

        If txtClaUni.Text = "00.00.00.00" Then
            sNivelUn = "CNB"
        ElseIf Microsoft.VisualBasic.Right(txtClaUni.Text, 8) = "00.00.00" Then
            sNivelUn = "CM"
        ElseIf Microsoft.VisualBasic.Right(txtClaUni.Text, 5) = "00.00" Then
            sNivelUn = "CC"
        ElseIf Microsoft.VisualBasic.Right(txtClaUni.Text, 2) = "00" Then
            sNivelUn = "CP"
        Else
            sNivelUn = "CF"
        End If

        'Ler os Encargos do mandado ativo da unidade para carregar a grid de encargos
        cQuery = "Select EUN012.UN012_CODOCP as enc, EUN011.UN011_DESOCP as Encargo, " & _
            "EUN012.UN012_CODCOL as cfd, EUN003.UN003_NOMCOL as Confrade " & _
            "from ((EUN012 INNER JOIN EUN011 ON EUN011.UN011_CODOCP=EUN012.UN012_CODOCP) " & _
            "LEFT JOIN EUN003 ON EUN003.UN003_CODCOL=EUN012.UN012_CODCOL) " & _
            "INNER JOIN EUN016 ON EUN016.UN016_CODMDT=EUN012.UN012_CODMDT AND EUN016.UN016_CODRED=EUN012.UN012_CODRED " & _
            "WHERE EUN016.UN016_CODRED=" & txtID_CF.Text & " AND EUN011.UN011_NIVOCP='" & sNivelUn & "'" & _
            " ORDER BY EUN011.UN011_DESOCP"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable  
            da.Fill(dtGrid)
        End Using

        If dtGrid.Rows.Count = 0 Then
            'Não encontrou nenhum encargo cadastrado para esta unidade
            dtGrid.Clear()

            'Criar um Mandato provisório para associar os encargos
            nCodigomandato = InserirMandato(CDbl(txtID_CF.Text))


            'Ler os Encargos e atribuir a este mandato na Unidade
            cQuery = " select * from EUN011 where EUN011.UN011_NIVOCP='" & sNivelUn & "'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable  
                da.Fill(dtGrid)
            End Using
            If dtGrid.Rows.Count > 0 Then
                For x = 0 To dtGrid.Rows.Count - 1
                    'INSERIR O MANDATO
                    cQuery = "INSERT INTO EUN012 (UN012_CODMDT, UN012_CODOCP, UN012_CODCOL, UN012_CODRED) VALUES " & _
                        "(" & nCodigomandato & "," & dtGrid.Rows(x).Item("UN011_CODOCP") & ",0," & txtID_CF.Text & ")"
                    cmdEncargos = New OleDbCommand(cQuery, g_ConnectBanco)

                    Try
                        cmdEncargos.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                    End Try
                Next

                'REFAZER A LEITURA DOS ENCARGOS DO MANDATO
                dtGrid.Clear()
                cQuery = "Select EUN012.UN012_CODOCP as Cod, EUN011.UN011_DESOCP as Encargo, " & _
                    "EUN012.UN012_CODCOL as Cod, EUN003.UN003_NOMCOL as Confrade " & _
                    "from ((EUN012 INNER JOIN EUN011 ON EUN011.UN011_CODOCP=EUN012.UN012_CODOCP) " & _
                    "LEFT JOIN EUN003 ON EUN003.UN003_CODCOL=EUN012.UN012_CODCOL) " & _
                    "INNER JOIN EUN016 ON EUN016.UN016_CODMDT=EUN012.UN012_CODMDT AND EUN016.UN016_CODRED=EUN012.UN012_CODRED " & _
                    "WHERE EUN016.UN016_CODRED=" & txtID_CF.Text & " AND EUN011.UN011_NIVOCP='" & sNivelUn & "'" & _
                    " ORDER BY EUN011.UN011_DESOCP"
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                    ' Preencher o DataTable  
                    da.Fill(dtGrid)
                End Using
            Else
                Exit Sub
            End If
        End If

        dtgMandato.DataSource = dtGrid
        dtgMandato.Columns(0).Width = 50
        dtgMandato.Columns(1).Width = 300
        dtgMandato.Columns(2).Width = 50
        dtgMandato.Columns(3).Width = 300

    End Sub

    Private Sub txtUN016_DesMdt_GotFocus(sender As Object, e As EventArgs) Handles txtUN016_DesMdt.GotFocus, dtpUN016_DatIni.GotFocus, dtpUN016_DatFin.GotFocus
        btnAnt_Mandato.Enabled = True
    End Sub


    Private Sub btnProx_Mandato_Click(sender As Object, e As EventArgs) Handles btnProx_Mandato.Click
        Dim nCodigomandato As Integer

        If i_Mandato = dtMandato.Rows.Count - 1 And Not dtpUN016_DatFin.Value = dtpUN016_DatIni.Value Then
            nCodigomandato = InserirMandato(txtID_CF.Text)
            If nCodigomandato > 0 Then
                dtMandato.Clear()
                cQuery = "Select * from EUN016 where UN016_CODRED=" & txtID_CF.Text & _
                    " and UN016_CODMDT=" & nCodigomandato.ToString
                Using da As New OleDbDataAdapter()
                    da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                    ' Preencher o DataTable 
                    da.Fill(dtMandato)
                End Using

                i_Mandato = 0
                Call Carregardados_Mandato()

            End If
        Else
            i_Mandato += 1

        End If

    End Sub

    Private Sub btnAnt_Mandato_Click(sender As Object, e As EventArgs) Handles btnAnt_Mandato.Click

        If i_Mandato > 0 Then
            i_Mandato -= 1
        End If

    End Sub

    Private Sub btnGravar_Mandato_Click(sender As Object, e As EventArgs) Handles btnGravar_Mandato.Click
        Dim cmdMandato As OleDbCommand

        cQuery = "UPDATE EUN016 SET UN016_DESMDT='" & txtUN016_DesMdt.Text & "' " & _
            ", UN016_DATINI='" & FormatarData(dtpUN016_DatIni.Value) & "' " & _
            ", UN016_DATFIN='" & FormatarData(dtpUN016_DatFin.Value) & "' "
        cQuery += "WHERE UN016_CODMDT=" & txtUN016_CodMdt.Text & " AND UN016_CODRED=" & txtID_CF.Text
        cmdMandato = New OleDbCommand(cQuery, g_ConnectBanco)

        Try
            cmdMandato.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            ComandoPesquisar.Enabled = True
        End Try

    End Sub

    Private Sub ComandoPesquisar_Click(sender As Object, e As EventArgs) Handles ComandoPesquisar.Click
        Dim nPos As Integer = 99
        Dim nSeq As Integer = 0
        Dim nStart As Integer = 1
        Dim sNome(10) As String
        Dim sItemLista As String
        Dim dtPesquisar As DataTable = New DataTable("EUN003")

        If Not Trim(NomePesquisar.Text) = "" Then
            cQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_BAIRRO " & _
                ", EUN003.UN003_CIDADE, EUN003.UN003_SIGEST, EUN003.UN003_DTNASC from EUN003 " & _
                "where EUN003.UN003_SITCOL<>'I'"
            nStart = 1

            ListaPesquisa.Items.Clear()

            Do Until nPos = 0
                nPos = InStr(nStart, NomePesquisar.Text, " ")
                If nPos > 0 Then
                    sNome(nSeq) = Mid(NomePesquisar.Text, nStart, nPos - nStart)
                    nStart = nPos + 1
                    nSeq += 1
                Else
                    sNome(nSeq) = Mid(NomePesquisar.Text, nStart, Len(NomePesquisar.Text) - nStart + 1)
                End If
            Loop
            'Montar a Condição
            For nPos = 0 To nSeq
                cQuery += " and EUN003.UN003_NOMCOL LIKE '%" & sNome(nPos) & "%'"
            Next nPos
            cQuery += " order by EUN003.UN003_NOMCOL"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtPesquisar)
            End Using

            For nSeq = 0 To dtPesquisar.Rows.Count - 1
                sItemLista = Format(dtPesquisar.Rows(nSeq).Item("UN003_CODCOL"), "000000") & " | "
                sItemLista += IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL")), "", dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL"))
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC")) Then
                    sItemLista += " | " & Format(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC"), "dd/MM/yy")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")
                End If

                ListaPesquisa.Items.Add(sItemLista)
            Next nSeq

            dtPesquisar.Clear()
        Else
            MsgBox("Digite um nome para pesquisar. Para melhor performance, digitar nome e sobrenome.'")
        End If

    End Sub

    Private Sub ListaPesquisa_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListaPesquisa.MouseDoubleClick

        If dtgMandato.SelectedRows.Count > 0 Then
            'Gravar o Ocupante do Cargo
            Dim cmdMandato As OleDbCommand

            cQuery = "UPDATE EUN012 SET UN012_CODCOL=" & Microsoft.VisualBasic.Left(ListaPesquisa.SelectedItem, 6)
            cQuery += " WHERE UN012_CODMDT=" & txtUN016_CodMdt.Text
            cQuery += " AND UN012_CODOCP=" & dtgMandato.SelectedRows.Item(0).Cells(0).Value
            cQuery += " AND UN012_CODRED=" & txtID_CF.Text
            cmdMandato = New OleDbCommand(cQuery, g_ConnectBanco)

            Try
                cmdMandato.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGrid(txtUN016_CodMdt.Text)
            End Try

            'MsgBox(ListaPesquisa.SelectedItem & " grid: " & dtgMandato.SelectedRows.Item(0).Cells(0).Value)
        End If

    End Sub

    Private Sub dtgMandato_MouseClick(sender As Object, e As MouseEventArgs) Handles dtgMandato.MouseClick

        If dtgMandato.SelectedRows.Count > 0 Then
            NomePesquisar.Enabled = True
            ComandoPesquisar.Enabled = True
            labelEncargo.Text = dtgMandato.SelectedRows.Item(0).Cells(1).Value
            If IsDBNull(dtgMandato.SelectedRows.Item(0).Cells(3).Value) Then
                NomePesquisar.Text = ""
            Else
                NomePesquisar.Text = dtgMandato.SelectedRows.Item(0).Cells(3).Value
            End If
        Else
            NomePesquisar.Enabled = False
            ComandoPesquisar.Enabled = False
            NomePesquisar.Text = ""
            ListaPesquisa.Items.Clear()
        End If

    End Sub

    Private Sub btnPesqMembro_Click(sender As Object, e As EventArgs) Handles btnPesqMembro.Click
        Dim nPos As Integer = 99
        Dim nSeq As Integer = 0
        Dim nStart As Integer = 1
        Dim sNome(10) As String
        Dim sItemLista As String
        Dim dtPesquisar As DataTable = New DataTable("EUN003")

        If Not Trim(txtPesqMembro.Text) = "" Then
            cQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_BAIRRO, " & _
                "EUN003.UN003_CIDADE, EUN003.UN003_SIGEST, EUN003.UN003_DTNASC from EUN003 " & _
                "where EUN003.UN003_SITCOL<>'I' AND EUN003.UN003_CODUNI=0"
            nStart = 1

            lstPesqMembro.Items.Clear()

            Do Until nPos = 0
                nPos = InStr(nStart, txtPesqMembro.Text, " ")
                If nPos > 0 Then
                    sNome(nSeq) = Mid(txtPesqMembro.Text, nStart, nPos - nStart)
                    nStart = nPos + 1
                    nSeq += 1
                Else
                    sNome(nSeq) = Mid(txtPesqMembro.Text, nStart, Len(txtPesqMembro.Text) - nStart + 1)
                End If
            Loop
            'Montar a Condição
            For nPos = 0 To nSeq
                cQuery += " and EUN003.UN003_NOMCOL LIKE '%" & sNome(nPos) & "%'"
            Next nPos
            cQuery += " order by EUN003.UN003_NOMCOL"

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtPesquisar)
            End Using

            For nSeq = 0 To dtPesquisar.Rows.Count - 1
                sItemLista = Format(dtPesquisar.Rows(nSeq).Item("UN003_CODCOL"), "000000") & " | "
                sItemLista += IIf(IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL")), "", dtPesquisar.Rows(nSeq).Item("UN003_NOMCOL"))
                
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC")) Then
                    sItemLista += " | " & Format(dtPesquisar.Rows(nSeq).Item("UN003_DTNASC"), "dd/MM/yy")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_SIGEST")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_CIDADE")
                End If
                If Not IsDBNull(dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")) Then
                    sItemLista += " | " & dtPesquisar.Rows(nSeq).Item("UN003_BAIRRO")
                End If

                lstPesqMembro.Items.Add(sItemLista)
            Next nSeq

            dtPesquisar.Clear()
        Else
            MsgBox("Digite um nome para pesquisar. Para melhor performance, digitar nome e sobrenome.'")
        End If

    End Sub

    Private Sub lstPesqMembro_DoubleClick(sender As Object, e As EventArgs) Handles lstPesqMembro.DoubleClick

        If lstPesqMembro.SelectedItem.ToString <> "" Then
            'Incluir o Colaborador na Conferência
            Dim cmdMandato As OleDbCommand

            cQuery = "UPDATE EUN003 SET UN003_CODUNI=" & txtID_CF.Text
            cQuery += " WHERE UN003_CODCOL=" & Microsoft.VisualBasic.Left(lstPesqMembro.SelectedItem, 6)
            cmdMandato = New OleDbCommand(cQuery, g_ConnectBanco)

            Try
                cmdMandato.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGridMembro()
            End Try

            'MsgBox(ListaPesquisa.SelectedItem & " grid: " & dtgMandato.SelectedRows.Item(0).Cells(0).Value)
        End If

    End Sub

    Private Sub CarregarGridMembro()
        Dim dtGridMembro As DataTable = New DataTable("EUN003")

        dtgMembroAtivo.DataSource = Nothing

        cQuery = "Select EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN003.UN003_DTNASC " & _
            "from EUN003 WHERE EUN003.UN003_CODUNI=" & txtID_CF.Text & _
            " ORDER BY EUN003.UN003_NOMCOL"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable  
            da.Fill(dtGridMembro)
        End Using

        lblContadorMembro.Text = "Número de Membros Ativos: " & Format(dtGridMembro.Rows.Count, "###0")
        dtgMembroAtivo.DataSource = dtGridMembro
        dtgMembroAtivo.Columns(0).HeaderText = "Cod."
        dtgMembroAtivo.Columns(0).Width = 50
        dtgMembroAtivo.Columns(1).HeaderText = "Nome"
        dtgMembroAtivo.Columns(1).Width = 300
        dtgMembroAtivo.Columns(2).HeaderText = "Dt.Nasc."
        dtgMembroAtivo.Columns(2).Width = 100

    End Sub

    Private Sub dtgMembroAtivo_DoubleClick(sender As Object, e As EventArgs) Handles dtgMembroAtivo.DoubleClick

        If dtgMembroAtivo.SelectedRows.Count > 0 Then
            'Preparar o comando para Exluir o Colaborador
            'txtPesqMembro.Text = dtgMembroAtivo.SelectedRows.Item(0).Cells(2).Value
            If IsDBNull(dtgMembroAtivo.SelectedRows.Item(0).Cells(1).Value) Then
                txtPesqMembro.Text = ""
            Else
                txtPesqMembro.Text = dtgMembroAtivo.SelectedRows.Item(0).Cells(1).Value
            End If

            'Comando para retirar o associado
            Dim cmdRetirarMembro As OleDbCommand

            cQuery = "UPDATE EUN003 SET UN003_CODUNI=0"
            cQuery += " WHERE UN003_CODCOL=" & dtgMembroAtivo.SelectedRows.Item(0).Cells(0).Value
            cmdRetirarMembro = New OleDbCommand(cQuery, g_ConnectBanco)

            Try
                cmdRetirarMembro.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                CarregarGridMembro()
            End Try

            txtPesqMembro.Text = ""

        End If

    End Sub

    Private Sub ListaPesquisa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListaPesquisa.SelectedIndexChanged

    End Sub

    Private Sub btnLocCParticular_Click(sender As Object, e As EventArgs) Handles btnLocCParticular.Click
        Dim options = New dlgConselho

        dlgConselho.txtPesquisa.Text = txtClaUni.Text
        If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtClaUni.Text = Microsoft.VisualBasic.Left(getParametro(options.txtPesquisa.Text, "|", 2), 8) & ".??"
            Call CarregarUnidadesOwner_CF(txtClaUni.Text)
        End If
    End Sub

    Private Sub CarregarUnidadesOwner_CF(fClasseCP As String)
        ' Preencher os campos das estruturas owner da nova conferência 
        Dim nCodigo As Integer
        Dim sNomeUnidade As String = ""

        If Microsoft.VisualBasic.Len(fClasseCP) < 11 Then
            Exit Sub
        End If

        'CM
        nCodigo = LerCod_Unidade(Microsoft.VisualBasic.Left(fClasseCP, 2) & ".00.00.00", sNomeUnidade)
        If nCodigo > 0 Then
            txtCM.Text = Microsoft.VisualBasic.Left(fClasseCP, 2) & ".00.00.00" & " - " & sNomeUnidade
        Else
            txtCM.Text = ""
        End If

        'CC
        nCodigo = LerCod_Unidade(Microsoft.VisualBasic.Left(fClasseCP, 5) & ".00.00", sNomeUnidade)
        If nCodigo > 0 Then
            txtCC.Text = Microsoft.VisualBasic.Left(fClasseCP, 5) & ".00.00" & " - " & sNomeUnidade
        Else
            txtCC.Text = ""
        End If

        'CP
        nCodigo = LerCod_Unidade(Microsoft.VisualBasic.Left(fClasseCP, 8) & ".00", sNomeUnidade)
        If nCodigo > 0 Then
            txtCP.Text = Microsoft.VisualBasic.Left(fClasseCP, 8) & ".00" & " - " & sNomeUnidade
        Else
            txtCP.Text = ""
        End If

        'Call MostrarClasse()

    End Sub

End Class