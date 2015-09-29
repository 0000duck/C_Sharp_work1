using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;            // add. For Assembly
using TestClassLibrary_MsgBox1;     // add. For MyMsgBox

namespace TestStaticDll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyMsgBox mymsgbox = new MyMsgBox();
            mymsgbox.ShowMyMsgBox("Test Message1!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var asm = Assembly.LoadFrom("C:\\Users\\Public\\work\\Cシャープ\\TestClassLibrary_MsgBox1\\TestClassLibrary_MsgBox1\\bin\\Release\\TestClassLibrary_MsgBox1.dll");
            //var asm = Assembly.LoadFrom("C:\\Users\\Public\\work\\Cシャープ\\TestClassLibrary_MsgBox1\\TestClassLibrary_MsgBox1\\bin\\Debug\\TestClassLibrary_MsgBox1.dll");
            var typeInfo = asm.GetType("TestClassLibrary_MsgBox1.MyMsgBox");
            dynamic dy = Activator.CreateInstance(typeInfo);
            dy.ShowMyMsgBox("Test Message2!!");

        }
    }
}
