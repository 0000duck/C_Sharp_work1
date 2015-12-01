//---------------------------------------------------------------------
//  ItemsControl確認用Sample Project
//
//  ItemsControl 
//      ItemsPanel
//          Container
//              Item
//  の階層を確認するための Sample Project
//  csファイルで、DataContext に Text="a", X=20, Y=80 から
//  Text="d", X=20, Y=20 まで生成し、接続
//  xaml の方で、ButtonのContent、Canvasの LeftとTopで Binding
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

namespace TestWpfItemsCnt1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new[]
            {
                new { Text="a", X=20, Y=80 },
                new { Text="b", X=80, Y=80 },
                new { Text="c", X=80, Y=20 },
                new { Text="d", X=20, Y=20 },
            };
        }
    }

}
