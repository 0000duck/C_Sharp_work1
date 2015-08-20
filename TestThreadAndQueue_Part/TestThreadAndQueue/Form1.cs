//-------------------------------------------------------------------------
//	Thread に Event通知し、内容をキューを使って伝えるサンプル
//	
//		[Start Thread]ボタン(Init)：Event、Queue、Thread生成
//		[msg 5]ボタン(button1)：Thread に "cmd5"通知
//		[msg 6]ボタン(button3)：Thread に "cmd6"通知
//		[End Thred and Exit]ボタン(button2)：Thread に "End"通知、本アプリ終了開始
//
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;     // Thread

namespace TestThreadAndQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //----- member -----
        //  Threadでキュー読み出し(Count確認中に)別で、Setしてずれないように
        //  Semaphoreを使用しています。

        Thread myThread;
        ThreadStart myThreadDelegate;
        MyEventQueue myEvQ;             // 独自EventQueueクラス

        //----- method -----
        // Thread関数
        void ThreadFunction() 
        {
            string wkstr = string.Empty;

            while(true) {
                // EventQueue待ち
                wkstr = myEvQ.GetEvent();               

                // キュー内容確認
                if( string.Compare(wkstr, "cmd5")==0 ) {
                    MessageBox.Show("Recv cmd5.", "msg", 0);
                }
                else if( string.Compare(wkstr, "cmd6")==0 ) {
                    MessageBox.Show("Recv cmd6.", "msg", 0);
                }
                else {
                    MessageBox.Show("Recv ThreadEnd", "msg", 0);
                    break;      // スレッド抜ける
                }
            }
        }

        // Thread開始ボタン
        private void Init_Click(object sender, EventArgs e)
        {
            Init.Enabled = false;                   // 自ボタン禁止
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

            // EventQueue生成
            myEvQ = new MyEventQueue();
            myEvQ.InitProc();

            // スレッド起動
            myThreadDelegate = new ThreadStart(ThreadFunction);
            myThread = new Thread(myThreadDelegate);
            myThread.Start();

           
        }

        // Thread へ "cmd5"送信ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            // スレッドへ "cmd5"送信
            string cmd_str = "cmd5";
            myEvQ.SetEvent(cmd_str);
        }


        // Thread へ "cmd6"送信ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            // スレッドへ "cmd6"送信
            string cmd_str = "cmd6";
            myEvQ.SetEvent(cmd_str);
           
        }

        // Thread へ "End"送信ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            // スレッドへ "End"送信
            string cmd_str = "End";
            myEvQ.SetEvent(cmd_str);

            Close();                                // 本アプリ終了開始
        }

        // 起動時処理
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        // 終了処理
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread.Join(5000);    // スレッド終了待ち(5秒timeout付) 
            myEvQ.EndProc();        // EventQueue解放
        }
    }

}

