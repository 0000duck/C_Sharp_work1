//----------------------------------------------------------------------------
//	LINQ to SQL SampleProject
//
//		DataClass を使用して、Table の Add/Del について作成
//		button1："Asan"追加
//		button2："Asan"削除
//		button3：Table "Asan"と"Bsan"表示
//		button4："Bsan"追加
//		button5："Bsan"削除
//	[memop]
//		< DataBase & Table作成 >
//		Visual Studio の [ソリューションエクスプローラ] で、
//		TestLinqToSql2 プロジェクトを右クリックして、 [追加] → [新しい項目] 
//		ダイアログが開いたら、[カテゴリ] から [データ] → [サービスベースのデータベース] を選んで、
//		 適当な名前をつけて（例 TestDatabase1.mdf とします） [追加] ボタンを押します。
//			"データベースモデルの選択"が表示されるので"EntityDataModel"を選び"次へ"
//		"データベース生成"から
//			TestDataBse1.mdf
//			TestDatabase1Enities を選び
//		"テーブル" を Check して"完了"を押す
//
//		"サーバーエクスプローラー"起動
//		 [テーブル] のところを右クリックして [新しいテーブルの追加] 
//		列名	データ型
//		Id		int
//		Name	nvarchar(50)	いいえ
//		で Table枠作成
//		Id は、 列を右クリックして [主キーを設定] し、 [列のプロパティ] で [IDENTITY の指定] を [はい] にします。
//		 （一意な ID 番号が自動的に振られるようになる。）
//		主キーを付ける。"Id" 上で、右クリックし、"主キー追加"を選択
//
//		< LINQ to SQL クラス作成 >
//		先のテーブルに対応したクラスを自前で手書きもできるらしいが、 
//		Visual Studio にはデータベースから LINQ to SQL クラスを自動生成する機能を使用。。
//		"ソリューションエクスプローラ]"で、"TestLinqToSql2" プロジェクトを右クリックして、
//		 [追加] → [新しい項目] を選択。
//		ダイアログが開くので、今度は、 [カテゴリ] から [データ] → [LINQ to SQL クラス] を選択。 
//		例：DataClasses1.dbml とした。。
//		DataClasses1.dbml を開くと、 「サーバーエクスプローラかツールボックスから項目をドラッグしてください」
//		というような旨のメッセージが表示されるので、 [サーバーエクスプローラ] の "テーブル"内にある
//		"Table1"を Drac & Drop します。
//		これでクラス編集ができます。Tableが複数ある場合は、すべて Dropし、ToolBoxにある
//		"関連付け"を選び TabelからTableを選ぶとダイアログが表示されるので、そこで各 Id 名を選んで
//		Tableどおしの結合もできます。
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestLinqToSql2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //----- DB 接続先準備 -----
        static string basedir = AppDomain.CurrentDomain.BaseDirectory;
        // DataSource：接続先
        // Integrated Sequrity：true=データベースへアクセスするときに現在のユーザーのコンテキストで
        //                              データベースにアクセスするを示す。
        // User Instance：mdfへの接続のみ許可で接続ユーザごとに新しいProcess起動可能
        //                  セキュリティ上、本番は false 推奨らしい。
        static string ConnectionString =
          "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"" +
          basedir + "TestDataBase1.mdf\";Integrated Security=True;User Instance=True";

        /**
        * @brief   AddOneData
        * @param[in]    string  nameString  登録名
        * @return     void
        * @note       Table に 引数で受けた名前のデータ登録
        */
        void AddOneData(string nameStr)
        {
            var db = new DataClasses1DataContext(ConnectionString);
            Table1 tb = new Table1();
            tb.Name = nameStr;                    // 作者
            db.Table1.InsertOnSubmit(tb);
            db.SubmitChanges();
        }

        /**
         * @brief   DelData
         * @param[in]    string  nameString  登録名
         * @return     void
         * @note       Table から 引数で受けた名前のデータ削除
         */
        void DelData(string nameStr)
        {
            var db = new DataClasses1DataContext(ConnectionString);
            var que = from tb in db.Table1
                      where tb.Name.Contains(nameStr)
                      select tb;
            foreach (var slct in que)
            {
                db.Table1.DeleteOnSubmit(slct);
            }
            db.SubmitChanges();
        }

        /**
           * @brief   button1_Click
           * @param[in]  object     e
           * @param[in]  EventArgs  e 
           * @return     void 
           * @note       "Asan"登録
           */
        private void button1_Click(object sender, EventArgs e)
        {
            AddOneData("Asan");
        }

        /**
          * @brief   button2_Click
          * @param[in]  object     e
          * @param[in]  EventArgs  e 
          * @return     void 
          * @note       "Asan"削除
          */
        private void button2_Click(object sender, EventArgs e)
        {
            DelData("Asan");
        }

        /**
           * @brief   button4_Click
           * @param[in]  object     e
           * @param[in]  EventArgs  e 
           * @return     void 
           * @note       "Bsan"登録
           */
        private void button4_Click(object sender, EventArgs e)
        {
            AddOneData("Bsan");
        }

        /**
           * @brief   button5_Click
           * @param[in]  object     e
           * @param[in]  EventArgs  e 
           * @return     void 
           * @note       "Bsan"削除
           */
        private void button5_Click(object sender, EventArgs e)
        {
            DelData("Bsan");
        }

        /**
           * @brief   button3_Click
           * @param[in]  object     e
           * @param[in]  EventArgs  e 
           * @return     void 
           * @note       "Asan"/"Bsan" textBox1 に表示
           */
        private void button3_Click(object sender, EventArgs e)
        {
            var db = new DataClasses1DataContext(ConnectionString);

            var q =
              from s in db.Table1
              where s.Name.Contains("Asan") || s.Name.Contains("Bsan")
              select new { Title = s.Name };

            textBox1.Clear();
            foreach (var s in q)
            {
                Console.Write("{0}\n", s.Title);
                textBox1.AppendText(s.Title + "\n");
            }
        }


    }
}
