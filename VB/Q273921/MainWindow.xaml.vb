Imports System.Data
Imports System.Windows
Imports System.Data.OleDb
Imports DevExpress.Xpf.PivotGrid
Imports System.Collections.Generic
Imports sd = System.Drawing

Namespace Q273921

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim connection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../nwind.mdb")
            Dim command As OleDbCommand = New OleDbCommand("select [Sales Person], CategoryName, [Extended Price] from SalesPerson", connection)
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim dataSet As DataSet = New DataSet()
            adapter.Fill(dataSet, "SalesPerson")
            Me.salesPersonPivot.DataSource = dataSet.Tables("SalesPerson")
        End Sub

        Private Sub salesPersonPivot_CellClick(ByVal sender As Object, ByVal e As PivotCellEventArgs)
            Dim pivot As PivotGridControl = CType(sender, PivotGridControl)
            Dim selection As List(Of sd.Point) = New List(Of sd.Point)()
            For i As Integer = 0 To pivot.ColumnCount - 1
                If i <> e.ColumnIndex Then selection.Add(New sd.Point(i, e.RowIndex))
            Next

            pivot.MultiSelection.SetSelection(selection.ToArray())
        End Sub
    End Class
End Namespace
