//-----------------------------------------------------------------------
//  Observer Pattern Sample Project
//
//    ConcreteObserver を GUI(Form2,3) に見立て、 Form2、3 を用意
//    ConcreteSubject を 共通データとして、 CommonData を用意
//    Form1 で CommonData 更新後、CommonDataに変更通知すると
//    Form2、Form3 に伝わり Form2、Form3から CommonData にデータを
//    取りにくるサンプル。
//    ( Pull方式というらしい。他に Push方式があり、こちらは
//    取りにいくのではなく、引数で与える。)
//    ＊一応、OvserberPatternのつもりですが、Form クラスを使用したため
//      ちょっと変な構造になっています。すみません。
//    [ポイント]
//    C# は多重継できないので、Interface IObserver とした。
//    Observerパターンを使うことで、Form1から、Form2や3の更新メソッドを
//    呼び出さなくて良くなるというのがねらいらしい。
//    Form4、5... と増えても call は増えない。
//    SetChanged() 1回 callすれば、すべての Formに伝わる。
//
//-----------------------------------------------------------------------
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CommonData common_data;
        Form2 f2;
        Form3 f3;

        private void Form1_Load(object sender, EventArgs e)
        {
            common_data = new CommonData();     // 共通データクラス生成
            f2 = new Form2(common_data);
            f3 = new Form3(common_data);
            common_data.AddOvserv(f2);          // Form2 を Observer に登録
            common_data.AddOvserv(f3);          // Form3 を Observer に登録

            // Form2, 3 起動
            f2.Show();
            f3.Show();
        }

        /**
        * @brief    共通データに "1"設定
        * @note		各Formにデータ変更通知し、Form上に反映
        */
        private void button1_Click(object sender, EventArgs e)
        {
            common_data.set_form2or3_int_data1(1);
            common_data.SetChanged();
        }

        /**
        * @brief    共通データに "2"設定
        * @note		各Formにデータ変更通知し、Form上に反映
        */
        private void button2_Click(object sender, EventArgs e)
        {
            common_data.set_form2or3_int_data1(2);
            common_data.SetChanged();
        }
    }
}
