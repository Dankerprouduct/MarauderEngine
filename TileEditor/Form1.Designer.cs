namespace TileEditor
{
    partial class Form1
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
            this.listviewPanel = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.monogamePanel = new System.Windows.Forms.Panel();
            this.editor1 = new TileEditor.Editor();
            this.heightTextbox = new System.Windows.Forms.TextBox();
            this.widthTextbox = new System.Windows.Forms.TextBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listviewPanel.SuspendLayout();
            this.monogamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // listviewPanel
            // 
            this.listviewPanel.Controls.Add(this.listView1);
            this.listviewPanel.Location = new System.Drawing.Point(0, 164);
            this.listviewPanel.Name = "listviewPanel";
            this.listviewPanel.Size = new System.Drawing.Size(437, 985);
            this.listviewPanel.TabIndex = 0;
            this.listviewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.listviewPanel_Paint);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(437, 985);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // monogamePanel
            // 
            this.monogamePanel.Controls.Add(this.editor1);
            this.monogamePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.monogamePanel.Location = new System.Drawing.Point(443, 0);
            this.monogamePanel.Name = "monogamePanel";
            this.monogamePanel.Size = new System.Drawing.Size(1449, 1154);
            this.monogamePanel.TabIndex = 1;
            // 
            // editor1
            // 
            this.editor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor1.Location = new System.Drawing.Point(0, 0);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(1449, 1154);
            this.editor1.TabIndex = 0;
            this.editor1.Text = "editor1";
            this.editor1.Click += new System.EventHandler(this.editor1_Click);
            // 
            // heightTextbox
            // 
            this.heightTextbox.Location = new System.Drawing.Point(116, 47);
            this.heightTextbox.Name = "heightTextbox";
            this.heightTextbox.Size = new System.Drawing.Size(165, 29);
            this.heightTextbox.TabIndex = 2;
            // 
            // widthTextbox
            // 
            this.widthTextbox.Location = new System.Drawing.Point(116, 12);
            this.widthTextbox.Name = "widthTextbox";
            this.widthTextbox.Size = new System.Drawing.Size(165, 29);
            this.widthTextbox.TabIndex = 3;
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 16);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(57, 25);
            this.widthLabel.TabIndex = 4;
            this.widthLabel.Text = "width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(12, 51);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(65, 25);
            this.heightLabel.TabIndex = 5;
            this.heightLabel.Text = "height";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(302, 12);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(135, 64);
            this.createButton.TabIndex = 6;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 88);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(181, 35);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(243, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(194, 35);
            this.button3.TabIndex = 8;
            this.button3.Text = "Load";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 129);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(420, 29);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1892, 1154);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.widthTextbox);
            this.Controls.Add(this.heightTextbox);
            this.Controls.Add(this.monogamePanel);
            this.Controls.Add(this.listviewPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.listviewPanel.ResumeLayout(false);
            this.monogamePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel listviewPanel;
        private System.Windows.Forms.Panel monogamePanel;
        private System.Windows.Forms.ListView listView1;
        private Editor editor1;
        private System.Windows.Forms.TextBox heightTextbox;
        private System.Windows.Forms.TextBox widthTextbox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

