//---------------------------------------------------------------------
//  ポリモーフィズム Sample Project
//
//  Mecha1と2 は、Abstract AMecha を継承、AMecha は
//  interface IMecha を継承。
//  ループのみで、2メカを実行する
//  各メカは、Processing() メソッドを持ち、その中の
//  個々の処理は、GetContent() メソッドで実装。
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPolymorphism1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 各メカのクラスを１つのリストに集める
            mechaList = new List<IMecha>();
            mechaList.Add(new Mecha1());
            mechaList.Add(new Mecha2());
        }


        List<IMecha> mechaList;     // 各Mecha をList保持するメンバ
        
        private void Btn_ExecMecha_Click(object sender, EventArgs e)
        {
            foreach (IMecha mecha in mechaList)
            {
                TxtBox_Mecha.Text = mecha.Processing();   // Mecha毎に実行
            }
            //
            // 上記処理をポリモフィズム使用しないと
            // 以下のように、switch などの条件分岐を実装することになる
            //for(int mechaNo=0; mechaNo<2; mechaNo++)
            //{
            //    TxtBox_Mecha.Text = MechaProcessing(mechaNo);
            //}
            //
            //string MechaProcesing(int no)
            //{
            //    string processing_string = string.Empty;
            //
            //    switch (no)
            //    {
            //        case 0:
            //            processing_string = "Mecha1 excecuted.";
            //            break;
            //
            //        case 1:
            //            processing_string = "Mecha2 excecuted.";
            //            break;
            //    }
            //
            //    return processing_string;
            //}
        }
    }
}
