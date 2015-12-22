//-------------------------------------------------------------------------
//  nameof Sample Project
//
//  nameof は、引数に与えた、変数やメソッドの名前を返す。
//  そこで、sample では、textBlock 用 PropertyChange用変数
//  TextBlockData に nameof を使用。
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

using System.ComponentModel;

namespace TestWpfNameOf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mvm;

        public MainWindow()
        {
            InitializeComponent();

            // ViewModel作成
            mvm = new MainWindowViewModel();

            // ViewModel を MainWindow.xaml に設定
            this.DataContext = mvm;
        }

        /**
         * @brief   button1 Click
         * @note    TextBlock に固定文字列設定
         */
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Binding してある TextBlockDataに値を設定して
            // TextBlock に反映
            mvm.TextBlockData = "set data by button_click.";
        }
    }

    /**
     * @brief   MainWindowViewModel Class
     * @note    MainWindow用ViewModel
     */
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string textBlockData;
        public string TextBlockData  // <--- MainWindow.xaml textBlock1に Bindingされる
        {
            get { return textBlockData; }
            set {
                textBlockData = value;

                string wk = nameof(TextBlockData);
                MyNotifyPropertyChanged(nameof(TextBlockData));
                // 上記、nameof を用いないと以下
                // MyNotifyPropertyChanged("TextBlockData");
            }
        }

        protected void MyNotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
