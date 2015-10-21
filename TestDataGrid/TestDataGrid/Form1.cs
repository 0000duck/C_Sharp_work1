//-------------------------------------------------------
//	DataGridView Sampleプロジェクト
//
//		DataGirdViewの数ある機能の中から独断で絞り込んで
//		サンプルプロジェクトとしました。
//		
//			列、行の幅固定、削除禁止、列のソート禁止設定
//			最終新規行の非表示
//			ソース内で 4x4のデータ追加。
//			データ文字クリック時の座標取得
//			左クリックした cellデータを右クリックで Copy選択時
//			textBox2へ反映
//			cellをクリックで選択した状態で selectボタン
//			押した場合、全cellデータを textBox1に表示
//-------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDataGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string currentCellDataStr = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            int idx;

            // 各種設定
            dtGrid1.AutoGenerateColumns = false;    // 列が自動的に作成されないようにする
            dtGrid1.AllowUserToDeleteRows = false;  // 行をユーザーが削除できないようにする
            dtGrid1.AllowUserToResizeColumns = false;   // 列の幅をユーザーが変更できないようにする
            dtGrid1.AllowUserToResizeRows = false;  // 行の高さをユーザーが変更できないようにする
            //dtGrid1.RowHeadersVisible = false;      // 行ヘッダ非表示

            // 列ヘッダーの高さを任意に変えるためにColumnHeadersHeightSizeModeを設定
            dtGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dtGrid1.ColumnHeadersHeight = 30;       // 列ヘッダの高さ指定
            dtGrid1.RowHeadersWidth = 40;           // 行ヘッダの幅指定


            // 4列 Headerのみ作成
            for (int i = 0; i < 4; i++)
            {
                DataGridViewTextBoxColumn wkColumn = new DataGridViewTextBoxColumn();
                wkColumn.Name = "Column" + i.ToString();
                wkColumn.HeaderText = "Column" + i.ToString();
                wkColumn.SortMode = DataGridViewColumnSortMode.NotSortable;   // Header押しても列でソートさせない。
                wkColumn.Width = 100;               // 列の幅指定
                dtGrid1.Columns.Add(wkColumn);
            }

            // 4行4列データ作成
            for (int j = 0; j < 4; j++)
            {
                dtGrid1.Rows.Add();
                idx = dtGrid1.Rows.Count - 2;
                for (int i = 0; i < 4; i++)
                {
                    dtGrid1.Rows[idx].Cells[i].Value = "data_" + j.ToString() + i.ToString();
                    dtGrid1.Rows[idx].Cells[i].ReadOnly = true; // 書き換え禁止
                }
            }

            // 行ヘッダーに行番号を表示する
            for (int i = 0; i < dtGrid1.Rows.Count; i++)
            {
                dtGrid1.Rows[i].HeaderCell.Value = i.ToString();
                dtGrid1.Rows[i].Height = 20;        // 行の高さ指定
            }


            // (注)Columns, Rows 追加後に以下を禁止しないで先にやると追加できない
            dtGrid1.AllowUserToAddRows = false;     // 最終新規行非表示

            
        }

        /**
         *  @brief  Select Button Click Event
         *  @param[in]  object  sender
         *  @param[in]  EventArgs   e
         *  @return     void
         *  @note       選択された Cellデータを textBox1に表示
         */
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            // 選択したセルの値を TextBoxに表示
            foreach (DataGridViewCell cell in dtGrid1.SelectedCells)
            {
                textBox1.AppendText(cell.Value.ToString()+"\n");
                // 列位置取得：cell.ColumnIndex
                // 行位置取得：cell.RowIndex
            }


        }

        /**
          *  @brief  Cell内の文字をクリックしたとき Event
          *  @param[in]  object  sender
          *  @param[in]  EventArgs   e
          *  @return     void
          *  @note       データ選択されたcell座標を label1と2に表示
          */
        private void dtGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewImageCell cell = (DataGridViewImageCell)dtGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            label1.Text = e.RowIndex.ToString();
            label2.Text = e.ColumnIndex.ToString();
        }


        /**
         *  @brief  Cell内クリックしたとき Event
         *  @param[in]  object  sender
         *  @param[in]  DataGridViewCellMouseEventArgs   e
         *  @return     void
         *  @note       右クリックは判断はしているが処理なし。
         *              左クリックは、クリックされたCellデータの文字を保持  
         */
        private void dtGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 右ボタンのクリック
            if (e.Button == MouseButtons.Right)
            {
                // ヘッダ以外のセル？
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataGridViewCell cell = dtGrid1[e.ColumnIndex, e.RowIndex];
                }
            }

            // 左ボタンのクリック
            if (e.Button == MouseButtons.Left)
            {
                // ヘッダ以外のセル？
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataGridViewCell cell = dtGrid1[e.ColumnIndex, e.RowIndex];
                    currentCellDataStr = cell.Value.ToString();
                }
            }

        }

        /**
         *  @brief  DataGridView への右クリック時のMenu表示
         *  @param[in]  object  sender
         *  @param[in]  DataGridViewCellContextMenuStripNeededEventArgs   e
         *  @return     void
         *  @note       Form1 に ContextMenustrip を ツールボックスより追加。
         *              Form1[デザイン]上、dtGridのイベントで "CellContextMenuStripNeeded"追加
         */
        private void dtGrid1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //if (e.RowIndex < 0)
            //{
            //    //列ヘッダーに表示するContextMenuStripを設定
            //    e.ContextMenuStrip = this.contextMenuStrip2;
            //}
            //else if (e.ColumnIndex < 0)
            //{
            //    //行ヘッダーに表示するContextMenuStripを設定
            //    e.ContextMenuStrip = this.contextMenuStrip3;
            //}
            //else if (dgv[e.ColumnIndex, e.RowIndex].Value is int)
            if (dgv[e.ColumnIndex, e.RowIndex].Value is int)
            {
                //セルが整数型のときに表示するContextMenuStripを変更
                e.ContextMenuStrip = this.contextMenuStrip1;
            }
        }

        /**
         *  @brief  ContextMenuStrip で CopyValue Item選択されたEvent処理
         *  @param[in]  object  sender
         *  @param[in]  DataGridViewCellContextMenuStripNeededEventArgs   e
         *  @return     void
         *  @note       dtGrid1_CellMouseClick 左クリック判断で保持した文字列を textBox2へ反映
         */
        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = currentCellDataStr;
        }

    }

}
