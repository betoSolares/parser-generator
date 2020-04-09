using Helpers;
using RegularExpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class MachineView : Form
    {
        private readonly Expression expression = new Expression();
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
            }
        }
    }
}