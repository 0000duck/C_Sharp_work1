using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace TestTcpSrvSample
{
    /**
     * @brief   ServerSideTcpClient Class
     * @note    TCP/IP Server側 Client処理クラス。
     *          終了時 Dispose() の使用が必要。
     */
    class ServerSideTcpClient : IDisposable
    {
        
        TcpClient myClient = null;
        NetworkStream stream = null;

        MyLogger mylogger;
        byte[] oneRcvBytes = new byte[256];                                     // 1回のRead分読み出しBufferとして使用
        System.IO.MemoryStream rcvDataMemStream = new System.IO.MemoryStream(); // 受信Bufferとして使用

        bool endFlag;                   // true:recieve後、Ack/Nack sendし、使用したObjectを解放した。  
        bool _dispose = false;          // true:Dispose()処理ずみ

        
        Thread clientThread = null;
        ThreadStart clientThreadDelegate;

        // Destractor
        ~ServerSideTcpClient()
        {
            Console.WriteLine("ServerSideTcpClient destructor called.");
        }

        /**
         *  @brief      Dispose
         *  @param[in]  void
         *  @return     void
         */
        public void Dispose() 
        {
            Console.WriteLine("ServerSideTcpClient Dispose called.");
            Dispose(true);              // manage解放
            GC.SuppressFinalize(this);  // unmanage解放念のため
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                {   
                    // ManageResource 解放
                    // スレッド内ですべて解放済みの予定
                }
                _dispose = true;
            }
        }


        /**
         *  @brief      setTcpClient
         *  @param[in]  TcpClient   接続してきた Clientの TcpClientクラスObject
         *  @return     void
         */
        public void setTcpClient(TcpClient client)
        {
            myClient = client;
            //client.Client.Handle.ToInt32();
            mylogger = MyLogger.getInstance();
            //System.Net.ServicePointManager.SetTcpKeepAlive(true, 1000, 5);
            myClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1000);
            //System.Net.ServicePointManager.SetTcpKeepAlive(true, 1000, 5);
           
        }

        /**
         *  @brief      getEndFlag
         *  @param[in]  void
         *  @return     bool    endFlag値
         */
        public bool getEndFlag()
        {
            return endFlag;
        }

        /**
         *  @brief      startProc
         *  @param[in]  void
         *  @return     void
         *  @note      Clientとの通信処理スレッド起動処理。  
         */
        public void startProc()
        {
            // スレッド起動
            clientThreadDelegate = new ThreadStart(ServerSideClientThreadFuction);
            clientThread = new Thread(clientThreadDelegate);
            clientThread.Start();
        }

        /**
         *  @brief      endProc
         *  @param[in]  void
         *  @return     void
         *  @note      Clientとの通信処理スレッド終了依頼/待ち処理。  
         */
        public void endProc()
        {
            bool bret;

            if (clientThread != null)
            {
                // stream.Read中もあるので、強制停止せず、ServerSideClientThreadFuction()終了を待つ。
                bret = clientThread.Join(10000);
                if (bret == false)
                    Console.WriteLine("clientThread Join timeout.");
            }
        }


        /**
         *  @brief  Server側Cleint通信処理スレッド
         *  @param[in]  void
         *  @return     void
         */
        void ServerSideClientThreadFuction()
        {
            int rsize = 0;
            stream = myClient.GetStream();
            stream.ReadTimeout = 5000;      // 5sec

            while (true)
            {
                try
                {
                    // 受信処理
                    rsize = stream.Read(oneRcvBytes, 0, oneRcvBytes.Length);

                    if (rsize == 0)     // 切断された
                    {
                        if (stream != null)
                        {
                            stream.Close();
                            stream = null;
                        }

                        if (myClient != null)
                        {
                            myClient.Close();
                            myClient = null;
                        }
                        break;
                    }
                    else
                    {
                        rcvDataMemStream.Write(oneRcvBytes, 0, rsize);  // 受信DataをBufferring
                        int leng = (int)rcvDataMemStream.Length;

                        if (leng >= 2)                                  // 最後がCRLFか？
                        {
                            string rcvStr = Encoding.ASCII.GetString(rcvDataMemStream.GetBuffer(), 0, leng);
                            if ((string.Compare(rcvStr.Substring(leng - 2, 1), "\r") == 0) &&
                                (string.Compare(rcvStr.Substring(leng - 1, 1), "\n") == 0))
                            {
                                // 受信データ解析
                                string sendData = AnalyzeRcvData(rcvStr);
                                sendData = sendData + "\r\n";

                                //以下の2行は、強制切断で Client側に切断が伝わるのを
                                //確認するのに使用した。その後、デバッグを停止。
                                //stream.Close();
                                //myClient.Close();

                                // Ack/Nack送信
                                byte[] sendBytes = Encoding.ASCII.GetBytes(sendData);
                                //stream.Write(sendBytes, 0, sendBytes.Length);
                                stream.BeginWrite(sendBytes, 0, sendBytes.Length, OnWriteComplete, null);

                                break;  // exit while(true)
                            }
                        }
                    }
                }
                catch (System.IO.IOException ioerr)
                {
                    //if ((ioerr.GetType() == typeof(System.Net.Sockets.SocketException)) &&
                    //    ((ioerr.InnerException as System.Net.Sockets.SocketException).ErrorCode == 10060))
                    if ( (ioerr.InnerException as System.Net.Sockets.SocketException).ErrorCode == 10060)
                    {
                        // ReadTimeout なので、再度Timeout時間設定し、再Readへ
                        stream.ReadTimeout = 5000;
                    }
                    else
                    {
                        throw;  // 例外を投げる。
                    }
                }

            }   // end of while
        }

        /**
         *  @brief  OnWriteComplete
         *  @param[in]  IAsyncResult    ar  非同期呼び出し応答
         *  @return     void
         *  @note       BeginWrite()処理完了時、callされるMethod
         */
        private void OnWriteComplete(IAsyncResult ar)
        {
            Console.WriteLine("OnWriteComplete called.");

            stream.EndWrite(ar);        // 非同期書き込みの終了を処理

            // 作成した Object解放
            rcvDataMemStream.Close();

            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (myClient != null)
            {
                myClient.Close();
                myClient = null;
            }

            endFlag = true;
        }

        /**
         *  @brief  AnalyzeRcvData
         *  @param[in]  string    rcvDataStr    受信データ
         *  @return     string      Ack/Nack
         *  @note       rcvDataStrを解析し、Ack/Nack判断して return
         */
        string AnalyzeRcvData(string rcvDataStr)
        {
            string rtnStr;

            string wkStr = "Rcv(" + myClient.Client.Handle + "):" + rcvDataStr;
            Console.WriteLine(wkStr);
            mylogger.AppendMyLogger(wkStr);

            if (rcvDataStr.IndexOf("CMD") >= 0)
                rtnStr = "Ack";
            else
                rtnStr = "Nack";

            return rtnStr;
        }
    }
}
