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
using System.Windows;

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
        public void DeleteFile(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfo.Delete();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 文字列型の比較を行う 差異があればtrue
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public bool IsUpdated(string before, string after)
        {
            // 変更前が空の場合
            if (string.IsNullOrEmpty(before))
            {
                return !string.IsNullOrEmpty(after);
            }

            // 変更前が空以外の場合
            return !before.Equals(after);

        }
    }
}
