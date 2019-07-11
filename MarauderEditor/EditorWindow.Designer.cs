namespace MarauderEditor
{
    partial class EditorWindow
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AssetBrowserGroupBox = new System.Windows.Forms.GroupBox();
            this.AssetBrowserImageList = new System.Windows.Forms.ImageList(this.components);
            this.mEditor1 = new MarauderEditor.MEditor();
            this.AssetBrowserContainer = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            this.AssetBrowserGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AssetBrowserContainer)).BeginInit();
            this.AssetBrowserContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1203, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItem_Click);
            // 
            // AssetBrowserGroupBox
            // 
            this.AssetBrowserGroupBox.Controls.Add(this.AssetBrowserContainer);
            this.AssetBrowserGroupBox.Location = new System.Drawing.Point(213, 493);
            this.AssetBrowserGroupBox.Name = "AssetBrowserGroupBox";
            this.AssetBrowserGroupBox.Size = new System.Drawing.Size(990, 177);
            this.AssetBrowserGroupBox.TabIndex = 2;
            this.AssetBrowserGroupBox.TabStop = false;
            this.AssetBrowserGroupBox.Text = "groupBox1";
            // 
            // AssetBrowserImageList
            // 
            this.AssetBrowserImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.AssetBrowserImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.AssetBrowserImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mEditor1
            // 
            this.mEditor1.Dock = System.Windows.Forms.DockStyle.Right;
            this.mEditor1.Location = new System.Drawing.Point(213, 24);
            this.mEditor1.MouseHoverUpdatesOnly = false;
            this.mEditor1.Name = "mEditor1";
            this.mEditor1.Size = new System.Drawing.Size(990, 646);
            this.mEditor1.TabIndex = 0;
            this.mEditor1.Text = "mEditor1";
            this.mEditor1.Click += new System.EventHandler(this.MEditor1_Click);
            // 
            // AssetBrowserContainer
            // 
            this.AssetBrowserContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssetBrowserContainer.Location = new System.Drawing.Point(3, 16);
            this.AssetBrowserContainer.Name = "AssetBrowserContainer";
            this.AssetBrowserContainer.Size = new System.Drawing.Size(984, 158);
            this.AssetBrowserContainer.SplitterDistance = 328;
            this.AssetBrowserContainer.TabIndex = 0;
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 670);
            this.Controls.Add(this.AssetBrowserGroupBox);
            this.Controls.Add(this.mEditor1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditorWindow";
            this.Text = "Marauder Engine";
            this.Load += new System.EventHandler(this.EditorWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.AssetBrowserGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AssetBrowserContainer)).EndInit();
            this.AssetBrowserContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MEditor mEditor1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.GroupBox AssetBrowserGroupBox;
        private System.Windows.Forms.ImageList AssetBrowserImageList;
        private System.Windows.Forms.SplitContainer AssetBrowserContainer;
    }
}

