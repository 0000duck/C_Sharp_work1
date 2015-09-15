using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestObserver
{
    /**
    * @brief    共通データ更新確認のための3番目のGUI
    */
    public partial class Form3 : Form, IObserver
    {
        CommonData common_data;

        /**
         * @brief   Constractor
         */
        public Form3(CommonData l_common_data)
        {
            InitializeComponent();
            common_data = l_common_data;
        }

        /**
        * @brief    共通データの情報を textBox1 に反映
        * @note	    Subjectクラスの NotifyToObservers() から呼ばれる予定
        */
        public void Update_Observer()
        {
            int data;

            data = 0;
            data = common_data.get_form2or3_int_data1();    // CommonDataから共有データ取得
            textBox1.Text = data.ToString();
        }
    }
}
