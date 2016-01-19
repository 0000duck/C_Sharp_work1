//-------------------------------------------------------------------
//  NullObject サンプル Project
//
//  あまりいい例では、無いかもしれませんが
//  command の文字列に終端(delimiter)を付けるクラスで
//  NullObject クラスを用意してみました。
//  サンプルは"command"の文字列に終端を付けているだけです。
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestNullObject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 起動時は NullObject 設定
            delimi = new NullObjDelimter();
        }

        IDelimiter delimi;      // Delimiter用

        private void Form1_Load(object sender, EventArgs e)
        {
            string commandBaseStr = "Command1";
            string commandStr = "";

            // delimi を NullObjDelmiter にしてあるので
            // if(delimi!=null) の判断しない。
            // AddDelimier を callしても 例外にならない。
            // 処理も何もしない、引数で受けたものをそのまま返す。
            commandStr = delimi.AddDelimiter(commandBaseStr);
            textBox1.Text = commandStr;
            commandStr = "";

            // delimiter を設定
            delimi = new Delimiter(";");                        // delimiterを';'とした
            commandStr = delimi.AddDelimiter(commandBaseStr);   // 引数に Delimiter追加
            textBox2.Text = commandStr;
        }
    }

    /**
     * @brief   IDelimiter Class
     * @note    Delimiter interfaceクラス。
     */
    public interface IDelimiter
    {
        string AddDelimiter(string src);
    }

    /**
     * @brief   Delimiter Class
     * @note    Delimiter クラス。
     */
    class Delimiter : IDelimiter
    {
        string delimiter;

        // コンストラクタで終端の文字を設定
        public Delimiter(string del)
        {
            delimiter = del;
        }

        // 引数に終端を追加して返す。
        public string AddDelimiter(string src)
        {
            return src + delimiter;
        }
    }

    /**
     * @brief   NullObjDelimter Class
     * @note    Delimiter NullObjectクラス。
     */
    class NullObjDelimter : IDelimiter
    {
        public string AddDelimiter(string src)  // 基本何もしない。
        {
            return src;
        }
    }
}
