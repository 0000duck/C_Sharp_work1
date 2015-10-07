using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStatePattern
{
    /**
      * @brief   Exec2State Class
      * @note    EXEC2 状態クラス
      */
    public class Exec2State : IState
    {
        private static Exec2State exec2StateClassObj = null;  // 自クラス実態
        private static string stateName;

        /**
         * @brief       自クラスの実態返信
         * @return      自クラスの実態
         */
        public static Exec2State getInstance()
        {
            if (exec2StateClassObj == null)
            {
                exec2StateClassObj = new Exec2State();      // 実態が無いときのみ生成
                stateName = "STATE_EXEC2";
            }
            return exec2StateClassObj;                      // 自クラスの実態を返信
        }

        /**
         *  @brief  本クラス名取得
         *  @param[in]  void
         *  @return     string  状態名
         */
        public string getStateName() { return stateName; }

        /**
         *  @brief  button1 Event処理
         *  @param[in]  IContext    処理用クラス
         *  @return     void
         */
        public void btn1EventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // Eventなし
            context.ChangeState(Exec2State.getInstance());  // 自分へ遷移

        }

        /**
         *  @brief  button2 Event処理
         *  @param[in]  IContext    処理用クラス
         *  @return     void
         */
        public void btn2EventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");                // Event生成なし
            context.ChangeState(Exec2State.getInstance());   // 自分へ遷移 
        }

        /**
         *  @brief  Abort button Event処理
         *  @param[in]  IContext    処理用クラス
         *  @return     void
         */
        public void AbtEventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // Event生成なし
            context.ChangeState(IdleState.getInstance());   // Idle へ遷移
            context.SetTimer1EnbOrDsb(false);               // Timer1停止
        }

        /**
          *  @brief  Timer1Timeout Event処理
          *  @param[in]  IContext    処理用クラス
          *  @return     void
          */
        public void ToutEventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // Event生成なし
            context.ChangeState(IdleState.getInstance());  // 自分へ遷移 
        }

        /**
         *  @brief  終了 Event処理
         *  @param[in]  IContext    処理用クラス
         *  @return     void
         */
        public void EndEventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");
            // 状態遷移なし
        }
    }
}
