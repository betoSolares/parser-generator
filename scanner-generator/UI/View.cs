using System;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class View : Form
    {
        /// <summary>Constructor</summary>
        public View()
        {
            InitializeComponent();
        }

        /// <summary>Let the user select the file and put the path in the screen.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChooseFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_path.Text = dialog.FileName;
            }
            dialog.Dispose();
        }
    }
}
