//-----------------------------------------------------------------------
//  Control 追加、移動の Sample Project
//
//  [内容]
//     ボタンが押されるたびに Canvas に Control(thumb)を追加し
//     マウスクリックで移動できるサンプル改造
//  [改造点]
//     thumb に Template を使って、TextBlock表示できるようにした。
//      作成時は、BackをBitmap表示させている。右クリックでの
//      Background変更は、色を変え、Bitmap表示はさせていない。
//      急ぎ確認のため element はベタで作成しています。
//-----------------------------------------------------------------------
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

namespace TestWpfThumb4
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

        // 移動中の Thumb情報
        int thb_x;  // マウスでの移動前位置(Left)
        int thb_y;  // マウスでの移動前位置(Top)
        System.Windows.Controls.Primitives.Thumb currentThumb;   // 右クリック中の Thumb Object   


        // Thumbを Canvasに追加
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // 新規に Thumb を追加
            thb_x = 10;
            thb_y = 100;
            System.Windows.Controls.Primitives.Thumb thb;
            thb = new System.Windows.Controls.Primitives.Thumb();
            // Mouseでの移動、右クリックEvent登録
            thb.DragCompleted += new System.Windows.Controls.Primitives.DragCompletedEventHandler(mark_DragCompleted);
            thb.DragStarted += new System.Windows.Controls.Primitives.DragStartedEventHandler(mark_DragStarted);
            thb.DragDelta += new System.Windows.Controls.Primitives.DragDeltaEventHandler(mark_DragDelta);
            thb.MouseRightButtonDown += new MouseButtonEventHandler(thumb_MouseRightBtnDown);
            // Thumbの大きさと色指定
            thb.Width = 80;
            thb.Height = 20;
            thb.Name = "mark";
            // 要素指定
            FrameworkElementFactory element = new FrameworkElementFactory(typeof(TextBlock));
            element.SetValue(TextBlock.TextProperty, "TextBlock");
            element.SetValue(TextBlock.WidthProperty, 80.0);
            element.SetValue(TextBlock.HeightProperty, 20.0);
            //element.SetValue(TextBlock.BackgroundProperty, new SolidColorBrush(Colors.Aqua));
            var bmp = new BitmapImage(new Uri("migiya.png", UriKind.Relative));
            var br = new ImageBrush(bmp);
            element.SetValue(TextBlock.BackgroundProperty, br);

            //FrameworkElementFactory element = new FrameworkElementFactory(typeof(Button));
            //element.SetValue(Button.ContentProperty, "Button");
            //element.SetValue(Button.WidthProperty, 80.0);
            //element.SetValue(Button.HeightProperty, 20.0);
            //element.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Aqua));

            ControlTemplate template = new ControlTemplate(typeof(System.Windows.Controls.Primitives.Thumb));
            template.VisualTree = element;
            thb.Template = template;
            //thb.Background = Brushes.Green;
            //var bmp = new BitmapImage(new Uri("migiya.png", UriKind.Relative));
            //var br = new ImageBrush(bmp);
            //thb.Background = br;

            // 右クリックメニュー作成 ( BkBlue, BkLightBlue )
            ContextMenu cm = new ContextMenu();
            MenuItem mi1 = new MenuItem();
            mi1.Header = "BkBlue";
            mi1.Click += new RoutedEventHandler(thumbItem_MouseLeftBtnDown);
            cm.Items.Add(mi1);

            MenuItem mi2 = new MenuItem();
            mi2.Header = "BkLightBlue";
            mi2.Click += new RoutedEventHandler(thumbItem_MouseLeftBtnDown);
            cm.Items.Add(mi2);

            thb.ContextMenu = cm;       // Thumb に メニュー追加
            canvas1.Children.Add(thb);  // Canvas に Thumb追加

            // Canvas上のThumb表示位置指定
            Canvas.SetLeft(thb, 10);
            Canvas.SetTop(thb, 100);
        }

        // Thumb上で、右クリックしたときの処理
        void thumb_MouseRightBtnDown(object sender, RoutedEventArgs e)
        {
            // 右クリックした Thumb取得
            currentThumb = sender as System.Windows.Controls.Primitives.Thumb;
        }

        // Thumbで右クリックで表示した Itemを選択したときの 処理
        void thumbItem_MouseLeftBtnDown(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem; // MenuItem mi = (MenuItem)sender;
            string hdStr = (string)mi.Header;
            // 選択名に合わせて Background変更
            if (hdStr == "BkBlue") {
                FrameworkElementFactory element = new FrameworkElementFactory(typeof(TextBlock));
                element.SetValue(TextBlock.TextProperty, "TextBlock");
                element.SetValue(TextBlock.WidthProperty, 80.0);
                element.SetValue(TextBlock.HeightProperty, 20.0);
                element.SetValue(TextBlock.BackgroundProperty, new SolidColorBrush(Colors.Blue));
                ControlTemplate template = new ControlTemplate(typeof(System.Windows.Controls.Primitives.Thumb));
                template.VisualTree = element;
                currentThumb.Template = template;
            }
            if (hdStr == "BkLightBlue") {
                FrameworkElementFactory element = new FrameworkElementFactory(typeof(TextBlock));
                element.SetValue(TextBlock.TextProperty, "TextBlock");
                element.SetValue(TextBlock.WidthProperty, 80.0);
                element.SetValue(TextBlock.HeightProperty, 20.0);
                element.SetValue(TextBlock.BackgroundProperty, new SolidColorBrush(Colors.LightBlue));
                ControlTemplate template = new ControlTemplate(typeof(System.Windows.Controls.Primitives.Thumb));
                template.VisualTree = element;
                currentThumb.Template = template;
            }
        }

        // Thumb マウス移動終了 Event処理
        private void mark_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var thumb = sender as System.Windows.Controls.Primitives.Thumb;
            thumb.Background = Brushes.Green;
        }

        // Thumb マウス移動開始 Event処理
        private void mark_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            var thumb = sender as System.Windows.Controls.Primitives.Thumb;
            thumb.Background = Brushes.LightGreen;
        }

        // Thumb マウス移動中 Event処理
        private void mark_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // 移動量を計算し、Thumb Left/Top を設定
            // GetLeft, GetTop は開始座標、e.Horiとe.Verti は移動量
            var thumb = sender as System.Windows.Controls.Primitives.Thumb;
            int x = (int)Canvas.GetLeft(thumb);
            int y = (int)Canvas.GetTop(thumb);
            if (x >= 0) { thb_x = x; }
            if (y >= 0) { thb_y = y; }

            Canvas.SetLeft(thumb, (thb_x + e.HorizontalChange));
            Canvas.SetTop(thumb, (thb_y + e.VerticalChange));
        }
    }
}
