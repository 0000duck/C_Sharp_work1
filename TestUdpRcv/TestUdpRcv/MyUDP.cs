//---------------------------------------------------------------------------------
//	独自 UDP 送信、受信処理クラス
//
//  UdpClientを使用した独自 UDP処理クラスです。
//	受信完了は callBackを使用しています。
//	SetCallMethodAtRcvEnd()の引数に本クラス呼び出し側へ
//	受信が完了したときに通知する関数の登録が必要です。
//	その後、RecvProc(string ipaddr, int portno) を callしてもらえば、受信開始。
//	送信に関しては、SendProc(string ipaddr, int portno, string msg) を呼ぶのみです。
//
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;           // add
using System.Net.Sockets;   // add

namespace OriginalUDP
{
    /**
     *  @brief  SomeList Class
     *  @note   T:型パラメータ
     */
    class MyUDP
    {
        UdpClient rcvUdp = null;    // 受信用UDP
        UdpClient sndUdp = null;    // 送信用UDP

        public delegate void void_str_delegate(string str);
        void_str_delegate upper_function;

        /**
        　*  @brief  SetCallMethodAtRcvEnd
        　*  @param[in]  void_str_delegate f
        　*  @return     void
        　*  @note       受信完了時、本クラスを生成した上位クラスの特定Methodを呼び出すために
          *              登録するMethod 
        　*/
        public void SetCallMethodAtRcvEnd(void_str_delegate f)
        {
            upper_function = f;
        }

        /**
          *  @brief  RecvProc
          *  @param[in]  string ipaddr  IP Address
          *  @param[in]  int    portno  PortNo.  
          *  @return     void
          *  @note       
          */
        public void RecvProc(string ipaddr, int portno)
        {
            //string localIpStr = "127.0.0.1";
            System.Net.IPAddress localAddr = System.Net.IPAddress.Parse(ipaddr);

            //IPEndPoint ipEndp = new IPEndPoint(IPAddress.Any, 7777);
            IPEndPoint ipEndp = new IPEndPoint(localAddr, portno);
            rcvUdp = new UdpClient(ipEndp);

            // 受信開始。受信完了か強制終了時、ReceiveEndProc() call
            rcvUdp.BeginReceive(ReceiveEndProc, rcvUdp);
        }

        /*
         *  @brief  ReceiveEndProc
         *  @param[in]  IAsyncResult    ar
         *  @return     none
         *  @note       受信完了時にcallback される関数
         *              受信待機中に強制停止(Close()された場合もcallされる
         */
        private void ReceiveEndProc(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;

            //非同期受信を終了
            System.Net.IPEndPoint remoteEndPt = null;
            byte[] rcvBytes;
            try
            {
                rcvBytes = udp.EndReceive(ar, ref remoteEndPt);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("receive error.", ex.Message, ex.ErrorCode);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                string errStr = ex.Message;
                Console.WriteLine("Closed socket.");
                return;
            }

            //データを文字列に変換する
            //string rcvStr = System.Text.Encoding.UTF8.GetString(rcvBytes);
            string rcvStr = Encoding.ASCII.GetString(rcvBytes);

            // 本クラス使用元に 受信データ送信
            upper_function(rcvStr);

            if(rcvUdp!=null)
                rcvUdp.Close();
            rcvUdp = null;
        }

        /*
         *  @brief  RecvForceStop
         *  @return     none
         *  @note       受信用UdpClient強制閉じ。
         */
        public void RecvForceStop()
        {
            if (rcvUdp != null)
                rcvUdp.Close();
        }

        /*
        　*  @brief  SendProc
        　*  @param[in]  string ipaddr  IP Address
          *  @param[in]  int    portno  PortNo.  
          *  @param[in]  string msg     送信文字列
        　*  @return     none
        　*/
        public void SendProc(string ipaddr, int portno, string msg)
        {
            sndUdp = new UdpClient();
            byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(msg);
            sndUdp.Send(sendBytes, sendBytes.Length, ipaddr, portno);
            sndUdp.Close();
            sndUdp = null;
        }
    }
}
