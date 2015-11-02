//-----------------------------------------------------------------------
//	MDI(MultipleDocumentInterface) Sample Projet
//
//	Form1を TopFormにし、
//  子Form ChildFormType1 を2個、子Form ChildControlForm を 1個
//  持った、MDI Sample Project
//  ChildFormType1 は、buttonを押すと Form1に Event callbackします。
//  ChildControlForm は、
//      button1 を押すと 自分も含め 子Form の名前を textBox1に表示
//		button2 を押すと 自分も含め 子Form すべてを閉じます。
//  TopFormの Menuは、Tveri は 子Formを整列表示します。
//-----------------------------------------------------------------------
// MDIにするために Form1 の IsMDIContainer プロパティを true にしました。
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMDI
{
    //-----------------------------------------------------------------------
    /**
     * @brief   Form1
     * @note    TopForm クラス
     */
    //-----------------------------------------------------------------------
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ChildFormType1 childFormTp1_1;      // 子Form
        ChildFormType1 childFormTp1_2;      // 子Form
        ChildControlForm childControlForm;  // 子Form制御子Form

        /**
         * @brief   Form1_Load
         * @param[in]   object      sender
         * @param[in]   EventArgs   e
         * @return      子Form を生成
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            childFormTp1_1 = new ChildFormType1();
            childFormTp1_1.MdiParent = this;
            childFormTp1_1.Name = "ChildForm1";
            childFormTp1_1.Text = "ChildForm1";
            // 子FormでのEvent発生時、現在クラスのMethod callされるように設定
            childFormTp1_1.MyEvent += new ChildFormType1.MyEventHandler(CallBackFromChild);
            childFormTp1_1.Show();

            childFormTp1_2 = new ChildFormType1();
            childFormTp1_2.MdiParent = this;
            childFormTp1_2.Name = "ChildForm2";
            childFormTp1_2.Text = "ChildForm2";
            childFormTp1_2.MyEvent += new ChildFormType1.MyEventHandler(CallBackFromChild);
            childFormTp1_2.Show();

            childControlForm = new ChildControlForm();
            childControlForm.MdiParent = this;
            childControlForm.Name = "ControlForm";
            childControlForm.Show();
        }

        /**
         * @brief   TVertiToolStripMenuItem1_Click
         * @param[in]   object      sender
         * @param[in]   EventArgs   e
         * @return      TopForm Menu Item 1詰め選択Event処理
         *              子Formを垂直に並べる
         */
        private void TVertiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);     // 垂直にすべて表示
        }

        /**
          * @brief   CallBackFromChild
          * @param[in]   MyEventArgs      e
          * @return     void 
          * @note       子FormでEvent発生させたときにcallされるために用意した
          *             Method
          */
        private void CallBackFromChild(MyEventArgs e)
        {
            // Even発生側のForm名をevent引数で取得し、Control子Formに表示
            childControlForm.textBox2.AppendText(e.workStr + "\n");
            //textBox1.AppendText(e.TestNumValue.ToString() + ":" + e.TestStringValue);
        }

        /**
          * @brief   Form1_Resize
          * @param[in]  sender      e
          * @param[in]  EventArgs   e 
          * @return     void 
          * @note       TopFormの大きさを取得し、子Formのサイズを再設定
          */
        private void Form1_Resize(object sender, EventArgs e)
        {
            int w = this.Width/3;       // TopFormの幅/3 を 子Formの幅に
            int h = this.Height - 70;   // 子Formの高さを少し少なめに
            if (w >= 8)
                w = w - 8;

            childControlForm.Left = 0;  // Controlを一番左へ
            childControlForm.Width = w;
            childControlForm.Height = h;

            childFormTp1_1.Left = w;    // 子Form1を左から2番目へ
            childFormTp1_1.Width = w;
            childFormTp1_1.Height = h;

            childFormTp1_2.Left = w + w;    // 子Form2を左から3番目へ
            childFormTp1_2.Width = w;
            childFormTp1_2.Height = h;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

    }
}
