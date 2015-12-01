//--------------------------------------------------------------------
//  MultiBinding Sample Project
//
//  IMultiValueConverter 継承した MyAddString を作り
//  textBlock に Binding
//  MainViewModel 継承した INotifyPropertyChanged を作り
//  textBox(Pre/Aft) に Binding。 textBlock にも Multi Binding
//  textBox Pre/Aft どちらか 文字入力すると Pre+Aft で textBlockに
//  文字列表示
//   
//--------------------------------------------------------------------
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
//using System.Windows.Input;
using System.Globalization;     // for CultureInfo


namespace TestWpfMutiBind
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            vm = new MainViewModel();
            this.DataContext = vm;
        }
    }

    /**
     * @brief ViewModel
     * @note  AftString, PreString Binding用
     */
    public class MainViewModel : INotifyPropertyChanged
    {
        // 結合用前文字列
        private string aftString;
        public string AftString
        {
            get { return aftString; }
            set
            {
                aftString = value;
                NotifyPropertyChanged("AftString");
            }
        }

        // 結合用後文字列
        private string preString;
        public string PreString
        {
            get { return preString; }
            set
            {
                preString = value;
                NotifyPropertyChanged("PreString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /**
     * @brief MyAddString
     * @note  AftString, PreString Multi Binding用
     */
    public class MyAddString : IMultiValueConverter
    {
        // ソース値をバインディング ターゲットの値に変換
        public object Convert(object[] value, Type type, object parameter, CultureInfo culture)
        {
            string aft = (string)value[0];
            string pre = (string)value[1];
            return string.Format("{0} {1}", pre, aft);
        }

        // バインディング ターゲット値をソース値に変換
        public object[] ConvertBack(object value, Type[] type, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            string[] items = name.Split(' ');
            return new string[] { items[1], items[0] };
        }

    }

}
