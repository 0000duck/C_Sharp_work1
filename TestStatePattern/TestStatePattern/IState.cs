using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStatePattern
{
    /**
    * @brief   IState Class
    * @note    各状態用 interfaceクラス。
    */
    public interface IState
    {
        string getStateName();                  // 状態名取得
        void btn1EventProc(IContext context);   // button1Event処理
        void btn2EventProc(IContext context);   // button2Event処理
        void AbtEventProc(IContext context);    // AbortButtonEvent処理
        void EndEventProc(IContext context);    // 終了Event処理
        void ToutEventProc(IContext context);   // TimeoutEvent処理
    }
}
