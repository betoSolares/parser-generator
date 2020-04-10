namespace scanner_generator.UI
{
    partial class MachineView
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
            this.message = new System.Windows.Forms.TextBox();
            this.firstLastTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.firstLastTable)).BeginInit();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message.Cursor = System.Windows.Forms.Cursors.Default;
            this.message.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.message.Location = new System.Drawing.Point(12, 12);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.Size = new System.Drawing.Size(741, 324);
            this.message.TabIndex = 8;
            this.message.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.message.Visible = false;
            // 
            // firstLastTable
            // 
            this.firstLastTable.AllowUserToAddRows = false;
            this.firstLastTable.AllowUserToDeleteRows = false;
            this.firstLastTable.BackgroundColor = System.Drawing.Color.White;
            this.firstLastTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstLastTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.firstLastTable.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.firstLastTable.Location = new System.Drawing.Point(12, 44);
            this.firstLastTable.Name = "firstLastTable";
            this.firstLastTable.ReadOnly = true;
            this.firstLastTable.RowHeadersVisible = false;
            this.firstLastTable.Size = new System.Drawing.Size(413, 221);
            this.firstLastTable.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(160, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "First, Last Table";
            this.label1.Visible = false;
            // 
            // MachineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(765, 348);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstLastTable);
            this.Controls.Add(this.message);
            this.Name = "MachineView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Machine State View";
            this.Load += new System.EventHandler(this.MachineView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.firstLastTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.DataGridView firstLastTable;
        private System.Windows.Forms.Label label1;
    }
}