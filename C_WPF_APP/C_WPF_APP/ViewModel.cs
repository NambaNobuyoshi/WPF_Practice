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
        public Memo? TempMemoX
        {
            get => s_model.TmpMemoX;
            set
            {
                s_model.TmpMemoX = value;
                OnPropertyChanged(nameof(TempMemoX));
            }
        }

        public AllMemoInfo AllMemo
        {
            get => s_model.AllMemo;
            set
            {
                s_model.AllMemo = value;
                OnPropertyChanged(nameof(AllMemo));
            }
        }


        // 起動時画面バインディング

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
        // 起動時画面
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
        // 登録画面
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
        /// <summary>
        /// 保存 - 登録画面
        /// </summary>
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
        public ViewModel()
        {
            // フォルダ作成
            s_commonMethod.CreateDirectory(StatData.MemoFolder);

            // ファイル作成
            s_commonMethod.CreateFile(StatData.AllMemoInfoFile);

            // オブジェクトの初期化
            MemoX = new Memo();
            TempMemoX = new Memo();
            AllMemo = new AllMemoInfo();

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
            // 新規IDを割り振る
            TempMemoX.Id = AllMemo.AllMemoList.Count + 1;
            TempMemoX.EditDate = s_commonMethod.Now_yMd();

            SaveMemo();

            // 保存したメモをAllMemoListに追加
            AllMemo.AllMemoList.Add(TempMemoX.PassValue());
            
            GoStartMemo();
        }

        // 画面遷移時の処理
        /// <summary>
        /// 起動時画面→登録画面
        /// </summary>
        public void GoNewMemoFromStartMemo()
        {
            // 作業用オブジェクトの作成
            TempMemoX = new Memo();

            GoNewMemo();
        }
        /// <summary>
        /// 登録画面→起動時画面
        /// </summary>
        public void GoStartMemoFromNewMemo()
        {
            // 内容が記述されている場合
            if (!string.IsNullOrEmpty(TempMemoX.Title) || !string.IsNullOrEmpty(TempMemoX.Content) || TempMemoX.IsMarked)
            {
                MessageBoxResult Result = MessageBox.Show("内容を破棄しますか?", "確認", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            // TempMemoXの内容を破棄
            TempMemoX = new Memo();

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
            MemoX = TempMemoX.PassValue();

            string filePath = StatData.MemoFolder + "\\" + MemoX.Id + ".json";

            MemoX.WriteInJson(filePath);

            MessageBox.Show("保存しました", "保存", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        
    }
}
