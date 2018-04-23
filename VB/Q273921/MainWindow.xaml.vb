Imports Microsoft.VisualBasic
Imports System
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
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			Dim connection As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../nwind.mdb")
			Dim command As New OleDbCommand("select [Sales Person], CategoryName, [Extended Price] from SalesPerson", connection)
			Dim adapter As New OleDbDataAdapter(command)
			Dim dataSet As New DataSet()
			adapter.Fill(dataSet, "SalesPerson")
			salesPersonPivot.DataSource = dataSet.Tables("SalesPerson")
		End Sub

		Private Sub salesPersonPivot_CellClick(ByVal sender As Object, ByVal e As PivotCellEventArgs)
			Dim pivot As PivotGridControl = CType(sender, PivotGridControl)
			Dim selection As New List(Of sd.Point)()
			For i As Integer = 0 To pivot.ColumnCount - 1
				If i <> e.ColumnIndex Then
					selection.Add(New sd.Point(i, e.RowIndex))
				End If
			Next i
			pivot.MultiSelection.SetSelection(selection.ToArray())
		End Sub
	End Class
End Namespace
