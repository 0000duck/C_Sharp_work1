//----------------------------------------------------------------------
//  UDP 送信サンプルプロジェクト
//
//  button1を押すと、MyUDPクラスを使用して、コマンドを送信し
//  "ACK"が送られてくるのを待つ。
//  送信後、すぐ送られてくることも考え念のため、受信待ちを先に開始して
//  送信をするようにしてあります。(逆でも大丈夫そうでした。)
//  受信側は PortNo=7778
//  送信側は PortNo=7777 とした
//----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OriginalUDP;              // Add

namespace TestUdpSnd
{
    public partial class Form1 : Form
    {
        delegate void void_string_delegate(string str);
        delegate void void_bool_delegate(bool value);
        MyUDP rcvUDP;
        MyUDP sndUDP;

        public Form1()
        {
            InitializeComponent();
        }

        /**
        　*  @brief  button1_Click
        　*  @param[in]  object  sender
          *  @param[in]  EventArgs    e
        　*  @return     void
        　*  @note       UDP で、textBox1の内容を送信。受信開始。
        　*/
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            // UDP 受信開始 (コマンド送るとすぐ ACKが来るので前もって受信待ちにしておく)
            rcvUDP = new MyUDP();
            rcvUDP.SetCallMethodAtRcvEnd(ReadEndProc);  // 受信時に Method callされるように登録
            rcvUDP.RecvProc("127.0.0.1", 7778);         // 引数指定した内容で UDP受信待ち開始

            // UDP 送信
            string sndStr = textBox1.Text + "\r\n";
            sndUDP = new MyUDP();
            sndUDP.SendProc("127.0.0.1", 7777, sndStr);

            // UDP 受信開始 (コマンド送るとすぐ ACKが来るので前もって受信待ちにしておく)
            //rcvUDP = new MyUDP();
            //rcvUDP.SetCallMethodAtRcvEnd(ReadEndProc);  // 受信時に Method callされるように登録
            //rcvUDP.RecvProc("127.0.0.1", 7778);         // 引数指定した内容で UDP受信待ち開始
        }

        /**
         *  @brief  ReadEndProc
         *  @param[in]  string  rcvStr  受信した文字列
         *  @return     void
         *  @note       UDPで受信した文字列をtextBoxに追加
         */
        public void ReadEndProc(string rcvStr)
        {
            // 受信データを TextBox2に表示
            AddTextBox2(rcvStr);

            rcvUDP = null;
            sndUDP = null;

            Btn1EnbDsb(true);
        }

        /**
         *  @brief  textBox1 Text追加
         *  @param[in]   string  value  textBox2への追加文字列
         *  @return     void
         */
        void AddTextBox2(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new void_string_delegate(AddTextBox2), new object[] { value });
            }
            else
            {
                textBox2.AppendText(value);
            }

        }

        /**
         *  @brief  Btn1EnbDsb
         *  @param[in]  bool    value   true/false
         *  @return     void
         *  @note       button1 Enable/Disable
         */
        void Btn1EnbDsb(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new void_bool_delegate(Btn1EnbDsb), new object[] { value });
            }
            else
            {
                button1.Enabled = value;
            }
        }
    }
}
