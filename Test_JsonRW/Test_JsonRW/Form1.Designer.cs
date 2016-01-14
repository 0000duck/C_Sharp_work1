namespace Test_JsonRW
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Rd = new System.Windows.Forms.Button();
            this.TxtBox_RdData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBox_ID = new System.Windows.Forms.TextBox();
            this.TxtBox_NAME = new System.Windows.Forms.TextBox();
            this.TxtBox_TYPE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Btn_Wt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Rd
            // 
            this.Btn_Rd.Location = new System.Drawing.Point(12, 12);
            this.Btn_Rd.Name = "Btn_Rd";
            this.Btn_Rd.Size = new System.Drawing.Size(108, 31);
            this.Btn_Rd.TabIndex = 0;
            this.Btn_Rd.Text = "Read Json File";
            this.Btn_Rd.UseVisualStyleBackColor = true;
            this.Btn_Rd.Click += new System.EventHandler(this.Btn_Rd_Click);
            // 
            // TxtBox_RdData
            // 
            this.TxtBox_RdData.BackColor = System.Drawing.Color.Gainsboro;
            this.TxtBox_RdData.Location = new System.Drawing.Point(12, 70);
            this.TxtBox_RdData.Multiline = true;
            this.TxtBox_RdData.Name = "TxtBox_RdData";
            this.TxtBox_RdData.ReadOnly = true;
            this.TxtBox_RdData.Size = new System.Drawing.Size(386, 45);
            this.TxtBox_RdData.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Read Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID";
            // 
            // TxtBox_ID
            // 
            this.TxtBox_ID.Location = new System.Drawing.Point(55, 175);
            this.TxtBox_ID.Name = "TxtBox_ID";
            this.TxtBox_ID.Size = new System.Drawing.Size(100, 19);
            this.TxtBox_ID.TabIndex = 4;
            // 
            // TxtBox_NAME
            // 
            this.TxtBox_NAME.Location = new System.Drawing.Point(55, 200);
            this.TxtBox_NAME.Name = "TxtBox_NAME";
            this.TxtBox_NAME.Size = new System.Drawing.Size(100, 19);
            this.TxtBox_NAME.TabIndex = 5;
            // 
            // TxtBox_TYPE
            // 
            this.TxtBox_TYPE.Location = new System.Drawing.Point(55, 225);
            this.TxtBox_TYPE.Name = "TxtBox_TYPE";
            this.TxtBox_TYPE.Size = new System.Drawing.Size(100, 19);
            this.TxtBox_TYPE.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "NAME";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "TYPE";
            // 
            // Btn_Wt
            // 
            this.Btn_Wt.Location = new System.Drawing.Point(12, 138);
            this.Btn_Wt.Name = "Btn_Wt";
            this.Btn_Wt.Size = new System.Drawing.Size(108, 31);
            this.Btn_Wt.TabIndex = 9;
            this.Btn_Wt.Text = "Write Json File";
            this.Btn_Wt.UseVisualStyleBackColor = true;
            this.Btn_Wt.Click += new System.EventHandler(this.Btn_Wt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 309);
            this.Controls.Add(this.Btn_Wt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtBox_TYPE);
            this.Controls.Add(this.TxtBox_NAME);
            this.Controls.Add(this.TxtBox_ID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBox_RdData);
            this.Controls.Add(this.Btn_Rd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Rd;
        private System.Windows.Forms.TextBox TxtBox_RdData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBox_ID;
        private System.Windows.Forms.TextBox TxtBox_NAME;
        private System.Windows.Forms.TextBox TxtBox_TYPE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Btn_Wt;
    }
}

