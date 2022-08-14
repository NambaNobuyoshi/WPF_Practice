﻿using System;
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

        public AllMemoInfo AllMemo
        {
            get => model.AllMemo;
            set
            {
                model.AllMemo = value;
                OnPropertyChanged(nameof(AllMemo));
            }
        }


        // 起動時画面バインディング

        // ページの表示・非表示
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

            SaveMemo();

            // 保存したメモをAllMemoListに追加
            AllMemo.AllMemoList.Add(TempMemoX.PassValue());
            
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

        // 保存
        /// <summary>
        /// MemoXをJsonファイルに書き出す
        /// </summary>
        public void SaveMemo()
        {
            MemoX = TempMemoX.PassValue();

            string filePath = StatData.MemoFolder + "\\" + MemoX.Id + ".json";

            MemoX.WriteInJson(filePath);

            // Allmemo.jsonを上書き
            AllMemo.WriteJson(StatData.AllMemoInfoFile);

            MessageBox.Show("保存しました", "保存", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        
    }
}
