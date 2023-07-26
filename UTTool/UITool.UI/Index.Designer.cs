namespace UITool.UI
{
    partial class Index
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.trvDescripterTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofLoadFile = new System.Windows.Forms.OpenFileDialog();
            this.fbLoad = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.trvObjectList = new System.Windows.Forms.TreeView();
            this.pnlQueryBox = new System.Windows.Forms.Panel();
            this.trvQueryList = new System.Windows.Forms.TreeView();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.pnlCheckList = new System.Windows.Forms.Panel();
            this.btnAllSelect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.checkList = new System.Windows.Forms.CheckedListBox();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlQueryBox.SuspendLayout();
            this.pnlCheckList.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvDescripterTree
            // 
            this.trvDescripterTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvDescripterTree.ContextMenuStrip = this.contextMenuStrip1;
            this.trvDescripterTree.ImageIndex = 0;
            this.trvDescripterTree.ImageList = this.imageList1;
            this.trvDescripterTree.Location = new System.Drawing.Point(12, 39);
            this.trvDescripterTree.Name = "trvDescripterTree";
            this.trvDescripterTree.SelectedImageIndex = 0;
            this.trvDescripterTree.Size = new System.Drawing.Size(304, 464);
            this.trvDescripterTree.TabIndex = 1;
            this.trvDescripterTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDescripterTree_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Dir");
            this.imageList1.Images.SetKeyName(1, "blank");
            this.imageList1.Images.SetKeyName(2, "code");
            this.imageList1.Images.SetKeyName(3, "Screenshot 2023-03-14 143543.png");
            this.imageList1.Images.SetKeyName(4, "Screenshot 2023-03-14 143558.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1224, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = "版本号 1.1.2";
            // 
            // ofLoadFile
            // 
            this.ofLoadFile.Filter = "dll|*.dll";
            this.ofLoadFile.FileOk += new System.ComponentModel.CancelEventHandler(this.ofLoadFile_FileOk);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1224, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.directoryToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::UITool.UI.Properties.Resources.od;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(37, 28);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // directoryToolStripMenuItem
            // 
            this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
            this.directoryToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.directoryToolStripMenuItem.Text = "Directory";
            this.directoryToolStripMenuItem.Click += new System.EventHandler(this.directoryToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(322, 39);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(894, 464);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(125, 175);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(1224, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(33, 543);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(20, 46);
            this.toolStripMenuItem1.Text = "Object";
            this.toolStripMenuItem1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(20, 43);
            this.toolStripMenuItem2.Text = "Query";
            this.toolStripMenuItem2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // trvObjectList
            // 
            this.trvObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvObjectList.ContextMenuStrip = this.contextMenuStrip1;
            this.trvObjectList.Location = new System.Drawing.Point(893, 0);
            this.trvObjectList.Name = "trvObjectList";
            this.trvObjectList.Size = new System.Drawing.Size(331, 543);
            this.trvObjectList.TabIndex = 7;
            this.trvObjectList.Visible = false;
            this.trvObjectList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvObjectList_NodeMouseClick);
            // 
            // pnlQueryBox
            // 
            this.pnlQueryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQueryBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlQueryBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQueryBox.Controls.Add(this.trvQueryList);
            this.pnlQueryBox.Controls.Add(this.txtQuery);
            this.pnlQueryBox.Location = new System.Drawing.Point(893, 0);
            this.pnlQueryBox.Name = "pnlQueryBox";
            this.pnlQueryBox.Size = new System.Drawing.Size(331, 543);
            this.pnlQueryBox.TabIndex = 8;
            this.pnlQueryBox.Visible = false;
            // 
            // trvQueryList
            // 
            this.trvQueryList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvQueryList.ContextMenuStrip = this.contextMenuStrip1;
            this.trvQueryList.Location = new System.Drawing.Point(14, 40);
            this.trvQueryList.Name = "trvQueryList";
            this.trvQueryList.Size = new System.Drawing.Size(308, 490);
            this.trvQueryList.TabIndex = 1;
            this.trvQueryList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvQueryList_NodeMouseClick);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(14, 11);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(308, 23);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // pnlCheckList
            // 
            this.pnlCheckList.AllowDrop = true;
            this.pnlCheckList.BackColor = System.Drawing.SystemColors.Menu;
            this.pnlCheckList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCheckList.Controls.Add(this.btnAllSelect);
            this.pnlCheckList.Controls.Add(this.btnClose);
            this.pnlCheckList.Controls.Add(this.btnGenerate);
            this.pnlCheckList.Controls.Add(this.checkList);
            this.pnlCheckList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCheckList.Location = new System.Drawing.Point(346, 187);
            this.pnlCheckList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlCheckList.Name = "pnlCheckList";
            this.pnlCheckList.Size = new System.Drawing.Size(326, 298);
            this.pnlCheckList.TabIndex = 10;
            this.pnlCheckList.Visible = false;
            // 
            // btnAllSelect
            // 
            this.btnAllSelect.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnAllSelect.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAllSelect.Location = new System.Drawing.Point(265, 5);
            this.btnAllSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAllSelect.Name = "btnAllSelect";
            this.btnAllSelect.Size = new System.Drawing.Size(44, 20);
            this.btnAllSelect.TabIndex = 3;
            this.btnAllSelect.Text = "全选";
            this.btnAllSelect.UseVisualStyleBackColor = false;
            this.btnAllSelect.Click += new System.EventHandler(this.btnAllSelect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(230, 270);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(148, 270);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(78, 20);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // checkList
            // 
            this.checkList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkList.FormattingEnabled = true;
            this.checkList.Location = new System.Drawing.Point(15, 29);
            this.checkList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkList.Name = "checkList";
            this.checkList.ScrollAlwaysVisible = true;
            this.checkList.Size = new System.Drawing.Size(295, 238);
            this.checkList.TabIndex = 0;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1257, 543);
            this.Controls.Add(this.pnlCheckList);
            this.Controls.Add(this.pnlQueryBox);
            this.Controls.Add(this.trvObjectList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.trvDescripterTree);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Index";
            this.Text = "Index";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlQueryBox.ResumeLayout(false);
            this.pnlQueryBox.PerformLayout();
            this.pnlCheckList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView trvDescripterTree;
        private ContextMenuStrip contextMenuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private OpenFileDialog ofLoadFile;
        private FolderBrowserDialog fbLoad;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem directoryToolStripMenuItem;
        private TextBox textBox1;
        private ImageList imageList1;
        private ToolStripPanel BottomToolStripPanel;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private TreeView trvObjectList;
        private ToolStripMenuItem toolStripMenuItem2;
        private Panel pnlQueryBox;
        private TreeView trvQueryList;
        private TextBox txtQuery;
        private Panel pnlCheckList;
        private CheckedListBox checkList;
        private Button btnAllSelect;
        private Button btnClose;
        private Button btnGenerate;
    }
}