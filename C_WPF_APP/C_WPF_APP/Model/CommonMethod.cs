using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace C_WPF_APP.Model
{
    internal class CommonMethod
    {
        public string Now_yMd()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("yyyyMMdd");
        }

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
    }

    class JsonMethod
    {
        /// <summary>
        /// MemoクラスのオブジェクトをJsonファイルに書き込む
        /// </summary>
        /// <param name="filePath">作成するファイルのフルパス</param>
        /// <param name="memo">対象Memoオブジェクト</param>
        public void WriteJson(string filePath, Memo memo)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    string json = JsonSerializer.Serialize(memo);
                }

            }
            catch
            {

            }
        }
    }
}
