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
     * @brief   ChildControlForm
     * @note    子Form制御Form クラス
     */
    //-----------------------------------------------------------------------
    public partial class ChildControlForm : Form
    {
        public ChildControlForm()
        {
            InitializeComponent();
        }

        /**
          * @brief   ShowChildFormName_button_Click
          * @param[in]   object      sender
          * @param[in]   EventArgs   e
          * @return      void
          * @note        各子Formの名前を本Form textBox1に表示
          */
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
            {
                textBox1.Text += this.MdiParent.MdiChildren[i].Name + "\r\n";
            }
        }

        /**
          * @brief   CloseAllChildFormClose_button_Click
          * @param[in]   object      sender
          * @param[in]   EventArgs   e
          * @return      void
          * @note    全子Form閉じ
          */
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                child.Close();
            }
        }
    }

 

}
