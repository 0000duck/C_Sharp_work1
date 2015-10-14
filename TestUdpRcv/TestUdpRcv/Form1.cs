//----------------------------------------------------------------------
//  UDP 受信サンプルプロジェクト
//
//  MyUDPクラスを使用して、コマンドが送られてくるのを待ち、
//  受信後、"ACK"を送信する
//  受信側は PortNo=7777
//  送信側は PortNo=7778 とした
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

namespace TestUdpRcv
{
    public partial class Form1 : Form
    {
        delegate void void_string_delegate(string str);
        delegate void void_bool_delegate(bool value);

        MyUDP rcvUDP;   // 受信用UDP
        MyUDP sndUDP;   // 送信用UDP

        public Form1()
        {
            InitializeComponent();
        }

        /**
        　*  @brief  button1_Click
        　*  @param[in]  object  sender
          *  @param[in]  EventArgs    e
        　*  @return     void
        　*  @note       UDP で、受信待ち開始
        　*/
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            // UDP 受信開始
            rcvUDP = new MyUDP();
            rcvUDP.SetCallMethodAtRcvEnd(ReadEndProc);  // 受信時に ReadEndProc() callされるように登録
            rcvUDP.RecvProc("127.0.0.1", 7777);         // 引数指定した内容で UDP受信待ち開始
        }

        /**
         *  @brief  ReadEndProc
         *  @param[in]  string  rcvStr  受信した文字列
         *  @return     void
         *  @note       UDPで受信した文字列をtextBoxに追加し、"ACK"をUDPで送信
         */
        public void ReadEndProc(string rcvStr)
        {
            // 受信データを TextBoxに表示
            AddTextBox1(rcvStr);
            rcvUDP = null;

            // 通常はここで、受信した rcvStrの frame解析をして、ACK/NACK送信を決めたりする。
            // Sampleのため省略
            
            // UDP送信開始
            sndUDP = new MyUDP();
            sndUDP.SendProc("127.0.0.1", 7778, "ACK\r\n");
            Btn1EnbDsb(true);
        }

        /**
         *  @brief  textBox1 Text追加
         *  @param[in]   string  value  textBox1への追加文字列
         *  @return     void
         */
        void AddTextBox1(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new void_string_delegate(AddTextBox1), new object[] { value });
            }
            else
            {
                textBox1.AppendText(value);
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
            if(InvokeRequired)
            {
                Invoke(new void_bool_delegate(Btn1EnbDsb), new object[] { value });
            }
            else{
                button1.Enabled = value;
            }
        }

        /**
         *  @brief  Form1_FormClosing
         *  @param[in]  object  sender
         *  @param[in]  FormClosingeventArgs    e
         *  @return     void
         *  @note       本アプリケーション終了時の処理
         */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 以下をcallして、もし受信中の場合は強制停止させる。
            if(rcvUDP!=null)
                rcvUDP.RecvForceStop();
        }


    }
}
