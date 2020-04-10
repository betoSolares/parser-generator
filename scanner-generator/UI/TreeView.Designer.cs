namespace scanner_generator.UI
{
    partial class TreeView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.close_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.regex = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // close_btn
            // 
            this.close_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.close_btn.Location = new System.Drawing.Point(324, 408);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(159, 30);
            this.close_btn.TabIndex = 9;
            this.close_btn.Text = "Close";
            this.close_btn.UseVisualStyleBackColor = false;
            this.close_btn.Click += new System.EventHandler(this.Close);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Regular Expression: ";
            // 
            // regex
            // 
            this.regex.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.regex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regex.Cursor = System.Windows.Forms.Cursors.Default;
            this.regex.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regex.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.regex.Location = new System.Drawing.Point(159, 12);
            this.regex.Multiline = true;
            this.regex.Name = "regex";
            this.regex.ReadOnly = true;
            this.regex.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.regex.Size = new System.Drawing.Size(629, 43);
            this.regex.TabIndex = 11;
            this.regex.WordWrap = false;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 61);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(776, 341);
            this.panel.TabIndex = 12;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            // 
            // TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.regex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close_btn);
            this.Name = "TreeView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tree View";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox regex;
        private System.Windows.Forms.Panel panel;
    }
}