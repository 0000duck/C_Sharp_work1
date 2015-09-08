//-------------------------------------------------------------------
//  MyLog Sample Project
//
//      FileStreamとStreamWriter を使用した 独自LogFile作成クラスSample。
//      
//     出力Format"YYYY/MM/DD hh:mm:ss:nnn > msg文\n"
//     例："2015/09/08 15:33:46:316 > data count = 1\n" 
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestLogFile
{
    public partial class Form1 : Form
    {
        MyLog mylog;
        int count;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string log_path = "C:\\\\TEMP\\mylog.log";
            count = 0;
            mylog = new MyLog();
            mylog.Open(log_path);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mylog.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "data count = ";
            count++;
            msg = string.Concat(msg, count.ToString());
            mylog.OutLog(msg);
        }
    }
}
