//----------------------------------------------------
//  ArrayList と List<T> の使い方比較 Sample Project
//  
//  ITestClassAB を継承した TestClassA, TestClassB を用意
//  それぞれを、ArrayList と List に入れて確認
//  Listは string 型もためした。
// 
//  ●ArrayList
//	  可変サイズの一次元リスト・コレクション
//
//  特徴
//      ArrayListには格納するオブジェクトの型名の指定をせず，
//	    格納されるときにすべてObject型にアップキャストされる。
//  メリット
//      同じ ArrayList に何でも入れられる。
//  デメリット
//    値を呼び出して使用する際にはキャストが必要である。
//	  foreach文などで順に処理する際、
//	  異なる方のオブジェクトが混ざっているとエラーになってしまうことがある。
//
//  ●List
//    ジェネリックの仕組みを使った Listジェネリッククラス
//  特徴
//      型の指定をしてインスタンス化
//  メリット
//      値を呼び出して使用する際，キャストが不要である．
//  デメリット
//      ？
//----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestListOrArrayList
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //----------------------------------
            // Example ArrayList
            //----------------------------------
            System.Collections.ArrayList alist;
            alist = new System.Collections.ArrayList();
            alist.Add(new TestClassA());
            alist.Add(new TestClassB());

            // forreach参照
            foreach (Object _list in alist)
            {
                ITestClassAB classAB = (ITestClassAB)_list;
                Console.WriteLine(classAB.GetName());
            }

            // index参照
            for (int i = 0; i < alist.Count; i++)
            {
                ITestClassAB classAB = (ITestClassAB)alist[i];
                Console.WriteLine(classAB.GetName());
            }

            // list 1つ削除
            //alist.Remove(alist[0]);
            //ITestClassAB classAB1 = (ITestClassAB)alist[0];
            //string str = classAB1.GetName();

            //----------------------------------
            // Example List<T>
            //----------------------------------
            List<string> strList = new List<string>();
            strList.Add("Sample1.");
            strList.Add("Sample2.");

            // forreach参照
            foreach (string strdata in strList)
            {
                Console.WriteLine(strdata);
            }

            // index参照
            for (int i = 0; i < strList.Count; i++)
            {
                Console.WriteLine(strList[i]);
            }

            // list 1つ削除
            //strList[0].Remove(0);


            //----------------------------------
            // Example List<T>, その2
            //----------------------------------
            List<ITestClassAB> list = new List<ITestClassAB>();
            list.Add(new TestClassA());
            list.Add(new TestClassB());

            // forreach参照
            foreach (ITestClassAB testclass in list)
            {
                Console.WriteLine(testclass.GetName());
            }

            // index参照
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].GetName());
            }
        }
    }

    public interface ITestClassAB
    {
        string GetName();
    }

    public class TestClassA : ITestClassAB
    {
        public TestClassA()
        {
            Name = "default.A";
        }

        private string Name;
        public string GetName() { return Name; }
    }

    public class TestClassB : ITestClassAB
    {
        public TestClassB()
        {
            Name = "default.B";
            IdNo = 0;
        }
        private string Name;
        public int IdNo { set; get; }

        public string GetName() { return Name; }
    }
}
