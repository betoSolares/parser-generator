using System.Collections.Generic;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class MachineView : Form
    {
        private readonly string regex = string.Empty;
        
        /// <summary>Constructor</summary>
        /// <param name="tokens">The dictionary with the tokens</param>
        public MachineView(Dictionary<string, string> tokens)
        {
            InitializeComponent();
        }
    }
}
