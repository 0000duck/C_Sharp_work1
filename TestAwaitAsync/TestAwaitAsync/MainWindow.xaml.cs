//------------------------------------------------------------------------------
//  await 他サンプル Project
//
//  async await
//      button は 1sec Sleep しながら label1 更新
//      button1 も同様だが、async,await を使用しているので、処理中も
//      Dummyボタンが押せる。その差をみるサンプル
//  他に C#6.0 で追加となった
//      Propetryの初期値設定
//      null条件演算子
//      async/wait によるTask制御。 button3 で開始、停止制御
//      の例も記載してある。
//------------------------------------------------------------------------------

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
using System.Collections;           // add for ArrayList

namespace TestAwaitAsync
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //----- Property の初期化 -----
        //public string Name { get; private set;  }
        // クラスのコンストラクタ内に Name = "namae" ; などと記述していたが
        //  C#6.0からは 以下のように記述可能
        public string Name { get; private set; } = "namae";


        //----- Async/Wait -----
        private void button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 3; i++)
            {
                System.Threading.Thread.Sleep(1000);
                label1.Content = i.ToString();
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Action func = async () =>
            {
                for (int i = 1; i <= 3; i++)
                {
                    await Task.Run(() => System.Threading.Thread.Sleep(1000));
                    label1.Content = i.ToString();
                }
            };
            //func();
            // Action myfunc = () => { 1 + 1; };
            //Action func   = () => { SampleTask(); } ;
            func();
        }



        //----- null条件演算子"?."  -----
        private void button2_Copy_Click(object sender, RoutedEventArgs e)
        {
            List<string> names = null;

            // if(names.Count!=null) { var cnt = names.Count }
            var cnt = names?.Count;

            names = new List<string>();
            names.Add("data1");
            names.Add("data2");
            cnt = names?.Count;
        }


        bool btn3Mode = false;      // false:Btn3表示"Start"、false:Btn3表示"End"
        bool stopTaskReq = false;   // true SampleTask 停止要求

        // Taskメソッド
        private async void SampleTask()
        {

            
            await Task.Run(() => {
                while (true)
                {
                    if (stopTaskReq == true)
                        break;
                    System.Threading.Thread.Sleep(1000);
                    
                    Console.WriteLine("written　by Thread.");
                    //label1.Content = "written　by Thread.";

                }
            });

            stopTaskReq = false;
            Console.WriteLine("Thread End.");
        }

        // Task停止要求 メソッド
        private void StopTask()
        {
            stopTaskReq = true;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (btn3Mode == false)
            {
                btn3Mode = true;
                button3.Content = "End Task";

                SampleTask();
            }
            else{
                btn3Mode = false;
                button3.Content = "Start Task";

                StopTask();
            }
        }
    }
}
