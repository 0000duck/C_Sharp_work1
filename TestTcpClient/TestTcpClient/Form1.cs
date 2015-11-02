//------------------------------------------------------------
//  TCP Client Sample Project
//
//  TcpClient を使用した TCP Client 処理
//  textBox1の内容に CRLF 追加して、送信、
//  受信内容は、textBox2に表示。
//  受信処理は、callbackを使用してみた。
//------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;        // add
using System.Threading; // add
using System.Net;           // add
using System.Net.Sockets;   // add

namespace TestTcpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void void_string_delegate(string str);
        delegate void void_bool_delegate(bool value);


        System.Net.Sockets.TcpClient tcpClient = null;  // TcpClientクラス変数
        NetworkStream ntstrm = null;                    // 接続後使用する Stream
        byte[] readBytes;                               // receieve data buffer


        /**
         *  @brief      TcpClientStart
         *  @param[in]  string  ipAddStr    ServerIPAddress
         *  @param[in]  int     portno      PortNo
         *  @param[in]  string  sndStr      送信文字列
         *  @return     void
         *  @note       TcpClient接続、コマンド送信、受信待ち開始
         */
        bool TcpClientStart(string ipAddStr, int portno, string sndStr)
        {
            
            try
            {
                readBytes = new byte[512];

                // TcpClinet でコマンド送信
                tcpClient = new System.Net.Sockets.TcpClient(ipAddStr, portno);
                ntstrm = tcpClient.GetStream();

                // コマンドを Byte型に変換
                sndStr = sndStr + "\r\n";
                byte[] sendBytes = Encoding.ASCII.GetBytes(sndStr);

                // コマンド送信
                ntstrm.Write(sendBytes, 0, sendBytes.Length);

                // Ack/Nack 受信待ち。 callback Method登録
                ntstrm.BeginRead(readBytes, 0, readBytes.Length, new AsyncCallback(RecvCallback), null);
                return true;
            }
            catch(System.Net.Sockets.SocketException skerr)
            {
                if (ntstrm != null)
                {
                    ntstrm.Close();
                    ntstrm = null;
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }
                skerr.ToString();
                return false;
            }
        }

        /**
         *  @brief      RecvCallback
         *  @param[in]  IAsyscResult    ar
         *  @return     void
         *  @note       受信時 call される Method
         */
        private void RecvCallback(IAsyncResult ar)
        {
            try
            {
                //ntstrm = tcpClient.GetStream();

                // 読み込んだバイト数を取得
                int rcvBytes = ntstrm.EndRead(ar);
                string rcvStr = Encoding.ASCII.GetString(readBytes);
                if(rcvStr.Length==0)
                    Console.WriteLine("Closed socket.");    // たぶん切断された
                else
                    AddTextBox2(rcvStr);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("receive error.", ex.Message, ex.ErrorCode);
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                string errStr = ex.Message;
                Console.WriteLine("Closed socket.");
            }
            catch (IOException ioerr)
            {
                Console.WriteLine(ioerr.Message);
            }
            finally
            {
                if (ntstrm != null)
                {
                    ntstrm.Close();
                    ntstrm = null;
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }
                Btn1EnbDsb(true);
            }
        }

        /**
         *  @brief      button1_Click
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         *  @note       TcpClientで送受信開始
         */
        private void button1_Click(object sender, EventArgs e)
        {
            bool bret;

            button1.Enabled = false;

            bret = TcpClientStart("127.0.0.1", 7777, textBox1.Text);
            if (bret == false)
                textBox2.AppendText("connet failure.\n");

        }

        /**
         *  @brief      textBox1 Text追加
         *  @param[in]  string  value  textBox1への追加文字列
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
         *  @brief      textBox1 Text追加
         *  @param[in]  string  value  textBox1への追加文字列
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
         *  @brief      Btn1EnbDsb
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
