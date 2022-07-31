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
        
        // ページの表示
        public bool IsShownMemoStart { get; set; }
        public bool IsShownMemoNew { get; set; }
        

    }

    /// <summary>
    /// 個別メモクラス
    /// </summary>
    [DataContract]
    class Memo : NoticePrpChange
    { 

        private int _id;
        private string? _title;
        private string? _content;
        private string? _editDate;
        private bool _flg;
        [DataMember(Name ="ID", Order = 0, EmitDefaultValue =false)]
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
        [DataMember(Name = "Title", Order = 1, EmitDefaultValue = false)]
        /// <summary>
        /// メモタイトル
        /// </summary>
        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        [DataMember(Name = "Content", Order = 2, EmitDefaultValue = false)]
        /// <summary>
        /// メモ内容
        /// </summary>
        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content)); ;
            }
        }
        [DataMember(Name = "EditDate", Order = 3)]
        /// <summary>
        /// 更新日(yyyyMMdd HH:mm:ss)
        /// </summary>
        public string? EditDate
        {
            get => _editDate;
            set
            {
                _editDate = value;
                OnPropertyChanged(nameof(EditDate));
            }
        }
        [DataMember(Name = "IsImportant", Order = 4, EmitDefaultValue = false)]
        /// <summary>
        /// 重要フラグ(重要:true、普通:false)
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
