namespace WebServerScan
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbUrls = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.plStatus = new System.Windows.Forms.Panel();
            this.rb_Status_All = new System.Windows.Forms.RadioButton();
            this.rb_Status_Other = new System.Windows.Forms.RadioButton();
            this.rb_Status_Timeout = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ts_StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rb_ClassC = new System.Windows.Forms.RadioButton();
            this.rb_ClassB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAlive = new System.Windows.Forms.DataGridViewImageColumn();
            this.HttpStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.plStatus.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网址";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.rb_ClassB);
            this.splitContainer1.Panel1.Controls.Add(this.rb_ClassC);
            this.splitContainer1.Panel1.Controls.Add(this.tbUrls);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel2.Controls.Add(this.plStatus);
            this.splitContainer1.Size = new System.Drawing.Size(586, 87);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.TabIndex = 11;
            // 
            // tbUrls
            // 
            this.tbUrls.AcceptsReturn = true;
            this.tbUrls.Location = new System.Drawing.Point(3, 0);
            this.tbUrls.Name = "tbUrls";
            this.tbUrls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUrls.Size = new System.Drawing.Size(357, 21);
            this.tbUrls.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(10, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 52);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // plStatus
            // 
            this.plStatus.Controls.Add(this.rb_Status_All);
            this.plStatus.Controls.Add(this.rb_Status_Other);
            this.plStatus.Controls.Add(this.rb_Status_Timeout);
            this.plStatus.Enabled = false;
            this.plStatus.Location = new System.Drawing.Point(3, 63);
            this.plStatus.Name = "plStatus";
            this.plStatus.Size = new System.Drawing.Size(200, 22);
            this.plStatus.TabIndex = 8;
            // 
            // rb_Status_All
            // 
            this.rb_Status_All.AutoSize = true;
            this.rb_Status_All.Checked = true;
            this.rb_Status_All.Location = new System.Drawing.Point(7, 3);
            this.rb_Status_All.Name = "rb_Status_All";
            this.rb_Status_All.Size = new System.Drawing.Size(47, 16);
            this.rb_Status_All.TabIndex = 8;
            this.rb_Status_All.TabStop = true;
            this.rb_Status_All.Text = "全部";
            this.rb_Status_All.UseVisualStyleBackColor = true;
            this.rb_Status_All.Click += new System.EventHandler(this.rb_Status_All_Click);
            // 
            // rb_Status_Other
            // 
            this.rb_Status_Other.AutoSize = true;
            this.rb_Status_Other.Location = new System.Drawing.Point(113, 3);
            this.rb_Status_Other.Name = "rb_Status_Other";
            this.rb_Status_Other.Size = new System.Drawing.Size(47, 16);
            this.rb_Status_Other.TabIndex = 10;
            this.rb_Status_Other.Text = "其它";
            this.rb_Status_Other.UseVisualStyleBackColor = true;
            this.rb_Status_Other.Click += new System.EventHandler(this.rb_Status_All_Click);
            // 
            // rb_Status_Timeout
            // 
            this.rb_Status_Timeout.AutoSize = true;
            this.rb_Status_Timeout.Location = new System.Drawing.Point(60, 3);
            this.rb_Status_Timeout.Name = "rb_Status_Timeout";
            this.rb_Status_Timeout.Size = new System.Drawing.Size(47, 16);
            this.rb_Status_Timeout.TabIndex = 9;
            this.rb_Status_Timeout.Text = "超时";
            this.rb_Status_Timeout.UseVisualStyleBackColor = true;
            this.rb_Status_Timeout.Click += new System.EventHandler(this.rb_Status_All_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 362);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(592, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ts_StatusLabel
            // 
            this.ts_StatusLabel.Name = "ts_StatusLabel";
            this.ts_StatusLabel.Size = new System.Drawing.Size(32, 17);
            this.ts_StatusLabel.Text = "就绪";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 255);
            this.panel1.TabIndex = 3;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Domain,
            this.Url,
            this.IP,
            this.IsAlive,
            this.HttpStatus});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(592, 255);
            this.dgvResult.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "文本文件|*.txt";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.FileName = "urls.txt";
            this.saveFileDialog1.Filter = "文本文件|*.txt";
            // 
            // rb_ClassC
            // 
            this.rb_ClassC.AutoSize = true;
            this.rb_ClassC.Checked = true;
            this.rb_ClassC.Location = new System.Drawing.Point(48, 38);
            this.rb_ClassC.Name = "rb_ClassC";
            this.rb_ClassC.Size = new System.Drawing.Size(41, 16);
            this.rb_ClassC.TabIndex = 3;
            this.rb_ClassC.TabStop = true;
            this.rb_ClassC.Text = "C类";
            this.rb_ClassC.UseVisualStyleBackColor = true;
            // 
            // rb_ClassB
            // 
            this.rb_ClassB.AutoSize = true;
            this.rb_ClassB.Location = new System.Drawing.Point(95, 38);
            this.rb_ClassB.Name = "rb_ClassB";
            this.rb_ClassB.Size = new System.Drawing.Size(41, 16);
            this.rb_ClassB.TabIndex = 4;
            this.rb_ClassB.Text = "B类";
            this.rb_ClassB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "扫描：";
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 72.70084F;
            this.ID.HeaderText = "排名";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 60;
            // 
            // Domain
            // 
            this.Domain.DataPropertyName = "Domain";
            this.Domain.FillWeight = 72.70084F;
            this.Domain.HeaderText = "域名";
            this.Domain.Name = "Domain";
            this.Domain.ReadOnly = true;
            // 
            // Url
            // 
            this.Url.DataPropertyName = "Url";
            this.Url.HeaderText = "Url";
            this.Url.Name = "Url";
            this.Url.ReadOnly = true;
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IP.DefaultCellStyle = dataGridViewCellStyle1;
            this.IP.FillWeight = 72.70084F;
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            // 
            // IsAlive
            // 
            this.IsAlive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsAlive.DataPropertyName = "AliveImage";
            this.IsAlive.FillWeight = 129.6576F;
            this.IsAlive.HeaderText = "可连接";
            this.IsAlive.Name = "IsAlive";
            this.IsAlive.ReadOnly = true;
            this.IsAlive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsAlive.Width = 50;
            // 
            // HttpStatus
            // 
            this.HttpStatus.DataPropertyName = "HttpStatus";
            this.HttpStatus.FillWeight = 72.70084F;
            this.HttpStatus.HeaderText = "WEB请求";
            this.HttpStatus.Name = "HttpStatus";
            this.HttpStatus.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 384);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "网址测试（扫描整个段）";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.plStatus.ResumeLayout(false);
            this.plStatus.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbUrls;
        private System.Windows.Forms.RadioButton rb_Status_Other;
        private System.Windows.Forms.RadioButton rb_Status_Timeout;
        private System.Windows.Forms.RadioButton rb_Status_All;
        private System.Windows.Forms.Panel plStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.ToolStripStatusLabel ts_StatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_ClassB;
        private System.Windows.Forms.RadioButton rb_ClassC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Domain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewImageColumn IsAlive;
        private System.Windows.Forms.DataGridViewTextBoxColumn HttpStatus;
    }
}

