//----------------------------------------------------------------------------
//	IsEnabled を xaml の Binding のみでの設定(button1)と 
//	Style 参照後、 Binding と INotifyPropertyChanged 
//	で、設定(button2)する場合 の Sample Project
//
//	button1
//		xaml側
//			<Button x:Name="button1" Content="Btn1"  IsEnabled="{Binding BtnsEnb}" ... 
//		cs側
//			mvm.BtnsEnb = false;
//
//	button2　(Enb/Dsbの設定は特にしていません。)
//      xaml側
//          <Style x:Key="btn2EnbDsb" TargetType="Button">
//		    <Button x:Name="button2" Content="Btn2"  Style="{StaticResource btn2EnbDsb
//      cs側
//          特になし。
//----------------------------------------------------------------------------
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
using System.Timers;

namespace TestWpfBtnEnb2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        Timer OneShotOneSecTimer;

        public MainWindow()
        {
            InitializeComponent();

            mvm = new MainViewModel();
            this.DataContext = mvm;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //mvm.Btn1Enb = true;
            mvm.Btn1Enb = false;                     // button1禁止

            OneShotOneSecTimer = new Timer(5000);
            OneShotOneSecTimer.Elapsed += timeoutProc;
            OneShotOneSecTimer.AutoReset = false;    //  Event1回のみ発生
            OneShotOneSecTimer.Start();
        }

        // Timer timeout 処理(callback)
        void timeoutProc(object sender, ElapsedEventArgs e)
        {
            OneShotOneSecTimer.Stop();
            mvm.Btn1Enb = true;                     // button2許可
            Console.WriteLine("Timer timeout.");
        }
    }


    // ViewModel
    //
    //  button1と2 の Enb/Dsb bool Property を用意
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        bool _btn1Enb;
        public bool Btn1Enb
        {
            get { return _btn1Enb; }
            set { _btn1Enb = value; NotifyPropertyChanged("Btn1Enb"); }
        }

        bool _btn2Enb;
        public bool Btn2Enb
        {
            get { return _btn2Enb; }
            set { _btn2Enb = value; NotifyPropertyChanged("Btn2Enb"); }
        }
    }
}
