using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;     // add. [参照設定]→"参照の追加"→".NET"タグで選択し追加した。

namespace TestClassLibrary_MsgBox1
{
    public class MyMsgBox
    {
        public void ShowMyMsgBox(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, "msg", 0);
        }
    }
}
