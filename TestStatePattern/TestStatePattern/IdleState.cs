using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStatePattern
{
    /**
    * @brief   IdleState Class
    * @note    IDLE 状態クラス
    */
    public class IdleState : IState
    {
        private static IdleState idleStateClassObj = null;  // 自クラス実態
        private static string stateName;

        /**
         * @brief       自クラスの実態返信
         * @return      自クラスの実態
         */
        public static IdleState getInstance()
        {
            if (idleStateClassObj == null)
            {
                idleStateClassObj = new IdleState();        // 実態が無いときのみ生成
                stateName = "STATE_IDLE";
            }

            return idleStateClassObj;                    // 自クラスの実態を返信
        }

        public string getStateName() { return stateName; }

        public void btn1EventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // 状態内で Event発生時はこれを実行。特にEventないときは "NO_EVENT"

            context.ChangeState(Exec1State.getInstance());  // Exe1へ遷移
        }

        public void btn2EventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // 
            context.ChangeState(IdleState.getInstance());   // 自分へ遷移 
        }

        public void AbtEventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");               // 
            context.ChangeState(IdleState.getInstance());   // 自分へ遷移 
        }

        public void ToutEventProc(IContext context) {
            context.SetMainEvent("NO_EVENT");               // 
            context.ChangeState(IdleState.getInstance());   // 自分へ遷移 
        }

        public void EndEventProc(IContext context)
        {
            context.SetMainEvent("NO_EVENT");
            // 状態遷移なし
        }
    }
}
