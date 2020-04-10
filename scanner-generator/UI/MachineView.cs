using Helpers;
using RegularExpression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class MachineView : Form
    {
        private readonly Expression expression = new Expression();
        private readonly Table table = new Table();
        private readonly Regex regex = null;

        /// <summary>Constructor</summary>
        /// <param name="tokens">The dictionary with the tokens</param>
        public MachineView(Dictionary<string, string> tokens)
        {
            InitializeComponent();
            try
            {
                regex = new Regex(expression.MakeExpression(tokens));
            }
            catch (Exception ex)
            {
                message.Text = "An unexpected error ocurred: " + ex.Message;
            }
        }

        /// <summary>things to do on the form load</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">Object that is being handled</param>
        private void MachineView_Load(object sender, EventArgs e)
        {
            if (regex == null)
            {
                message.ForeColor = Color.Maroon;
                message.Visible = true;
                firstLastTable.Visible = false;
            }
            else
            {
                label1.Visible = true;
                LoadFirstLastTable();
                label2.Visible = true;
                LoadFollowTable();
            }
        }

        /// <summary>Load the first and last data into the table</summary>
        private void LoadFirstLastTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Symbol");
            dataTable.Columns.Add("First");
            dataTable.Columns.Add("Last");
            dataTable.Columns.Add("Nullable");

            foreach (KeyValuePair<Tuple<int, string>, Tuple<List<int>[], bool>> item in regex.FirstLastTable)
            {
                dataTable.Rows.Add(item.Key.Item1, item.Key.Item2, table.GetList(item.Value.Item1[0]),
                                   table.GetList(item.Value.Item1[1]), item.Value.Item2);
            }

            firstLastTable.DataSource = dataTable;
        }

        /// <summary>Load the follow data into the table</summary>
        private void LoadFollowTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Symbol");
            dataTable.Columns.Add("First");

            foreach (KeyValuePair<Tuple<int, string>, List<int>> item in regex.FollowsTable)
            {
                dataTable.Rows.Add(item.Key.Item1, item.Key.Item2, table.GetList(item.Value));
            }

            followTable.DataSource = dataTable;
        }
    }
}