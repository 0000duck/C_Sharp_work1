using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestTcpSrvSample
{
    class MyLogger
    {
        delegate void void_string_delegate(string value);   // return void, 引数 string型用delegate

        private static MyLogger myLoggerClsSingl = null;
        Form1 parentForm = null;
        
        
        public static MyLogger getInstance()
        {
            if (myLoggerClsSingl == null)
                myLoggerClsSingl = new MyLogger();

            return myLoggerClsSingl;

        }

        public void setMyLoggerObject(Form1 form)
        {
            parentForm = form;
        }

        /**
        *  @brief  textBox2 Text追加
        *  @param[in]   string  value  textBox2への追加文字列
        *  @return     void
        */
        public void AddMyLogger(string value)
        {
            if (parentForm.InvokeRequired)
            {
                parentForm.Invoke(new void_string_delegate(AddMyLogger), new object[] { value });
            }
            else
            {
                string wkStr = value + "\n";
                if (parentForm != null)
                    parentForm.textBox1.AppendText(wkStr);
                }
        }

        public void AppendMyLogger(string msgStr)
        {
            AddMyLogger(msgStr);
        }
    }
}
