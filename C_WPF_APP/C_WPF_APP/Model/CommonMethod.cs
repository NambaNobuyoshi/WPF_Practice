using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_WPF_APP.Model
{
    internal class CommonMethod
    {
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
}
