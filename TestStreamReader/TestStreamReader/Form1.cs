//-------------------------------------------------------------------
//  FileRead Sample Project
//
//     FileStream で file open の mode を指定し
//     StreamReader で open して Read し 内容を richTextBox に表示
//     ( Fileは ExeclOpen中での Openを試すために csv ファイル形式を想定。 )
//     [Point]
//          StreamReaderでいきなりFile指定しないで、FileStreamを使用しているところ。
//          (AccessModeが指定できる。)
//          Readを一行づつ行わず、一気に読んで、 Splitを使用して後で、改行ごとに
//          分けているところ。
//          richTextBoxのカーソル移動方法。
//     [memo]
//          OpenFileDialog が デバッガで動作させるとWindowsが固まるので以下対応。
//          "C:\Windows\System32\drivers\cymon.sysを削除すると現象が発生しない"
//           cymon.sys のバグらしい。対応策をWebで調べて対応する必用がありそう。
//          とりあえず、Renameしたら、固まらなくなった。
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestStreamReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //
        //  File選択処理
        //    OpenFileDialog で選択した csv ファイルを richTextBoxに表示
        //    FileShare.ReadWrite を指定することで、そのファイルが Excelで Open
        //    されていても Readできる。
        private void button1_Click(object sender, EventArgs e)
        {
            string filepath_str;
            string list_tmp_str;
            StreamReader sr;
 
            //  File選択Diaglog クラス準備
            OpenFileDialog opfd = new OpenFileDialog();


            //  はじめに表示されるフォルダを指定。
            //  昔 VC++のころ CurrentDirectoryが
            //  InitialDirectoryで指定した場所に変わってしまうことがあった。
            //  今は無い？
            opfd.InitialDirectory = @"C:\";

            //  [ファイルの種類]に表示される選択肢を指定
            opfd.Filter = "CSVファイル(*.csv;*.csv)|*.csv;*.csv|すべてのファイル(*.*)|*.*";

            sr = null;
            // FileSelectDiaglog を表示
            if (opfd.ShowDialog() == DialogResult.OK)
            {
                // Openファイルの Mode を指定
                filepath_str = opfd.FileName;
                FileStream fs = new FileStream( filepath_str, System.IO.FileMode.Open,
			        FileAccess.Read,			// Read/ReadWrite/Write
			        FileShare.ReadWrite );		// ReadWrite 後続の読みとり用、書き込み用のファイルOpenを許可
										        // Read/Write
										        // None:File共有を解除
										        // Delete：後続のファイルの削除を許可
                try
                {
                    // FileOpen
                    sr = new StreamReader(fs, Encoding.GetEncoding("shift_jis"));

                    list_tmp_str = sr.ReadToEnd();                  // Fileの内容読み出し。
                    string[] list_str = list_tmp_str.Split('\r');   // 改行ごとに分解

                    foreach (string one_line in list_str)
                    {
                        // 1行づつ追加
                        richTextBox1.AppendText(one_line);
                    }

                    // to last Focus ( カーソルを最後へ )
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.Focus();
                    richTextBox1.ScrollToCaret();
                    richTextBox1.Refresh();
                    sr.Close();
                    fs.Close();
                }
                catch (System.IO.IOException ie)
                {
                    Console.WriteLine("", ie.Message);
                }
                finally
                {
                    if( sr != null )
                        sr.Close();
                }
            }
            // File選択キャンセル時以下へ来る。
            else
            {
                ;//特に処理を書いていません。
            }
        }
    }
}

