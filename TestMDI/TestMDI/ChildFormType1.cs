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
     * @brief   ChildFormType1
     * @note    子Form クラス
     */
    //-----------------------------------------------------------------------
    public partial class ChildFormType1 : Form
    {
        public ChildFormType1()
        {
            InitializeComponent();
        }

        public event MyEventHandler MyEvent;                // TopFormに伝えるEventHandler定義
        public delegate void MyEventHandler(MyEventArgs e); // EventHandlerのdelegate定義

        /**
         *  @brief  Eventを伝えるMethod
         *  @param[in]  int num Event受信側に int型データを渡すときに使用
         *  @param[in]  string  workStr Event受信側に stringデータを渡すときに使用
         *  @return     void
         *  @note       
         */
        private void SendEvToOtherForm(int num, string workStr)
        {
            MyEvent( new MyEventArgs(num, workStr) );
        }

        /**
         * @brief   TopFormへEvent通知するボタン処理
         * @param[in]   object      sender
         * @param[in]   EventArgs   e
         * @return      void
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            string msgStr = this.Name + " Form Event Occurred.";

            SendEvToOtherForm(1, msgStr);
        }
    }


    //-----------------------------------------------------------------------
    /**
     * @brief   MyEventArgs
     * @note    独自Eventクラス
     *          渡せるイベントデータ引数、EventArgsを継承したクラス
     */
    //-----------------------------------------------------------------------
    public class MyEventArgs : EventArgs
    {
        private readonly int _num;
        private readonly string _workStr;

        public MyEventArgs(int num, string workStr)
        {
            _num = num;
            _workStr = workStr;
        }
        public int num { get { return _num; } }
        public string workStr { get { return _workStr; } }
    }

}
