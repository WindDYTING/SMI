
namespace SMI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCrawler = new System.Windows.Forms.Button();
            this.selectionKind = new System.Windows.Forms.ComboBox();
            this.selectionQueryRange = new System.Windows.Forms.ComboBox();
            this.timeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrawler
            // 
            this.btnCrawler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCrawler.Location = new System.Drawing.Point(0, 0);
            this.btnCrawler.Name = "btnCrawler";
            this.btnCrawler.Size = new System.Drawing.Size(105, 94);
            this.btnCrawler.TabIndex = 0;
            this.btnCrawler.Text = "Crawler";
            this.btnCrawler.UseVisualStyleBackColor = true;
            this.btnCrawler.Click += new System.EventHandler(this.btnCrawler_Click);
            // 
            // selectionKind
            // 
            this.selectionKind.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionKind.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectionKind.FormattingEnabled = true;
            this.selectionKind.Items.AddRange(new object[] {
            "全部",
            "取得或處分資產公告",
            "取得或處分私募有價證券公告",
            "依證交法第43條之1第1項取得股份公告",
            "採候選人提名制選任董監事及股東提案權相關公告(公開發行公司)",
            "採候選人提名制選任董監事相關公告（上市櫃及興櫃公司）",
            "資金貸與公告",
            "背書保證公告",
            "因股份轉換、合併、收購、受讓他公司股份或分割而發行新股後，主辦證券承銷商之評估意見",
            "外國人應辦理公告申報事項",
            "發行新股、公司債暨有價證券交付或發放股利前辦理之公告(公司法第252及273條)",
            "股票或公司債核准上市（櫃）或終止上市（櫃）之公告",
            "因員工認股權轉換而發行新股公告",
            "因轉換公司債、附認股權公司債轉換而發行新股公告",
            "海外公司債公告",
            "分離後認股權憑證之公告",
            "董事會決議買回股份作為員工認股權憑證履約之公告事項",
            "董事會決議變更發行及認股辦法之公告事項",
            "財務報告無虛偽或隱匿之聲明書公告",
            "內控聲明書公告",
            "內部控制專案審查報告",
            "變更會計師公告查詢",
            "會計主管不符資格條件調整職務公告",
            "赴大陸投資資訊實際數與自結數差異公告",
            "投資海外子公司資訊實際數與自結數差異公告",
            "公開發行股票全面轉換無實體發行公告",
            "召開股東常（臨時）會(公開發行及94.5.5前之全體公司)",
            "召開股東常(臨時)會及受益人大會(94.5.5後之上市櫃/興櫃公司)",
            "決定分派股息及紅利或其他利益(公開發行及94.5.5前之全體公司)",
            "決定分配股息及紅利或其他利益(94.5.5後之上市櫃/興櫃公司)",
            "董事會議決事項未經審計委員會通過或獨立董事有反對或保留意見",
            "依發行辦法約定收回(買)已發行之限制員工權利新股之公告",
            "會計變動公告",
            "其他依公司法規定公告"});
            this.selectionKind.Location = new System.Drawing.Point(0, 0);
            this.selectionKind.Name = "selectionKind";
            this.selectionKind.Size = new System.Drawing.Size(998, 28);
            this.selectionKind.TabIndex = 1;
            // 
            // selectionQueryRange
            // 
            this.selectionQueryRange.FormattingEnabled = true;
            this.selectionQueryRange.Items.AddRange(new object[] {
            "當天",
            "最近3天",
            "最近1週",
            "最近1月",
            "最近3月",
            "最近半年",
            "最近1年"});
            this.selectionQueryRange.Location = new System.Drawing.Point(7, 9);
            this.selectionQueryRange.Margin = new System.Windows.Forms.Padding(3, 3, 50, 3);
            this.selectionQueryRange.Name = "selectionQueryRange";
            this.selectionQueryRange.Size = new System.Drawing.Size(130, 23);
            this.selectionQueryRange.TabIndex = 2;
            // 
            // timeout
            // 
            this.timeout.Location = new System.Drawing.Point(302, 10);
            this.timeout.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.timeout.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(73, 23);
            this.timeout.TabIndex = 3;
            this.timeout.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(50, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "查詢 Timeout (secs)";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1103, 675);
            this.textBox1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 675);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCrawler);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1103, 94);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.selectionKind);
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(105, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(998, 94);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.timeout);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.selectionQueryRange);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(998, 40);
            this.panel4.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(0, 68);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(972, 26);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "目前僅支援: 取得或處分資產公告, 取得或處分私募有價證券公告, 依證交法第43條之1第1項取得股份公告\r\n";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 769);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MainForm";
            this.Text = "SMI 0.0.0.1-beta (目前僅支援取得或處分資產公告, 取得或處分私募有價證券公告, 依證交法第43條之1第1項取得股份公告)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCrawler;
        private System.Windows.Forms.ComboBox selectionKind;
        private System.Windows.Forms.ComboBox selectionQueryRange;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblStatus;
    }
}

