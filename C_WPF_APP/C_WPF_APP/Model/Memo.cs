﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace C_WPF_APP.Model
{
    /// <summary>
    /// 個別メモクラス
    /// </summary>
    [DataContract]
    class Memo : NoticePrpChange
    {

        private int _id;
        private string? _title;
        private string? _content;
        private string? _editDate;
        private bool _flg;
        [DataMember(Name = "ID", Order = 0, EmitDefaultValue = false)]
        /// <summary>
        /// メモID
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        [DataMember(Name = "Title", Order = 1, EmitDefaultValue = false)]
        /// <summary>
        /// メモタイトル
        /// </summary>
        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        [DataMember(Name = "Content", Order = 2, EmitDefaultValue = false)]
        /// <summary>
        /// メモ内容
        /// </summary>
        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content)); ;
            }
        }
        [DataMember(Name = "EditDate", Order = 3)]
        /// <summary>
        /// 更新日(yyyyMMdd HH:mm:ss)
        /// </summary>
        public string? EditDate
        {
            get => _editDate;
            set
            {
                _editDate = value;
                OnPropertyChanged(nameof(EditDate));
            }
        }
        [DataMember(Name = "IsImportant", Order = 4, EmitDefaultValue = false)]
        /// <summary>
        /// 重要フラグ(重要:true、普通:false)
        /// </summary>
        public bool IsMarked
        {
            get => _flg;
            set
            {
                _flg = value;
                OnPropertyChanged(nameof(IsMarked));
            }
        }

        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
        public Memo()
        {
            this.Id = 0;
            this.Title = "";
            this.Content = "";
            this.EditDate = "";
            this.IsMarked = false;
        }

        //--------------------------------------------
        // メソッド
        //--------------------------------------------
        /// <summary>
        /// 自身のフィールドを値渡しする
        /// </summary>
        /// <returns></returns>
        public Memo PassValue()
        {
            var memo = new Memo
            {
                Id = this.Id,
                Title = this.Title,
                Content = this.Content,
                EditDate = this.EditDate,
                IsMarked = this.IsMarked
            };

            return memo;
        }

        /// <summary>
        /// Jsonファイルに自分の情報を出力
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteInJson(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "\t"))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Memo));
                    serializer.WriteObject(writer, this);
                }

            }
            catch (SerializationException)
            {

            }
            catch (IOException)
            {

            }
        }
    }

    /// <summary>
    /// メモの総量を扱うクラス
    /// </summary>
    [DataContract]
    class AllMemoInfo : NoticePrpChange
    {
        private ObservableCollection<Memo> _allMemoList;

        [DataMember(Name = "EditDate", Order = 0)]
        public string EditDate { get; set; }

        [DataMember(Name = "MemoList", Order = 1)]
        public ObservableCollection<Memo> AllMemoList
        {
            get => _allMemoList;
            set
            {
                _allMemoList = value;
                OnPropertyChanged(nameof(AllMemoList));
            }
        }


        //--------------------------------------------
        // コンストラクタ
        //--------------------------------------------
        public AllMemoInfo()
        {
            this.EditDate = "";
            this.AllMemoList = GetAllMemoList();
        }

        public ObservableCollection<Memo> GetAllMemoList()
        {
            var allMemoList = new ObservableCollection<Memo>();


            return allMemoList;
        }


        /// <summary>
        /// 自身のオブジェクト情報をJsonファイルに書き込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="allMemo"></param>
        public void WriteJson(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "\t"))
                {
                    var serializer = new DataContractJsonSerializer(typeof(AllMemoInfo));
                    serializer.WriteObject(writer, this);
                }

            }
            catch (SerializationException)
            {

            }
            catch (IOException)
            {

            }
        }

    }
}
