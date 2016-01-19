namespace TestMakeExcelFile
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
            this.Btn_MkExcel = new System.Windows.Forms.Button();
            this.Btn_RdExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_MkExcel
            // 
            this.Btn_MkExcel.Location = new System.Drawing.Point(12, 24);
            this.Btn_MkExcel.Name = "Btn_MkExcel";
            this.Btn_MkExcel.Size = new System.Drawing.Size(125, 23);
            this.Btn_MkExcel.TabIndex = 0;
            this.Btn_MkExcel.Text = "Make Excel File";
            this.Btn_MkExcel.UseVisualStyleBackColor = true;
            this.Btn_MkExcel.Click += new System.EventHandler(this.Btn_MkExcel_Click);
            // 
            // Btn_RdExcel
            // 
            this.Btn_RdExcel.Location = new System.Drawing.Point(12, 66);
            this.Btn_RdExcel.Name = "Btn_RdExcel";
            this.Btn_RdExcel.Size = new System.Drawing.Size(125, 23);
            this.Btn_RdExcel.TabIndex = 1;
            this.Btn_RdExcel.Text = "Read Excel File";
            this.Btn_RdExcel.UseVisualStyleBackColor = true;
            this.Btn_RdExcel.Click += new System.EventHandler(this.Btn_RdExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 146);
            this.Controls.Add(this.Btn_RdExcel);
            this.Controls.Add(this.Btn_MkExcel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_MkExcel;
        private System.Windows.Forms.Button Btn_RdExcel;
    }
}

