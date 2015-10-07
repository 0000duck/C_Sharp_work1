//-----------------------------------------------------------------------
//  状態遷移処理 StatePattern版 SampleProject
//
//  StatePattern SampleProject です。
//  スレッドで、Eventを受けるたびに状態にあった処理関数
//  state_idle_function/state_exe1_function/state_exe2_function を実行します。
//  IDLE状態で、button1が押されると EXE1へ遷移、EXE1で button2が押されると
//  timer1をスタートしてEXE2へ遷移。EXE2で timeoutすると IDLEへ遷移します。
//  全状態、Abort を押されると、IDLEへ遷移します。
//  全状態、×がおされると終了処理します。
//  Eventは、以前Sampleで作成した、独自MyEventQueueクラスを使用しています。
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;     // Thread

namespace TestStatePattern
{
    public partial class Form1 : Form, IContext
    {
        /**
        * @brief   Form1 Class
        * @note    StatePattern サンプルProject Form
        */
        public Form1()
        {
            InitializeComponent();
        }

        // Thread用
        Thread myThread;
        ThreadStart myThreadDelegate;
        MyEventQueue myEvQ;             // 独自EventQueueクラス

        int timer1Count;                // 500msecごとにカウント
        bool closingFlag;               // true:Closing開始

        // States
        IState currentStateCls;         // 状態保持変数

        /**
         *  @brief  Eventの設定
         *  @param[in]  string  eventStr    Eventの種類
         *  @return     void
         */
        public void SetMainEvent(string eventStr)
        {
            if (string.Compare(eventStr, "NO_EVENT") != 0)
            {
                myEvQ.SetEvent(eventStr);
            }
        }

        /**
         *  @brief  現在状態の設定
         *  @param[in]  int  state  設定状態値
         *  @return     void
         */
        public void ChangeState(IState state)
        {
            // 現在状態変更
            currentStateCls = state;
            SetTextBox1(currentStateCls.getStateName());

            // Logger(textBox2)へ情報出力
            string txtBox2Text = "ChangeState = " + currentStateCls.getStateName() + "\n";
            AddTextBox2(txtBox2Text);
        }


        /**
         *  @brief  状態遷移処理スレッド
         *  @param[in]  void
         *  @return     void
         */
        void ThreadFunction()
        {
            string txtBox2Text = string.Empty;
            string eventStr = string.Empty;

            while (true)
            {
                // EventQueue待ち
                eventStr = myEvQ.GetEvent();

                // Logger(textBox2)へ情報出力
                txtBox2Text = "CurrentState = " + currentStateCls.getStateName() + "\tRecvEvent = " + eventStr + "\n";
                AddTextBox2(txtBox2Text);


                // Event処理
                if (string.Compare(eventStr, "BTN1_EVENT") == 0)
                {
                    currentStateCls.btn1EventProc(this);
                }
                else if (string.Compare(eventStr, "BTN2_EVENT") == 0)
                {
                    currentStateCls.btn2EventProc(this);
                }
                else if (string.Compare(eventStr, "TOUT_EVENT") == 0)
                {
                    currentStateCls.ToutEventProc(this);
                }
                else if (string.Compare(eventStr, "ABT_EVENT") == 0)
                {
                    currentStateCls.AbtEventProc(this);
                }
                else if (string.Compare(eventStr, "END_EVENT") == 0)
                {
                    currentStateCls.EndEventProc(this);
                    break;                              // スレッド終了
                }
                else
                {
                    ;// none
                }
               
            }
        }


        // delegate宣言
        delegate void void_bool_delegate(bool value);       // return void, 引数 bool型用delegate
        delegate void void_string_delegate(string value);   // return void, 引数 string型用delegate

        /**
         *  @breif  textBox1 Text設定
         *  @param[in]   string  value  textBox1への設定文字列
         *  @return     void
         */
        void SetTextBox1(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new void_string_delegate(SetTextBox1), new object[] { value });
            }
            else
            {
                textBox1.Text = value;
            }
        }

        /**
         *  @brief  textBox2 Text追加
         *  @param[in]   string  value  textBox2への追加文字列
         *  @return     void
         */
        void AddTextBox2(string value)
        {
            if (closingFlag == false)   // Closing前のみアクセス許可
            {
                if (InvokeRequired)
                {
                    Invoke(new void_string_delegate(AddTextBox2), new object[] { value });
                }
                else
                {
                    textBox2.AppendText(value);
                }
            }
        }

        /**
         *  @brief  timer1許可禁止設定
         *  @param[in]  bool  value  true/false
         *  @return     void
         */
        public void SetTimer1EnbOrDsb(bool value)
        {
            if (closingFlag == false)       // Closing前のみアクセス許可
            {
                if (InvokeRequired)
                {
                    Invoke(new void_bool_delegate(SetTimer1EnbOrDsb), new object[] { value });
                }
                else
                {
                    if (value == true)      // 許可時、Counterクリア
                    {
                        timer1Count = 0;
                    }
                    timer1.Enabled = value;
                }
            }
        }

        /**
         *  @brief  起動処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeState(IdleState.getInstance());   // 起動時 Idle状態へ

            closingFlag = false;                    // Closing中Flagクリア

            // EventQueue生成
            myEvQ = new MyEventQueue();
            myEvQ.InitProc();

            // スレッド起動
            myThreadDelegate = new ThreadStart(ThreadFunction);
            myThread = new Thread(myThreadDelegate);
            myThread.Start();
        }

        /**
         *  @brief  終了処理開始処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetTimer1EnbOrDsb(false);
            closingFlag = true;

            // スレッドへ "End"送信
            string eventStr = "END_EVENT";
            myEvQ.SetEvent(eventStr);

            myThread.Join(5000);    // スレッド終了待ち(5秒timeout付) 
            myEvQ.EndProc();        // EventQueue解放
        }

        /**
         *  @brief  button1処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void btn_1_Click(object sender, EventArgs e)
        {
            string eventStr = "BTN1_EVENT";
            myEvQ.SetEvent(eventStr);
        }

        /**
         *  @brief  button2処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void btn_2_Click(object sender, EventArgs e)
        {
            string eventStr = "BTN2_EVENT";
            myEvQ.SetEvent(eventStr);
        }

        /**
         *  @brief  Abort button処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void btn_abort_Click(object sender, EventArgs e)
        {
            string eventStr = "ABT_EVENT";
            myEvQ.SetEvent(eventStr);
        }

        /**
         *  @brief  Timer1処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         *  @note       500msecごとに本関数callされる。timer1Count==6(約3秒)のとき
         *              Timeoutと判断。
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1Count++;
            if (timer1Count == 6)   // 約3秒たった
            {
                timer1Count = 0;
                SetTimer1EnbOrDsb(false);
                string eventStr = "TOUT_EVENT";
                myEvQ.SetEvent(eventStr);
            }
        }
    }
}
