//--------------------------------------------------------
//  MVVM での Rectangle 追加表示、削除のサンプル Project
//
//  [内容]
//      MVVMで、DrawRectボタンを押されると1つRectangleを描き
//      DelRectボタンを押されると1つRectangleを消去する
//      scrollviewer+Canvas内に描き、sliderで
//      拡大/縮小もできる。ただし、Rectangleの追加は
//      横方向のみ。scrollviewerの枠を超えての追加は
//      存在するが、Formには表示されない。スクロールも
//      範囲以上はできない。実際には、行を折り返して
//      表示する処理も実装が必要ですが、サンプルの目的は
//      MVVMでの、Rectangle追加削除なのでご了承ください。
//--------------------------------------------------------
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
using System.ComponentModel;


namespace TestMvvmRectangle3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vmodel;

        public MainWindow()
        {
            InitializeComponent();
            vmodel = new ViewModel(this);
            DataContext = vmodel;
        }
    }

    /**
     *  @brief  TxtBlkItem
     *  @note   TextBlock のための各プロパティ情報クラス
     *          ViewModelで使用
     */
    public class TxtBlkItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; }
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
    public class ViewModel : INotifyPropertyChanged
    {
        MainWindow mainWin;
        double rectX;
        int rectNo;
        public ICommand DrawRectCommand { get; set; }
        public ICommand DelRectCommand { get; set; }

        public ObservableCollection<RectItem> RectItems { get; set; }
        public ObservableCollection<TxtBlkItem> TxtBlkItems { get; set; }

        const int RECTANGLE_HEIGH_WIDTH = 20;

        /**
         *  @brief      ViewModelコンストラクタ
         *  @param[in]  MainWindow  View情報
         */
        public ViewModel(MainWindow _mw)
        {
            mainWin = _mw;       // MainWindow アクセス用

            // View上にある Control と Binding
            // Event発生時に動作するメソッド定義
            DrawRectCommand = new RelayCommand(drawRectangle);
            DelRectCommand = new RelayCommand(delRectangle);

            // View上にある Rectangleのための ItemControlとBinding
            RectItems = new ObservableCollection<RectItem>();
            TxtBlkItems = new ObservableCollection<TxtBlkItem>();
            rectX = 10;         // 1つめ描画の X座標
            rectNo = 0;
        }

        // slider と Binding用プロパティ
        private double currentSliderVal;
        public double CurrentSliderVal
        {
            get { return currentSliderVal; }
            set
            {
                currentSliderVal = value;
                NotifyPropertyChanged("CurrentSliderVal");
                slideValueChange(value);
            }
        }

        // Map表示の Canvasの拡大縮小時の倍率を保持
        double canvasWidthMagnifi = 1;
        double canvasHeightMagnifi = 1;

        /**
         *  @brief      sliderValueChange
         *  @param[in]  double  _valuse slider値
         *  @note       sliderとBindingした CurrentSliderVal で
         *              取得した slider値を取得し、 Canvasの拡大/縮小
         *              を行う
         */
        void slideValueChange(double _value)
        {
            // 1.x 倍？
            // slider.Value値が、0.0x ぐらいなので、それを基に計算
            // 1～10 TickFrequency=1
            double width_magnifi = 1 + _value;
            double height_magnifi = 1 + _value;

            // 縦横それぞれの倍率を保持。Mouse左クリック時使用
            canvasWidthMagnifi = width_magnifi;
            canvasHeightMagnifi = height_magnifi;

            // xaml で宣言ずみ、canvas上 mtrans に設定
            Matrix m0 = new Matrix();
            m0.Scale(width_magnifi, height_magnifi);
            mainWin.mtrans.Matrix = m0;

            // スクロールバーの範囲を拡大/縮小するために
            // canvasの大きさをcanvasの拡大縮小に合わせて、再設定
            mainWin.myCanvas.Width = mainWin.scrollViewer1.Width * width_magnifi;
            mainWin.myCanvas.Height = mainWin.scrollViewer1.Height * height_magnifi;
        }

        // 各Property Binding 用
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /**
         *  @brief      drawRectangle
         *  @note       RectangleとTextBlock１つ描いて、
         *              次の描き開始位置を計算
         */
        void drawRectangle()
        {
            RectItems.Add(new RectItem
            { X = rectX, Y = 10, Width = RECTANGLE_HEIGH_WIDTH, Height = RECTANGLE_HEIGH_WIDTH, RadiusX = 2, RadiusY = 2, Fill = new SolidColorBrush(Colors.Aqua) }
            );

            TxtBlkItems.Add(new TxtBlkItem
            { Text = rectNo.ToString(), X = rectX, Y = 10, Width = RECTANGLE_HEIGH_WIDTH, Height = RECTANGLE_HEIGH_WIDTH }
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
                RectItems.RemoveAt(rectNo);                 // 1つ削除 (Rectangle+TextBlock)
                TxtBlkItems.RemoveAt(rectNo);

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
