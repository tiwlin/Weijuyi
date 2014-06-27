namespace UploadTool
{
    partial class Upload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upload));
            this.lblFilePath = new System.Windows.Forms.Label();
            this.dlgSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pbUpload = new System.Windows.Forms.ProgressBar();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.tvCategory = new System.Windows.Forms.TreeView();
            this.rlbAccount = new System.Windows.Forms.RadioListBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFilePath.Image = ((System.Drawing.Image)(resources.GetObject("lblFilePath.Image")));
            this.lblFilePath.Location = new System.Drawing.Point(356, 260);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(65, 12);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "文件路径：";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(659, 256);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "浏览";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(422, 257);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(231, 21);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // btnUpload
            // 
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(740, 256);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pbUpload
            // 
            this.pbUpload.Location = new System.Drawing.Point(358, 300);
            this.pbUpload.Name = "pbUpload";
            this.pbUpload.Size = new System.Drawing.Size(457, 20);
            this.pbUpload.TabIndex = 4;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(116, 203);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(0, 12);
            this.lblContent.TabIndex = 5;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(864, 339);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 12);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // picClose
            // 
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(844, 1);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(16, 16);
            this.picClose.TabIndex = 7;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // tvCategory
            // 
            this.tvCategory.Location = new System.Drawing.Point(12, 36);
            this.tvCategory.Name = "tvCategory";
            this.tvCategory.Size = new System.Drawing.Size(338, 348);
            this.tvCategory.TabIndex = 9;
            // 
            // rlbAccount
            // 
            this.rlbAccount.BackColor = System.Drawing.SystemColors.Control;
            this.rlbAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rlbAccount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.rlbAccount.FormattingEnabled = true;
            this.rlbAccount.ItemHeight = 20;
            this.rlbAccount.Location = new System.Drawing.Point(358, 36);
            this.rlbAccount.Name = "rlbAccount";
            this.rlbAccount.Size = new System.Drawing.Size(447, 180);
            this.rlbAccount.TabIndex = 8;
            this.rlbAccount.Transparent = true;
            this.rlbAccount.SelectedIndexChanged += new System.EventHandler(this.rlbAccount_SelectedIndexChanged);
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(861, 396);
            this.Controls.Add(this.tvCategory);
            this.Controls.Add(this.rlbAccount);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.pbUpload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Upload";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传资料";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Upload_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Upload_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.OpenFileDialog dlgSelectFile;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ProgressBar pbUpload;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.RadioListBox rlbAccount;
        private System.Windows.Forms.TreeView tvCategory;
    }
}

