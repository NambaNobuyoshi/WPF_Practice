using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace C_WPF_APP.Model
{
    /// <summary>
    /// バインディング元となる情報を格納するクラス
    /// </summary>
    internal class Model
    {
        //--------------------------------------------
        // プロパティ
        //--------------------------------------------
        // 作業対象のオブジェクト
        public Memo? MemoX { get; set; }
        // 編集用のオブジェクト
        public Memo? TmpMemoX { get; set; }
        // メモ起動時画面 バインディング用
        public ObservableCollection<Memo>? MemoList { get; set; }
        public AllMemoInfo AllMemo { get; set; }
        
        // ページの表示
        public bool IsShownMemoStart { get; set; }
        public bool IsShownMemoNew { get; set; }
        

    }

    // MVVMモデルにおける定型
    [DataContract]
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
    /// <summary>
    /// Commandバインディング用クラス
    /// </summary>
    class Command : ICommand
    {
        private Action _action;

#pragma warning disable CS0067 // イベント～は使用されていません 
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 

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
