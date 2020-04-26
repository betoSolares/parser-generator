namespace parser_generator.UI
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
            this.label2 = new System.Windows.Forms.Label();
            this.followTable = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.transitionsTable = new System.Windows.Forms.DataGridView();
            this.mainview_btn = new System.Windows.Forms.Button();
            this.showtree_btn = new System.Windows.Forms.Button();
            this.creator_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.firstLastTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.followTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionsTable)).BeginInit();
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
            this.message.Size = new System.Drawing.Size(833, 519);
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
            this.firstLastTable.Location = new System.Drawing.Point(12, 30);
            this.firstLastTable.Name = "firstLastTable";
            this.firstLastTable.ReadOnly = true;
            this.firstLastTable.RowHeadersVisible = false;
            this.firstLastTable.Size = new System.Drawing.Size(512, 221);
            this.firstLastTable.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(218, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "First, Last Table";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(638, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Follow Table";
            this.label2.Visible = false;
            // 
            // followTable
            // 
            this.followTable.AllowUserToAddRows = false;
            this.followTable.AllowUserToDeleteRows = false;
            this.followTable.BackgroundColor = System.Drawing.Color.White;
            this.followTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.followTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.followTable.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.followTable.Location = new System.Drawing.Point(530, 30);
            this.followTable.Name = "followTable";
            this.followTable.ReadOnly = true;
            this.followTable.RowHeadersVisible = false;
            this.followTable.Size = new System.Drawing.Size(315, 221);
            this.followTable.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(395, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Transitions";
            this.label3.Visible = false;
            // 
            // transitionsTable
            // 
            this.transitionsTable.AllowUserToAddRows = false;
            this.transitionsTable.AllowUserToDeleteRows = false;
            this.transitionsTable.BackgroundColor = System.Drawing.Color.White;
            this.transitionsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transitionsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transitionsTable.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.transitionsTable.Location = new System.Drawing.Point(12, 277);
            this.transitionsTable.Name = "transitionsTable";
            this.transitionsTable.ReadOnly = true;
            this.transitionsTable.RowHeadersVisible = false;
            this.transitionsTable.Size = new System.Drawing.Size(833, 221);
            this.transitionsTable.TabIndex = 13;
            // 
            // mainview_btn
            // 
            this.mainview_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mainview_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainview_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.mainview_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainview_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainview_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.mainview_btn.Location = new System.Drawing.Point(12, 510);
            this.mainview_btn.Name = "mainview_btn";
            this.mainview_btn.Size = new System.Drawing.Size(97, 30);
            this.mainview_btn.TabIndex = 15;
            this.mainview_btn.Text = "Go Back";
            this.mainview_btn.UseVisualStyleBackColor = false;
            this.mainview_btn.Visible = false;
            this.mainview_btn.Click += new System.EventHandler(this.ChangeView);
            // 
            // showtree_btn
            // 
            this.showtree_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.showtree_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showtree_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.showtree_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showtree_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showtree_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.showtree_btn.Location = new System.Drawing.Point(740, 510);
            this.showtree_btn.Name = "showtree_btn";
            this.showtree_btn.Size = new System.Drawing.Size(105, 30);
            this.showtree_btn.TabIndex = 16;
            this.showtree_btn.Text = "Show Tree";
            this.showtree_btn.UseVisualStyleBackColor = false;
            this.showtree_btn.Visible = false;
            this.showtree_btn.Click += new System.EventHandler(this.ShowTree);
            // 
            // creator_btn
            // 
            this.creator_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.creator_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.creator_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.creator_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.creator_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creator_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.creator_btn.Location = new System.Drawing.Point(369, 510);
            this.creator_btn.Name = "creator_btn";
            this.creator_btn.Size = new System.Drawing.Size(155, 30);
            this.creator_btn.TabIndex = 17;
            this.creator_btn.Text = "Generate Project";
            this.creator_btn.UseVisualStyleBackColor = false;
            this.creator_btn.Visible = false;
            this.creator_btn.Click += new System.EventHandler(this.ShowCreator);
            // 
            // MachineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(857, 552);
            this.Controls.Add(this.creator_btn);
            this.Controls.Add(this.showtree_btn);
            this.Controls.Add(this.mainview_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.transitionsTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.followTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstLastTable);
            this.Controls.Add(this.message);
            this.Name = "MachineView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Machine State View";
            this.Load += new System.EventHandler(this.MachineView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.firstLastTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.followTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.DataGridView firstLastTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView followTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView transitionsTable;
        private System.Windows.Forms.Button mainview_btn;
        private System.Windows.Forms.Button showtree_btn;
        private System.Windows.Forms.Button creator_btn;
    }
}