using RegularExpression;
using System;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class TreeView : Form
    {
        private readonly Node tree = null;

        /// <summary>Constructor</summary>
        /// <param name="expression">The regular expression of the tree</param>
        /// <param name="node">The root node of the tree</param>
        public TreeView(string expression, Node node)
        {
            InitializeComponent();
            tree = node;
            regex.Text = expression;
        }

        /// <summary>Close the window</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            Close();
        }
    }
}
