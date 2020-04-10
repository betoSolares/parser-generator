﻿namespace scanner_generator.UI
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
            ((System.ComponentModel.ISupportInitialize)(this.firstLastTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.followTable)).BeginInit();
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
            this.message.Size = new System.Drawing.Size(833, 324);
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
            this.followTable.Location = new System.Drawing.Point(530, 44);
            this.followTable.Name = "followTable";
            this.followTable.ReadOnly = true;
            this.followTable.RowHeadersVisible = false;
            this.followTable.Size = new System.Drawing.Size(315, 221);
            this.followTable.TabIndex = 11;
            // 
            // MachineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(857, 348);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.DataGridView firstLastTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView followTable;
    }
}