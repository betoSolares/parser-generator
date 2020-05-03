using System.IO;

namespace SolutionGenerator
{
    public class MainUI
    {
        /// <summary>Write all the files</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">Tha path for the files</param>
        public void WriteFiles(string name, string path)
        {
            WriteUI(name, path);
            WriteDesigner(name, path);
            WriteResx(path);
        }

        /// <summary>Write the cs file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the files</param>
        private void WriteUI(string name, string path)
        {
            string text = @"using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace " + name + @".UI
{
    public partial class MainView : Form
    {
        private readonly Tokenizer tokenizer = new Tokenizer();
        private Evaluator evaluator;

        /// <summary>Constructor</summary>
        public MainView()
        {
            InitializeComponent();
        }

        /// <summary>Choose the file to scan</summary>
        /// <param name=""sender"">The object that raised the event.</param>
        /// <param name=""e"">Object that is being handled</param>
        private void Load_File(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = ""txt files (*.txt)|*.txt|All files (*.*)|*.*""
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color;
                string fileText;
                try
                {
                    fileText = File.ReadAllText(dialog.FileName);
                    color = Color.Black;
                }
                catch (Exception ex)
                {
                    fileText = ""Error loading the file: "" + ex.Message;
                    color = Color.Maroon;
                }
                text.Text = fileText;
                text.ForeColor = color;
            }
            dialog.Dispose();
        }

        /// <summary>Scan the text</summary>
        /// <param name=""sender"">The object that raised the event.</param>
        /// <param name=""e"">Object that is being handled</param>
        private void Scan_Text(object sender, EventArgs e)
        {
            Queue<string> tokens = tokenizer.TokenizeText(text.Text);
            evaluator = new Evaluator(tokens);
            message.Visible = true;
            if (evaluator.Evaluate())
            {
                message.Text = ""The language is correct"";
                message.ForeColor = Color.White;
                show_lexeme_btn.Visible = true;
            }
            else
            {
                message.Text = ""There are erros in your language"";
                message.ForeColor = Color.Maroon;
                show_lexeme_btn.Visible = false;
            }
        }

        /// <summary>Change to the lexemes view</summary>
        /// <param name=""sender"">The object that raised the event.</param>
        /// <param name=""e"">Object that is being handled</param>
        private void Show_Lexemes(object sender, EventArgs e)
        {
            Hide();
            using (LexemeView lexemeView = new LexemeView(evaluator.GetLexemes()))
            {
                lexemeView.ShowDialog();
            }
            Close();
        }
    }
}";
            File.WriteAllText(Path.Combine(path, "MainView.cs"), text);
        }

