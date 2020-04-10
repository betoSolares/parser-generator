using System;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class TreeView : Form
    {
        /// <summary>Constructor</summary>
        public TreeView()
        {
            InitializeComponent();
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
