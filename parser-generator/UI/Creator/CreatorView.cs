using Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace parser_generator.UI
{
    public partial class CreatorView : Form
    {
        private readonly Dictionary<string, string> _sets;
        private readonly Dictionary<string, string> _tokens;
        private readonly Dictionary<string, string> _actions;
        private readonly Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions;

        /// <summary>Constructor</summary>
        public CreatorView(Dictionary<string, string> sets, Dictionary<string, string> tokens, Dictionary<string, string> actions,
                           Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> trans)
        {
            InitializeComponent();
            _sets = sets;
            _tokens = tokens;
            _actions = actions;
            Transitions = trans;
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
                message.ForeColor = Color.White;
            }
            else
            {
                try
                {
                    Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\GENERIC_SOLUTION", file_path.Text + "\\Solution");
                    Code code = new Code(_tokens, _sets, _actions, Transitions);
                    code.WriteList(file_path.Text + "\\Solution\\GENERIC_SOLUTION\\Helpers");
                    code.WriteSets(file_path.Text + "\\Solution\\GENERIC_SOLUTION\\Helpers");
                    code.WriteAutomata(file_path.Text + "\\Solution\\GENERIC_SOLUTION\\Helpers");
                    code.WriteEvaluator(file_path.Text + "\\Solution\\GENERIC_SOLUTION\\Helpers");
                    code.WriteLexemas(file_path.Text + "\\Solution\\GENERIC_SOLUTION\\Helpers");
                    message.Text = "Solution generated";
                    message.ForeColor = Color.White;
                }
                catch (Exception ex)
                {
                    message.Text = "An error ocurred: " + ex.Message;
                    message.ForeColor = Color.Maroon;
                }
            }
        }

        /// <summary>Copy ech file and subdirectory</summary>
        /// <param name="source">The source directory path</param>
        /// <param name="target">The target directory path</param>
        private void Copy(string source, string target)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(source);
            DirectoryInfo targetDirectory = new DirectoryInfo(target);
            CopyAll(sourceDirectory, targetDirectory);
        }

        /// <summary>Copy each file and subdirectory</summary>
        /// <param name="source">The source directory</param>
        /// <param name="target">The target directory</param>
        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
            
            foreach (DirectoryInfo sub in source.GetDirectories())
            {
                DirectoryInfo targetsub = target.CreateSubdirectory(sub.Name);
                CopyAll(sub, targetsub);
            }
        }
    }
}
