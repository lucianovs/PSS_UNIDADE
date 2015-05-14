Imports System.Windows.Forms

Public Class mdiPrincipal

    Private Sub mdiPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Argumentos() As String = Environment.GetCommandLineArgs
        Dim sNomeUsuario As String

        g_Modulo = "UNIDADES"

        'Ativar os Parâmetros iniciais de Segurança
        'Resgatar as Informações da Chamada
        If Environment.GetCommandLineArgs.Length > 1 Then
            g_Login = Environment.CommandLine(1)
        Else
            'Ativar estas Linhas quando for colocar em produção
            'MsgBox("Este programa não tem permissão para ser executado. Contactar o administrador da rede!!", MsgBoxStyle.Critical)
            'Application.Exit()

            'Parâmetros Padrão - utilizar somente quando estiver em desenvolvimento
            'g_Login = (ClassCrypt.Encrypt("ssvp$00"))
            'MsgBox(g_Login)
            g_Login = ClassCrypt.Encrypt("Admin")
        End If
        'Conection String
        g_ConnectString = (LerDadosINI(nomeArquivoINI(), "CONEXAO", "ConnectString", _
            ClassCrypt.Encrypt("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=SSVP.accdb;Persist Security Info=False;")))

        'Conectar com o Banco de dados
        If Not ConectarBanco() Then
            Application.Exit()
        End If

        'Ler o Usuário e Validar o Acesso
        sNomeUsuario = LerUsuario(ClassCrypt.Decrypt(g_Login), Nothing)

        If sNomeUsuario <> "" Then
            Me.Text = "Modulo " & g_Modulo & " - Usuário: " & sNomeUsuario
        Else
            Application.Exit()
        End If

        'Verificar o acesso às opções do sistema
        Dim cModulo As Integer = getCodModulo(g_Modulo) 'Pegar o código do Módulo
        Dim nCodUsuario As Integer = getCodUsuario(ClassCrypt.Decrypt(g_Login)) 'pegar o código do usuario

        For Each _control As Object In Me.Controls
            If TypeOf (_control) Is MenuStrip Then
                For Each itm As ToolStripMenuItem In _control.items
                    If itm.Text <> "&Sair" And itm.Name.ToString.StartsWith("menu") Then
                        itm.Tag = NivelAcesso(nCodUsuario, cModulo, itm.Name, "")
                        itm.Enabled = itm.Tag > 0
                        'Função para Verificar os SubItens do menu
                        If itm.DropDownItems.Count > 0 Then LoopMenuItems(itm, nCodUsuario, cModulo, itm.Name)
                    End If
                Next
            End If
        Next


    End Sub

    Private Function LoopMenuItems(ByVal parent As ToolStripMenuItem, nCodUsuario As Integer, cModulo As Integer, fPrincOpcao As String) As Object
        Dim retval As Object = Nothing

        For Each child As Object In parent.DropDownItems

            'MessageBox.Show("Child : " & child.name)

            If TypeOf (child) Is ToolStripMenuItem Then
                If child.Text <> "Sair" And child.Name.ToString.StartsWith("menu") Then
                    child.Tag = NivelAcesso(nCodUsuario, cModulo, child.Name, fPrincOpcao)
                    child.Enabled = child.Tag > 0
                    If child.DropDownItems.Count > 0 Then
                        retval = LoopMenuItems(child, nCodUsuario, cModulo, child.name)
                        If Not retval Is Nothing Then Exit For
                    End If
                End If
            End If
        Next

        Return retval
    End Function

    Private Sub menuConfiguracoes_Click(sender As Object, e As EventArgs) Handles menuSisConfiguracoes.Click
        Dim ChildForm As New Parametros

        ChildForm.MdiParent = Me
        ChildForm.Tag = menuSisConfiguracoes.Tag 'é gravado no tag do menu o nível de acesso
        ChildForm.Show()

    End Sub

    Private Sub menuUsuarios_Click(sender As Object, e As EventArgs) Handles menuSisUsuarios.Click
        '?? Alterar os parâmetros para passar ao Browse (Entudade e Form. do Cadastro) ??
        Dim frmBrowse_Usuario As frmBrowse = New frmBrowse("ESI000", "frmUsuario")

        frmBrowse_Usuario.MdiParent = Me
        frmBrowse_Usuario.Tag = menuSisUsuarios.Tag 'é gravado no tag do menu o nível de acesso
        frmBrowse_Usuario.Text = menuSisUsuarios.Text
        frmBrowse_Usuario.Show()

    End Sub

    Private Sub menuRelatorios_Click(sender As Object, e As EventArgs) Handles menuRelatorios.Click

    End Sub

    Private Sub menuImpUsuario_Click(sender As Object, e As EventArgs) Handles menuRelUsuario.Click

    End Sub

    Private Sub menuCadTipoDeOcupacao_Click(sender As Object, e As EventArgs) Handles menuCadTipoDeOcupacao.Click
        Dim frmBrowse_TipoDeOcupacao As frmBrowse = New frmBrowse("EUN011", "frmTipoDeOcupacao")

        frmBrowse_TipoDeOcupacao.MdiParent = Me
        frmBrowse_TipoDeOcupacao.Tag = menuCadTipoDeOcupacao.Tag 'é gravado no tag do menu o nível de acesso
        frmBrowse_TipoDeOcupacao.Text = menuCadTipoDeOcupacao.Text
        frmBrowse_TipoDeOcupacao.Show()
    End Sub

    Private Sub menuCadUnidades_Click(sender As Object, e As EventArgs) Handles menuCadUnidades.Click
        Dim frmBrowse_Unidades As frmBrowse = New frmBrowse("EUN000", "frmUnidades")

        frmBrowse_Unidades.MdiParent = Me
        frmBrowse_Unidades.Tag = menuCadUnidades.Tag 'é gravado no tag do menu o nível de acesso
        frmBrowse_Unidades.Text = menuCadUnidades.Text
        frmBrowse_Unidades.Show()
    End Sub
End Class
