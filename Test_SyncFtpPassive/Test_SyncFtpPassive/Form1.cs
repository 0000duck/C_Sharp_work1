//-------------------------------------------------------------------
//  Ftp Sample Project
//
//      WebRequest, FtpWebRequest を使用した
//      独自 1ファイルFtpUp/Downloadクラスを使用したSample。
//      
//      Login、Password を入力し、FtpUp/Downloadしたい URIとlocalを
//      指定し、ボタンを押すと実行します。。
//      PassiveMode、Binary転送指定となっています。
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_SyncFtpPassive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // File Download button
        private void btn_filedown_Click(object sender, EventArgs e)
        {
            bool bret = true;
            SyncFtpPassive s_ftp_p = new SyncFtpPassive();  // 独自 Ftp処理クラス生成

            // 1ファイルDownload要求
            bret = s_ftp_p.FtpOneFileDown(txtBox_name.Text, txtBox_pass.Text, txtBox_dwn_uri.Text, txtBox_dwn_local.Text);
            if (bret == false)
            {
                MessageBox.Show(s_ftp_p.GetLastErrStr(), "err", 0);
            }
            else
            {
                MessageBox.Show("FtpDownOK.", "msg", 0);
            }
        }

        // File Upload button
        private void btn_fileup_Click(object sender, EventArgs e)
        {
            bool bret = true;
            SyncFtpPassive s_ftp_p = new SyncFtpPassive();  // 独自 Ftp処理クラス生成

            // 1ファイルUpload要求
            bret = s_ftp_p.FtpOneFileUp(txtBox_name.Text, txtBox_pass.Text, txtBox_up_uri.Text, txtBox_up_local.Text);
            if (bret == false)
            {
                MessageBox.Show(s_ftp_p.GetLastErrStr(), "err", 0);
            }
            else
            {
                MessageBox.Show("FtpUpOK.", "msg", 0);
            }
        }
    }
}
