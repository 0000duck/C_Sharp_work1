using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;   // Queue
using System.Threading;     // Semaphore, Queue

namespace TestThreadAndQueue
{
    //--------------------------------------------
    //  ManualResetEvent と Queueクラスを使用した
    //  独自EventQueueクラス
    //  使用前に InitProc()で各Object生成必要あり
    //  終了時に EndProc()で解放の処理必要あり
    //--------------------------------------------
    class MyEventQueue
    {
        public MyEventQueue()
        {
        }

        //----- member -----
        ManualResetEvent mre;           // Event通知
        Queue myQ;                      // Event内容
        Semaphore my_smph;

        //----- method -----

        // Init処理
        public void InitProc()
        {
            my_smph = new Semaphore(1, 1);          // セマフォ生成
            mre = new ManualResetEvent(false);		// Event(true=初期状態シグナル、false=初期状態非シグナル)
            myQ = new Queue();                      // キュー生成
        }

        // End処理
        public void EndProc()
        {
            mre.Close();
            myQ.Clear();
            my_smph.Close();
            
        }

        // SetEvent
        public void SetEvent(string msg) {
            my_smph.WaitOne();
                myQ.Enqueue(msg);           // キューへ追加
                mre.Set();					// Event発生
            my_smph.Release();
        }

        // GetEvent
        public string GetEvent()
        {
            int cnt;
            string wk_str = string.Empty;

            mre.WaitOne();
                // キュー読み出し
                my_smph.WaitOne();
                wk_str = (string)myQ.Dequeue();
                cnt = myQ.Count;
                if (cnt == 0)			    // キューが空になったらEventをReset
                	mre.Reset();
            my_smph.Release();

            return wk_str; 
        }
    }
}
