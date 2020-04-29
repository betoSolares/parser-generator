using System;
using System.Windows.Forms;

namespace parser_generator.UI
{
    public partial class CreatorView : Form
    {
        /// <summary>Constructor</summary>
        public CreatorView()
        {
            InitializeComponent();
        }

        /// <summary>Let the user select the folder.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChooseFile(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                file_path.Text = dialog.SelectedPath;
            }
            dialog.Dispose();
        }

        /// <summary>Generate a new VS project in the specific path</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void GenerateProject(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(file_path.Text) || string.IsNullOrWhiteSpace(file_path.Text))
            {
                message.Text = "Please select a directory for the solution";
            }
            else
            {
                // Copy solution
            }
        }
    }
}
