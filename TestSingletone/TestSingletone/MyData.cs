using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TestSingletone
{
    /**
     * @brief		Data保存用 singletone class
     * @note		singletone を使用し、唯一のデータを保存
     */
    sealed class MyData
    {
        private static MyData mydata_cls_singl = null;  // 自クラス実態
        private int mydata1 = 0;                        // 保存用int変数

        /**
         * @brief       自クラスの実態返信
         * @return      MyData  自クラスの実態
         */
        public static MyData getInstance()
        {
            if (mydata_cls_singl == null)
                mydata_cls_singl = new MyData();        // 実態が無いときのみ生成

            return mydata_cls_singl;                    // 自クラスの実態を返信
        }


        /**
         * @brief       SaveData
         * @param[in]   int l_data  保存データ
         */
        public void set_my_data1(int l_data)
        {
            mydata1 = l_data;
        }

        /**
         * @brief       ReadData
         * @return      int 保存されているデータ
         */
        public int get_my_data1()
        {
            return mydata1;
        }
    }
}
