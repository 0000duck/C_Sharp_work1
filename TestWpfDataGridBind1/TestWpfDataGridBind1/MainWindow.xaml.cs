//----------------------------------------------------------------------------
//  Wpf DataGrid Sample Project
//
//
//  DataGird の List は ObservableCollection を使用
//  Add は固定値追加
//  Del は index=2 を削除
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

using System.Collections.ObjectModel;
using System.ComponentModel;


namespace TestWpfDataGridBind1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        UserList usersList;

        public MainWindow()
        {
            InitializeComponent();

            usersList = new UserList();
            this.DataContext = usersList;

            
        }

        // 固定User 追加 (追加 Method動作確認のため実装 )
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            usersList.Users.Add(new User { Name = "Csan", Place = "TokyoC" });
        }

        // 固定List 削除 (削除 Method動作確認のため実装 )
        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            usersList.Users.RemoveAt(2);
        }
    }

    /**
     * @brief Userクラス
     * @note  User 情報保持クラス
     */
    public class User
    {
        public string Name { get; set; }
        public string Place { get; set; }
    }


    /**
     * @brief UserListクラス
     * @note Userクラスを List で保持するクラス
     *       ObservableCollection を List枠 として使用。
     */
    public class UserList
    {
        // ObservableCollection : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
        public ObservableCollection<User> Users { get; set; }

        // コンストラクタ
        public UserList()
        {
            // Defaultデータ作成
            Users = new ObservableCollection<User>   // これを DataGridで Binding
            {
                new User { Name="Asan", Place="TokyoA" },
                new User { Name="Bsan", Place="TokyoB" }
            };
        }
    }
}
