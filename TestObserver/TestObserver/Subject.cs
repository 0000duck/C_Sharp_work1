using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TestObserver
{
    /**
    * @brief    Subject Class
    * @note	    各Observerを登録。変更通知処理。
    */
    public abstract class Subject : TestObserver.ISubject
    {
        ArrayList ary_observ = new ArrayList();

        /**
         * @brief       Observerの追加
         * @param[in]   IObserver l_observ  対象ObserverのObject(Form2/3)
         */
        public void AddOvserv(IObserver l_observ)
        {
            ary_observ.Add(l_observ);
        }

        /**
         * @brief       Observerの削除
         * @param[in]   IObserver l_observ  対象ObserverのObject(Form2/3)
         */
        public void DelOvserv(IObserver l_observ)
        {
            ary_observ.Remove(l_observ);
        }

        /**
         * @brief       各ObserverへUpDate通知
         * @note        各ObserverへUpDate通知
         */
        void NotifyToObservers()
        {
            foreach (IObserver l_observ in ary_observ)
            {
                l_observ.Update_Observer();
            }
        }

        /**
        * @brief      変更通知処理
        * @note       NotifyToObserver() を使用して、各ObserverへUpDate通知
        */
        public void SetChanged()
        {
            NotifyToObservers();
        }
    }

}
