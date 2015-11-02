//------------------------------------------------------------
//  Windowsサービスサンプル Project
//
//  OnStart で EventLog 出力し、timerスタート
//  100ms timer で約、1s カウントしたら EventLog出力
//  [memo]
//      Dos窓、installUtil で手動Installし
//      "コンピュータの管理"の"サービスとアプリケーション"の
//        "サービス"から手動で、開始/停止
//      Projectの[プロパティ]の"ビルド"の
//      プラットフォームターゲットを"Any CPU"か"x64"にした
//------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TestService1
{
    public partial class TestService1 : ServiceBase
    {
        public TestService1()
        {
            InitializeComponent();
        }

        int timerCount;
        
                
        /**
         * @brief   OnStart
         * @param[in]  string[]     args
         * @return     void 
         * @note       サービス開始時処理
         */
        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();   // Add

            // 情報ログの出力
            int eventID = 2;
            byte[] rawData = new byte[] { 1, 2, 3 };
            short category = 0;
            string infoMsg = "Test EvengLog の情報\n\\nで複数ラインのエラーメッセージを表示できます。\nまた、rawDataにより、データを表示できます。(Start)";
            this.eventLog1.WriteEntry(infoMsg,
                   System.Diagnostics.EventLogEntryType.Information, eventID, category, rawData);

            timerCount = 0;
            timer1.Enabled = true;
        }

        /**
         * @brief   OnStop
         * @param[in]  string[]     args
         * @return     void 
         * @note       サービス停止時処理
         */
        protected override void OnStop()
        {
            // 情報ログの出力
            int eventID = 2;
            byte[] rawData = new byte[] { 1, 2, 3 };
            short category = 0;
            string infoMsg = "Test EvengLog の情報\n\\nで複数ラインのエラーメッセージを表示できます。\nまた、rawDataにより、データを表示できます。(End)";
            this.eventLog1.WriteEntry(infoMsg,
                    System.Diagnostics.EventLogEntryType.Information,
                    eventID, category, rawData);
        }

        /**
         * @brief   timer1_Elapsed
         * @param[in]   object             sender
         * @param[in]   ElapsedEventArgs   e
         * @return     void 
         * @note       100msec Timer処理
         */
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int eventID = 2;                            // イベントID
            byte[] rawData = new byte[] { 1, 2, 3 };    // 詳細タグに表示するデータ
            short category = 0;

            timerCount++;
            if(timerCount>=10) {
                timerCount = 0;
                string infoMsg = "timer1 timeout. 1sec";
                this.eventLog1.WriteEntry(infoMsg,
                        System.Diagnostics.EventLogEntryType.Information,
                        eventID, category, rawData);
            }
        }
    }
}
