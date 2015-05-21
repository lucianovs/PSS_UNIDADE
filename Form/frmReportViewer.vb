Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReportViewer
    Public Sub ShowReport(ByVal RptName As String, _
                     ByVal RptPath As String,
                     ByVal rptFormula As String)

        Dim ObjRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        ObjRpt.Load(RptPath & RptName)

        Application.DoEvents()

        'ObjRpt.SetDataSource(DataTbl.Tables(0))
        Me.CrystalReportViewer1.ReportSource = ObjRpt
        Me.CrystalReportViewer1.SelectionFormula = rptFormula

        'Dim myDBConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo()

        'myDBConnectionInfo = ObjRpt.DataSourceConnections.Item(ChaveConexao("Data Source"), ChaveConexao("Initial Catalog"))

        'With myDBConnectionInfo
        '.ServerName = ChaveConexao("Data Source")
        '.DatabaseName = ChaveConexao("Initial Catalog")
        '.UserID = ChaveConexao("User ID")
        '.Password = ChaveConexao("Password")
        'End With

        With crConnectionInfo
            .ServerName = ChaveConexao("Data Source")
            .DatabaseName = ChaveConexao("Initial Catalog")
            .UserID = ChaveConexao("User ID")
            .Password = ChaveConexao("Password")
        End With

        MsgBox("DS:" & ChaveConexao("Data Source") & _
               " IC:" & ChaveConexao("Initial Catalog") & _
               " UN:" & ChaveConexao("User ID"))

        CrTables = ObjRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        Me.CrystalReportViewer1.Refresh()
        Me.Show()

    End Sub

   
End Class