        /// <summary>Write the designer file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the files</param>
        private void WriteDesigner(string name, string path)
        {
            string text = @"namespace " + name + @".UI
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name=""disposing"">true if managed resources should be disposed; otherwise, false.</param>
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
            this.text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scan_btn = new System.Windows.Forms.Button();
            this.loadFile_btn = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.show_lexeme_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text.Font = new System.Drawing.Font(""Yu Gothic UI"", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text.Location = new System.Drawing.Point(12, 37);
            this.text.Multiline = true;
            this.text.Name = ""text"";
            this.text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text.Size = new System.Drawing.Size(395, 271);
            this.text.TabIndex = 0;
            this.text.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font(""Yu Gothic UI"", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(145, 9);
            this.label1.Name = ""label1"";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = ""Text Scanner"";
            // 
            // scan_btn
            // 
            this.scan_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scan_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scan_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.scan_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scan_btn.Font = new System.Drawing.Font(""Yu Gothic"", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scan_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.scan_btn.Location = new System.Drawing.Point(126, 357);
            this.scan_btn.Name = ""scan_btn"";
            this.scan_btn.Size = new System.Drawing.Size(159, 30);
            this.scan_btn.TabIndex = 9;
            this.scan_btn.Text = ""Scan Text"";
            this.scan_btn.UseVisualStyleBackColor = false;
            this.scan_btn.Click += new System.EventHandler(this.Scan_Text);
            // 
            // loadFile_btn
            // 
            this.loadFile_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.loadFile_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadFile_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.loadFile_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFile_btn.Font = new System.Drawing.Font(""Yu Gothic"", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadFile_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.loadFile_btn.Location = new System.Drawing.Point(126, 321);
            this.loadFile_btn.Name = ""loadFile_btn"";
            this.loadFile_btn.Size = new System.Drawing.Size(159, 30);
            this.loadFile_btn.TabIndex = 10;
            this.loadFile_btn.Text = ""Load File"";
            this.loadFile_btn.UseVisualStyleBackColor = false;
            this.loadFile_btn.Click += new System.EventHandler(this.Load_File);
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message.Cursor = System.Windows.Forms.Cursors.Default;
            this.message.Font = new System.Drawing.Font(""Yu Gothic UI"", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.message.Location = new System.Drawing.Point(12, 393);
            this.message.Multiline = true;
            this.message.Name = ""message"";
            this.message.ReadOnly = true;
            this.message.Size = new System.Drawing.Size(395, 69);
            this.message.TabIndex = 11;
            this.message.Visible = false;
            // 
            // show_lexeme_btn
            // 
            this.show_lexeme_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.show_lexeme_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.show_lexeme_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuText;
            this.show_lexeme_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_lexeme_btn.Font = new System.Drawing.Font(""Yu Gothic"", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_lexeme_btn.ForeColor = System.Drawing.SystemColors.MenuText;
            this.show_lexeme_btn.Location = new System.Drawing.Point(126, 463);
            this.show_lexeme_btn.Name = ""show_lexeme_btn"";
            this.show_lexeme_btn.Size = new System.Drawing.Size(159, 30);
            this.show_lexeme_btn.TabIndex = 12;
            this.show_lexeme_btn.Text = ""Show Lexemes"";
            this.show_lexeme_btn.UseVisualStyleBackColor = false;
            this.show_lexeme_btn.Visible = false;
            this.show_lexeme_btn.Click += new System.EventHandler(this.Show_Lexemes);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(419, 505);
            this.Controls.Add(this.show_lexeme_btn);
            this.Controls.Add(this.message);
            this.Controls.Add(this.loadFile_btn);
            this.Controls.Add(this.scan_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text);
            this.Name = ""MainView"";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ""Main View"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button scan_btn;
        private System.Windows.Forms.Button loadFile_btn;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.Button show_lexeme_btn;
    }
}";
            File.WriteAllText(Path.Combine(path, "MainView.Designer.cs"), text);
        }

        /// <summary>Write the resx file</summary>
        /// <param name="path">The path for the files</param>
        private void WriteResx(string path)
        {
            string text = @"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
  <!-- 
    Microsoft ResX Schema 
    
    Version 2.0
    
    The primary goals of this format is to allow a simple XML format 
    that is mostly human readable. The generation and parsing of the 
    various data types are done through the TypeConverter classes 
    associated with the data types.
    
    Example:
    
    ... ado.net/XML headers & schema ...
    <resheader name=""resmimetype"">text/microsoft-resx</resheader>
    <resheader name=""version"">2.0</resheader>
    <resheader name=""reader"">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>
    <resheader name=""writer"">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>
    <data name=""Name1""><value>this is my long string</value><comment>this is a comment</comment></data>
    <data name=""Color1"" type=""System.Drawing.Color, System.Drawing"">Blue</data>
    <data name=""Bitmap1"" mimetype=""application/x-microsoft.net.object.binary.base64"">
        <value>[base64 mime encoded serialized .NET Framework object]</value>
    </data>
    <data name=""Icon1"" type=""System.Drawing.Icon, System.Drawing"" mimetype=""application/x-microsoft.net.object.bytearray.base64"">
        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>
        <comment>This is a comment</comment>
    </data>
                
    There are any number of ""resheader"" rows that contain simple 
    name/value pairs.
    
    Each data row contains a name, and value. The row also contains a 
    type or mimetype. Type corresponds to a .NET class that support 
    text/value conversion through the TypeConverter architecture. 
    Classes that don't support this are serialized and stored with the 
    mimetype set.
    
    The mimetype is used for serialized objects, and tells the 
    ResXResourceReader how to depersist the object. This is currently not 
    extensible. For a given mimetype the value must be set accordingly:
    
    Note - application/x-microsoft.net.object.binary.base64 is the format 
    that the ResXResourceWriter will generate, however the reader can 
    read any of the formats listed below.
    
    mimetype: application/x-microsoft.net.object.binary.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            : and then encoded with base64 encoding.
    
    mimetype: application/x-microsoft.net.object.soap.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.bytearray.base64
    value   : The object must be serialized into a byte array 
            : using a System.ComponentModel.TypeConverter
            : and then encoded with base64 encoding.
    -->
  <xsd:schema id=""root"" xmlns="""" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"">
    <xsd:import namespace=""http://www.w3.org/XML/1998/namespace"" />
    <xsd:element name=""root"" msdata:IsDataSet=""true"">
      <xsd:complexType>
        <xsd:choice maxOccurs=""unbounded"">
          <xsd:element name=""metadata"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" use=""required"" type=""xsd:string"" />
              <xsd:attribute name=""type"" type=""xsd:string"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""assembly"">
            <xsd:complexType>
              <xsd:attribute name=""alias"" type=""xsd:string"" />
              <xsd:attribute name=""name"" type=""xsd:string"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""data"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
                <xsd:element name=""comment"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""2"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" msdata:Ordinal=""1"" />
              <xsd:attribute name=""type"" type=""xsd:string"" msdata:Ordinal=""3"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" msdata:Ordinal=""4"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""resheader"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name=""resmimetype"">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name=""version"">
    <value>2.0</value>
  </resheader>
  <resheader name=""reader"">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name=""writer"">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
</root>";
            File.WriteAllText(Path.Combine(path, "MainView.resx"), text);
        }
    }
}
