using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace C_WPF_APP.Model
{
    internal class Model
    {
        //--------------------------------------------
        // プロパティ
        //--------------------------------------------
        // 作業対象のオブジェクト
        public Memo MemoX { get; set; }
        // 編集用のオブジェクト
        public Memo TmpMemoX { get; set; }
        // メモ起動時画面 バインディング用
        public ObservableCollection<Memo> MemoList { get; set; }
        
        // ページの表示
        public bool IsShownMemoStart { get; set; }
        public bool IsShownMemoNew { get; set; }
        

    }

    // メモ格納用クラス
    class Memo : NoticePrpChange
    { 

        private int _id;
        private string _title;
        private string _content;
        private bool _flg;
        /// <summary>
        /// メモID
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        /// <summary>
        /// メモタイトル
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        /// <summary>
        /// メモ内容
        /// </summary>
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content)); ;
            }
        }
        /// <summary>
        /// 重要フラグ
        /// </summary>
        public bool IsImportant
        {
            get => _flg;
            set
            {
                _flg = value;
                OnPropertyChanged(nameof(IsImportant));
            }
        }
    }

    /// <summary>
    /// プロパティ変更通知用クラス
    /// </summary>
    class NoticePrpChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    class Command : ICommand
    {
        private Action _action;
        public event EventHandler? CanExecuteChanged;

        public Command(Action action)
        {
            this._action = action;
        }
        
        public bool CanExecute(object? parameter)
        {
            return _action != null;
        }

        public void Execute(object? parameter)
        {
            _action.Invoke();
        }
    }
}
