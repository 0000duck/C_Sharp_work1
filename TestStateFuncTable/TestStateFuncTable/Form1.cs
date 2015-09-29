//-----------------------------------------------------------------------
//  状態遷移処理 関数Table版 SampleProject
//
//  あまり使用しないかもしれないが、C#で関数Tableを実現するには
//  どのように記述するか試した SampleProject です。
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

namespace TestStateFuncTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Thread用
        Thread myThread;
        ThreadStart myThreadDelegate;
        MyEventQueue myEvQ;             // 独自EventQueueクラス

        int  timer1Count;               // 500msecごとにカウント
        bool closingFlag;               // true:Closing開始

        // States
        int currentState;               // 状態保持変数
        const int NONE_STATE = 0;
        const int IDLE_STATE = 1;
        const int EXE1_STATE = 2;
        const int EXE2_STATE = 3;
        const int END_STATE  = 4;
        Dictionary<int, string> stateStrings = new Dictionary<int, string>  // 状態の種類
        {
            {0, "STATE_NONE"},
            {1, "STATE_IDLE"},
            {2, "STATE_EXEC1"},
            {3, "STATE_EXEC2"},
            {4, "STATE_END"}
        };

        // Evvents
        const string NONE_EVENT = "NONE_EVENT";
        const string BTN1_EVENT = "BTN1_EVENT";
        const string BTN2_EVENT = "BTN2_EVENT";
        const string ABT_EVENT  = "ABT_EVENT";
        const string TOUT_EVENT = "TOUT_EVENT";
        const string END_EVENT  = "END_EVENT";


        // delegate宣言
        delegate void void_bool_delegate(bool value);       // return void, 引数 bool型用delegate
        delegate void void_string_delegate(string value);   // return void, 引数 string型用delegate

        /**
         *  @breif  textBox1 Text設定
         *  @param[in]   string  value  textBox1への設定文字列
         *  @return     void
         */
        void SetTextBox1(string value) {
            if (InvokeRequired)
            {
                Invoke(new void_string_delegate(SetTextBox1), new object[] { value });
            }
            else {
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
        void SetTimer1EnbOrDsb(bool value)
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
         *  @brief  現在状態の設定
         *  @param[in]  int  state  設定状態値
         *  @return     void
         */
        void ChangeState(int state)
        {
            // 現在状態変更
            currentState = state;
            SetTextBox1(stateStrings[state]);

            // Logger(textBox2)へ情報出力
            string txtBox2Text = "ChangeState = " + stateStrings[currentState] + "\n";
            AddTextBox2(txtBox2Text);
        }


        /**
         *  @brief  状態遷移処理スレッド
         *  @param[in]  void
         *  @return     void
         */
        void ThreadFunction()
        {   
            bool bret = false;

            // はじめ IDLE状態にする。
            ChangeState(IDLE_STATE);


            // ★★★ 関数Table bool型引数string生成。Table参照は int ★★★
            var funcTable =
                new System.Collections.Generic.Dictionary<int, Func<string, bool>> // varによるコンパイル時型推論，Func汎用デリゲート
                { // コレクション初期化子
                    { NONE_STATE, state_none_function },
                    { IDLE_STATE, state_idle_function },
                    { EXE1_STATE, state_exe1_function },
                    { EXE2_STATE, state_exe2_function }
                };

            string txtBox2Text = string.Empty;
            string eventStr = string.Empty;

            while (true)
            {
                // EventQueue待ち
                eventStr = myEvQ.GetEvent();

                // Logger(textBox2)へ情報出力
                txtBox2Text = "CurrentState = " + stateStrings[currentState] + "\tRecvEvent = " + eventStr + "\n";
                AddTextBox2(txtBox2Text);

                // 現在Stateにあった Event処理
                bret = funcTable[currentState](eventStr);   // ★ 関数TableJump★
                if (bret == false)
                {
                    break;                                 // 状態遷移終了時、本スレッド終了
                }
            }
        }


        /**
         *  @brief  NONE 状態処理関数
         *  @param[in]  string   eventStr    Event文字列
         *  @return     bool     true:状態処理継続。  false：状態処理終了
         */
        bool state_none_function(string eventsStr)
        {
            // no change state, no processing
            return true;
        }

        /**
         *  @brief  IDLE 状態処理
         *  @param[in]  string   eventStr    Event文字列
         *  @return     bool     true:状態処理継続。  false：状態処理終了
         */
        bool state_idle_function(string eventStr)
        {
            bool bret;

            bret = true;
            if(string.Compare(eventStr, BTN1_EVENT)==0) {
                ChangeState(EXE1_STATE);
            }
            else if(string.Compare(eventStr, BTN2_EVENT)==0) {
                ChangeState(IDLE_STATE);
            }
            else if(string.Compare(eventStr, ABT_EVENT)==0) {
                ChangeState(IDLE_STATE);
            }
            else if (string.Compare(eventStr, TOUT_EVENT) == 0)
            {
                ChangeState(IDLE_STATE);
            }
            else if (string.Compare(eventStr, END_EVENT) == 0)
            {
                bret = false;
            }

            return bret;
        }

        /**
         *  @brief  EXECUTE1 状態処理
         *  @param[in]  string   eventStr    Event文字列
         *  @return     bool     true:状態処理継続。  false：状態処理終了
         */
        bool state_exe1_function(string eventStr)
        {
            bool bret;

            bret = true;
            if (string.Compare(eventStr, BTN1_EVENT) == 0)
            {
                ChangeState(EXE1_STATE);
            }
            else if (string.Compare(eventStr, BTN2_EVENT) == 0)
            {
                ChangeState(EXE2_STATE);
                SetTimer1EnbOrDsb(true);
            }
            else if (string.Compare(eventStr, ABT_EVENT) == 0)
            {
                ChangeState(IDLE_STATE);
            }
            else if (string.Compare(eventStr, TOUT_EVENT) == 0)
            {
                ChangeState(EXE1_STATE);
            }
            else if (string.Compare(eventStr, END_EVENT) == 0)
            {
                bret = false;
            }

            return bret;
        }

        /**
         *  @brief  EXECUTE2 状態処理
         *  @param[in]  string   eventStr    Event文字列
         *  @return     bool     true:状態処理継続。  false：状態処理終了
         */
        bool state_exe2_function(string eventStr)
        {
            bool bret;

            bret = true;
            if (string.Compare(eventStr, BTN1_EVENT) == 0)
            {
                ChangeState(EXE2_STATE);
            }
            else if (string.Compare(eventStr, BTN2_EVENT) == 0)
            {
                ChangeState(EXE2_STATE);
            }
            else if (string.Compare(eventStr, ABT_EVENT) == 0)
            {
                ChangeState(IDLE_STATE);
                SetTimer1EnbOrDsb(false);
            }
            else if (string.Compare(eventStr, TOUT_EVENT) == 0)
            {
                ChangeState(IDLE_STATE);
            }
            else if (string.Compare(eventStr, END_EVENT) == 0)
            {
                bret = false;
            }

            return bret;
        }

        /**
         *  @brief  起動処理
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            closingFlag = false;

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
