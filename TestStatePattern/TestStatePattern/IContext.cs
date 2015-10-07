using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStatePattern
{
    /**
    * @brief   IContext Class
    * @note    各State処理用interfaceクラス
    */
    public interface IContext
    {
        void SetMainEvent(string eventStr);
        void ChangeState(IState state);
        void SetTimer1EnbOrDsb(bool value);
    }
}
