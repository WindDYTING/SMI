using System;

namespace SMI.Example.Winform
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
        private void InitializeComponent() {
            btnCrawler = new System.Windows.Forms.Button();
            selectionQueryRange = new System.Windows.Forms.ComboBox();
            timeout = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            txtLog = new System.Windows.Forms.TextBox();
            panel1 = new System.Windows.Forms.Panel();
            panel6 = new System.Windows.Forms.Panel();
            btnLogClear = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            checkQueryRecent = new System.Windows.Forms.CheckBox();
            checkSpecifyDate = new System.Windows.Forms.CheckBox();
            queryTo = new System.Windows.Forms.DateTimePicker();
            queryForm = new System.Windows.Forms.DateTimePicker();
            selectionKind = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            lblStatus = new System.Windows.Forms.Label();
            lbl1 = new System.Windows.Forms.Label();
            selectionEventList = new System.Windows.Forms.CheckedListBox();
            panel5 = new System.Windows.Forms.Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            checkTelegramSendToWho = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)timeout).BeginInit();
            panel1.SuspendLayout();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCrawler
            // 
            btnCrawler.Dock = System.Windows.Forms.DockStyle.Right;
            btnCrawler.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCrawler.Location = new System.Drawing.Point(1223, 0);
            btnCrawler.Margin = new System.Windows.Forms.Padding(6);
            btnCrawler.Name = "btnCrawler";
            btnCrawler.Size = new System.Drawing.Size(142, 161);
            btnCrawler.TabIndex = 0;
            btnCrawler.Text = "Get";
            btnCrawler.UseVisualStyleBackColor = true;
            btnCrawler.Click += btnCrawler_Click;
            // 
            // selectionQueryRange
            // 
            selectionQueryRange.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            selectionQueryRange.FormattingEnabled = true;
            selectionQueryRange.Items.AddRange(new object[] { "當天", "最近3天", "最近1週", "最近1月", "最近3月", "最近半年", "最近1年" });
            selectionQueryRange.Location = new System.Drawing.Point(266, 78);
            selectionQueryRange.Margin = new System.Windows.Forms.Padding(6, 6, 92, 6);
            selectionQueryRange.Name = "selectionQueryRange";
            selectionQueryRange.Size = new System.Drawing.Size(310, 33);
            selectionQueryRange.TabIndex = 2;
            // 
            // timeout
            // 
            timeout.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            timeout.Location = new System.Drawing.Point(309, 12);
            timeout.Margin = new System.Windows.Forms.Padding(6);
            timeout.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            timeout.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            timeout.Name = "timeout";
            timeout.Size = new System.Drawing.Size(170, 33);
            timeout.TabIndex = 3;
            timeout.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(22, 15);
            label1.Margin = new System.Windows.Forms.Padding(92, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 25);
            label1.TabIndex = 4;
            label1.Text = "查詢逾時 (單位: 秒)";
            // 
            // txtLog
            // 
            txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            txtLog.Location = new System.Drawing.Point(0, 0);
            txtLog.Margin = new System.Windows.Forms.Padding(6);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(1365, 1236);
            txtLog.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(1199, 0);
            panel1.Margin = new System.Windows.Forms.Padding(6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1365, 1522);
            panel1.TabIndex = 6;
            // 
            // panel6
            // 
            panel6.Controls.Add(txtLog);
            panel6.Controls.Add(btnLogClear);
            panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            panel6.Location = new System.Drawing.Point(0, 221);
            panel6.Margin = new System.Windows.Forms.Padding(4);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(1365, 1301);
            panel6.TabIndex = 8;
            // 
            // btnLogClear
            // 
            btnLogClear.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnLogClear.Location = new System.Drawing.Point(0, 1236);
            btnLogClear.Margin = new System.Windows.Forms.Padding(4);
            btnLogClear.Name = "btnLogClear";
            btnLogClear.Size = new System.Drawing.Size(1365, 65);
            btnLogClear.TabIndex = 0;
            btnLogClear.Text = "Log Clear";
            btnLogClear.UseVisualStyleBackColor = true;
            btnLogClear.Click += btnLogClear_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(6);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1365, 221);
            panel2.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.Controls.Add(checkQueryRecent);
            panel4.Controls.Add(checkSpecifyDate);
            panel4.Controls.Add(queryTo);
            panel4.Controls.Add(queryForm);
            panel4.Controls.Add(selectionKind);
            panel4.Controls.Add(btnCrawler);
            panel4.Controls.Add(timeout);
            panel4.Controls.Add(selectionQueryRange);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(label1);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(0, 60);
            panel4.Margin = new System.Windows.Forms.Padding(6);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(1365, 161);
            panel4.TabIndex = 6;
            // 
            // checkQueryRecent
            // 
            checkQueryRecent.AutoSize = true;
            checkQueryRecent.Checked = true;
            checkQueryRecent.CheckState = System.Windows.Forms.CheckState.Checked;
            checkQueryRecent.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkQueryRecent.Location = new System.Drawing.Point(33, 80);
            checkQueryRecent.Margin = new System.Windows.Forms.Padding(4);
            checkQueryRecent.Name = "checkQueryRecent";
            checkQueryRecent.Size = new System.Drawing.Size(154, 29);
            checkQueryRecent.TabIndex = 8;
            checkQueryRecent.Text = "查詢最近公告";
            checkQueryRecent.UseVisualStyleBackColor = true;
            checkQueryRecent.CheckedChanged += checkbox_CheckedChanged;
            // 
            // checkSpecifyDate
            // 
            checkSpecifyDate.AutoSize = true;
            checkSpecifyDate.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkSpecifyDate.Location = new System.Drawing.Point(641, 84);
            checkSpecifyDate.Margin = new System.Windows.Forms.Padding(4);
            checkSpecifyDate.Name = "checkSpecifyDate";
            checkSpecifyDate.Size = new System.Drawing.Size(114, 29);
            checkSpecifyDate.TabIndex = 7;
            checkSpecifyDate.Text = "指定日期";
            checkSpecifyDate.UseVisualStyleBackColor = true;
            checkSpecifyDate.CheckedChanged += checkbox_CheckedChanged;
            // 
            // queryTo
            // 
            queryTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            queryTo.Location = new System.Drawing.Point(1050, 83);
            queryTo.Margin = new System.Windows.Forms.Padding(4);
            queryTo.MaxDate = new DateTime(3000, 12, 31, 0, 0, 0, 0);
            queryTo.MinDate = new DateTime(1940, 2, 1, 0, 0, 0, 0);
            queryTo.Name = "queryTo";
            queryTo.Size = new System.Drawing.Size(183, 35);
            queryTo.TabIndex = 6;
            // 
            // queryForm
            // 
            queryForm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            queryForm.Location = new System.Drawing.Point(815, 83);
            queryForm.Margin = new System.Windows.Forms.Padding(4);
            queryForm.MaxDate = new DateTime(3000, 12, 31, 0, 0, 0, 0);
            queryForm.MinDate = new DateTime(1940, 2, 1, 0, 0, 0, 0);
            queryForm.Name = "queryForm";
            queryForm.Size = new System.Drawing.Size(183, 35);
            queryForm.TabIndex = 6;
            queryForm.Value = new DateTime(2025, 5, 8, 0, 0, 0, 0);
            // 
            // selectionKind
            // 
            selectionKind.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            selectionKind.FormattingEnabled = true;
            selectionKind.Items.AddRange(new object[] { "取得或處分資產公告", "取得或處分私募有價證券公告", "依證交法第43條之1第1項取得股份公告", "採候選人提名制選任董監事及股東提案權相關公告(公開發行公司)", "採候選人提名制選任董監事相關公告（上市櫃及興櫃公司）", "資金貸與公告", "背書保證公告", "因股份轉換、合併、收購、受讓他公司股份或分割而發行新股後，主辦證券承銷商之評估意見", "外國人應辦理公告申報事項", "發行新股、公司債暨有價證券交付或發放股利前辦理之公告(公司法第252及273條)", "股票或公司債核准上市（櫃）或終止上市（櫃）之公告", "因員工認股權轉換而發行新股公告", "因轉換公司債、附認股權公司債轉換而發行新股公告", "海外公司債公告", "分離後認股權憑證之公告", "董事會決議買回股份作為員工認股權憑證履約之公告事項", "董事會決議變更發行及認股辦法之公告事項", "財務報告無虛偽或隱匿之聲明書公告", "內控聲明書公告", "內部控制專案審查報告", "變更會計師公告查詢", "會計主管不符資格條件調整職務公告", "赴大陸投資資訊實際數與自結數差異公告", "投資海外子公司資訊實際數與自結數差異公告", "公開發行股票全面轉換無實體發行公告", "召開股東常（臨時）會(公開發行及94.5.5前之全體公司)", "召開股東常(臨時)會及受益人大會(94.5.5後之上市櫃/興櫃公司)", "決定分派股息及紅利或其他利益(公開發行及94.5.5前之全體公司)", "決定分配股息及紅利或其他利益(94.5.5後之上市櫃/興櫃公司)", "董事會議決事項未經審計委員會通過或獨立董事有反對或保留意見", "依發行辦法約定收回(買)已發行之限制員工權利新股之公告", "會計變動公告", "其他依公司法規定公告" });
            selectionKind.Location = new System.Drawing.Point(675, 10);
            selectionKind.Margin = new System.Windows.Forms.Padding(4);
            selectionKind.Name = "selectionKind";
            selectionKind.Size = new System.Drawing.Size(676, 33);
            selectionKind.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(529, 15);
            label4.Margin = new System.Windows.Forms.Padding(92, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(92, 25);
            label4.TabIndex = 4;
            label4.Text = "公告種類";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(1004, 84);
            label5.Margin = new System.Windows.Forms.Padding(92, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(27, 25);
            label5.TabIndex = 4;
            label5.Text = "~";
            // 
            // panel3
            // 
            panel3.Controls.Add(lblStatus);
            panel3.Controls.Add(lbl1);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(6);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(1365, 60);
            panel3.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblStatus.Location = new System.Drawing.Point(456, 0);
            lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(909, 60);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "--";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl1
            // 
            lbl1.Dock = System.Windows.Forms.DockStyle.Left;
            lbl1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lbl1.Location = new System.Drawing.Point(0, 0);
            lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new System.Drawing.Size(456, 60);
            lbl1.TabIndex = 0;
            lbl1.Text = "手動抓取公告之設定區";
            lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectionEventList
            // 
            selectionEventList.Dock = System.Windows.Forms.DockStyle.Fill;
            selectionEventList.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            selectionEventList.FormattingEnabled = true;
            selectionEventList.Items.AddRange(new object[] { "取得或處分資產公告", "取得或處分私募有價證券公告", "依證交法第43條之1第1項取得股份公告", "採候選人提名制選任董監事及股東提案權相關公告(公開發行公司)", "採候選人提名制選任董監事相關公告（上市櫃及興櫃公司）", "資金貸與公告", "背書保證公告", "因股份轉換、合併、收購、受讓他公司股份或分割而發行新股後，主辦證券承銷商之評估意見", "外國人應辦理公告申報事項", "發行新股、公司債暨有價證券交付或發放股利前辦理之公告(公司法第252及273條)", "股票或公司債核准上市（櫃）或終止上市（櫃）之公告", "因員工認股權轉換而發行新股公告", "因轉換公司債、附認股權公司債轉換而發行新股公告", "海外公司債公告", "分離後認股權憑證之公告", "董事會決議買回股份作為員工認股權憑證履約之公告事項", "董事會決議變更發行及認股辦法之公告事項", "財務報告無虛偽或隱匿之聲明書公告", "內控聲明書公告", "內部控制專案審查報告", "變更會計師公告查詢", "會計主管不符資格條件調整職務公告", "赴大陸投資資訊實際數與自結數差異公告", "投資海外子公司資訊實際數與自結數差異公告", "公開發行股票全面轉換無實體發行公告", "召開股東常（臨時）會(公開發行及94.5.5前之全體公司)", "召開股東常(臨時)會及受益人大會(94.5.5後之上市櫃/興櫃公司)", "決定分派股息及紅利或其他利益(公開發行及94.5.5前之全體公司)", "決定分配股息及紅利或其他利益(94.5.5後之上市櫃/興櫃公司)", "董事會議決事項未經審計委員會通過或獨立董事有反對或保留意見", "依發行辦法約定收回(買)已發行之限制員工權利新股之公告", "會計變動公告", "其他依公司法規定公告" });
            selectionEventList.Location = new System.Drawing.Point(0, 0);
            selectionEventList.Margin = new System.Windows.Forms.Padding(4);
            selectionEventList.Name = "selectionEventList";
            selectionEventList.Size = new System.Drawing.Size(1199, 1522);
            selectionEventList.TabIndex = 8;
            selectionEventList.SelectedValueChanged += selectionEventList_SelectedValueChanged;
            // 
            // panel5
            // 
            panel5.Controls.Add(groupBox1);
            panel5.Controls.Add(selectionEventList);
            panel5.Dock = System.Windows.Forms.DockStyle.Left;
            panel5.Location = new System.Drawing.Point(0, 0);
            panel5.Margin = new System.Windows.Forms.Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(1199, 1522);
            panel5.TabIndex = 9;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkTelegramSendToWho);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            groupBox1.Location = new System.Drawing.Point(0, 1338);
            groupBox1.Margin = new System.Windows.Forms.Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4);
            groupBox1.Size = new System.Drawing.Size(1199, 184);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "要發送的群組";
            // 
            // checkTelegramSendToWho
            // 
            checkTelegramSendToWho.ColumnWidth = 200;
            checkTelegramSendToWho.Dock = System.Windows.Forms.DockStyle.Fill;
            checkTelegramSendToWho.FormattingEnabled = true;
            checkTelegramSendToWho.Location = new System.Drawing.Point(4, 30);
            checkTelegramSendToWho.Margin = new System.Windows.Forms.Padding(4);
            checkTelegramSendToWho.MultiColumn = true;
            checkTelegramSendToWho.Name = "checkTelegramSendToWho";
            checkTelegramSendToWho.Size = new System.Drawing.Size(1191, 150);
            checkTelegramSendToWho.TabIndex = 9;
            checkTelegramSendToWho.SelectedIndexChanged += checkTelegramSendToWho_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(2564, 1522);
            Controls.Add(panel1);
            Controls.Add(panel5);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(6);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SMI (目前僅支援取得或處分資產公告, 取得或處分私募有價證券公告, 依證交法第43條之1第1項取得股份公告)";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)timeout).EndInit();
            panel1.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCrawler;
        private System.Windows.Forms.ComboBox selectionQueryRange;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckedListBox selectionEventList;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox selectionKind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker queryForm;
        private System.Windows.Forms.CheckBox checkSpecifyDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker queryTo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox checkQueryRecent;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnLogClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkTelegramSendToWho;
    }
}

