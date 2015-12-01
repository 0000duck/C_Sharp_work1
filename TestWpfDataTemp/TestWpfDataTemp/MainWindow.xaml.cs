//--------------------------------------------------------------
// DataTemplate Sample Project
//
//  UI Control の Contents の外観を指定する
//  DataTemplate 例
//  ColorViewModel Class 追加
//  MainWindow.xaml で、Rectangle に Binding し、combobox で使用
//--------------------------------------------------------------
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

namespace TestWpfDataTemp
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

        public static IList<ColorViewModel> ColorList {  get { return colors; } }

        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
