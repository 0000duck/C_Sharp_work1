using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestWpfMvvmModelTask
{
    class MainModel
    {
        MainViewModel mainVM;

        string currentState;
        const string IDLE = "IDLE";
        const string EXE1 = "EXE1";
        const string EXE2 = "EXE2";
        const string END  = "END";

        MyEventQueue myEvQ;
        Timer OneShotOneSecTimer;


        // コンストラクタ
        public MainModel(MainViewModel mvm)
        {
            mainVM = mvm;
            ChangeState(IDLE);

            myEvQ = new MyEventQueue();
            myEvQ.InitProc();

            mvm.CurrenState = "Init";

            CenterTask();
        }

        // Model中心処理タスク
        private async void CenterTask()
        {
            string cmdStr = string.Empty;
            await Task.Run( () => {
                while(true)
                {
                    cmdStr = myEvQ.GetEvent();

                    // Idle 状態
                    if (currentState==IDLE)
                    {
                        if (cmdStr == "StartCmd")
                        {
                            ChangeState(EXE1);
                            System.Threading.Thread.Sleep(2000);    // わざと2Sec Sleep
                            myEvQ.SetEvent("ReqEXE2Cmd");
                        }
                        else if(cmdStr== "AbortCmd")
                        {
                            ChangeState(IDLE);
                        }
                        else if (cmdStr == "EndCmd")
                        {
                            ChangeState(END);
                            break;
                        }
                    }
                    
                    // EXE1状態
                    else if(currentState==EXE1)
                    {
                        if(cmdStr=="ReqEXE2Cmd")
                        {
                            ChangeState(EXE2);

                            // 5秒 OneShotTimer 開始
                            OneShotOneSecTimer = new Timer(5000);
                            OneShotOneSecTimer.Elapsed += timeoutProc;
                            OneShotOneSecTimer.AutoReset = false;    //  Event1回のみ発生
                            OneShotOneSecTimer.Start();

                        }
                        else if (cmdStr == "AbortCmd")
                        {
                            ChangeState(IDLE);
                        }
                        else if (cmdStr == "EndCmd")
                        {
                            ChangeState(END);
                            break;
                        }
                    }
                    
                    // EXE2状態
                    else if(currentState==EXE2)
                    {
                        if (cmdStr== "SeqEndCmd")
                        {
                            ChangeState(IDLE);
                        }
                        else if (cmdStr == "AbortCmd")
                        {
                            OneShotOneSecTimer.Stop();
                            ChangeState(IDLE);
                        }
                        else if (cmdStr == "EndCmd")
                        {
                            ChangeState(END);
                            break;
                        }
                    }
                }

                myEvQ.EndProc();
                Console.WriteLine("Exit CenterTask.");
            });
        }


       // Timer timeout 処理(callback)
        void timeoutProc(object sender, ElapsedEventArgs e)
        {
            OneShotOneSecTimer.Stop();
            myEvQ.SetEvent("SeqEndCmd");
            Console.WriteLine("Timer timeout.");
        }

        // 状態遷移処理
        void ChangeState(string l_stsStr)
        {
            currentState = l_stsStr;
            mainVM.CurrenState = l_stsStr;
        }

        // ViewModel 公開用 コマンドメソッド
        public void StartCommand() { myEvQ.SetEvent("StartCmd"); }
        public void AbortCommand() { myEvQ.SetEvent("AbortCmd"); }
        public void EndCommand()   { myEvQ.SetEvent("EndCmd"); }
    }
}
