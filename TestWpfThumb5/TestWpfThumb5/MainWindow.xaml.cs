//-----------------------------------------------------------------------
//  Control 追加、移動の Sample Project (Thumb追加 XAML側編)
//
//  [内容]
//     ボタンが押されるたびに Canvas に Control(thumb)を追加し
//     マウスクリックで移動できるサンプル。
//     各Control右クリックで、BackColorを Bule/LigthBuleに変更できる
//     (ただし、マウス移動したら、元の色に戻ります。)
//  [Point]
//      Thumb 追加は、XAML側。ContextMenu ItemMenu選択後の処理を
//      cs側で処理
//     
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

using System.Windows.Controls.Primitives;   // Add


namespace TestWpfThumb5
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

        Thumb currentThumb = null; 

        // Thumb を 1つ追加
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // DragList is ItemsControl(at Mainwindow.xaml)
            DragList.Items.Add(DragList.Items.Count + 1);
        }

        //Thumbコントロールのドラッグイベント処理
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null) return;

            //親コントロールを探す
            //var parent = thumb.Parent();
            var parent = VisualTreeHelper.GetParent(thumb) as UIElement;
            if (parent == null) return;

            double x = Canvas.GetLeft(parent);
            if (double.IsNaN(x)) x = 0;         // はじめ初期化されてないので初期化
            double y = Canvas.GetTop(parent);
            if (double.IsNaN(y)) y = 0;         // はじめ初期化されてないので初期化

            //ドラッグ量に応じてThumbコントロールを移動する
            Canvas.SetLeft(parent, x + e.HorizontalChange);
            Canvas.SetTop(parent, y + e.VerticalChange);
        }

        // Thumbの 色変更
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem; // MenuItem mi = (MenuItem)sender;
            string hdStr = (string)mi.Header;
            // 選択名に合わせて 楕円の色変更
            if (currentThumb == null) { return; }
            if (hdStr == "BkBlue")
            {
                //FrameworkElementFactory element = new FrameworkElementFactory(typeof(Ellipse));
                //element.SetValue(Ellipse.FillProperty, new SolidColorBrush(Colors.Blue));
                //element.SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.Black));
                //ControlTemplate template = new ControlTemplate(typeof(System.Windows.Controls.Primitives.Thumb));
                //template.VisualTree = element;
                //currentThumb.Template = template;
                //currentThumb.Template.VisualTree.SetValue(Ellipse.FillProperty, (new SolidColorBrush(Colors.Blue)));
                //currentThumb.Template.VisualTree.SetValue(TextBlock.TextProperty, "test");

                var elps = currentThumb.Template.FindName("daen", currentThumb) as Ellipse;
                elps.Fill = new SolidColorBrush(Colors.Blue);
                elps.Stroke = new SolidColorBrush(Colors.Black);
            }
            else if (hdStr == "BkLightGreen")
            {
                var elps = currentThumb.Template.FindName("daen", currentThumb) as Ellipse;
                elps.Fill = new SolidColorBrush(Colors.LightGreen);
                elps.Stroke = new SolidColorBrush(Colors.Green);
            }
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            
        }

        private void Thumb_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentThumb = sender as Thumb;
        }
    }

    // Comboboxように用意。未使用となった
    public class ComboboxColorData
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
