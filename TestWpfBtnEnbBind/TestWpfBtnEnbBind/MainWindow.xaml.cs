//---------------------------------------------------
//  IsEnabled を ViewModelから変更する Sample
//
//  Button の IsEnabeld を例にしてあります。
//  ViewModel の IsRunning を true/false にすることで
//  Binding先のコントローラの IsEnabled Property を設定しています。
//
//  MainWindow.xaml には、
//    <DataTrigger Value = "True" Binding="{Binding IsRunning}">
//    <Setter Property="IsEnabled" Value="True"/>
//    または
//    <DataTrigger Value = "False" Binding="{Binding IsRunning}">
//    <Setter Property="IsEnabled" Value="False"/> を
//  を記載。
//---------------------------------------------------
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

namespace TestWpfBtnEnbBind
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();

            mvm = new MainViewModel();
            this.DataContext = mvm;
        }

        // MainWindow.xaml 参照用
        public void Execute_Click(object sender, RoutedEventArgs e)
        {
            mvm.Execute_Click();    // Binding IsEnabled用
        }

        // MainWindow.xaml 参照用
        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            mvm.Stop_Click();    // Binding IsEnabled用
        }
    }

    // ViewModel  
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                NotifyPropertyChanged("IsRunning");
            }
        }


        public void Execute_Click()
        {
            IsRunning = true;
        }

        public void Stop_Click()
        {
            IsRunning = false;
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
