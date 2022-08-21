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
        private readonly Model.Model model = new();
        private readonly CommonMethod commonMethod = new();

        // 起動時画面
        private Command _clickGoNewMemo;
        private Command _clickGoDetailMemo;
        // 登録画面
        private Command _clickGoStartMemo;
        private Command _clickSaveInNewMemo;
        // 詳細画面
        private Command _clickGoStartMemoFromDetail;
        private Command _clickSaveContentInDetailMemo;
        private Command _clickDeleteMemoInDetailMemo;
        
        //--------------------------------------------
        // プロパティ
        //--------------------------------------------
        // 作業対象
        public Memo? MemoX
        {
            get => model.MemoX;
            set
            {
                model.MemoX = value;
                OnPropertyChanged(nameof(MemoX));
            }
        }
        // 編集用
        public Memo? TempMemoX
        {
            get => model.TmpMemoX;
            set
            {
                model.TmpMemoX = value;
                OnPropertyChanged(nameof(TempMemoX));
            }
        }
        // メモ一覧
        public AllMemoInfo AllMemo
        {
            get => model.AllMemo;
            set
            {
                model.AllMemo = value;
                OnPropertyChanged(nameof(AllMemo));
            }
        }

        // ページの表示・非表示(MainWindowとバインディング)
        // 新規でページを作成する場合はここに追加すること
        public bool IsShownStartMemo
        {
            get => model.IsShownMemoStart;
            set
            {
                model.IsShownMemoStart = value;
                OnPropertyChanged(nameof(IsShownStartMemo));
            }
        }
        public bool IsShownNewMemo
        {
            get => model.IsShownMemoNew;
            set
            {
                model.IsShownMemoNew = value;
                OnPropertyChanged(nameof(IsShownNewMemo));
            }
        }
        public bool IsShownMemoDetail
        {
            get => model.IsShownMemoDetail;
            set
            {
                model.IsShownMemoDetail = value;
                OnPropertyChanged(nameof(IsShownMemoDetail));
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
        public Command ClickGoDetailMemo
        {
            get
            {
                _clickGoDetailMemo = new Command(GoDetailMemoFromStartMemo);
                return _clickGoDetailMemo;
            }
            set => _clickGoDetailMemo = value;
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
        // 詳細画面
        /// <summary>
        ///  一覧へ戻るボタン - 詳細画面
        /// </summary>
        public Command ClickGoStartMemoFromDetailMemo
        {
            get
            {
                _clickGoStartMemoFromDetail = new Command(GoStartMemoFromDetailMemo);
                return _clickGoStartMemoFromDetail;
            }
            set => _clickGoStartMemoFromDetail = value;
        }
        /// <summary>
        /// 保存 - 詳細画面
        /// </summary>
        public Command ClickSaveMemoInDetailMemo
        {
            get
            {
                _clickSaveContentInDetailMemo = new Command(SaveMemoInDetailMemo);
                return _clickSaveContentInDetailMemo;
            }
            set => _clickSaveContentInDetailMemo = value;
        }
        /// <summary>
        /// 削除 - 詳細画面
        /// </summary>
        public Command ClickDeleteMemoInDetailMemo
        {
            get
            {
                _clickDeleteMemoInDetailMemo = new Command(DeleteMemoInDetailMemo);
                return _clickDeleteMemoInDetailMemo;
            }
            set => _clickDeleteMemoInDetailMemo = value;
        }
        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
        public ViewModel()
        {
            // フォルダ作成
            commonMethod.CreateDirectory(StatData.MemoFolder);

            // ファイル作成
            commonMethod.CreateFile(StatData.AllMemoInfoFile);

            // オブジェクトの初期化
            MemoX = new Memo();
            TempMemoX = new Memo();
            AllMemo = AllMemoInfo.GetAllMemoList();


            // 起動時画面を表示
            GoStartMemo();
        }

        //--------------------------------------------
        // メソッド
        //--------------------------------------------
        // メモ起動時画面
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
        /// 起動時画面→詳細画面
        /// </summary>
        public void GoDetailMemoFromStartMemo()
        {
            // リストで選択した要素を作業用インスタンスに値渡し
            TempMemoX = MemoX.PassValue();

            GoDetailMemo();
        }
        // メモ新規登録画面
        /// <summary>
        /// 内容を保存する - 新規登録画面
        /// </summary>
        public void SaveInNewMemo()
        {
            // 新規IDを割り振る
            TempMemoX.Id = AllMemo.AllMemoList.Count + 1;
            TempMemoX.EditDate = commonMethod.Now_yMd();

            // メモを保存
            SaveMemo();

            // 保存したメモをAllMemoListに追加
            AllMemo.AllMemoList.Add(TempMemoX.PassValue());
            SaveAllMemo();

            MessageBox.Show("保存しました", "保存", MessageBoxButton.OK, MessageBoxImage.Information);
            GoStartMemo();
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

        // メモ詳細画面
        /// <summary>
        /// 詳細画面→一覧画面
        /// </summary>
        public void GoStartMemoFromDetailMemo()
        {
            if (MemoX.IsUpdated(TempMemoX))
            {
                MessageBoxResult Result = MessageBox.Show("更新を破棄して、戻りますか", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Cancel) return;
            }

            GoStartMemo();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveMemoInDetailMemo()
        {
            // もともとのメモを削除
            int TargetMemoIndex = AllMemo.AllMemoList.IndexOf(MemoX);
            AllMemo.AllMemoList.RemoveAt(TargetMemoIndex);

            TempMemoX.EditDate = commonMethod.Now_yMd();
            // 
            SaveMemo();

            // 修正後のメモをリストに追加
            AllMemo.AllMemoList.Insert(TargetMemoIndex, MemoX);

            SaveAllMemo();

            MessageBox.Show("保存しました", "保存", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        /// <summary>
        /// メモの削除
        /// </summary>
        public void DeleteMemoInDetailMemo()
        {
            MessageBoxResult Result = MessageBox.Show("このメモを削除しますか", "確認", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (Result == MessageBoxResult.Cancel) return;

            int TargetMemoIndex = AllMemo.AllMemoList.IndexOf(MemoX);
            AllMemo.AllMemoList.RemoveAt(TargetMemoIndex);

            // MemoXのファイルを削除 →IDがずれてしまう。要検討
            //try
            //{
            //    DeleteMemo();
            //}
            //catch
            //{
            //    MessageBox.Show("削除に失敗しました", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            // AllMemoのファイルを更新
            SaveAllMemo();

            // MemoXを初期化
            MemoX = new();

            MessageBox.Show("削除しました", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
            GoStartMemo();
        }

        // 画面遷移専用
        public void GoStartMemo()
        {
            IsShownStartMemo = true;
            IsShownNewMemo = false;
            IsShownMemoDetail = false;
        }
        public void GoNewMemo()
        {
            IsShownStartMemo = false;
            IsShownNewMemo = true;
            IsShownMemoDetail = false;
        }
        public void GoDetailMemo()
        {
            IsShownStartMemo = false;
            IsShownNewMemo = false;
            IsShownMemoDetail = true;
        }
        // 削除
        public void DeleteMemo()
        {
            MemoX = TempMemoX.PassValue();

            string filePath = StatData.MemoFolder + "\\" + MemoX.Id + ".json";

            try
            {
                commonMethod.DeleteFile(filePath);
                MemoX = new();
            }
            catch
            {
                throw;
            }
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
        }
        public void SaveAllMemo()
        {
            // Allmemo.jsonを上書き
            AllMemo.WriteJson(StatData.AllMemoInfoFile);
        }
        
    }
}
