//---------------------------------------------------------------------
//  Monitor Sample
//
//      Queue を Monitor を使用して排他制御をサンプルで作成。
//      [memo]
//      スレッド起動時、10s sleepするので、
//      その間に button2 を押すと、20個コマンド送信できるので
//      Queueサイズ10としているので、Monitorで待たせるのを確認できます
//      
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;     // Add
using System.Collections;   // Queue

namespace TestMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void void_string_delegate(string str);
        bool closingFlag = false;
        Thread myThread;
        ThreadStart myThreadDelegate;
        Queue myQ;
        Object lockQ;

        const int QUEUE_MAX = 11;

        /**
          * @brief   ThreadFunction
          * @param[in]  void
          * @return     void 
          * @note       スレッド処理。 10秒おきに キュー確認
          */
        void ThreadFunction()
        {
            Thread.Sleep(10000);    // 先にQueue積まれるの確認するために　10s寝る

            string msgStr = string.Empty;
            while (true)
            {
                msgStr = GetReq();
                if (string.Compare(msgStr, "END") == 0)
                    break;
                else
                    Console.WriteLine(msgStr);
            }
        }

        /**
          * @brief   SetReq
          * @param[in]  string	msgStr	キューに積む文字列
          * @return     void 
          * @note       msgStr キューに積む
          */
        void SetReq(string msgStr)
        {
            Monitor.Enter(lockQ);
            Console.WriteLine("SetReq Enter.\n");
            try
            {
                while (myQ.Count >= 10)
                {    // Queue full then
                    Console.WriteLine("SetReq Wait.\n");
                    Monitor.Wait(lockQ);    // lockを解放し、現在のスレッドがlockを再取得するまでそのスレッドをBlock
                }

                myQ.Enqueue(msgStr);        // Queueに要求積む
                Console.WriteLine("SetReq PulseAll.\n");
                Monitor.PulseAll(lockQ);    // Objectの状態が変更されたことを、待機中のすべてのスレッドに通知
            }
            catch { }
            finally
            {
                Console.WriteLine("SetReq Exit.\n");
                Monitor.Exit(lockQ);        // unlock
            }
        }

        /**
          * @brief   GetReq
          * @param[in]  void
          * @return     string	msgStr	キューから取り出した文字列
          * @note       キュー から msgStr 取り出す
          */
        string GetReq()
        {
            string msgStr = string.Empty;

            Monitor.Enter(lockQ);           // lock
            Console.WriteLine("GetReq Enter.\n");
            try
            {
                while (myQ.Count == 0)
                {      // Queue空なら lock解放
                    Console.WriteLine("GetReq Wait.\n");
                    Monitor.Wait(lockQ);
                }

                msgStr = (string)myQ.Dequeue(); // Queueから読み出し
                //AddTextBox1(msgStr + "\n");
                //Console.WriteLine("rcv:" + msgStr + "\n");
                Console.WriteLine("GetReq PulseAll.\n");
                Monitor.PulseAll(lockQ);
            }
            catch { }
            finally 
            {
                Console.WriteLine("GetReq Exit.\n");
                Monitor.Exit(lockQ);        // unlcok
            }
            return msgStr;
        }
 
        /**
          * @brief   Form1_Load
          * @param[in]  object     e
          * @param[in]  EventArgs  e 
          * @return     void 
          * @note       スレッドと キュー生成
          */
        private void Form1_Load(object sender, EventArgs e)
        {
            // Queue生成
            myQ = new Queue(QUEUE_MAX);

            // Monitor用lock Object生成
            lockQ = new Object();

            // スレッド起動
            myThreadDelegate = new ThreadStart(ThreadFunction);
            myThread = new Thread(myThreadDelegate);
            myThread.Start();
        }

        /**
          * @brief   button1_Click
          * @param[in]  object     e
          * @param[in]  EventArgs  e 
          * @return     void 
          * @note       "CMD1"をスレッドに送信
          */
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("CMD1");
            SetReq("CMD1");
        }

        /**
          * @brief   button2_Click
          * @param[in]  object     e
          * @param[in]  EventArgs  e 
          * @return     void 
          * @note       "CMD2"をスレッドに20回送信
          */
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                textBox1.AppendText(i.ToString() + ":CMD2\n");
                SetReq("CMD2");
            }
        }

        /**
          * @brief   button3_Click
          * @param[in]  object     e
          * @param[in]  EventArgs  e 
          * @return     void 
          * @note       "END"をスレッドに送信
          */
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            SetReq("END");              // 終了を要求
            closingFlag = true;
            Close();
        }

        /**
         *  @brief  textBox2 Text追加
         *  @param[in]   string  value  textBox2への追加文字列
         *  @return     void
         */
        void AddTextBox1(string value)
        {
            if (closingFlag == false)   // Closing前のみアクセス許可
            {
                if (InvokeRequired)
                {
                    Invoke(new void_string_delegate(AddTextBox1), new object[] { value });
                }
                else
                {
                    textBox1.AppendText(value);
                }
            }
        }

    }
}
