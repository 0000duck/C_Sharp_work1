namespace Test_SyncFtpPassive
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
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_filedown = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_dwn_local = new System.Windows.Forms.TextBox();
            this.txtBox_dwn_uri = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_fileup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBox_up_local = new System.Windows.Forms.TextBox();
            this.txtBox_up_uri = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBox_name = new System.Windows.Forms.TextBox();
            this.txtBox_pass = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_filedown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBox_dwn_local);
            this.groupBox1.Controls.Add(this.txtBox_dwn_uri);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(22, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FtpDown";
            // 
            // btn_filedown
            // 
            this.btn_filedown.Location = new System.Drawing.Point(281, 77);
            this.btn_filedown.Name = "btn_filedown";
            this.btn_filedown.Size = new System.Drawing.Size(101, 23);
            this.btn_filedown.TabIndex = 6;
            this.btn_filedown.Text = "FileDownload";
            this.btn_filedown.UseVisualStyleBackColor = true;
            this.btn_filedown.Click += new System.EventHandler(this.btn_filedown_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "local";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "URI";
            // 
            // txtBox_dwn_local
            // 
            this.txtBox_dwn_local.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_dwn_local.Location = new System.Drawing.Point(66, 43);
            this.txtBox_dwn_local.Name = "txtBox_dwn_local";
            this.txtBox_dwn_local.Size = new System.Drawing.Size(316, 19);
            this.txtBox_dwn_local.TabIndex = 5;
            this.txtBox_dwn_local.Text = "C:\\test_e.exe";
            // 
            // txtBox_dwn_uri
            // 
            this.txtBox_dwn_uri.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_dwn_uri.Location = new System.Drawing.Point(66, 18);
            this.txtBox_dwn_uri.Name = "txtBox_dwn_uri";
            this.txtBox_dwn_uri.Size = new System.Drawing.Size(316, 19);
            this.txtBox_dwn_uri.TabIndex = 4;
            this.txtBox_dwn_uri.Text = "ftp://localhost/test_e.exe";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_fileup);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtBox_up_local);
            this.groupBox2.Controls.Add(this.txtBox_up_uri);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(22, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 115);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FtpUp";
            // 
            // btn_fileup
            // 
            this.btn_fileup.Location = new System.Drawing.Point(281, 77);
            this.btn_fileup.Name = "btn_fileup";
            this.btn_fileup.Size = new System.Drawing.Size(101, 23);
            this.btn_fileup.TabIndex = 10;
            this.btn_fileup.Text = "FileUpload";
            this.btn_fileup.UseVisualStyleBackColor = true;
            this.btn_fileup.Click += new System.EventHandler(this.btn_fileup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "local";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "URI";
            // 
            // txtBox_up_local
            // 
            this.txtBox_up_local.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_up_local.Location = new System.Drawing.Point(66, 43);
            this.txtBox_up_local.Name = "txtBox_up_local";
            this.txtBox_up_local.Size = new System.Drawing.Size(316, 19);
            this.txtBox_up_local.TabIndex = 9;
            this.txtBox_up_local.Text = "C:\\test_up.exe";
            // 
            // txtBox_up_uri
            // 
            this.txtBox_up_uri.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_up_uri.Location = new System.Drawing.Point(66, 18);
            this.txtBox_up_uri.Name = "txtBox_up_uri";
            this.txtBox_up_uri.Size = new System.Drawing.Size(316, 19);
            this.txtBox_up_uri.TabIndex = 8;
            this.txtBox_up_uri.Text = "ftp://localhost/test_up.exe";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(20, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(20, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Login Name";
            // 
            // txtBox_name
            // 
            this.txtBox_name.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_name.Location = new System.Drawing.Point(101, 18);
            this.txtBox_name.Name = "txtBox_name";
            this.txtBox_name.Size = new System.Drawing.Size(180, 19);
            this.txtBox_name.TabIndex = 1;
            this.txtBox_name.Text = "name";
            // 
            // txtBox_pass
            // 
            this.txtBox_pass.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_pass.Location = new System.Drawing.Point(101, 43);
            this.txtBox_pass.Name = "txtBox_pass";
            this.txtBox_pass.Size = new System.Drawing.Size(180, 19);
            this.txtBox_pass.TabIndex = 2;
            this.txtBox_pass.Text = "passwd";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 323);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBox_name);
            this.Controls.Add(this.txtBox_pass);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "OneFileUpDown Sample.";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_filedown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBox_dwn_local;
        private System.Windows.Forms.TextBox txtBox_dwn_uri;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_fileup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBox_up_local;
        private System.Windows.Forms.TextBox txtBox_up_uri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBox_name;
        private System.Windows.Forms.TextBox txtBox_pass;
    }
}

