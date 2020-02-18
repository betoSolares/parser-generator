namespace scanner_generator.UI
{
    partial class View
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
            this.label1 = new System.Windows.Forms.Label();
            this.chooser_btn = new System.Windows.Forms.Button();
            this.file_path = new System.Windows.Forms.TextBox();
            this.analyze_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "File: ";
            // 
            // chooser_btn
            // 
            this.chooser_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chooser_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chooser_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.chooser_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooser_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooser_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.chooser_btn.Location = new System.Drawing.Point(270, 20);
            this.chooser_btn.Name = "chooser_btn";
            this.chooser_btn.Size = new System.Drawing.Size(75, 27);
            this.chooser_btn.TabIndex = 1;
            this.chooser_btn.Text = "Browse";
            this.chooser_btn.UseVisualStyleBackColor = false;
            // 
            // file_path
            // 
            this.file_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.file_path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_path.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_path.ForeColor = System.Drawing.SystemColors.MenuText;
            this.file_path.Location = new System.Drawing.Point(57, 20);
            this.file_path.Name = "file_path";
            this.file_path.ReadOnly = true;
            this.file_path.Size = new System.Drawing.Size(214, 27);
            this.file_path.TabIndex = 2;
            // 
            // analyze_btn
            // 
            this.analyze_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.analyze_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.analyze_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.analyze_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analyze_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analyze_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.analyze_btn.Location = new System.Drawing.Point(143, 61);
            this.analyze_btn.Name = "analyze_btn";
            this.analyze_btn.Size = new System.Drawing.Size(75, 30);
            this.analyze_btn.TabIndex = 3;
            this.analyze_btn.Text = "Analyze";
            this.analyze_btn.UseVisualStyleBackColor = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(357, 103);
            this.Controls.Add(this.analyze_btn);
            this.Controls.Add(this.file_path);
            this.Controls.Add(this.chooser_btn);
            this.Controls.Add(this.label1);
            this.Name = "View";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanner Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooser_btn;
        private System.Windows.Forms.TextBox file_path;
        private System.Windows.Forms.Button analyze_btn;
    }
}