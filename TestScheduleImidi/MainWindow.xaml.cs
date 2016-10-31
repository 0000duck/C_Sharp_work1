//-------------------------------------------------------------------------
// Rx(ReactiveExtensions) Scheduler 動作確認 Sample Project
//
//    Scheduler と スレッドでのコントロールアクセスを
//    試すために作成した Project です。
//    今回は CurrentThread, Immeditate, NewThread,TaskPoolを試しました。
//    [info]
//      CurrentThreadScheduler：現在実行中のスレッド上で処理を行います。
//                             処理はキューに登録されたものから順に処理されます。  
//      ImmediateScheduler：現在実行中のスレッド上で処理を行います。処理は即座に実行されます。  
//      NewThreadScheduler：処理をそれぞれ別スレッドで行います。  
//      EventLoopScheduler：指定されたスレッド上で処理を行います。  
//      ThreadPoolScheduler：処理をスレッドプール上で行います。  
//      TaskPoolScheduler：指定されたTaskFactoryを利用して処理を行います。  
//-------------------------------------------------------------------------

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


using System.Reactive.Concurrency;  // add


namespace TestScheduleImidi
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // ScheduleTasksで、CurrentThreadと Immediate を確認
            Console.WriteLine("----- CurrentThread -----");
            ScheduleTasks(Scheduler.CurrentThread);

            // Immediate は、先に随時開始してしまう
            Console.WriteLine("----- Immediate -----");
            ScheduleTasks(Scheduler.Immediate);



            // 直ラムダ式で、現在プロセスで実行
            Scheduler.CurrentThread.Schedule( () => { Console.WriteLine("CurSche RamdaFunc"); txtBox1.Text = "1"; } );


            // スレッド作って、実行。 delegateなしに txtBox1 アクセス不可
            Scheduler.NewThread.Schedule(() => {
                Console.WriteLine("NewSche RamdaFunc");
                SetTxtBox("set by thread.");
            });


            // TaskPool でスレッド動かしてみた
            Scheduler.TaskPool.Schedule(() => { Console.WriteLine("TaskSche RamdaFunc"); });   

        }


        /**
         *  @brief      SetTxtBox
         *  @param[in]  string  strv    txtBox への設定文字列
         *  @return     void
         *  @note       前(Winアプリ)では、delegateでスレッドからのControlアクセス
         *              を制御したが、今は、Dispatcher.BeginInvokeで行うらしい。
         *              そのとき ラムダ式を使うので、書式をいくつか試してみた
         */
        private void SetTxtBox(string strv)
        {
            //Invoke：Main Threadの作業が終わるまでまって、Work Threadの次の作業に入る
            //BeginInvoke：Main Threadの作業を待たずにWork Threadがどんどん作業して結果を寄せる

            //this.Dispatcher.Invoke((Action)(() =>
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                txtBox1.Text = strv;
            }));
            
            // 上記を ラムダ式で実現
            Action<string> funcs = (string x) => { txtBox2.Text = x; };
            this.Dispatcher.BeginInvoke(funcs, strv);
            
            // 上記を 直でラムダ式を書く
            this.Dispatcher.BeginInvoke((Action<string>)((x) =>
            {
                txtBox3.Text = x;
            }), strv);

        }


        /**
         *  @brief      ScheduleTasks
         *  @param[in]  IScheduler  scheduler    種類(CurrentThread/Immediate)
         *  @return     void
         *  @note       Scheduleを使って、スレッドのラムダ式メソッドを実行
         *              CurrentThreadは、順次動作、Immediateは、瞬時開始するが、動作順は不明
         */
        void ScheduleTasks(IScheduler scheduler)
        {
            Action third = () =>
            {
                Console.WriteLine("#3");
            };

            Action second = () =>
            {
                Console.WriteLine("#2 : Start");
                scheduler.Schedule(third);
                Console.WriteLine("#2 : End");
            };

            Action first = () =>
            {
                Console.WriteLine("#1 : Start");
                scheduler.Schedule(second);
                Console.WriteLine("#1 : End");
            };

            // 上記までは、ラムダ式宣言なので、以下から実行
            scheduler.Schedule(first);
        }

        /**
         *  @brief      button_Click
         *  @param[in]  object sender
         *  @param[in]  RoutedEventArgs e
         *  @return     void
         *  @note       button_click Event での listBox追加動作確認用。
         *              昔は listObjectとかのアクセスは GUIのスレッド上ではアクセスできず
         *              Invoke をつかったらしい。
         */
        private void button_Click(object sender, RoutedEventArgs e)
        {
            txtBox1.Text = "btn1";

            listBox1.Items.Clear();
            listBox1.Items.Add("d1");
            listBox1.Items.Add("d2");
        }

        /**
         *  @brief      button1_Click
         *  @param[in]  object sender
         *  @param[in]  RoutedEventArgs e
         *  @return     void
         *  @note       Scheduleを使って、スレッドのラムダ式メソッドを実行
         *              CurrentThreadは、順次動作、Immediateは、瞬時開始するが、動作順は不明
         */
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            this.button1.IsEnabled = false;
            await Task.Run( () => TaskFunc1() );
            this.button1.IsEnabled = true;

        }

        /**
         *  @brief      TaskFunc1
         *  @return     void
         *  @note       async button1_Click での非同期動作用Task 試に5秒Sleep
         *              その間 ButtonClick Event処理終了していなくても Form固まらない。
         */
        void TaskFunc1()
        {
            SetTxtBox("set by thread.2");
            System.Threading.Thread.Sleep(5000);
        }

        
    }
}
