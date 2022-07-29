using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_WPF_APP.Model
{
    /// <summary>
    /// 固定値、静的変数を扱うクラス
    /// </summary>
    public class StatData
    {
        // フォルダ類
        private static string _basePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))));
        public static string MemoFolder = _basePath + "\\memo";
        public static string LogFolder = _basePath + "\\log";

    }
}
