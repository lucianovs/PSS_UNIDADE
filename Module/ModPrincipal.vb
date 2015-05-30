'Módulo Principal com as funções Globais
Imports System.Data.OleDb
Imports System.IO
Imports System.Text

Module ModPrincipal
    'Declaração das variáveis Globais
    Public g_ConnectString As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=SSVP.mdb"
    Public g_ConnectBanco = New OleDbConnection()
    'Variaveis Controle Login
    Public g_Login As String
    Public g_Nivel As Integer
    Public g_AtuBrowse As Boolean 'Variavel para identificar se deve atualiza o browse no timer

    'Guarda os parâmetros para passar do Browse para aos cadastros
    Public g_Param(6) As String
    'Definição do Projeto (Deve ser incializado no Load do Form Principal e ter o mesmo nome do projeto (Sensitivo)
    Public g_Modulo As String
    'Definição do comando que o browse envio para o formulário de cadastro
    Public g_Comando As String
    'Variável para Identificar a Estrutura para Inserir Unidades
    Public g_Unidade As String
    'Variavel para o DialogBox da Impressora


    'Variáveis utilizados na Pesquisa de registros
    Friend g_Pesq1, g_Pesq2, g_Pesq3 As String

    Private Declare Auto Function GetPrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    'Usa a função GetPrivateProfileString para obter os valores do Arquivo Ini
    Public Function LerDadosINI(ByVal file_name As String, ByVal section_name As String, ByVal key_name As String, ByVal default_value As String) As String
        Const MAX_LENGTH As Integer = 500

        Dim string_builder As New StringBuilder(MAX_LENGTH)

        GetPrivateProfileString(section_name, key_name, default_value, string_builder, MAX_LENGTH, file_name)

        Return string_builder.ToString()

    End Function

    ' Retorna o nome do arquivo INI 
    Public Function nomeArquivoINI() As String
        Dim nome_arquivo_ini As String = Application.StartupPath

        'Return nome_arquivo_ini & "\" & Application.ProductName & ".ini"
        Return nome_arquivo_ini & "\SSVP.ini"

    End Function

    Public Sub GravarDadosIni(ByVal section_name As String, ByVal key_name As String, cValor As String)
        Dim nome_arquivo_ini As String = nomeArquivoINI()

        WritePrivateProfileString(section_name, key_name, cValor, nome_arquivo_ini)

    End Sub

    Public Function ConectarBanco() As Boolean
        Dim bConnect As Boolean

        'Descriptografar o String de Conexao
        g_ConnectString = ClassCrypt.Decrypt(g_ConnectString)
        '???
        'g_ConnectString = Replace(g_ConnectString, "C:\", "D:\")
        '???

        If Not g_ConnectBanco.state = 1 Then
            Try
                g_ConnectBanco.ConnectionString = g_ConnectString
                g_ConnectBanco.Open()
                bConnect = True
            Catch ex As Exception
                MsgBox("Erro ao conectar ao Banco de Dados do Sistema." & vbCrLf & ex.Message)
                bConnect = False
            Finally

            End Try
        Else
            bConnect = True
        End If
        'Criptografar Novamente
        g_ConnectString = ClassCrypt.Encrypt(g_ConnectString)

        Return bConnect

    End Function

    Public Function LerUsuario(ByVal fLgiUsu As String, Optional fPswUsu As String = "") As String
        Dim cQuery As String
        Dim dtUsuario As DataTable = New DataTable("ESI000")

        cQuery = "SELECT SI000_LGIUSU, SI000_NOMUSU, SI000_PASLGI, SI000_DATEXP, SI000_ALTPAS " & _
            "FROM ESI000 where SI000_LGIUSU = '" & fLgiUsu & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)

            If dtUsuario.Rows.Count() > 0 Then
                If dtUsuario.Rows(0).Item("SI000_DATEXP") < Today() And Format(dtUsuario.Rows(0).Item("SI000_DATEXP"), "yyyy") <> 1900 Then
                    MsgBox("Usuário expirado. Favor contactar o administrador.")
                    LerUsuario = ""
                Else
                    LerUsuario = dtUsuario.Rows(0).Item("SI000_LGIUSU")
                    If Not IsNothing(fPswUsu) Then
                        If fPswUsu <> ClassCrypt.Decrypt(dtUsuario.Rows(0).Item("SI000_PASLGI")) Then
                            LerUsuario = ""
                        Else
                            If dtUsuario.Rows(0).Item("SI000_ALTPAS") = 1 Then
                                g_Param(2) = dtUsuario.Rows(0).Item("SI000_LGIUSU")
                            Else
                                g_Param(2) = ""
                            End If
                        End If
                    End If
                End If
            Else
                LerUsuario = ""
            End If
        End Using
        dtUsuario.Clear()

    End Function

    Public Function Usuario_GerAgregacao(ByVal fLgiUsu As String) As Integer
        'Retorna 0 - Não gerencia
        'Retorna 1 - Gerencia 
        Dim cQuery As String
        Dim dtUsuario As DataTable = New DataTable("ESI000")

        cQuery = "SELECT SI000_LGIUSU, SI000_NOMUSU, SI000_PASLGI, SI000_DATEXP, SI000_ALTPAS " & _
            "FROM ESI000 where SI000_LGIUSU = '" & fLgiUsu & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)

            If dtUsuario.Rows.Count() > 0 Then
                Usuario_GerAgregacao = IIf(IsDBNull(dtUsuario.Rows(0).Item("SI000_GERAGR")), 0, dtUsuario.Rows(0).Item("SI000_GERAGR"))
            Else
                Usuario_GerAgregacao = 0
            End If
        End Using
        dtUsuario.Clear()
    End Function

    Public Function NivelAcesso(fCodusuario As Integer, fCodModulo As Integer, fNomeOpcao As String, fPrincOpcao As String) As Integer
        Dim cQuery As String
        Dim dtAcesso As DataTable = New DataTable("ESI002")
        'Dim dtGrupoAcesso As DataTable = New DataTable("ESI000")
        Dim nCodOpcao As Integer
        Dim nNivelAcesso As Integer

        nCodOpcao = getCodOpcao(fCodModulo, fNomeOpcao, fPrincOpcao)

        cQuery = "SELECT SI002_NIVACE FROM ESI002 where SI002_CODUSU = " & fCodusuario & _
                " and SI002_CODOPC = " & nCodOpcao

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtAcesso)
        End Using

        If dtAcesso.Rows.Count() > 0 Then
            nNivelAcesso = dtAcesso.Rows(0).Item("SI002_NIVACE")
        Else
            nNivelAcesso = 0
        End If

        If nNivelAcesso < 4 Then
            'Verificar a permissão pelo grupo é Superior
            cQuery = "SELECT SI000_CODUSU, max(SI002_NIVACE) as NIVACE FROM ESI002, ESI000, ESI006 " & _
                " where SI006_CODUSU=SI000_CODUSU and SI002_CODGRU=SI006_CODGRU " & _
                " and SI000_CODUSU = " & fCodusuario & " and SI002_CODOPC = " & nCodOpcao & _
                " group by SI000_CODUSU"

            dtAcesso.Reset()

            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

                ' Preencher o DataTable 
                da.Fill(dtAcesso)
            End Using

            If dtAcesso.Rows.Count() > 0 Then
                If dtAcesso.Rows(0).Item("NIVACE") > nNivelAcesso Then
                    nNivelAcesso = dtAcesso.Rows(0).Item("NIVACE")
                End If
            End If
        End If
        Return nNivelAcesso

    End Function

    Public Function getCodUsuario(ByVal fLoginUsuario As String) As Integer
        Dim cQuery As String
        Dim dtUsuario As DataTable = New DataTable("ESI000")

        cQuery = "SELECT SI000_CODUSU FROM ESI000 where SI000_LGIUSU = '" & Trim(fLoginUsuario) & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)

            If dtUsuario.Rows.Count() > 0 Then
                Return dtUsuario.Rows(0).Item("SI000_CODUSU")
            Else
                Return 0
            End If
        End Using

    End Function

    Public Function getLoginUsuario(ByVal fCodUsuario As Integer) As String
        Dim cQuery As String
        Dim dtUsuario As DataTable = New DataTable("ESI000")

        cQuery = "SELECT SI000_LGIUSU FROM ESI000 where SI000_CODUSU  = " & fCodUsuario.ToString

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)

            If dtUsuario.Rows.Count() > 0 Then
                Return dtUsuario.Rows(0).Item("SI000_LGIUSU")
            Else
                Return ""
            End If
        End Using

    End Function

    Public Function getCodGrupo(ByVal fDescrGrupo As String) As Integer
        Dim cQuery As String
        Dim dtGrupo As DataTable = New DataTable("ESI001")

        cQuery = "SELECT SI001_CODGRU FROM ESI001 where SI001_DESGRU = '" & Trim(fDescrGrupo) & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtGrupo)

            If dtGrupo.Rows.Count() > 0 Then
                Return dtGrupo.Rows(0).Item("SI001_CODGRU")
            Else
                Return 0
            End If
        End Using
        dtGrupo.Clear()

    End Function

    Public Function getDescrGrupo(ByVal fCodGrupo As Integer) As String
        Dim cQuery As String
        Dim dtGrupo As DataTable = New DataTable("ESI001")

        cQuery = "SELECT SI001_DESGRU FROM ESI001 where SI001_CODGRU = " & Trim(fCodGrupo)

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtGrupo)

            If dtGrupo.Rows.Count() > 0 Then
                Return dtGrupo.Rows(0).Item("SI001_DESGRU")
            Else
                Return ""
            End If
        End Using
        dtGrupo.Clear()

    End Function

    Public Function getCodModulo(ByVal fNomeModulo As String) As Integer
        Dim sQuery As String
        Dim dtModulo As DataTable = New DataTable("ESI004")

        sQuery = "SELECT SI004_CODMOD FROM ESI004 where SI004_DESMOD = '" & Trim(fNomeModulo) & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtModulo)

            If dtModulo.Rows.Count() > 0 Then
                Return dtModulo.Rows(0).Item("SI004_CODMOD")
            Else
                Return 0
            End If
        End Using

    End Function

    Public Function getCodOpcao(fCodModulo As Integer, fNomeOpcao As String, fPrincOpcao As String) As Integer
        Dim sQuery As String
        Dim dtModulo As DataTable = New DataTable("ESI003")

        sQuery = "SELECT SI003_CODOPC FROM ESI003 where SI003_NOMOPC = '" & Trim(fNomeOpcao) & _
                "' AND SI003_CODMOD = " & fCodModulo.ToString

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtModulo)

            If dtModulo.Rows.Count() > 0 Then
                Return dtModulo.Rows(0).Item("SI003_CODOPC")
            Else
                'Se a Função não estiver cadastrada, cadastrar
                Return InserirOpcao(fCodModulo, fNomeOpcao, fPrincOpcao)
            End If
        End Using

    End Function

    Private Function InserirOpcao(fCodModulo As Integer, fNomeOpcao As String, fPrincOpcao As String) As Integer
        Dim cSql As String = ""
        Dim cmd As OleDbCommand
        Dim nCodOpcao As Integer

        nCodOpcao = ProxCodOpcao() 'Buscar o próximo código da opção

        'Gravar a Opcao no Banco
        cSql = "INSERT INTO ESI003 (SI003_CODOPC, SI003_DESOPC, SI003_NOMOPC, SI003_PRIOPC, SI003_CODMOD)"
        cSql += " values (" & nCodOpcao & ", '" & fNomeOpcao & "','" & fNomeOpcao & _
                "','" & fPrincOpcao & "'," & fCodModulo & ")"

        cmd = New OleDbCommand(cSql, g_ConnectBanco)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            'Se não ocorrer erros, gravar o acesso para o grupo de administradores
            cSql = "INSERT INTO ESI002 (SI002_CODUSU, SI002_CODGRU, SI002_CODOPC, SI002_NIVACE)"
            cSql += " values (0,1," & nCodOpcao & ",4)"
            'Constantes no Insert ESI002
            'Codigo do usuario = 0 -> Valer a permissão do grupo
            'Codigo Grupo = 1 -> Grupo de Administradores (Padrão)
            'Permissão = 4 -> Acesso Total
            cmd = New OleDbCommand(cSql, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
        End Try

        Return nCodOpcao

    End Function

    Public Function ProxCodOpcao() As Double
        Dim cQuery As String
        Dim dtCodOpcao As DataTable = New DataTable("ESI003")

        cQuery = "SELECT max(SI003_CODOPC) as UltCod FROM ESI003"

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtCodOpcao)
            If IsDBNull(dtCodOpcao.Rows(0).Item("UltCod")) Then
                Return 1
            Else
                Return dtCodOpcao.Rows(0).Item("UltCod") + 1
            End If

        End Using
    End Function

    Public Function ProxCodChave(fEntidade As String, fChave As String, Optional fValorChave As String = "") As Double
        Dim cQuery As String
        Dim cCampoMax As String = ""
        Dim cGroupBy As String = ""
        Dim cWhere As String = ""
        Dim x As Integer
        Dim dtCodOpcao As DataTable = New DataTable(fEntidade)
        Dim cCampos() As String = fChave.Split(",")
        Dim cValores() As String = fValorChave.Split(",")

        For x = 0 To cCampos.Length - 1
            If x = cCampos.Length - 1 Then
                cCampoMax = cCampos(x)
            Else
                If x > 0 Then
                    cGroupBy += ", "
                    cWhere += " and "
                End If
                cGroupBy += cCampos(x)
                cWhere += cCampos(x) & "=" & cValores(x)
            End If
        Next
        'If cGroupBy <> "" Then cGroupBy += ", "

        cQuery = "SELECT " & cGroupBy & IIf(Trim(cGroupBy) = "", "", ", ") & "max(" & cCampoMax & ") as UltCod FROM " & fEntidade
        If Not Trim(cWhere) = "" Then cWhere = " Where " & cWhere
        If Not Trim(cGroupBy) = "" Then cGroupBy = " group by " & cGroupBy
        cQuery += cWhere & " " & cGroupBy

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtCodOpcao)
            If dtCodOpcao.Rows.Count() > 0 Then
                If IsDBNull(dtCodOpcao.Rows(0).Item("UltCod")) Then
                    Return 1
                Else
                    Return dtCodOpcao.Rows(0).Item("UltCod") + 1
                End If
            Else
                Return 1
            End If

        End Using
        dtCodOpcao.Clear()

    End Function

    Public Function LerCod_Unidade(fClass_Uni As String, Optional ByRef fNomeUnidade As String = "") As Double
        Dim cQuery As String
        Dim dtLerCodUni As DataTable = New DataTable("EUN000")

        cQuery = "SELECT UN000_CODRED, UN000_NOMUNI FROM EUN000 where UN000_CLAUNI = '" & fClass_Uni & "'"

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerCodUni)
            If dtLerCodUni.Rows.Count > 0 Then
                fNomeUnidade = dtLerCodUni.Rows(0).Item("UN000_NOMUNI")
                Return dtLerCodUni.Rows(0).Item("UN000_CODRED")
            Else
                Return 0
            End If
        End Using

        dtLerCodUni.Clear()

    End Function

    Public Function LerClasse_Unidade(fCodReduzido As Integer, Optional ByRef fNomeUnidade As String = "") As String
        Dim cQuery As String
        Dim dtLerCodUni As DataTable = New DataTable("EUN000")

        cQuery = "SELECT UN000_CLAUNI, UN000_NOMUNI FROM EUN000 where UN000_CODRED = " & fCodReduzido

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerCodUni)
            If dtLerCodUni.Rows.Count > 0 Then
                fNomeUnidade = dtLerCodUni.Rows(0).Item("UN000_NOMUNI")
                Return dtLerCodUni.Rows(0).Item("UN000_CLAUNI")
            Else
                Return ""
            End If
        End Using

        dtLerCodUni.Clear()

    End Function

    Public Function ProxSeq_Unidade(fClaUnidade As String, fUnidadeOwner As String) As String
        Dim cQuery As String = ""
        Dim dtProxSeqUni As DataTable = New DataTable("EUN000")
        Dim nPos As Integer
        Dim sProxSeq As String = ""

        ProxSeq_Unidade = ""
        If fClaUnidade = "CM" Then 'CM
            cQuery = "right(UN000_CLAUNI,8)='00.00.00'"
            nPos = 1
        ElseIf fClaUnidade = "CC" Then 'CC
            cQuery = "right(UN000_CLAUNI,5)='00.00' and left(UN000_CLAUNI,2) = '" & Microsoft.VisualBasic.Left(fUnidadeOwner, 2) & "'"
            nPos = 4
        ElseIf fClaUnidade = "CP" Then 'CP
            cQuery = "right(UN000_CLAUNI,2)='00' and left(UN000_CLAUNI,5) = '" & Microsoft.VisualBasic.Left(fUnidadeOwner, 5) & "'"
            nPos = 7
        Else 'CF
            cQuery = "right(UN000_CLAUNI,2)<>'00' and left(UN000_CLAUNI,8) = '" & Microsoft.VisualBasic.Left(fUnidadeOwner, 8) & "'"
            nPos = 10
        End If

        cQuery = "SELECT max(UN000_CLAUNI) as UltSeq FROM EUN000 where " & cQuery & " and UN000_STAUNI<>'I'"

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtProxSeqUni)
            If dtProxSeqUni.Rows.Count > 0 Then
                If Not IsDBNull(dtProxSeqUni.Rows(0).Item("UltSeq")) Then
                    sProxSeq = Microsoft.VisualBasic.Mid(dtProxSeqUni.Rows(0).Item("UltSeq"), nPos, 2)
                Else
                    sProxSeq = "00"
                End If
                sProxSeq = (Val(sProxSeq) + 1).ToString("00")

                For x = 1 To 11
                    If x = nPos Then
                        ProxSeq_Unidade = ProxSeq_Unidade & Microsoft.VisualBasic.Left(sProxSeq, 1)
                    ElseIf x = nPos + 1 Then
                        ProxSeq_Unidade = ProxSeq_Unidade & Microsoft.VisualBasic.Right(sProxSeq, 1)
                    Else
                        ProxSeq_Unidade = ProxSeq_Unidade & Microsoft.VisualBasic.Mid(fUnidadeOwner, x, 1)
                    End If
                Next
            End If
        End Using

        dtProxSeqUni.Clear()

    End Function

    Public Function ConfigIcones(fLogin As String, fNomeOpcao As String, ByRef fLocalX As Integer, _
                ByRef fLocalY As Integer, ByRef fCorFundo As Integer) As Integer
        Dim sQuery As String
        Dim dtConfig As DataTable = New DataTable("ESI005")

        sQuery = "SELECT SI005_TAMOBJ, SI005_LOCALX, SI005_LOCALY, SI005_CORFDO FROM ESI005 where " & _
                "SI005_CODUSU = " & getCodUsuario(ClassCrypt.Decrypt(fLogin)) & _
                " and SI005_CODOPC = " & getCodOpcao(0, Trim(fNomeOpcao), "")

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(sQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtConfig)

            If dtConfig.Rows.Count() > 0 Then
                fLocalX = dtConfig.Rows(0).Item("SI005_LOCALX")
                fLocalY = dtConfig.Rows(0).Item("SI005_LOCALY")
                fCorFundo = dtConfig.Rows(0).Item("SI005_CORFDO")
                Return dtConfig.Rows(0).Item("SI005_TAMOBJ")
            Else
                'Se a Função não estiver cadastrada, cadastrar
                If InserirConfigIcones(getCodUsuario(ClassCrypt.Decrypt(fLogin)), getCodOpcao(0, Trim(fNomeOpcao), "")) Then
                    fLocalX = 400
                    fLocalY = 13
                    fCorFundo = 0
                    Return 0 'Tamanho pequeno
                Else
                    fLocalX = 0
                    fLocalY = 0
                    fCorFundo = 0
                    Return 0 'Tamanho pequeno
                End If
            End If
        End Using

    End Function

    Private Function InserirConfigIcones(fCodUsu As Integer, fCodOpcao As Integer) As Boolean
        Dim cSql As String = ""
        Dim cmd As OleDbCommand
        Dim pReturn As Boolean

        'Gravar a Configuração do Icone no Banco para o Usuário
        cSql = "INSERT INTO ESI005 (SI005_CODUSU, SI005_CODOPC, SI005_TAMOBJ, SI005_LOCALX, SI005_LOCALY, SI005_CORFDO)"
        cSql += " values (" & fCodUsu & ", " & fCodOpcao & ",0,400,13,0)"

        cmd = New OleDbCommand(cSql, g_ConnectBanco)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
            pReturn = False
        Finally
            pReturn = True
        End Try

        Return pReturn

    End Function

    Public Function GravarConfigIcones(fCodUsu As Integer, fCodOpcao As Integer, fTamanho As Integer, _
                    fLocalX As Integer, fLocalY As Integer, fCorFundo As Integer) As Boolean
        Dim cSql As String = ""
        Dim cmd As OleDbCommand
        Dim pReturn As Boolean

        'Gravar a Configuração do Icone no Banco para o Usuário
        cSql = "UPDATE ESI005 SET SI005_TAMOBJ=" & fTamanho.ToString & ", SI005_LOCALX=" & _
            fLocalX.ToString & ", SI005_LOCALY=" & fLocalY.ToString & ", SI005_CORFDO=" & _
            fCorFundo.ToString & " where SI005_CODUSU=" & fCodUsu.ToString & _
            " and SI005_CODOPC=" & fCodOpcao.ToString

        cmd = New OleDbCommand(cSql, g_ConnectBanco)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
            pReturn = False
        Finally
            pReturn = True
        End Try

        Return pReturn

    End Function

    Public Function Gravar_LogUnidade(fCodUsuario As Integer, fCodUnidade As Integer, fCampoAlt As String, fValorOld_Alt As String, fValorNew_Alt As String, ByVal fMsgRetorno As String) As Boolean
        Dim sSqlLog As String
        Dim cmd As OleDbCommand
        Dim bResult As Boolean

        'Gravar o Log da Exclusão
        sSqlLog = "INSERT INTO EUN014 (UN014_CODUSU, UN014_CODUNI, UN014_SEQALT, UN014_DATALT, UN014_CPOALT, UN014_VALOLD, UN014_VALNEW)"
        sSqlLog += " values (" & fCodUsuario.ToString & "," & fCodUnidade.ToString & "," & _
            ProxCodChave("EUN014", "UN014_CODUSU,UN014_CODUNI,UN014_SEQALT", fCodUsuario.ToString & "," & fCodUnidade.ToString).ToString & "," & FormatarData(Today()) & "," & _
            "'" & fCampoAlt & "','" & fValorOld_Alt & "','" & fValorNew_Alt & "')"
        cmd = New OleDbCommand(sSqlLog, g_ConnectBanco)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            bResult = False
            fMsgRetorno = ex.ToString
        Finally
            bResult = True
            fMsgRetorno = ""
        End Try

        Return bResult

    End Function

    Public Function Gravar_Mural(fMensagem As String, fNivel As Integer, fCodUsuario As Integer, Optional fCodMural As Integer = 0) As Integer
        Dim sSqlMural As String
        Dim cmd As OleDbCommand
        Dim bResult As Boolean = True
        Dim nCodMural As Integer

        'O código do Mural será passado qdo for para incluir Visualizador
        'Gravar o Mural
        If fCodMural = 0 Then
            'Gravar o Mural
            nCodMural = ProxCodChave("ESI007", "SI007_CODMUR")
            sSqlMural = "INSERT INTO ESI007 (SI007_CODMUR, SI007_DATPUB, SI007_MENMUR, SI007_STAMUR) "
            sSqlMural += "VALUES ("
            sSqlMural += nCodMural.ToString()
            sSqlMural += "," & FormatarData(Today()) & ""
            sSqlMural += ",'" & Microsoft.VisualBasic.Left(fMensagem, 100) & "'"
            sSqlMural += ",'PUBLICADA')"
            cmd = New OleDbCommand(sSqlMural, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                bResult = False
                MsgBox("ModPrincipal.Gravar_Mural: " & ex.ToString & Chr(13) & sSqlMural)
            Finally
                bResult = True
            End Try
        End If

        If bResult Then
            fCodMural = nCodMural
            'Gravar os Acessos a este Mural
            sSqlMural = "INSERT INTO ESI008 (SI008_CODUSU, SI008_CODMUR, SI008_NIVACE)"
            sSqlMural += " values ("
            sSqlMural += fCodUsuario.ToString
            sSqlMural += " ," & fCodMural.ToString
            sSqlMural += " ," & fNivel.ToString
            sSqlMural += ")"
            cmd = New OleDbCommand(sSqlMural, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                bResult = False
                MsgBox("ModPrincipal.Gravar_Mural: " & ex.ToString & Chr(13) & sSqlMural)
            Finally
                bResult = True
            End Try
        End If

        Return nCodMural

    End Function

    Public Function Permissao_Unidade(fClaUnidade As String, fCodUsu As Integer) As Integer
        Dim cQuery As String = ""
        Dim cWhere As String = ""
        Dim dtNivelAcesso As DataTable = New DataTable("EUN013")
        Dim nNivelAcesso As Integer = 0
        '***********************************
        ' 0 - Nenhum Acesso
        ' 1 - Leitura
        ' 2 - Alteração
        ' 3 - Gerenciamento
        '***********************************

        'Ler os Níveis de Acesso
        cQuery = "Select EUN013.UN013_PERACE FROM (EUN013 INNER JOIN EUN000 ON EUN013.UN013_CODUNI=EUN000.UN000_CODRED) " & _
                "WHERE EUN013.UN013_CODUSU=" & fCodUsu.ToString

        'Ver acesso ao CM
        'If Microsoft.VisualBasic.Right(fClaUnidade, 2) <> "00" Then
        If fClaUnidade = "01.01.04.00" Then
            MsgBox("Parada")
        End If
        cWhere = " and EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 2) & ".00.00.00'"
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtNivelAcesso)
            If dtNivelAcesso.Rows.Count() > 0 Then
                nNivelAcesso = dtNivelAcesso.Rows(0).Item("UN013_PERACE")
            End If
        End Using
        dtNivelAcesso.Clear()
        'End If

        If nNivelAcesso = 3 Then
            Permissao_Unidade = nNivelAcesso
            Exit Function
        End If

        'Ver acesso ao CP
        If nNivelAcesso > 0 And Microsoft.VisualBasic.Mid(fClaUnidade, 4, 2) <> "00" Then
            cWhere = " and EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 5) & ".00.00'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

                'Preencher o Data Table
                da.Fill(dtNivelAcesso)
                If dtNivelAcesso.Rows.Count() > 0 Then
                    nNivelAcesso = dtNivelAcesso.Rows(0).Item("UN013_PERACE")
                End If
            End Using
            dtNivelAcesso.Clear()
        End If

        If nNivelAcesso = 3 Then
            Permissao_Unidade = nNivelAcesso
            Exit Function
        End If

        'Ver acesso ao CC
        If nNivelAcesso > 0 And Microsoft.VisualBasic.Mid(fClaUnidade, 7, 2) <> "00" Then
            cWhere = " and EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 5) & ".00.00'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

                'Preencher o Data Table
                da.Fill(dtNivelAcesso)
                If dtNivelAcesso.Rows.Count() > 0 Then
                    nNivelAcesso = dtNivelAcesso.Rows(0).Item("UN013_PERACE")
                End If
            End Using
            dtNivelAcesso.Clear()
        End If

        If nNivelAcesso = 3 Then
            Permissao_Unidade = nNivelAcesso
            Exit Function
        End If

        'Ver acesso ao CF
        If nNivelAcesso > 0 And Microsoft.VisualBasic.Mid(fClaUnidade, 10, 2) <> "00" Then
            cWhere = " and EUN000.UN000_CLAUNI = '" & fClaUnidade & "'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

                'Preencher o Data Table
                da.Fill(dtNivelAcesso)
                If dtNivelAcesso.Rows.Count() > 0 Then
                    nNivelAcesso = dtNivelAcesso.Rows(0).Item("UN013_PERACE")
                End If
            End Using
            dtNivelAcesso.Clear()
        End If

        Permissao_Unidade = nNivelAcesso

    End Function

    Public Sub GravarPermissao_Unidade(fClaUnidade As String)
        Dim cmd As OleDbCommand
        Dim cQuery As String = ""
        Dim cQuery_Permissao As String
        Dim cWhere As String = ""
        Dim dtNivelAcesso As DataTable = New DataTable("EUN013")
        Dim nNivelAcesso As Integer = 0
        Dim nCodigoUnidade As Double

        '***********************************
        ' 0 - Nenhum Acesso
        ' 1 - Leitura
        ' 2 - Alteração
        ' 3 - Gerenciamento
        '***********************************
        nCodigoUnidade = LerCod_Unidade(fClaUnidade)

        'Ler os Níveis de Acesso
        cQuery = "Select EUN013.UN013_CODUSU, EUN013.UN013_PERACE,  " & _
            "(Select tab2.UN013_PERACE from  EUN013 tab2 where tab2.UN013_CODUSU=EUN013.UN013_CODUSU and " & _
            "tab2.UN013_CODUNI=" & nCodigoUnidade.ToString & ") as PER_GRAVADA " & _
            "FROM (EUN013 INNER JOIN EUN000 ON EUN013.UN013_CODUNI=EUN000.UN000_CODRED) " & _
                "WHERE "

        'Ver pelo CM
        cWhere = " EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 2) & ".00.00.00'"
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtNivelAcesso)
            For x = 0 To dtNivelAcesso.Rows.Count() - 1
                If dtNivelAcesso.Rows(x).Item("UN013_PERACE") = 3 Then
                    'Gravar a Permissao na Unidade
                    cQuery_Permissao = "INSERT INTO EUN013 (UN013_CODUSU, UN013_CODUNI, UN013_PERACE) " & _
                        " values (" & dtNivelAcesso.Rows(x).Item("UN013_CODUSU").ToString & "," & nCodigoUnidade.ToString & ",2)"
                    cmd = New OleDbCommand(cQuery_Permissao, g_ConnectBanco)
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                    End Try
                End If
            Next
        End Using
        dtNivelAcesso.Clear()
        'End If

        'Ver pelo CC
        cWhere = " EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 5) & ".00.00'"
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtNivelAcesso)
            For x = 0 To dtNivelAcesso.Rows.Count() - 1
                If Not IsDBNull(dtNivelAcesso.Rows(x).Item("PER_GRAVADA")) Then
                    If dtNivelAcesso.Rows(x).Item("UN013_PERACE") > dtNivelAcesso.Rows(x).Item("PER_GRAVADA") Then
                        nNivelAcesso = dtNivelAcesso.Rows(x).Item("UN013_PERACE")
                    Else
                        nNivelAcesso = 9
                    End If
                Else
                    nNivelAcesso = 0
                End If
                If nNivelAcesso = 0 Then
                    cQuery_Permissao = "INSERT INTO EUN013 (UN013_CODUSU, UN013_CODUNI, UN013_PERACE) " & _
                        " values (" & dtNivelAcesso.Rows(x).Item("UN013_CODUSU").ToString & "," & nCodigoUnidade.ToString & ",2)"
                ElseIf nNivelAcesso < 9 Then
                    cQuery_Permissao = "UPDATE EUN013 SET UN013_PERACE = " & (dtNivelAcesso.Rows(x).Item("UN013_PERACE") - 1).ToString & _
                        " where UN013_CODUSU=" & dtNivelAcesso.Rows(x).Item("UN013_CODUSU").ToString & " and UN013_CODUNI = " & _
                    nCodigoUnidade.ToString
                Else
                    cQuery_Permissao = ""
                End If

                If cQuery_Permissao <> "" Then
                    cmd = New OleDbCommand(cQuery_Permissao, g_ConnectBanco)
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                    End Try
                End If
            Next
        End Using
        dtNivelAcesso.Clear()
        'End If

        'Ver pelo CP
        cWhere = " EUN000.UN000_CLAUNI = '" & Microsoft.VisualBasic.Left(fClaUnidade, 8) & ".00'"
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery & cWhere, g_ConnectBanco)

            'Preencher o Data Table
            da.Fill(dtNivelAcesso)
            For x = 0 To dtNivelAcesso.Rows.Count() - 1
                If Not IsDBNull(dtNivelAcesso.Rows(x).Item("PER_GRAVADA")) Then
                    If dtNivelAcesso.Rows(x).Item("UN013_PERACE") > dtNivelAcesso.Rows(x).Item("PER_GRAVADA") Then
                        nNivelAcesso = dtNivelAcesso.Rows(x).Item("UN013_PERACE")
                    Else
                        nNivelAcesso = 9
                    End If
                Else
                    nNivelAcesso = 0
                End If
                If nNivelAcesso = 0 Then
                    cQuery_Permissao = "INSERT INTO EUN013 (UN013_CODUSU, UN013_CODUNI, UN013_PERACE) " & _
                        " values (" & dtNivelAcesso.Rows(x).Item("UN013_CODUSU").ToString & "," & nCodigoUnidade.ToString & ",2)"
                ElseIf nNivelAcesso < 9 Then
                    cQuery_Permissao = "UPDATE EUN013 SET UN013_PERACE = " & (dtNivelAcesso.Rows(x).Item("UN013_PERACE") - 1).ToString & _
                        " where UN013_CODUSU=" & dtNivelAcesso.Rows(x).Item("UN013_CODUSU").ToString & " and UN013_CODUNI = " & _
                    nCodigoUnidade.ToString
                Else
                    cQuery_Permissao = ""
                End If

                If cQuery_Permissao <> "" Then
                    cmd = New OleDbCommand(cQuery_Permissao, g_ConnectBanco)
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                    End Try
                End If
            Next
        End Using
        dtNivelAcesso.Clear()

    End Sub

    Public Function ExisteRelacao_Grupo(fCodGrupo As Double) As Boolean
        Dim dtExisteGrupo As DataTable = New DataTable("ESI006")
        Dim fQuery As String

        fQuery = "SELECT * FROM ESI006 WHERE ESI006.SI006_CODGRU=" & fCodGrupo.ToString
        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(fQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtExisteGrupo)
            If dtExisteGrupo.Rows.Count() > 0 Then
                ExisteRelacao_Grupo = True
            Else
                ExisteRelacao_Grupo = False
            End If
        End Using
        dtExisteGrupo.Clear()

    End Function

    Public Function FormatarData(fData As Date) As String
        Dim sConexao As String

        sConexao = ClassCrypt.Decrypt(g_ConnectString)

        If InStr(sConexao, "accdb", CompareMethod.Text) > 0 Then
            FormatarData = "#" & Format(fData, "dd/MM/yyyy") & "#"
        Else
            FormatarData = "'" & Format(fData, "yyyy/MM/dd") & "'"
        End If

    End Function

    Public Function ChaveConexao(fChave As String) As String
        'Provider=SQLOLEDB;
        'Data Source=192.168.2.1;
        'Initial Catalog=SSVP_CNB;
        'User ID=SSVP_User;
        'Password=ssvp@00;
        'Persist Security Info=true;
        'Connect Timeout=300;

        Dim nContIni As Integer
        Dim nContFin As Integer
        Dim sConnString As String

        sConnString = ClassCrypt.Decrypt(g_ConnectString)

        nContIni = InStr(1, sConnString, fChave)
        If nContIni > 0 Then
            nContIni += Len(fChave) + 1
            nContFin = InStr(nContIni, sConnString, ";")
            ChaveConexao = Microsoft.VisualBasic.Mid(sConnString, nContIni, nContFin - nContIni)
        Else
            ChaveConexao = ""
        End If

    End Function

    Public Function LerNomeCampo(sNome_Entidade As String, sDesc_Campo As String) As String
        Dim cQuery As String
        Dim dtLerNomeCampo As DataTable = New DataTable("ESI902")

        cQuery = "SELECT SI902_NOMCPO FROM ESI902 where SI902_DESCPO = '" & sDesc_Campo & "' and SI902_NOMENT='" & sNome_Entidade & "'"

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerNomeCampo)
            If dtLerNomeCampo.Rows.Count > 0 Then
                Return dtLerNomeCampo.Rows(0).Item("SI902_NOMCPO")
            Else
                Return 0
            End If
        End Using

        dtLerNomeCampo.Clear()

    End Function

    Public Function FormatarValor_SQL(fValor_String As String) As String
        fValor_String = Replace(fValor_String, ".", "")
        fValor_String = Replace(fValor_String, ",", ".")
        FormatarValor_SQL = fValor_String
    End Function

    Public Function LerNome_Colaborador(fCodigo_Col As Double) As String
        Dim cQuery As String
        Dim dtLerCol As DataTable = New DataTable("EUN003")

        cQuery = "SELECT UN003_NOMCOL FROM EUN003 where UN003_CODCOL = " & fCodigo_Col.ToString

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerCol)
            If dtLerCol.Rows.Count > 0 Then
                Return dtLerCol.Rows(0).Item("UN003_NOMCOL")
            Else
                Return 0
            End If
        End Using

        dtLerCol.Clear()

    End Function

    Public Function LerCargo_Colaborador(fCodigo_Col As Double, Optional ByRef sCodUnidade As Double = 0) As String
        Dim cQuery As String
        Dim dtLerCargo As DataTable = New DataTable("EUN011")

        cQuery = "SELECT EUN003.UN003_CODCOL, EUN003.UN003_NOMCOL, EUN011.UN011_DESOCP, EUN016.UN016_DESMDT, " & _
            "EUN016.UN016_CODRED FROM ((EUN003 INNER JOIN EUN012 ON EUN003.UN003_CODCOL = EUN012.UN012_CODCOL) " & _
            "INNER JOIN EUN011 ON EUN012.UN012_CODOCP = EUN011.UN011_CODOCP) INNER JOIN EUN016 ON " & _
            "(EUN012.UN012_CODRED = EUN016.UN016_CODRED) AND (EUN012.UN012_CODMDT = EUN016.UN016_CODMDT) " & _
            "where EUN016.UN016_DATINI <= DATE() AND EUN016.UN016_DATFIN >= DATE() AND " & _
            "UN003_CODCOL = " & fCodigo_Col.ToString

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerCargo)
            If dtLerCargo.Rows.Count > 0 Then
                sCodUnidade = dtLerCargo.Rows(0).Item("UN016_CODRED")
                Return dtLerCargo.Rows(0).Item("UN011_DESOCP")
            Else
                Return ""
            End If
        End Using

        dtLerCargo.Clear()

    End Function

    Public Function LerCodColaborador(ByVal fLoginUsuario As String) As Integer
        Dim cQuery As String
        Dim dtUsuario As DataTable = New DataTable("ESI000")

        cQuery = "SELECT SI000_CODASS FROM ESI000 where SI000_LGIUSU = '" & Trim(fLoginUsuario) & "'"

        Using da As New OleDbDataAdapter()
            da.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            da.Fill(dtUsuario)

            If dtUsuario.Rows.Count() > 0 Then
                Return dtUsuario.Rows(0).Item("SI000_CODASS")
            Else
                Return 0
            End If
        End Using

    End Function

    Public Function InserirMandato(fCodUnidade As Double) As Integer
        'Inserir um novo mandato a Unidade, com os encargos pré-cadastrados
        Dim cmdMandato As OleDbCommand
        Dim dtEncargos As DataTable = New DataTable("EUN011")
        Dim sSQL As String
        Dim x As Integer
        Dim nProxCodigo As Integer = ProxCodChave("EUN016", "UN016_CODRED,UN016_CODMDT", fCodUnidade)

        sSQL = "INSERT INTO EUN016 (UN016_CODMDT, UN016_CODRED, UN016_DESMDT, UN016_DATINI, UN016_DATFIN) VALUES " & _
        "(" & nProxCodigo.ToString & _
        "," & fCodUnidade & ",'MANDATO " & Year(Today).ToString & "'," & _
        FormatarData(Today) & "," & FormatarData(Today) & ")"
        cmdMandato = New OleDbCommand(sSQL, g_ConnectBanco)

        Try
            cmdMandato.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString())
            InserirMandato = 0
        Finally
            InserirMandato = nProxCodigo
        End Try

        If InserirMandato > 0 Then 'Inserir os Encargos
            'ler os Encargos para a Unidade e Inserir para o mandato
            sSQL = "Select * from EUN011 where UN011_NIVOCP='" & getTipoUnidade(LerClasse_Unidade(fCodUnidade)) & "'"
            Using da As New OleDbDataAdapter()
                da.SelectCommand = New OleDbCommand(sSQL, g_ConnectBanco)

                da.Fill(dtEncargos)

                For x = 0 To dtEncargos.Rows.Count() - 1
                    sSQL = "INSERT INTO EUN012 (UN012_CODMDT, UN012_CODRED, UN012_CODOCP, UN012_CODCOL) VALUES " & _
                    "(" & nProxCodigo.ToString & _
                    "," & fCodUnidade & "," & _
                    dtEncargos.Rows(x).Item("UN011_CODOCP").ToString & ",0)"
                    cmdMandato = New OleDbCommand(sSQL, g_ConnectBanco)

                    Try
                        cmdMandato.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                    End Try
                Next
            End Using

        End If

    End Function

    Public Function getParametro(fListaParametro As String, fSeparador As String, fItem As Integer) As String
        'Trazer um item de dentro de um parâmetro em forma de Lista/Array com separação
        If Trim(fListaParametro) <> "" And Trim(fSeparador) <> "" And fItem > 0 Then
            Dim sParametros() As String = fListaParametro.Split(fSeparador)

            getParametro = Trim(sParametros(fItem - 1))
        Else
            getParametro = ""
        End If
    End Function

    Public Function getTipoUnidade(fClaUnidade As String) As String
        'Trazer a sigla do Tipo da Unidade dentro do Conselho

        If Microsoft.VisualBasic.Mid(fClaUnidade, 1, 2) = "00" Then
            getTipoUnidade = "CNB" 'Conselho Nacional do Brasil
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 4, 2) = "00" Then
            getTipoUnidade = "CM" 'Conselho Metropolitano
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 7, 2) = "00" Then
            getTipoUnidade = "CC" 'Conselho Central
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 10, 2) = "00" Then
            getTipoUnidade = "CP" 'Conselho Particular
        Else
            getTipoUnidade = "CF" 'Conferência
        End If

    End Function

    Public Function getNivelUnidade(fClaUnidade As String) As Integer
        'Trazer o nível da Unidade na estrutura do Conselho

        If Microsoft.VisualBasic.Mid(fClaUnidade, 1, 2) = "00" Then
            getNivelUnidade = 9 'CNB
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 4, 2) = "00" Then
            getNivelUnidade = 0 'CM
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 7, 2) = "00" Then
            getNivelUnidade = 1 'CC
        ElseIf Microsoft.VisualBasic.Mid(fClaUnidade, 10, 2) = "00" Then
            getNivelUnidade = 2 'CP
        Else
            getNivelUnidade = 3 'CF
        End If

    End Function

    Public Function GetPermissaoExcluirUnidade(fCodUnidade As Double, Optional ByRef fMensagem As String = "") As Boolean
        Dim cQuery As String = ""
        Dim sClasseUnidade As String = ""
        Dim sNomeUnidade As String = ""
        Dim nNivelUni As Integer
        Dim bPermissao As Boolean = True
        Dim nQtdReg As Integer
    
        sClasseUnidade = LerClasse_Unidade(fCodUnidade, sNomeUnidade)

        'Verificar se é Conselho ou Conferencia
        nNivelUni = getNivelUnidade(sClasseUnidade)

        If nNivelUni = 3 Then
            'Verificar se Existe Colaboradores nesta Conferência
            nQtdReg = LerNumeroColab_Unidade(fCodUnidade)
            If nQtdReg > 0 Then
                bPermissao = False
                fMensagem = "Há " & nQtdReg.ToString
                If nQtdReg = 1 Then
                    fMensagem += " colaborador associado"
                Else
                    fMensagem += " colaboradores associados"
                End If
                fMensagem += " a esta conferência. Exclusão não permitida!!"
            End If
        Else
            nQtdReg = LerNumeroAgreg_Unidade(sClasseUnidade)
            If nQtdReg > 0 Then
                bPermissao = False
                fMensagem = "Há " & nQtdReg.ToString
                If nQtdReg = 1 Then
                    fMensagem += " unidade vinculada"
                Else
                    fMensagem += " unidades vinculadas"
                End If
                fMensagem += " a este conselho. Exclusão não permitida!!"
            End If
        End If

        'Forçar a Saída da Funcao 
        If bPermissao Then GoTo FimFuncao

        'Mais Criticas ...


