//-----------------------------------------------------------------
// C# Window Message Sample
//
//  Button1 を押すと wparam=1 で Msg送信
//  Button2 を押すと wparam=2 で Msg送信
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // for DllImport
using System.Diagnostics;               // Process

namespace TestSndMsg
{
    public partial class Form1 : Form
    {
        //[DllImport("user32.dll", EntryPoint = "SendMessageA")]

        // プラットフォーム呼び出しでの文字列を　マネージ形式(Unicode)にするか
        //  ANSI形式にするか選択。Auto：プラットフォーム呼び出しで選択。
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        // SendMessage定義
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);
        public const int WM_APP_BASE = 0x8000;
        public const int WM_MY_WND = WM_APP_BASE + 1;

        public Form1()
        {
            InitializeComponent();
        }

        // Message受信
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MY_WND) // 自分で定義した Messageか判断
            {
                if ( (int)(m.WParam) == 0x0001 )
                {
                    textBox1.Text = "Pushed button1";
                }
                if ((int)(m.WParam) == 0x0002)
                {
                    textBox1.Text = "Pushed button2";
                }
                    
            }
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            string wkstr = Process.GetCurrentProcess().MainWindowTitle;
            SendMessage(myh, WM_MY_WND, 0x0001, 0x0000);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            SendMessage(myh, WM_MY_WND, 0x0002, 0x0000);
        }
    }
}
