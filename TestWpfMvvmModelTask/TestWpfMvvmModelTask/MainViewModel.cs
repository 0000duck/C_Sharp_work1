using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.ComponentModel;

namespace TestWpfMvvmModelTask
{
    /**
     * @breif   MainWindow用 ViewModel クラス
     * @note    MainWindow Binding用 ICommand、INotifyPropertyChanged
     *          実装
     */
    class MainViewModel : INotifyPropertyChanged
    {
        public ICommand StartCommand { get; set; }
        public ICommand AbortCommand { get; set; }
        public ICommand EndCommand { get; set; }
        MainModel mainModel;

        // コンストラクタ
        public MainViewModel()
        {
            mainModel = new MainModel(this);							// Model生成
            															// View のために以下用意
            StartCommand = new RelayCommand(mainModel.StartCommand);	// StartCommand用意
            AbortCommand = new RelayCommand(mainModel.AbortCommand);	// AbortCommand用意
            EndCommand   = new RelayCommand(mainModel.EndCommand);		// EndCommand用意
        }

        public void EndProc()
        {
            mainModel.EndCommand();
        }
        
        private string currentStateVal;
        public string CurrenState
        {
            get { return currentStateVal; }
            set {
                currentStateVal = value;
                NotifyPropertyChanged("CurrenState");
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
