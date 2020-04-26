namespace parser_generator.UI
{
    partial class CreatorView
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
            this.generate_btn = new System.Windows.Forms.Button();
            this.file_path = new System.Windows.Forms.TextBox();
            this.chooser_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // generate_btn
            // 
            this.generate_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.generate_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.generate_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.generate_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generate_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generate_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.generate_btn.Location = new System.Drawing.Point(122, 105);
            this.generate_btn.Name = "generate_btn";
            this.generate_btn.Size = new System.Drawing.Size(130, 30);
            this.generate_btn.TabIndex = 11;
            this.generate_btn.Text = "Generate Project";
            this.generate_btn.UseVisualStyleBackColor = false;
            // 
            // file_path
            // 
            this.file_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.file_path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_path.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.file_path.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_path.ForeColor = System.Drawing.SystemColors.MenuText;
            this.file_path.Location = new System.Drawing.Point(74, 59);
            this.file_path.Name = "file_path";
            this.file_path.ReadOnly = true;
            this.file_path.Size = new System.Drawing.Size(199, 27);
            this.file_path.TabIndex = 10;
            // 
            // chooser_btn
            // 
            this.chooser_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chooser_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chooser_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.chooser_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooser_btn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooser_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.chooser_btn.Location = new System.Drawing.Point(272, 59);
            this.chooser_btn.Name = "chooser_btn";
            this.chooser_btn.Size = new System.Drawing.Size(75, 27);
            this.chooser_btn.TabIndex = 9;
            this.chooser_btn.Text = "Browse";
            this.chooser_btn.UseVisualStyleBackColor = false;
            this.chooser_btn.Click += new System.EventHandler(this.ChooseFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Path: ";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ForeColor = System.Drawing.SystemColors.MenuText;
            this.name.Location = new System.Drawing.Point(74, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(273, 27);
            this.name.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Name: ";
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message.Cursor = System.Windows.Forms.Cursors.Default;
            this.message.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.message.Location = new System.Drawing.Point(16, 155);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.Size = new System.Drawing.Size(331, 69);
            this.message.TabIndex = 14;
            this.message.Visible = false;
            // 
            // CreatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(359, 251);
            this.Controls.Add(this.message);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.generate_btn);
            this.Controls.Add(this.file_path);
            this.Controls.Add(this.chooser_btn);
            this.Controls.Add(this.label1);
            this.Name = "CreatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Creator View";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generate_btn;
        private System.Windows.Forms.TextBox file_path;
        private System.Windows.Forms.Button chooser_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox message;
    }
}