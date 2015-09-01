using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_SyncFtpPassive
{
    /**
     * @brief		Sync FtpPassive class
     * @note		同期 FTP PassiveMode OneFile Download/Upload 
     */
    class SyncFtpPassive
    {

        string err_msg_str;     // error発生時の内容

        /**
         * @brief       OneFile ftp download
         * @param[in]   string  name    login name
         * @param[in]   string  pass    password
         * @param[in]   string  uri     DownloadするFileの URI 例"ftp://localhost/test_e.exe"
         * @param[in]   string  local_path     :DownloadしたFileの保存先 例"C:\\test_e.exe"
         * @return      bool    true:ok  false:error
         */
        public bool FtpOneFileDown(string name, string pass, string uri, string local_path)
        {
            Uri uri_obj = new Uri(uri);
            string downFilePath = local_path;

            err_msg_str = "no info.";

            // FtpWebRequestの作成
            System.Net.FtpWebRequest ftp_req = (System.Net.FtpWebRequest)
            System.Net.WebRequest.Create(uri_obj);

            // ログインユーザー名とパスワードを設定
            ftp_req.Credentials = new System.Net.NetworkCredential(name, pass);

            // MethodにWebRequestMethods.Ftp.DownloadFile("RETR")を設定
            ftp_req.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            ftp_req.KeepAlive = false;	// 要求の完了後に接続を閉じる
            ftp_req.UseBinary = false;	// Binaryモードで転送する
            ftp_req.UsePassive = true;	// PASSIVEモードを有効にする

            bool ok_f = true;
            System.Net.FtpWebResponse ftp_res = null;
            System.IO.Stream res_strm = null;
            try
            {
                // FtpWebResponseを取得
                ftp_res = (System.Net.FtpWebResponse)ftp_req.GetResponse();

                // ファイルをダウンロードするためのStreamを取得
                res_strm = ftp_res.GetResponseStream();
            }
            catch (System.Net.WebException e_msg)
            {
                ok_f = false;
                err_msg_str = e_msg.Message;

                if (ftp_res != null)
                    ftp_res.Close();
                if (res_strm != null)
                    res_strm.Close();
            }

            if (ok_f == true)
            {
                // ダウンロードしたファイルを書き込むためのFileStreamを作成
                System.IO.FileStream fs = null;
                fs = new System.IO.FileStream(downFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                try
                {
                    // ダウンロードしたデータを書き込む
                    int size = 1024;
                    byte[] buffer = new byte[size];
                    while (true)
                    {
                        int readSize = res_strm.Read(buffer, 0, buffer.Length);
                        if (readSize == 0)
                            break;
                        fs.Write(buffer, 0, readSize);
                    }
                    
                    // FTPサーバーから送信されたステータスを表示
                    //Console.WriteLine("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);
                    
                }
                catch (System.IO.IOException ie)
                {
                    Console.WriteLine("", ie.Message);
                }
                finally {
                    if(fs!=null) 
                        fs.Close();

                    if (res_strm != null)
                        res_strm.Close();

                    if (ftp_res != null)
                        ftp_res.Close();
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        /**
         * @brief       OneFile ftp upload
         * @param[in]   string  name    login name
         * @param[in]   string  pass    password
         * @param[in]   string  uri     UploadするFileの URI 例"ftp://localhost/test_e.exe"
         * @param[in]   string  local_path     :UploadするFileの場所 例"C:\\test_e.exe"
         * @return      bool    true:ok  false:error
         */
        public bool FtpOneFileUp(string name, string pass, string uri, string local_path)
        {
            Uri uri_obj = new Uri(uri);
            string upFilePath = local_path;

            err_msg_str = "no info.";

            // FtpWebRequestの作成
            System.Net.FtpWebRequest ftp_req = (System.Net.FtpWebRequest)
            System.Net.WebRequest.Create(uri_obj);

            // ログインユーザー名とパスワードを設定
            ftp_req.Credentials = new System.Net.NetworkCredential(name, pass);

            // MethodにWebRequestMethods.Ftp.UploadFile("STOR")を設定
            ftp_req.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp_req.KeepAlive = false;	// 要求の完了後に接続を閉じる
            ftp_req.UseBinary = false;	// Binaryモードで転送する
            ftp_req.UsePassive = true;	// PASSIVEモードを有効にする
            bool ok_f = true;
            System.IO.Stream res_strm = null;
            try
            {
                // ファイルをアップロードするためのStreamを取得
                res_strm = ftp_req.GetRequestStream();
            }
            catch (System.Net.WebException e_msg)
            {
                ok_f = false;
                err_msg_str = e_msg.Message;

                if (res_strm != null)
                    res_strm.Close();
            }

            if (ok_f == true)
            {
                // アップロードするためのFileStreamを作成
                System.IO.FileStream fs = null;
                fs = new System.IO.FileStream(upFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                try
                {
                    // アップロード用Streamにデータを書き込む
                    int size = 1024;
                    byte[] buffer = new byte[size];
                    while (true)
                    {
                        int readSize = fs.Read(buffer, 0, buffer.Length);
                        if (readSize == 0)
                            break;
                        res_strm.Write(buffer, 0, readSize);
                    }
                }
                catch (System.IO.IOException ie)
                {
                    Console.WriteLine("", ie.Message);
                }
                finally {
                    if(fs!=null) 
                        fs.Close();

                    if (res_strm != null)
                        res_strm.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /**
         * @brief   Get Last Error informatoin
         * @return  string  error information
         */
        public string GetLastErrStr()
        {
            return err_msg_str;
        }
    }

}
