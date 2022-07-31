using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using C_WPF_APP.Model;

namespace C_WPF_APP
{
    /// <summary>
    /// バインディング用クラス
    /// </summary>
    internal class ViewModel : NoticePrpChange
    {
        //--------------------------------------------
        // フィールド
        //--------------------------------------------
        // 他クラスアクセス用
        private static readonly Model.Model s_model = new();
        private static readonly CommonMethod s_commonMethod = new();
        private static readonly JsonMethod s_jsonMethod = new();

        // イベント変数
        private Command _clickGoNewMemo;

        private Command _clickGoStartMemo;
        private Command _clickSaveInNewMemo;

        //--------------------------------------------
        // プロパティ
        //--------------------------------------------
        // 作業対象
        public Memo? MemoX
        {
            get => s_model.MemoX;
            set
            {
                s_model.MemoX = value;
                OnPropertyChanged(nameof(MemoX));
            }
        }
        // 編集用
        public Memo? TmpMemoX
        {
            get => s_model.TmpMemoX;
            set
            {
                s_model.TmpMemoX = value;
                OnPropertyChanged(nameof(TmpMemoX));
            }
        }


        // 起動時画面バインディング
        public ObservableCollection<Memo>? MemoList
        {
            get => s_model.MemoList;
            set
            {
                s_model.MemoList = value;
                OnPropertyChanged(nameof(MemoList));
            }
        }

        // ページの表示・非表示
        public bool IsShownStartMemo
        {
            get => s_model.IsShownMemoStart;
            set
            {
                s_model.IsShownMemoStart = value;
                OnPropertyChanged(nameof(IsShownStartMemo));
            }
        }
        public bool IsShownNewMemo
        {
            get => s_model.IsShownMemoNew;
            set
            {
                s_model.IsShownMemoNew = value;
                OnPropertyChanged(nameof(IsShownNewMemo));
            }
        }

        // クリックイベント
        /// <summary>
        /// 新規作成ボタン - 起動時画面
        /// </summary>
        public Command ClickGoNewMemo
        {
            get
            {
                _clickGoNewMemo = new Command(GoNewMemoFromStartMemo);
                return _clickGoNewMemo;
            }
            set => _clickGoNewMemo = value;
        }

        /// <summary>
        /// 一覧へ戻るボタン - 登録画面
        /// </summary>
        public Command ClickGoStartMemo
        {
            get
            {
                _clickGoStartMemo = new Command(GoStartMemoFromNewMemo);
                return _clickGoStartMemo;
            }
            set => _clickGoStartMemo = value;
        }

        public Command ClickSaveInNewMemo
        {
            get
            {
                _clickSaveInNewMemo = new Command(SaveInNewMemo);
                return _clickSaveInNewMemo;
            }
            set => _clickSaveInNewMemo = value;
        }
        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public ViewModel()
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        {
            // フォルダ作成
            s_commonMethod.CreateDirectory(StatData.MemoFolder);

            // メモの全量を読み込む
            MemoList = null;


            // 起動時画面を表示
            GoStartMemo();
        }

        //--------------------------------------------
        // メソッド
        //--------------------------------------------

        // メモ新規登録画面
        /// <summary>
        /// 内容を保存する - 新規登録画面
        /// </summary>
        public void SaveInNewMemo()
        {
            TmpMemoX.EditDate = s_commonMethod.Now_yMd();

            SaveMemo();
            
            GoStartMemo();
        }

        // 画面遷移時の処理
        public void GoNewMemoFromStartMemo()
        {
            // 作業用オブジェクトの作成
            TmpMemoX = new Memo
            {
                Title = "",
                Content = "",
                IsMarked = false
            };

            GoNewMemo();
        }
        public void GoStartMemoFromNewMemo()
        {
            GoStartMemo();
        }

        // 画面遷移専用
        public void GoStartMemo()
        {
            IsShownStartMemo = true;
            IsShownNewMemo = false;
        }
        public void GoNewMemo()
        {
            IsShownStartMemo = false;
            IsShownNewMemo = true;
        }

        // 保存
        /// <summary>
        /// MemoXをJsonファイルに書き出す
        /// </summary>
        public void SaveMemo()
        {
            MemoX = s_commonMethod.PassValue(TmpMemoX);

            string filePath = StatData.MemoFolder + "\\" + MemoX.Title + ".json";

            s_jsonMethod.WriteJson(filePath, MemoX);

            MessageBox.Show("保存しました", "保存", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        
    }
}
