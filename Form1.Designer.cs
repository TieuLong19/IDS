namespace ProjectIDS
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
            this.components = new System.ComponentModel.Container();
            this.bexit = new System.Windows.Forms.Button();
            this.blogin = new System.Windows.Forms.Button();
            this.ltest = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bCompare = new System.Windows.Forms.Button();
            this.bCheck = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bHash = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.bLog = new System.Windows.Forms.Button();
            this.terror = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.bfilter = new System.Windows.Forms.Button();
            this.tfilter = new System.Windows.Forms.TextBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // bexit
            // 
            this.bexit.Location = new System.Drawing.Point(1470, 759);
            this.bexit.Name = "bexit";
            this.bexit.Size = new System.Drawing.Size(140, 69);
            this.bexit.TabIndex = 16;
            this.bexit.Text = "Thoát";
            this.bexit.UseVisualStyleBackColor = true;
            this.bexit.Click += new System.EventHandler(this.bexit_Click);
            // 
            // blogin
            // 
            this.blogin.Location = new System.Drawing.Point(588, 761);
            this.blogin.Name = "blogin";
            this.blogin.Size = new System.Drawing.Size(154, 69);
            this.blogin.TabIndex = 11;
            this.blogin.Text = "Tạo đường dẫn";
            this.blogin.UseVisualStyleBackColor = true;
            this.blogin.Click += new System.EventHandler(this.blogin_Click);
            // 
            // ltest
            // 
            this.ltest.FormattingEnabled = true;
            this.ltest.ItemHeight = 20;
            this.ltest.Location = new System.Drawing.Point(32, 315);
            this.ltest.Name = "ltest";
            this.ltest.Size = new System.Drawing.Size(874, 424);
            this.ltest.TabIndex = 20;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bCompare
            // 
            this.bCompare.Location = new System.Drawing.Point(233, 759);
            this.bCompare.Name = "bCompare";
            this.bCompare.Size = new System.Drawing.Size(166, 65);
            this.bCompare.TabIndex = 21;
            this.bCompare.Text = "So sánh nội dung bị thay đổi";
            this.bCompare.UseVisualStyleBackColor = true;
            this.bCompare.Click += new System.EventHandler(this.bCompare_Click);
            // 
            // bCheck
            // 
            this.bCheck.Location = new System.Drawing.Point(1214, 759);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(154, 69);
            this.bCheck.TabIndex = 22;
            this.bCheck.Text = "Bắt đầu chạy";
            this.bCheck.UseVisualStyleBackColor = true;
            this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(932, 315);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1097, 424);
            this.listBox1.TabIndex = 23;
            // 
            // bHash
            // 
            this.bHash.Location = new System.Drawing.Point(913, 759);
            this.bHash.Name = "bHash";
            this.bHash.Size = new System.Drawing.Size(154, 69);
            this.bHash.TabIndex = 24;
            this.bHash.Text = "Tạo database excel";
            this.bHash.UseVisualStyleBackColor = true;
            this.bHash.Click += new System.EventHandler(this.bHash_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(932, 14);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(519, 244);
            this.listBox2.TabIndex = 25;
            // 
            // bLog
            // 
            this.bLog.Location = new System.Drawing.Point(932, 264);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(154, 34);
            this.bLog.TabIndex = 26;
            this.bLog.Text = "ViewLog";
            this.bLog.UseVisualStyleBackColor = true;
            this.bLog.Click += new System.EventHandler(this.bLog_Click);
            // 
            // terror
            // 
            this.terror.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terror.Location = new System.Drawing.Point(32, 214);
            this.terror.Name = "terror";
            this.terror.Size = new System.Drawing.Size(874, 44);
            this.terror.TabIndex = 27;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // bfilter
            // 
            this.bfilter.Location = new System.Drawing.Point(1489, 267);
            this.bfilter.Name = "bfilter";
            this.bfilter.Size = new System.Drawing.Size(100, 34);
            this.bfilter.TabIndex = 28;
            this.bfilter.Text = "Filter Log";
            this.bfilter.UseVisualStyleBackColor = true;
            this.bfilter.Click += new System.EventHandler(this.bfilter_Click);
            // 
            // tfilter
            // 
            this.tfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tfilter.Location = new System.Drawing.Point(1595, 266);
            this.tfilter.Name = "tfilter";
            this.tfilter.Size = new System.Drawing.Size(425, 32);
            this.tfilter.TabIndex = 29;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 20;
            this.listBox3.Location = new System.Drawing.Point(1489, 14);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(531, 244);
            this.listBox3.TabIndex = 30;
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2032, 867);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.tfilter);
            this.Controls.Add(this.bfilter);
            this.Controls.Add(this.terror);
            this.Controls.Add(this.bLog);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.bHash);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.bCheck);
            this.Controls.Add(this.bCompare);
            this.Controls.Add(this.ltest);
            this.Controls.Add(this.bexit);
            this.Controls.Add(this.blogin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bexit;
        private System.Windows.Forms.Button blogin;
        private System.Windows.Forms.ListBox ltest;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button bCompare;
        private System.Windows.Forms.Button bCheck;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button bHash;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.TextBox terror;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button bfilter;
        private System.Windows.Forms.TextBox tfilter;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Timer timer4;
    }
}

