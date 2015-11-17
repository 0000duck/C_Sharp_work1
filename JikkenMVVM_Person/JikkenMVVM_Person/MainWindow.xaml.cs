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

namespace JikkenMVVM_Person
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var p = new PersonCommandViewModel()
            {
                FirstName = "yama",
                LastName = "to",
                Age = 100,
            };

            this.DataContext = p;
        }
    }

    // ViewModel
    public class PersonCommandViewModel : INotifyPropertyChanged
    {
        // View で binding する コマンドMethod定義
        public ICommand IncrementAge { get; set; }
        public ICommand DecrementAge { get; set; }

        public PersonCommandViewModel()
        {
            IncrementAge = new RelayCommand(() => { Age++; });
            DecrementAge = new DecrementAgeCommand(this);
        }

        private string firstNameVal;
        public string FirstName
        {
            get { return firstNameVal; }
            set
            {
                firstNameVal = value;
                NotifyPropertyChanged("FirstName");	// FirstName Property変更を通知。 Viewに伝わる
                NotifyPropertyChanged("FullName");	// FullName  Property変更を通知。 Viewに伝わる
            }
        }

        private string lastNameVal;
        public string LastName
        {
            get { return lastNameVal; }
            set
            {
                lastNameVal = value;
                NotifyPropertyChanged("LastName");	// LastNAme  Property変更を通知。 Viewに伝わる
                NotifyPropertyChanged("FullName");	// FullName  Property変更を通知。 Viewに伝わる
            }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
            set
            {
                var names = value.Split(new[] { ' ' });
                if (names.Length == 2)
                {
                    FirstName = names[0];	// ここで、FirstName の get を call
                    LastName = names[1];	// ここで、LasttName の get を call
                }
                else
                {
                    FirstName = value;	// ここで、FirstName の get を call
                    LastName = "";		// ここで、LastName の get を call
                }
                NotifyPropertyChanged("FullName");	// FullName  Property変更を通知。 Viewに伝わる
            }
        }

        private int ageVal;
        public int Age
        {
            get { return ageVal; }
            set
            {
                ageVal = value;
                NotifyPropertyChanged("Age");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    // ViewModel用のコマンド用意 ( DecrementAge )
    public class DecrementAgeCommand : ICommand
    {
        private PersonCommandViewModel vm;

        public DecrementAgeCommand(PersonCommandViewModel viewmodel)
        {
            vm = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return vm.Age > 0;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            vm.Age--;
        }
    }

    // ViewModel用のコマンド用意 ( IncrementAge )
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
