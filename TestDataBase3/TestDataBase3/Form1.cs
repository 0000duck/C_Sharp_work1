﻿//-----------------------------------------------------------
//  SQL ServerCompact3.5 を使用した データベースアクセス
//  サンプルプログラム
//
//  [内容]
//  データベースも VisualStudioで作成。
//  詳細は、データベース関連資料.txt 参照。
//  LoadEvent で DataBase を DataGridView に接続
//  Button1 Event で、データベースの ZipCode 値で表示絞り込み
//          LINQ を使用
//  Button2 Event で、元のデータ表示
//  
//  ★注意点★
//  DataSet と LINQ 確認のためのサンプルです。
//  DataBase への追加 UpDate は、反映されませすが
//  削除が、反映できず、アプリ再起動するとデータが残っています。
//  DataSet の作り方が良くなかったかもしれません。
//  DataBaseへのUpdate自体は、TabelAdaport.Update(DataSet名)であっていると思います。
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization; // add

namespace TestDataBase3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /**
         * @brief   Form1_Load
         * @param[in]  sender      e
         * @param[in]  EventArgs   e 
         * @return     void 
         * @note       Database を DataGridViewに接続
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'myDataSet1_1.tbl_name_is_test_sdf' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.tbl_name_is_test_sdfTableAdapter.Fill(this.myDataSet1_1.tbl_name_is_test_sdf);     // 自動挿入された Code
            
            // Database を DataGridViewに接続
            dataGridView1.DataSource = bindingSource1;
        }

        /**
         * @brief   button1_Click
         * @param[in]  sender      e
         * @param[in]  EventArgs   e 
         * @return     void 
         * @note       LINQ使ってデータベースの ZipCode が 182以上のみのデータを
         *             DataGirdViewに表示
        */
        private void button1_Click(object sender, EventArgs e)
        {
            var query = from rs in myDataSet1_1.tbl_name_is_test_sdf
                        where rs.ZipCode > 181
                        orderby rs.ZipCode          // 昇順並べ変えの参照列指定
                        //orderby rs.CustomerName
                        select rs;

            bindingSource1.DataSource = query;

            //myDataSet1_1.tbl_name_is_test_sdf.DefaultView.RowFilter = "CustomerID, CustomerName";
            // いらない列が追加されるので削除(暫定Code)
            //dataGridView1.Columns.Remove("HasErrors");
            //dataGridView1.Columns.Remove("RowError");
            //dataGridView1.Columns.Remove("RowState");
            dataGridView1.Columns.Remove("Table");
        }

        /**
         * @brief   button2_Click
         * @param[in]  sender      e
         * @param[in]  EventArgs   e 
         * @return     void 
         * @note       もとのデータベースを dataGridViewに表示
        */
        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = myDataSet1_1.tbl_name_is_test_sdf;
        }

        /**
         * @brief   button3_Click
         * @param[in]   object      sender
         * @param[in]   EventArgs   e
         * @return      void
         * @note        tblに追加
         */
        private void button3_Click(object sender, EventArgs e)
        {
            // Tbl に 行追加
            DataRow workRow = myDataSet1_1.tbl_name_is_test_sdf.NewRow();
            int i = myDataSet1_1.tbl_name_is_test_sdf.Rows.Count;
            workRow[0] = i + 1;
            workRow[1] = "FSan";
            workRow[2] = "186";
            workRow[3] = "TokyoF";
            myDataSet1_1.tbl_name_is_test_sdf.Rows.Add(workRow);
            
            // データベースへ反映
            //tbl_name_is_test_sdfTableAdapter.Fill(myDataSet1_1.tbl_name_is_test_sdf);
            tbl_name_is_test_sdfTableAdapter.Update(myDataSet1_1);
            
            //bindingSource1.DataSource = myDataSet1_1.tbl_name_is_test_sdf;
        }

        /**
          * @brief   button4_Click
          * @param[in]   object      sender
          * @param[in]   EventArgs   e
          * @return      void
          * @note        tbl 最後を削除
          */
        private void button4_Click(object sender, EventArgs e)
        {
            int index = myDataSet1_1.tbl_name_is_test_sdf.Rows.Count;
            //myDataSet1_1.tbl_name_is_test_sdf.Rows[index - 1].Delete();
            myDataSet1_1.tbl_name_is_test_sdf.Rows.RemoveAt(index - 1);
            //myDataSet1_1.tbl_name_is_test_sdf.Select(
            //    DataViewRowState.Deleted);
            //var query = from rs in myDataSet1_1.tbl_name_is_test_sdf
            //            where rs.RowState==(DataRowState)DataViewRowState.Deleted
            //            select rs;
            //tbl_name_is_test_sdfTableAdapter.Update(query);
            
            
            // データベース本体を更新
            //tbl_name_is_test_sdfTableAdapter.Update(myDataSet1_1.tbl_name_is_test_sdf.Rows[index - 1]);
            tbl_name_is_test_sdfTableAdapter.Update(myDataSet1_1);

            // 更新したテーブルを bind に反映
            //bindingSource1.DataSource = myDataSet1_1.tbl_name_is_test_sdf;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
