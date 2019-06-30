namespace MarauderEditor.Forms.File
{
    partial class NewProjectForm
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
            this.ProjectNameBox = new System.Windows.Forms.TextBox();
            this.ProjectNameLabel = new System.Windows.Forms.Label();
            this.CreateProjectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProjectNameBox
            // 
            this.ProjectNameBox.Location = new System.Drawing.Point(92, 35);
            this.ProjectNameBox.Name = "ProjectNameBox";
            this.ProjectNameBox.Size = new System.Drawing.Size(224, 20);
            this.ProjectNameBox.TabIndex = 0;
            this.ProjectNameBox.Text = "New Project";
            this.ProjectNameBox.TextChanged += new System.EventHandler(this.ProjectNameBox_TextChanged);
            // 
            // ProjectNameLabel
            // 
            this.ProjectNameLabel.AutoSize = true;
            this.ProjectNameLabel.Location = new System.Drawing.Point(15, 38);
            this.ProjectNameLabel.Name = "ProjectNameLabel";
            this.ProjectNameLabel.Size = new System.Drawing.Size(71, 13);
            this.ProjectNameLabel.TabIndex = 1;
            this.ProjectNameLabel.Text = "Project Name";
            this.ProjectNameLabel.Click += new System.EventHandler(this.ProjectNameLabel_Click);
            // 
            // CreateProjectButton
            // 
            this.CreateProjectButton.Location = new System.Drawing.Point(92, 62);
            this.CreateProjectButton.Name = "CreateProjectButton";
            this.CreateProjectButton.Size = new System.Drawing.Size(176, 23);
            this.CreateProjectButton.TabIndex = 2;
            this.CreateProjectButton.Text = "Create Project";
            this.CreateProjectButton.UseVisualStyleBackColor = true;
            this.CreateProjectButton.Click += new System.EventHandler(this.CreateProjectButton_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateProjectButton);
            this.Controls.Add(this.ProjectNameLabel);
            this.Controls.Add(this.ProjectNameBox);
            this.Name = "NewProjectForm";
            this.Text = "New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProjectNameBox;
        private System.Windows.Forms.Label ProjectNameLabel;
        private System.Windows.Forms.Button CreateProjectButton;
    }
}