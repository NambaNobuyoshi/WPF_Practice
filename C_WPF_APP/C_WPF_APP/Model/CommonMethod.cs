using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace C_WPF_APP.Model
{
    internal class CommonMethod
    {
        /// <summary>
        /// 現在時刻を年月日の文字列で返す
        /// </summary>
        /// <returns></returns>
        public string Now_yMd()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("yyyyMMdd");
        }

        // ファイル操作
        /// <summary>
        /// 存在しないディレクトリであった場合、作成する
        /// </summary>
        /// <param name="folder">作成対象のディレクトリ</param>
        public void CreateDirectory(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                else
                {
                    Console.WriteLine("memoフォルダは既存です。");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("memo フォルダの作成に失敗しました。");
            }
            
        }
        /// <summary>
        /// 存在しないファイルであった場合、作成する
        /// </summary>
        /// <param name="file"></param>
        public void CreateFile(string file)
        {
            if (File.Exists(file))
            {
                return;
            }
            else
            {
                using var fs = new FileStream(file, FileMode.CreateNew);
            }
        }
    }
}
