//-------------------------------------------------------------------
//   Reactive, Oservable, FromEventPatternを利用して
//   Mouse でキャンバスに線を描くサンプルプログラム
//-------------------------------------------------------------------
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


namespace TestRx_DrawByMouse
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //----- Canvas 上の Mouse 位置を textBlk に表示 ------------------
            Observable.FromEventPattern<MouseEventArgs>(this.Canvas, "MouseMove")
            .Do(_ => this.textBlk1.Text = null)
            .Select(pattern => pattern.EventArgs.GetPosition(this))
            /* ポジションを絞り込みたいときは以下 */
            /*.Where(position => (position.X > this.Canvas.ActualWidth / 2) &&
                               (position.Y > this.Canvas.ActualHeight / 2))*/
            .Subscribe(position => this.textBlk1.Text = string.Format("(X, Y) = {0}", position));

            //----- Canvas に Mouse で線を引く処理の設定 ---------------------
            // MouseMoveEvent を Mouse一に変換
            var mv = Observable.
                FromEventPattern<MouseEventArgs>(this.Canvas, "MouseMove").Select(x => x.EventArgs.GetPosition(null));
            
            // Mouse位置に変換したものを直前値とペアで合成
            mv.Zip(mv.Skip(1), Tuple.Create)
                // 合成したものを　Downは Skipし、Upは Take
                // skipUntil は OnNext などの Event発生するまで skipされる?
                .SkipUntil(Observable.FromEventPattern(this.Canvas, "MouseDown"))
                .TakeUntil(Observable.FromEventPattern(this.Canvas, "MouseUp"))
                .Repeat()   // 上記処理を繰り返す
                // Mouse位置を線に変換
                .Select(x => new Line
                {
                    X1 = x.Item1.X,
                    Y1 = x.Item1.Y,
                    X2 = x.Item2.X,
                    Y2 = x.Item2.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 3
                })
                .Subscribe(x => this.Canvas.Children.Add(x));   // 描画

            //--- 以下参考集 ----------------------------------------------------------------
            // Tupleの参考。 1, 2, 3 と出力される
            //var tupleData1 = Tuple.Create<int, int, int>(1, 2, 3);
            //Console.WriteLine(tupleData1.Item1);
            //Console.WriteLine(tupleData1.Item2);
            //Console.WriteLine(tupleData1.Item3);

            // Skip と Take 参考。
            var sb = new System.Reactive.Subjects.Subject<int>();
            // 最初の3つをスキップして、その後3つを通知する
            sb.Skip(3).Take(3).Subscribe(
                i => Console.WriteLine("OnNext({0})", i),
                ex => Console.WriteLine("OnError({0})", ex.Message),
                () => Console.WriteLine("OnCompleted()"));
            // 1-10の値を流し込む
            Observable.Range(1, 10).ForEach(i => sb.OnNext(i));
            // 出力結果は以下
            //  OnNext(4)
            //  OnNext(5)
            //  OnNext(6)
            //  OnCompleted()
        }

        // Canvas に 描画したものを削除
        private void Btn_ClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            this.Canvas.Children.Clear();
        }
    }
}
