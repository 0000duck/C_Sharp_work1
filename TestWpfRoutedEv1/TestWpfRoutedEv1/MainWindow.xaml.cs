//---------------------------------------------------------------------
//  RoutedEvent Sample Project
//
//  [ RoutedEvent ]
//  ルーティング・イベントを利用することで、UI要素で発生したイベントを、
//  その親要素で一括して処理できる
//  [ Sample内容 ]
//  <ListBox>要素でButton.Clickイベントを拾い、
//  子要素として追加した<Button> 要素のClickイベントを一括処理している。
//  <Button>要素1つ1つにイベント・ハンドラを追加しなくても、
//  <ListBox>要素側の1度きりの追加でイベントの処理が可能になっている。
//---------------------------------------------------------------------
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

namespace TestWpfRoutedEv1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //this.DataContext = new[]
            this.list.ItemsSource = new[]
            {
              "A", "B", "C",
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            MessageBox.Show(button.DataContext.ToString());
        }
    }

    // Color List Class
    public class ColorViewModel
    {
        private static IList<ColorViewModel> colors = new List<ColorViewModel>
            {
                new ColorViewModel { Name = "Red", Color = Colors.Red },
                new ColorViewModel { Name = "Yellow", Color = Colors.Yellow },
                new ColorViewModel { Name = "Blue", Color = Colors.Blue }
            };

        public static IList<ColorViewModel> ColorList { get { return colors; } }

        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
