//-------------------------------------------------------------------
//  Singletone Sample Project
//
//      自分用Data Class MyData を Singletoneで生成し、
//      以降、そのクラスのメンバをButtonがおされるたびに インクリメントする
//      Singletone Sampleプロジェクト
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSingletone
{
    public partial class Form1 : Form
    {
        MyData myData_Cls;          // 独自データ保存クラス

        public Form1()
        {
            InitializeComponent();
            myData_Cls = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ldata = 0;

            myData_Cls = MyData.getInstance();  // 独自データ保存クラスの１つしかないObject生成/取得

            ldata = myData_Cls.get_my_data1();  // １つしかない独自データクラスからデータ取得
            ldata++;
            myData_Cls.set_my_data1(ldata);     // １つしかない独自データクラスへデータ反映

            textBox1.Text = ldata.ToString();
        }
    }
}
