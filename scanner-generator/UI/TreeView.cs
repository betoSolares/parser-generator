using RegularExpression;
using System;
using System.Drawing;
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

        /// <summary>Draw the tree</summary>
        /// <param name="node">The current node of the tree</param>
        /// <param name="x">The x postion to draw</param>
        /// <param name="y">The y position to draw</param>
        /// <param name="distance">The distance between each  node</param>
        private void DrawTree(Node node, float x, float y, int distance)
        {
            if (node != null)
            {
                Graphics graphics = panel.CreateGraphics();
                graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(x, y, 50, 25));
                graphics.DrawString(node.Value, new Font("Segoe UI", 11, FontStyle.Regular),
                                    new SolidBrush(Color.White), x + 5, y + 5);

                if (node.LeftChild != null)
                {
                    graphics.DrawLine(new Pen(Color.Black), x + 15, y + 15, x - distance + 15, y + 65);
                    DrawTree(node.LeftChild, x - distance, y + 50, distance / 2);
                }

                if (node.RightChild != null)
                {
                    graphics.DrawLine(new Pen(Color.Black), x + 15, y + 15, x + distance + 15, y + 65);
                    DrawTree(node.RightChild, x + distance, y + 50, distance / 2);
                }
            }
        }

        /// <summary>Draw the tree on the panel</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void Draw(object sender, PaintEventArgs e)
        {
            DrawTree(tree, Width / 2, 20, 450);
        }
    }
}
