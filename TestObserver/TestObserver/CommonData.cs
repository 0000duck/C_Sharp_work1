using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestObserver
{
    /**
    * @brief    Form2と3共通データ用クラス
    * @note	    Subject を継承しているので
    *           set_form2or3_int_data1()後、SetChangedで
    *           データ更新通知を各Observerにできる
    */
    public class CommonData : Subject
    {
        int form2or3_int_data1;

        /**
        * @brief    共通データの情報を textBox1 に反映
        * @note	    Subjectクラスの NotifyToObservers() から呼ばれる予定
        */
        public CommonData()
        {
            form2or3_int_data1 = 0;
        }

        /**
         * @brief       form2と3の共通データ設定
         * @return      none
         */
        public void set_form2or3_int_data1(int l_data)
        {
            form2or3_int_data1 = l_data;
        }

        /**
         * @brief       form2と3の共通データ読み出し
         * @return      int form2と3の共通データ
         */
        public int get_form2or3_int_data1()
        {
            return form2or3_int_data1;
        }
    }
}
