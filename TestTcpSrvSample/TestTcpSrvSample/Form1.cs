//---------------------------------------------------------
//  TCP Serve Sample Project
//
//  TcpListener を使用して、TCP Serverを実現
//  Client が接続されるたびに ServerSideTcpClientクラスを生成し
//  CRLFデリミタで　コマンド取得待ち、取得後、"CMDxx"なら "Ack"、
//  そうでないなら "Nack"送信。
//  受信はポーリングを使用してみました。
//  送信は、送信完了callbackも追加してみました。
//  Client 接続に対するkeepAlive設定は、試みましたがうまくいかなかったので
//  とりあえず、コメントアウトしてあります。
//  ServerSideTcpClient クラスは、独自クラスなので Disposeを実装してみました。
//  自分で Dispose/Close が必要なクラスライブラリを使用した場合は
//  記述追加が必要と考えます。
//  TcpListener は 独自、MyTcpServerで実装し
//  Client を ArrayList で保持するようにしました。
//  方法がしょぼいですが、毎回 Client を閉じたか ArrayListをCheckして
//  Clientとの通信が終了しているものは ArrayListから削除するようにしました。
//  すみません。取り急ぎ実装のため 終了処理は、ざっくりでしか作成していません。
//---------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;



namespace TestTcpSrvSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyTcpServer srv;
        MyLogger    mylogger;


        /**
         *  @brief      Form1_Load
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         *  @note       TcpServer起動
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            // MyLoggerクラスに textBox1設定
            mylogger = MyLogger.getInstance();
            mylogger.setMyLoggerObject(this);
            
            
            srv = new MyTcpServer();
            srv.startProc("127.0.0.1", 7777, 8);
            //srv.startProc("192.168.20.1", 7777, 8);
        }

        /**
          *  @brief      Form1_Load
          *  @param[in]  object      sender
          *  @param[in]  EventArgs   e
          *  @return     void
          *  @note       TcpServer終了
          */
        private void button1_Click(object sender, EventArgs e)
        {
            srv.endProc();
            Close();
        }
    }
}
