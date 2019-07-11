namespace MarauderEditor.Forms.EditorWindow.MenuBar.File.Projects
{
    partial class ProjectBrowserForm
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
            this.OpenProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ProjectPathTextBox = new System.Windows.Forms.TextBox();
            this.ProjectPathBrowseButton = new System.Windows.Forms.Button();
            this.ProjectPathLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenProjectFileDialog
            // 
            this.OpenProjectFileDialog.Filter = "\"Marauder Project Files (.mproj)|*.mproj\"";
            // 
            // ProjectPathTextBox
            // 
            this.ProjectPathTextBox.Location = new System.Drawing.Point(84, 13);
            this.ProjectPathTextBox.Name = "ProjectPathTextBox";
            this.ProjectPathTextBox.Size = new System.Drawing.Size(378, 20);
            this.ProjectPathTextBox.TabIndex = 0;
            this.ProjectPathTextBox.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // ProjectPathBrowseButton
            // 
            this.ProjectPathBrowseButton.Location = new System.Drawing.Point(469, 12);
            this.ProjectPathBrowseButton.Name = "ProjectPathBrowseButton";
            this.ProjectPathBrowseButton.Size = new System.Drawing.Size(88, 23);
            this.ProjectPathBrowseButton.TabIndex = 1;
            this.ProjectPathBrowseButton.Text = "Browse";
            this.ProjectPathBrowseButton.UseVisualStyleBackColor = true;
            this.ProjectPathBrowseButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ProjectPathLabel
            // 
            this.ProjectPathLabel.AutoSize = true;
            this.ProjectPathLabel.Location = new System.Drawing.Point(13, 16);
            this.ProjectPathLabel.Name = "ProjectPathLabel";
            this.ProjectPathLabel.Size = new System.Drawing.Size(65, 13);
            this.ProjectPathLabel.TabIndex = 2;
            this.ProjectPathLabel.Text = "Project Path";
            this.ProjectPathLabel.Click += new System.EventHandler(this.Label1_Click);
            // 
            // ProjectBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ProjectPathLabel);
            this.Controls.Add(this.ProjectPathBrowseButton);
            this.Controls.Add(this.ProjectPathTextBox);
            this.Name = "ProjectBrowserForm";
            this.Text = "Project Browser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenProjectFileDialog;
        private System.Windows.Forms.TextBox ProjectPathTextBox;
        private System.Windows.Forms.Button ProjectPathBrowseButton;
        private System.Windows.Forms.Label ProjectPathLabel;
    }
}