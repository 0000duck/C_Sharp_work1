//----------------------------------------------------------------
//  MVVM での Rectangle 追加表示、削除のサンプル Project
//
//  [内容]
//      MVVMで、DrawRectボタンを押されると1つRectangleを描き
//      DelRectボタンを押されると1つRectangleを消去する
//----------------------------------------------------------------
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

using System.Collections.ObjectModel;

namespace TestMvvmRectangle1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            vm = new ViewModel(this);
            DataContext = vm;
        }
    }

    /**
     *  @brief  RectItemクラス
     *  @note   Rectangle のための各プロパティ情報クラス
     *          ViewModelで使用
     */
    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }
        public SolidColorBrush Fill { get; set; }
    }

    /**
     *  @brief  ViewModelクラス
     *  @note   View と Binding できるように メソッドとメンバを定義
     */
    public class ViewModel
    {
        MainWindow mw;
        double rectX;
        int rectNo;
        public ICommand DrawRectCommand { get; set; }
        public ICommand DelRectCommand { get; set; }
        public ObservableCollection<RectItem> RectItems { get; set; }

        const int RECTANGLE_HEIGH_WIDTH = 20;

        /**
         *  @brief      ViewModelコンストラクタ
         *  @param[in]  MainWindow  View情報
         */
        public ViewModel(MainWindow _mw)
        {
            mw = _mw;       // MainWindow アクセス用
            //mw.TextBlock1.Text = "";

            // View上にある Control と Binding
            // Event発生時に動作するメソッド定義
            DrawRectCommand = new RelayCommand(drawRectangle);
            DelRectCommand = new RelayCommand(delRectangle);

            // View上にある Rectangleのための ItemControlとBinding
            RectItems = new ObservableCollection<RectItem>();
            rectX = 10;     // 1つめ描画の X座標
            rectNo = 0;
        }

        /**
         *  @brief      drawRectangle
         *  @note       Rectangle１つ描いて、次の描き開始位置を計算
         */
        void drawRectangle()
        {
            RectItems.Add( new RectItem
                { X = rectX, Y = 10, Width = RECTANGLE_HEIGH_WIDTH, Height = RECTANGLE_HEIGH_WIDTH, RadiusX = 2, RadiusY = 2, Fill = new SolidColorBrush(Colors.Aqua)}
            );

            rectX = rectX + RECTANGLE_HEIGH_WIDTH + 1;      // 次に描くX座標を変更
            rectNo++;
        }

        /**
         *  @brief  delRectangle
         *  @note   最後に描いたRectangleを削除
         */
        void delRectangle()
        {
            rectNo--;
            if (rectNo >= 0)
            {
                RectItems.RemoveAt(rectNo);                 // 1つ削除
                rectX = rectX - RECTANGLE_HEIGH_WIDTH - 1;  // 次に描くX座標を変更
            }
            else
            {
                rectNo = 0;
                rectX = 10;
            }
        }
    }
    

    // ViewModel用のコマンド用意
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
