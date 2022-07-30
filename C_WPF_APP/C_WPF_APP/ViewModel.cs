using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //--------------------------------------------
        // プロパティ
        //--------------------------------------------

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
        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
        public ViewModel()
        {
            // フォルダ作成
            s_commonMethod.CreateDirectory(StatData.MemoFolder);

            // メモの全量を読み込む


            // 起動時画面を表示
            GoStartMemo();
        }

        //--------------------------------------------
        // メソッド
        //--------------------------------------------

        // 画面遷移時の処理

        public void GoNewMemoFromStartMemo()
        {
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
        
    }
}
