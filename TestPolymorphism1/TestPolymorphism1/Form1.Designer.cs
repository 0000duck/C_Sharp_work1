namespace TestPolymorphism1
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
            this.TxtBox_Mecha = new System.Windows.Forms.TextBox();
            this.Btn_ExecMecha = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtBox_Mecha
            // 
            this.TxtBox_Mecha.Location = new System.Drawing.Point(12, 62);
            this.TxtBox_Mecha.Name = "TxtBox_Mecha";
            this.TxtBox_Mecha.Size = new System.Drawing.Size(112, 19);
            this.TxtBox_Mecha.TabIndex = 3;
            // 
            // Btn_ExecMecha
            // 
            this.Btn_ExecMecha.Location = new System.Drawing.Point(12, 12);
            this.Btn_ExecMecha.Name = "Btn_ExecMecha";
            this.Btn_ExecMecha.Size = new System.Drawing.Size(112, 32);
            this.Btn_ExecMecha.TabIndex = 2;
            this.Btn_ExecMecha.Text = "Execute mecha";
            this.Btn_ExecMecha.UseVisualStyleBackColor = true;
            this.Btn_ExecMecha.Click += new System.EventHandler(this.Btn_ExecMecha_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 106);
            this.Controls.Add(this.TxtBox_Mecha);
            this.Controls.Add(this.Btn_ExecMecha);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBox_Mecha;
        private System.Windows.Forms.Button Btn_ExecMecha;
    }
}

