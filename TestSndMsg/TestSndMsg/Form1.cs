//-------------------------------------------------------------------
//  SendMessage Sample Project
//
//
//  button1 : normal SendMessage wparam=1
//  button2 : normal SendMessage wparam=2
//  button3 : WM_COPYDATA SendMessage. data is string
//  button4 : WM_COPYDATA SendMessage. data is struct
//-------------------------------------------------------------------
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
    //[StructLayout(LayoutKind.Sequential)]
    //struct COPYDATASTRUCT2
    //{
    //    public IntPtr dwData;
    //    public int cbData;
    //    //[MarshalAs(UnmanagedType.LPWStr)]
    //    //public string lpData;
    //    public object lpData;
    //}

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

        // WM_COPYDATA用 SendMessage定義
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);
        public const int WM_COPYDATA = 0x004A;
        public struct COPYDATASTRUCT    // SendMessageに使用するデータ用構造体
        {
            public IntPtr dwData;
            public UInt32 cbData;
            public string lpData;
        }

        // WM_COPYDATA用 SendMessage定義 ( data is struct )
        [DllImport("USER32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT2 lParam);
        public struct COPYDATASTRUCT2
        {
            public IntPtr dwData;
            public int cbData;      // lpData の size設定
            //[MarshalAs(UnmanagedType.LPWStr)]
            //public string lpData;
            public object lpData;   // struct msgData 保持
        }
        public struct msgData   
        {
            public int no;
            public string name;
        }

        public Form1()
        {
            InitializeComponent();
        }

        // Message受信
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MY_WND)
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
            if (m.Msg == WM_COPYDATA)
            {
                if ((int)(m.WParam) == 0x0000)
                {
                    COPYDATASTRUCT st = new COPYDATASTRUCT();
                    st = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                    textBox1.Text = st.lpData;
                }
                if ((int)(m.WParam) == 0x0001)
                {
                    COPYDATASTRUCT2 st = new COPYDATASTRUCT2();
                    st = (COPYDATASTRUCT2)m.GetLParam(typeof(COPYDATASTRUCT2));

                    msgData dt = new msgData();
                    dt = (msgData)st.lpData;

                    textBox1.Text = dt.name;
                }

            }

            base.WndProc(ref m);
        }

        // SendMesage wparam = 1
        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            string wkstr = Process.GetCurrentProcess().MainWindowTitle;
            SendMessage(myh, WM_MY_WND, 0x0001, 0x0000);

        }

        // SendMessage wparam = 2
        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            SendMessage(myh, WM_MY_WND, 0x0002, 0x0000);
        }

        // SendMEssage WM_COPYDATA ( data is string )
        private void button3_Click(object sender, EventArgs e)
        {
            string str = "DataStr.";

            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            cds.dwData = IntPtr.Zero;
            cds.lpData = str;
            cds.cbData = (uint)str.Length + 1;
            
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            SendMessage(myh, WM_COPYDATA, 0, ref cds); 
        }

        // SendMEssage WM_COPYDATA ( data is struct msgData )
        private void button4_Click(object sender, EventArgs e)
        {
            // COPYDATASTRUCT2 に入れる data用構造対用意
            msgData msgdata = new msgData();
            msgdata.no = 1;
            msgdata.name = "msgData.name";
            
            // WM_COPYDATA用構造体に struct msgData を設定
            COPYDATASTRUCT2 cds = new COPYDATASTRUCT2();
            cds.dwData = IntPtr.Zero;
            cds.lpData = msgdata;
            cds.cbData = Marshal.SizeOf(msgdata);

            // Message送信
            IntPtr myh = Process.GetCurrentProcess().MainWindowHandle;
            SendMessage(myh, WM_COPYDATA, 1, ref cds); 
        }
    }
}
