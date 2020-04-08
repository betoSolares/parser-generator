using Helpers;
using RegularExpression;
using System.Collections.Generic;
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
            regex = new Regex(expression.MakeExpression(tokens));
        }
    }
}