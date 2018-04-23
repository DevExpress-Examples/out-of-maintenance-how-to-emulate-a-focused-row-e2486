using System;
using System.Data;
using System.Windows;
using System.Data.OleDb;
using DevExpress.Xpf.PivotGrid;
using System.Collections.Generic;
using sd = System.Drawing;

namespace Q273921 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window {
        public MainWindow() {
            InitializeComponent();
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../nwind.mdb");
            OleDbCommand command = new OleDbCommand("select [Sales Person], CategoryName, [Extended Price] from SalesPerson", connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "SalesPerson");
            salesPersonPivot.DataSource = dataSet.Tables["SalesPerson"];
        }

        private void salesPersonPivot_CellClick(object sender, PivotCellEventArgs e) {
            PivotGridControl pivot = (PivotGridControl)sender;
            List<sd.Point> selection = new List<sd.Point>();
            for (int i = 0; i < pivot.ColumnCount; i++)
                if (i != e.ColumnIndex) selection.Add(new sd.Point(i, e.RowIndex));
            pivot.MultiSelection.SetSelection(selection.ToArray());
        }
    }
}
