Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReportViewer
    Public Sub ShowReport(ByVal DataTbl As DataSet, _
                    ByVal RptName As String, _
                     ByVal RptPath As String)

        Dim ObjRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        ObjRpt.Load(RptPath & RptName)

        'MsgBox(RptPath & RptName)
        Application.DoEvents()

        ObjRpt.SetDataSource(DataTbl.Tables(0))

        Me.CrystalReportViewer1.ReportSource = ObjRpt

        'Dim myDBConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo()

        'myDBConnectionInfo = ObjRpt.DataSourceConnections.Item(ChaveConexao("Data Source"), ChaveConexao("Initial Catalog"))

        'With myDBConnectionInfo
        '.ServerName = ChaveConexao("Data Source")
        '.DatabaseName = ChaveConexao("Initial Catalog")
        '.UserID = ChaveConexao("User ID")
        '.Password = ChaveConexao("Password")
        'End With

        Me.CrystalReportViewer1.Refresh()
        Me.Show()

    End Sub

   
End Class