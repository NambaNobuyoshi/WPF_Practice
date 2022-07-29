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

        //--------------------------------------------
        // プロパティ
        //--------------------------------------------

        // ページの表示・非表示
        public bool IsShownMemoStart
        {
            get => s_model.IsShownMemoStart;
            set
            {
                s_model.IsShownMemoStart = value;
                OnPropertyChanged(nameof(IsShownMemoStart));
            }
        }
        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
        public ViewModel()
        {
            s_commonMethod.CreateDirectory(StatData.MemoFolder);
        }

        //--------------------------------------------
        // メソッド
        //--------------------------------------------
    }
}
