//------------------------------------------------------------------------------
//	MVVM Sample Project
//
// 
//	View
//		MainWindow.xaml	：画面構成
//		MainWindow.cs	：画面の Instance時処理。
//	
//	ViewModel
//		MainViewModel.cs	：Viewとの結合処理。 ICommand、INotifyChaned実装
//							　Model の Object もここで生成。(←この設計が良いかは？)
//	
//	Model
//		MainModel.cs		：Model処理。 Task(スレッド)で、中心の状態遷移を管理
//
//	[詳細]:TestWpfMvvmModelTask_について.txt 参照
//------------------------------------------------------------------------------
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

namespace TestWpfMvvmModelTask
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
            this.DataContext = mvm;			// View へ ViewModel 結合
        }

        // ×ボタン処理
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mvm.EndProc();
        }
    }
}
