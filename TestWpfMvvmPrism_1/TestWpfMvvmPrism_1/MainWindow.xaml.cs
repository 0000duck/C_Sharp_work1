//----------------------------------------------------------
//  Prism.BindableBase/Command Sample Project
//
//   Prism.BinableBase/Comand の
//   SetProperty と DelegateCommand を使用した Sample
//
//  ・Sampleの概要
//      Name用TextBoxは、GUIからの記入を PropertyChanged Eventで、 Name Propertyに通知
//      Age用TextBoxは、Age++/-- ボタンの DelegateCommand IncrementAge/DecrementAge()実行時
//      Age Propertyを変更し、それを SetPropertyで TextBoxに通知
//
//  ・Prism Project追加方法
//      ソリューションエクスプローラ、Project名で右クリックし
//      "NuGetのパッケージ管理"を選択。NuGetパッケージマネージャTagが起動するので
//      検索覧に "Prism Core"と入力して、検索。Prism Core が表示されたら
//      クリックして、選択し、インストールを押す。Prism追加完了
//      cs に "using Prism.Commands;"、"using Prism.Mvvm;"の記述を追加
//
//----------------------------------------------------------
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

using Prism.Commands;           // add
using Prism.Mvvm;               // add for BindableBase

namespace TestWpfMvvmPrism_1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    /// 

    /**
     * @brief   MaiWindow Class
     * @note    View
     */
    public partial class MainWindow : Window
    {
        MainWindowViewModel mvm;

        public MainWindow()
        {
            InitializeComponent();

            // ViewModle 生成
            mvm = new MainWindowViewModel();
            this.DataContext = mvm;

        }
    }


    /**
     * @brief   MainWindowViewModel Class
     * @note    ViewModel ( used Prism.BindableBase )
     */
    public class MainWindowViewModel : BindableBase
    {
        // コンストラクタ
        public MainWindowViewModel()
        {
            // 空の PropertyChange Event登録 ( 今回未使用 )
            //PropertyChanged += mvm_PropertyChanged;    // PropertyChanged exists BindableBase
        }
        //private void mvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    this.OnPropertyChanged(e.PropertyName);  // OnPropertyChanged exist BindableBase
        //}

        
        // TextBox(txtBox_Name) Binding用Property ( xaml -> cs へ通知 )
        //   xaml側に Binding記述あり (Text="{Binding Name, UpdateSourceTrigger=LostFocus}")
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                this.SetProperty(ref this.name, value);
                MessageBox.Show("Changed Name.");
            }
        }

        // TextBox(txtBox_Age) Binding用Property ( cs -> xaml へ通知 )
        //   xaml側に Binding記述あり (Text="{Binding Age}")
        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                this.SetProperty(ref this.age, value);
            }
        }

        // Button(Btn_IncAge) Binding 用 処理メソッド 
        private DelegateCommand incrementAge;      // DelegateCommand exists BindableBase
        public DelegateCommand IncrementAge
        {
            get
            {
                return this.incrementAge ??
                    (this.incrementAge = new DelegateCommand(IncrementAgeCmdExecute, CanCmdExecute));
            }
        }
        private void IncrementAgeCmdExecute()
        {
            Age++;
        }

        // Button(Btn_IncAge) Binding 用 処理メソッド 
        private DelegateCommand decrementAge;      // DelegateCommand exists BindableBase
        public DelegateCommand DecrementAge
        {
            get
            {
                return this.decrementAge ??
                    (this.decrementAge = new DelegateCommand(DecrementAgeExecute, CanCmdExecute));
            }
        }
        private void DecrementAgeExecute()
        {
            Age--;
        }

        // DelegateCommand 実行可能かCheckメソッド
        //   今回は Inc/Dec共通で、いつでも実行可能とし、trueのみ返信
        private bool CanCmdExecute()
        {
            return true;
        }
    }
}
