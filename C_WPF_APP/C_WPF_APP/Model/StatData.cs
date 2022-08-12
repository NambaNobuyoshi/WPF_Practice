using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace C_WPF_APP.Model
{
    /// <summary>
    /// 固定値、静的変数を扱うクラス
    /// </summary>
    public class StatData
    {
        // フォルダ類
        private static string _basePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)))));
        public static string MemoFolder = _basePath + "\\memo";
        public static string LogFolder = _basePath + "\\log";

        // ファイル類
        public static string AllMemoInfoFile = MemoFolder + "\\allmemo.json";

        // 固定メソッド
        /// <summary>
        /// アプリケーション終了時のメソッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void finishApplication(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var Result = MessageBox.Show("アプリケーションを終了しますか", "確認", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (Result == MessageBoxResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
