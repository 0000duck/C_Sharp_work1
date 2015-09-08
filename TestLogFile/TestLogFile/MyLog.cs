//-------------------------------------------------------------------
//  MyLog Sample Project
//
//      FileStreamとStreamWriter を使用した 独自LogFile作成クラスSample。
//      
//     出力Format"YYYY/MM/DD hh:mm:ss:nnn > msg文\n"
//     例："2015/09/08 15:33:46:316 > data count = 1\n" 
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestLogFile
{
    /**
     * @brief		MyLog class
     * @note		独自 LogFile 作成クラス
     */
    class MyLog
    {
        private
            StreamWriter sw;

        public
        /**
         * @brief   Constractor
         */
        MyLog()
        {
            sw = null;
        }

        /**
         * @brief       OpenLogFile
         * @param[in]   string  l_path_str  LogFileの絶対パス
         * @return      bool    true:ok  false:error
         */
        public bool Open(string l_path_str)
        {
            bool bret = true;

            // Openファイルの Mode を指定
            FileStream fs = new FileStream(l_path_str, System.IO.FileMode.Append,
                FileAccess.Write,			// Read/ReadWrite/Write
                FileShare.ReadWrite);		// ReadWrite 後続の読みとり用、書き込み用のファイルOpenを許可
            // Read/Write
            // None:File共有を解除
            // Delete：後続のファイルの削除を許可
            try
            {
                // FileOpen
                sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));
                bret = true;
            }
            catch (System.IO.IOException ie)
            {
                Console.WriteLine("", ie.Message);
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
                bret = false;
            }

            return bret;
        }

        /**
         * @brief       Log出力
         * @param[in]   string  l_str   Fileに出力したい文字列
         * @return      none
         */
        public void OutLog(string l_str)
        {
            DateTime daytime;
            string msg;
            string wk;

            if (sw != null)
            {
                daytime = DateTime.Now;     // 現在時間取得
                wk = "";
                wk = string.Concat(wk, daytime.Year.ToString(), "/");
                wk = string.Concat(wk, daytime.Month.ToString("D2"), "/");
		        wk = string.Concat(wk, daytime.Day.ToString("D2"), " ");
		        wk = string.Concat(wk, daytime.Hour.ToString("D2"), ":");
		        wk = string.Concat(wk, daytime.Minute.ToString("D2"), ":");
		        wk = string.Concat(wk, daytime.Second.ToString("D2"), ":");
		        wk = string.Concat(wk, daytime.Millisecond .ToString("D3"), " > ");
		        msg = string.Concat(wk, l_str);

                sw.WriteLine(msg);
                sw.Flush();
            }
        }

        /**
         * @brief       CloseFile
         * @return      none
         */
        public void Close()
        {
            if (sw != null)
            {
                sw.Close();
                sw = null;
            }
        }
    }
}