FimFuncao:
        GetPermissaoExcluirUnidade = bPermissao

    End Function

    Public Function LerNumeroColab_Unidade(fCodUnidade As Double) As Integer
        Dim cQuery As String
        Dim dtLerCol As DataTable = New DataTable("EUN003")

        cQuery = "SELECT count(*) as Total FROM EUN003 where UN003_CODUNI = " & fCodUnidade.ToString

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerCol)
            If dtLerCol.Rows.Count > 0 Then
                Return dtLerCol.Rows(0).Item("Total")
            Else
                Return 0
            End If
        End Using
        dtLerCol.Clear()

    End Function

    Public Function LerNumeroAgreg_Unidade(fClasseUnidade As String) As Integer
        Dim cQuery As String
        Dim nLeft As Integer
        Dim dtLerUni As DataTable = New DataTable("EUN000")

        'Retirar os .00 da Classe
        fClasseUnidade = Replace(fClasseUnidade, ".00", "")
        nLeft = Microsoft.VisualBasic.Len(fClasseUnidade)

        cQuery = "SELECT count(*) as Total FROM EUN000 where left(UN000_CLAUNI," & nLeft.ToString & ") = '" & fClasseUnidade & "'"

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(cQuery, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtLerUni)
            If dtLerUni.Rows.Count > 0 Then
                Return dtLerUni.Rows(0).Item("Total") - 1
            Else
                Return 0
            End If
        End Using
        dtLerUni.Clear()

    End Function

    Public Sub ReorganizarEstrutura(fUnidade As String)
        Dim dtReorgUni As DataTable = New DataTable("EUN000")
        Dim dtGravarLog As DataTable = New DataTable("EUN000")
        Dim nSeqTemp As Integer = 51
        Dim nNivel As Integer
        Dim sSQL As String
        Dim sSQL_Where As String
        Dim bReorganizar As Boolean = True
        Dim cmd As OleDbCommand
        Dim sMensagem As String
        Dim nCodUsuario As Integer = getCodUsuario(ClassCrypt.Decrypt(g_Login))
        Dim bGravarCM, bGravarCC, bGravarCP, bGravarCF As Boolean

        bGravarCM = False : bGravarCC = False : bGravarCP = False : bGravarCF = False

        'Saber o Nivel da Unidade
        nNivel = getNivelUnidade(fUnidade)

        If nNivel = 3 Then
            'Conferência
            sSQL_Where = "LEFT(UN000_CLAUNI,8) = '" & Microsoft.VisualBasic.Left(fUnidade, 8) & "'"
            sSQL_Where += " AND RIGHT(UN000_CLAUNI,2) <> '00'"
        ElseIf nNivel = 2 Then
            'CP
            sSQL_Where = "LEFT(UN000_CLAUNI,5) = '" & Microsoft.VisualBasic.Left(fUnidade, 5) & "'"
            sSQL_Where += " AND RIGHT(UN000_CLAUNI,2) = '00'"
        ElseIf nNivel = 1 Then
            'CC
            sSQL_Where = "LEFT(UN000_CLAUNI,2) = '" & Microsoft.VisualBasic.Left(fUnidade, 2) & "'"
            sSQL_Where += " AND RIGHT(UN000_CLAUNI,5) = '00.00'"
        Else
            'CM
            sSQL_Where = "RIGHT(UN000_CLAUNI,8) = '00.00.00'"
        End If

        sSQL = "SELECT count(*) as Total FROM EUN000 where " & sSQL_Where
        sSQL += " and un000_stauni<>'I' and un000_nivuni=" & nNivel.ToString
        sSQL += " and un000_datfun=" & FormatarData(CDate("01/01/1900")) & ""

        Using daTabela As New OleDbDataAdapter()
            daTabela.SelectCommand = New OleDbCommand(sSQL, g_ConnectBanco)

            ' Preencher o DataTable 
            daTabela.Fill(dtReorgUni)
            If dtReorgUni.Rows(0).Item("Total") > 0 Then
                bReorganizar = False
            End If
        End Using
        dtReorgUni.Clear()

        If bReorganizar Then

            'Executar a Reorganização
            sSQL = "SELECT un000_codred, un000_clauni, un000_nomuni FROM EUN000 where " & sSQL_Where
            sSQL += " and un000_stauni<>'I' and un000_nivuni=" & nNivel.ToString
            sSQL += " order by un000_datfun"

            Using daTabela As New OleDbDataAdapter()
                daTabela.SelectCommand = New OleDbCommand(sSQL, g_ConnectBanco)

                ' Preencher o DataTable 
                daTabela.Fill(dtReorgUni)
                If dtReorgUni.Rows.Count > 0 Then
                    For x = 0 To dtReorgUni.Rows.Count - 1
                        If nNivel = 3 Then 'CF
                            If Not Int(Microsoft.VisualBasic.Right(dtReorgUni.Rows(x).Item("un000_clauni"), 2)) = nSeqTemp Then
                                'Trocar a Sequência
                                sSQL = "UPDATE EUN000 SET UN000_CLAUNI = Left(un000_clauni,9) & '" & Microsoft.VisualBasic.Format(nSeqTemp, "00") & _
                                    "' where UN000_CLAUNI = '" & dtReorgUni.Rows(x).Item("un000_clauni") & "'" & _
                                    " and un000_stauni<>'I'"
                                cmd = New OleDbCommand(sSQL, g_ConnectBanco)
                                Try
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox(ex.ToString())
                                Finally
                                    bGravarCF = True
                                    'Gravar o Log da Mudança
                                    sMensagem = ""
                                    If Not Gravar_LogUnidade(nCodUsuario.ToString, dtReorgUni.Rows(x).Item("UN000_CODRED"), "UN000_CLAUNI", dtReorgUni.Rows(x).Item("UN000_clauni"), Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 9) & Microsoft.VisualBasic.Format(nSeqTemp, "00"), sMensagem) Then
                                        MsgBox(sMensagem)
                                    End If
                                    '************************
                                End Try
                            End If
                        ElseIf nNivel = 2 Then 'CP
                            If Not Int(Microsoft.VisualBasic.Mid(dtReorgUni.Rows(x).Item("un000_clauni"), 7, 2)) = nSeqTemp Then
                                bGravarCP = True
                                'Trocar a Sequência
                                sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(un000_clauni,6) & '" & Microsoft.VisualBasic.Format(nSeqTemp, "00") & "' & Right(un000_clauni, 3) " & _
                                    " where left(UN000_CLAUNI,8) = '" & Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 8) & "'" & _
                                    " and un000_stauni<>'I'"
                                cmd = New OleDbCommand(sSQL, g_ConnectBanco)
                                Try
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox(ex.ToString())
                                    bGravarCP = False
                                Finally
                                    bGravarCP = True
                                    'Gravar o Log do Principal
                                    sMensagem = ""
                                    If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                                        dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                                        "UN000_CLAUNI", _
                                        dtReorgUni.Rows(x).Item("un000_clauni"), _
                                        Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 6) & _
                                            Microsoft.VisualBasic.Format(nSeqTemp, "00") & _
                                            Microsoft.VisualBasic.Right(dtReorgUni.Rows(x).Item("un000_clauni"), 3), sMensagem) Then
                                        MsgBox(sMensagem)
                                        '************************
                                    End If
                                    'Gravar o Log da Mudança dos Subsidiários
                                    'sSQL = "select * from where left(UN000_CLAUNI,8) = '" & Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 8) & "'"
                                    'Using da As New OleDbDataAdapter()
                                    'da.SelectCommand = New OleDbCommand(sSQL, g_ConnectBanco)
                                    'da.Fill(dtGravarLog)
                                    'For y = 0 To dtGravarLog.Rows.Count() - 1

                                    'sMensagem = ""
                                    'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                                    '    dtGravarLog.Rows(y).Item("UN000_CODRED"), _
                                    '    "UN000_CLAUNI", _
                                    '    dtGravarLog.Rows(y).Item("clauni"), _
                                    '    Microsoft.VisualBasic.Left(dtGravarLog.Rows(y).Item("clauni"), 6) & _
                                    '        Microsoft.VisualBasic.Format(nSeqTemp, "00") & _
                                    '        Microsoft.VisualBasic.Right(dtGravarLog.Rows(y).Item("clauni"), 3), sMensagem) Then
                                    'MsgBox(sMensagem)
                                    '************************
                                    'End If
                                    'Next
                                    'dtGravarLog.Clear()
                                    'End Using
                                    '************************
                                End Try
                            End If
                        ElseIf nNivel = 1 Then 'CC
                            If Not Int(Microsoft.VisualBasic.Mid(dtReorgUni.Rows(x).Item("un000_clauni"), 4, 2)) = nSeqTemp Then
                                bGravarCC = True
                                'Trocar a Sequência
                                sSQL = "UPDATE EUN000 SET UN000_CLAUNI = Left(un000_clauni, 3) & '" & _
                                    Microsoft.VisualBasic.Format(nSeqTemp, "00") & _
                                    "' & Right(un000_clauni, 6) " & _
                                    " where left(UN000_CLAUNI,5) = '" & Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 5) & "'" & _
                                    " and un000_stauni<>'I'"
                                cmd = New OleDbCommand(sSQL, g_ConnectBanco)
                                Try
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox(ex.ToString())
                                    bGravarCC = False
                                Finally
                                    'Gravar o Log da Mudança
                                    sMensagem = ""
                                    If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                                            dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                                            "UN000_CLAUNI", _
                                            dtReorgUni.Rows(x).Item("un000_clauni"), _
                                            Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                                        MsgBox(sMensagem)
                                    End If
                                    '************************
                                End Try
                            End If
                        ElseIf nNivel = 0 Then 'CM
                            'MsgBox(Microsoft.VisualBasic.Mid(dtReorgUni.Rows(x).Item("un000_clauni"), 1, 2))
                            If Not Int(Microsoft.VisualBasic.Mid(dtReorgUni.Rows(x).Item("un000_clauni"), 1, 2)) = nSeqTemp - 50 Then
                                'Trocar a Sequência
                                sSQL = "UPDATE EUN000 SET UN000_CLAUNI = '" & _
                                        Microsoft.VisualBasic.Format(nSeqTemp, "00") & _
                                        "' & Right(un000_clauni, 9)" & _
                                    " where left(UN000_CLAUNI,2) = '" & Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("un000_clauni"), 2) & "'" & _
                                    " and un000_stauni<>'I'"
                                cmd = New OleDbCommand(sSQL, g_ConnectBanco)
                                Try
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox(ex.ToString())
                                Finally
                                    bGravarCM = True
                                    'Gravar o Log da Mudança
                                    sMensagem = ""
                                    If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                                            dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                                            "UN000_CLAUNI", _
                                            dtReorgUni.Rows(x).Item("un000_clauni"), _
                                            Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00.00", sMensagem) Then
                                        MsgBox(sMensagem)
                                    End If
                                    '************************
                                End Try
                            End If
                        End If
                        nSeqTemp += 1
                    Next
                End If
            End Using
            dtReorgUni.Clear()
        End If

        'Atualizar as Estruturas na Base (51-> 01)
        'CM
        If bGravarCM Then
            sSQL = "UPDATE EUN000 SET UN000_CLAUNI ='0' & Left(UN000_CLAUNI,2)-50 &" & _
                        " right(UN000_CLAUNI,9)" & _
                        " where left(UN000_CLAUNI,2) > 50 and left(UN000_CLAUNI,2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,2)-50 &" & _
                        " right(UN000_CLAUNI,9)" & _
                        " where left(UN000_CLAUNI,2) > 59 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

        End If

        'Atualizar as Estruturas na Base (51-> 01)
        'CC
        If bGravarCC Then
            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,3) & '0' & right(Left(UN000_CLAUNI,5),2)-50 &" & _
                        " right(UN000_CLAUNI,6)" & _
                        " where right(left(UN000_CLAUNI,5),2) > 50 and right(left(UN000_CLAUNI,5),2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,3) & right(Left(UN000_CLAUNI,5),2)-50 &" & _
                        " right(UN000_CLAUNI,6)" & _
                        " where right(left(UN000_CLAUNI,5),2) > 50 and right(left(UN000_CLAUNI,5),2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try
        End If

        'Atualizar as Estruturas na Base (51-> 01)
        'CP
        If bGravarCP Then
            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,6) & '0' & right(Left(UN000_CLAUNI,8),2)-50 &" & _
                        " right(UN000_CLAUNI,3)" & _
                        " where right(left(UN000_CLAUNI,8),2) > 50 and right(left(UN000_CLAUNI,8),2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,6) & right(Left(UN000_CLAUNI,8),2)-50 &" & _
                        " right(UN000_CLAUNI,3)" & _
                        " where right(left(UN000_CLAUNI,8),2) > 50 and right(left(UN000_CLAUNI,8),2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

        End If

        'Atualizar as Estruturas na Base (51-> 01)
        'CF
        If bGravarCF Then
            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,9) & '0' & right(UN000_CLAUNI,2)-50 " & _
                        " where right(UN000_CLAUNI,2) > 50 and right(UN000_CLAUNI,2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try

            sSQL = "UPDATE EUN000 SET UN000_CLAUNI =Left(UN000_CLAUNI,9) & right(UN000_CLAUNI,2)-50 " & _
                        " where right(UN000_CLAUNI,2) > 50 and right(UN000_CLAUNI,2) < 60 and un000_stauni<>'I'"
            cmd = New OleDbCommand(sSQL, g_ConnectBanco)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString())
            Finally
                'Gravar o Log da Mudança
                'sMensagem = ""
                'If Not Gravar_LogUnidade(nCodUsuario.ToString, _
                '       dtReorgUni.Rows(x).Item("UN000_CODRED"), _
                '       "UN000_CLAUNI", _
                '       dtReorgUni.Rows(x).Item("clauni"), _
                '       Microsoft.VisualBasic.Left(dtReorgUni.Rows(x).Item("clauni"), 3) & Microsoft.VisualBasic.Format(nSeqTemp, "00") & ".00.00", sMensagem) Then
                'MsgBox(sMensagem)
                'End If
                '************************
            End Try
        End If

    End Sub


End Module
