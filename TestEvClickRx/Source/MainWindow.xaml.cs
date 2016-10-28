//-----------------------------------------------------------------
//    Reactive について調査実験した Peoject
//
//      button を押すと、次々ときになった Reactive の機能について
//      確認するために、書いたコードを実行します。
//  
//      ReactiveExtentions を使うために
//      [ソリューションエクスプローラ]→[参照]→[NuGetパッケージ管理]→
//      で ReactiveExtentions をインストールしました。
//-----------------------------------------------------------------
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


using System.Reactive.Linq; // ★Add


namespace TestEvClickRx
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            // MouseDown偶数回目 Event発生
            Observable.FromEventPattern(this, "MouseDown").Where((x, index) => (index + 1) % 2 == 0).Subscribe(x => MessageBox.Show("偶数回目のクリック"));


            // 足される数と足す数をCheck
            var l = Observable.FromEventPattern(this.textBox1, "TextChanged").Select(_ => double.Parse(textBox1.Text)).Retry();
            var h = Observable.FromEventPattern(this.textBox2, "TextChanged").Select(_ => double.Parse(textBox2.Text)).Retry();
            // 足し算し、結果を textBlkAns へ
            Observable.CombineLatest(l, h, (x, y) => x + y).Subscribe(x => this.textBoxAns.Text = x.ToString());
        }

        // Reactive の機能をいろいろ試すボタン
        private void button_Click(object sender, RoutedEventArgs e)
        {

            // 偶数で　Event発生
            Observable.Range(0, 10).Where(x => 0 == x % 2).Subscribe(rcvEv);


            // 10秒後に Event発生
            int i = 10;
            System.Reactive.Linq.Observable.Return(i).Delay(TimeSpan.FromSeconds(10)).Subscribe(rcvEv);


            // 始めの3回のみ Event
            System.Reactive.Linq.Observable.Range(0, 10).Take(3).Subscribe(rcvEv);


            // Subject を使って Observerパターン
            var o = new System.Reactive.Subjects.Subject<string>();
            o.Subscribe(x => Console.WriteLine(x));
            o.OnNext(DateTime.Now.ToString("HH:mm:ss"));    // 変更を通知


            // Where でフィルタリング
            var si = new System.Reactive.Subjects.Subject<int>();
            si.Where(x => x % 2 == 0).Subscribe(
               x => MessageBox.Show(string.Format("OnNext: {0}", x)),
               () => MessageBox.Show("OnCompleted")
            );
            si.OnNext(2);
            si.OnCompleted();


            // Where で絞りこみ、Select で変換
            var si2 = new System.Reactive.Subjects.Subject<int>();
            si2.Where(x => x % 2 == 0).Select(x => x * 2).Subscribe(
               x => MessageBox.Show(string.Format("OnNext: {0}", x)),
               () => MessageBox.Show("OnCompleted")
            );
            si2.OnNext(1);
            si2.OnNext(2);


            // 非同期処理　5秒後の時間を取得し、MsgBox に表示
            Observable.FromAsync(() => GetDayTime("After5sec=")).Subscribe(x => msgBox(x));
        }


        // MsgBox表示用メソッド
        public void msgBox(string value)
        {
            MessageBox.Show(value);
        }

        // Event処理用メソッド
        public void rcvEv(int value)
        {
            string wk = "count" + value.ToString();
            MessageBox.Show(wk);
        }

        // 5秒後に 日時文字列返すメソッド
        static async Task<string> GetDayTime(string value)
        {
            await Task.Delay(5000);
            return value + DateTime.Now.ToString("HH:mm:ss");
        }
        
    }
}
