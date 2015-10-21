//---------------------------------------------------------------------------------------------
//	
//	delegate、匿名Method、ラムダ式、ジェネリック SampleProject
//		ラムダ式、ジェネリック の記述例についてまとめてみました。
//		delegate は、delegate を使って、
//		独自文字列変換 ChgUnderToHyph()メソッドを使用する例です。
//		匿名Method は、0～引数値までを textBox3に表示するメソッドを定義、使用した例です。
//		(ラムダ式のベースが 匿名Method なので、例を記載してみました。)
//		ラムダ式 は、0～引数値までを textBox4に表示するメソッドを定義、使用した例です。
//		ジェネリックは、独自 SomeList<T> クラスを用意し、T に string や DateTime を
//		渡して使用した例です。
//
//---------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestDelegateTokumeiRamda
{
 

    public partial class Form1 : Form
    {
        // delegate宣言 (delegate:Methodを参照できるObject)
        delegate string ChangeUnderToHyphen(string str);
        delegate int int_int_delegate_def(int v1);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /**
         *  @brief  ChgUnderToHyph
         *  @param[in]  string  str 変換対象文字列
         *  @return     string  変換後、文字列
         *  @note       strの中から'_'を'-'に変換して返す。
         *              delegate関数として使用される
         */
        string ChgUnderToHyph(string str)
        {
            return str.Replace('_', '-');
        }

        /**
         *  @brief  setSize
         *  @param[in]  calfunc 引数2つ int型, 戻りint型 ラムダ式
         *  @return     void
         *  @note       calfunc実行して、正ならtextBox8、不ならtextBox9 に表示
         */
        void Calc(Func<int, int, int> calfunc)
        {
            int result = 0;
            result = calfunc(1, 2);                     // ラムダ式を使用
            if(result>0)
                textBox8.Text = result.ToString();
            else
                textBox9.Text = result.ToString();
        }

        

        /**
         *  @brief  button1_Click
         *  @param[in]  object      sender
         *  @param[in]  EventArgs   e
         *  @return     void
         *  @note       Test delegate
         */
        private void button1_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------
            //  delegate例
            //-----------------------------------------------
            // delegate Object生成
            ChangeUnderToHyphen chgstr = new ChangeUnderToHyphen( ChgUnderToHyph );
            // delegateを使って Method呼び出し
            textBox2.Text = chgstr(textBox1.Text);


            //-----------------------------------------------
            //  匿名Method 例
            //-----------------------------------------------
            // delegete使って 匿名Method 定義
            //      void型,引数int型1つ
            //      匿名Method は、delegateを介してのみcallする場合などに使用。
            //      ラムダ式が匿名Methodベースということもあり、ご紹介。
            int_int_delegate_def myfunc = delegate(int v1)
            {
                for (int i = 0; i < v1; i++)
                    textBox3.AppendText(i.ToString());
                return 0;
            };
            // 上記 匿名 Method実行
            myfunc(2);

            //-----------------------------------------------
            //  ラムダ式 例
            //-----------------------------------------------
            // 上記を ラムダ式にすると (C#3.0以降)
            // ラムダ式：実質的には匿名Method を定義するもの
            // Func(returnあり)、Action(returnなし)
            // Func や Actionは "定義ずみdelegate" とのこと。
            Func<int, int> myfunc2 = (int v1) =>
            {
                for (int i = 0; i < v1; i++)
                    textBox4.AppendText(i.ToString());
                return 0;
            };
            int iret = myfunc2(4);

            Action<int> myfunc3 = (int v2) =>
            {
                for (int i = 0; i < v2; i++)
                    textBox7.AppendText(i.ToString());
            };
            myfunc3(5);

            // ラムダ式を渡すことで、外部からの処理をカスタマイズ
            Func<int, int, int> plusCalc  = (int v1, int v2) => { return (v1 + v2); };
            Func<int, int, int> minusCalc = (int v1, int v2) => { return (v1 - v2); };
            Calc(plusCalc);     // 上記ラムダ式を引数に Calc call
            Calc(minusCalc);    // 上記ラムダ式を引数に Calc call



            //-----------------------------------------------
            //  ジェネリック例
            //-----------------------------------------------
            //  パラメータ化されたクラス SomeList<T>を使用して
            //  2種類のList を作成し、使用する。
            SomeList<string> strings = new SomeList<string>();
            strings.setSize(3);
            strings.Add("data1. ");
            strings.Add("data2. ");
            strings.Add("data3. ");
            for(int j=0; j<strings.getSize(); j++)
            {
                textBox5.AppendText(strings[j]);
            }

            // 2詰めは DateTimeクラスのデータを SomeListでリスト化
            SomeList<DateTime> dates = new SomeList<DateTime>();
            dates.setSize(2);
            dates.Add(DateTime.Now);
            Thread.Sleep(5000);
            dates.Add(DateTime.Now);
            for (int j = 0; j < dates.getSize(); j++)
            {
                textBox6.AppendText((dates[j].ToString()+" "));
            }
            
        }
    }

    /**
     *  @brief  SomeList Class
     *  @note   T:型パラメータ
     *          setSize()で指定さ T型List枠を持ち、Add()で itemを追加保持できるクラス
     *          ジェネリック確認用 ( 例のため size管理が甘いところがあります。 )
    */
    public class SomeList<T>
    {
        T[] someList = null;         // Tで受けたObjectのList枠準備
        int listIndex = -1;           // someList[] index
        int listSize = 0;


        /**
         *  @brief  setSize
         *  @param[in]  int  size    T[] List のサイズ
         *  @return     void
         *  @note       T[] List Object 生成
         */
        public void setSize(int size)
        {
            someList = new T[size]; // List生成
            listSize = size;
        }

        /**
         *  @brief  getSize
         *  @return     int T[] List のサイズ
         */
        public int getSize()
        {
            return listSize;
        }

        // インデクサ
        public T this[int index]
        {
            get { return someList[index]; }
            set { someList[index] = value; }
        }

        /**
         *  @brief  Add
         *  @param[in]  T   item    T[] List に追加する１つのObject
         *  @return     void
         *  @note       T[] List Object 生成
         */
        public void Add(T item)
        {
            if (someList != null)
            {
                someList[++listIndex] = item;
            }
        }

    }

}
