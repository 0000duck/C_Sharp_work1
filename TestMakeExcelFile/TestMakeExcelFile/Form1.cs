//-----------------------------------------------------------------------
//  Excel ファイル作成 Sample Project
//
//  Btn_MkExcel で 固定Dummyデータの 固定Excelファイル作成
//  Btn_RdExcel で 作成した 固定Execel ファイルを読み込み 
//  グラフを作成して、保存後、Excelを起動して、表示するサンプル。
//
//  データ作成は、べた書きです。すみません。
//  もともと、シリアル通信などで、得たデータを、Excelに
//  保存して、グラフ表示できないか、試すために調べて
//  見よう見まねで作成したものです。
//
//  ソリューションエクスプローラで [参照]右クリック→[参照の追加]→
//  "COM"の"Microsoft Excel 14.0 Object Library" Check　が必要
//-----------------------------------------------------------------------
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop;     // Add

namespace TestMakeExcelFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
         *  @brief  Btn_MkExcel_Click
         *  @note   DummyData で Test1.xlsx を作成(グラフなし)
         */
        private void Btn_MkExcel_Click(object sender, EventArgs e)
        {

            string ExcelBookFileName = "C:\\TEMP\\Test1.xlsx";

            Microsoft.Office.Interop.Excel.Application ExcelApp
                 = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;
            Microsoft.Office.Interop.Excel._Workbook wb = ExcelApp.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet ws1 = wb.Sheets[1];
            ws1.Select(Type.Missing);

            //----- Excel Data 作成 ------
            // Cells[y, x]
            Microsoft.Office.Interop.Excel.Range rgn;
            // 列のヘッダ情報作成
            rgn = ws1.Cells[1, 2]; rgn.Value2 = 2014;
            rgn = ws1.Cells[1, 3]; rgn.Value2 = 2015;
            rgn = ws1.Cells[1, 4]; rgn.Value2 = "2016";
            // 縦のヘッダ情報作成
            rgn = ws1.Cells[2, 1]; rgn.Value2 = "Data1";
            rgn = ws1.Cells[3, 1]; rgn.Value2 = "Data2";
            rgn = ws1.Cells[4, 1]; rgn.Value2 = "Data3";
            rgn = ws1.Cells[5, 1]; rgn.Value2 = "Data4";
            // 1列目(2014)データ
            rgn = ws1.Cells[2, 2]; rgn.Value2 = 12;
            rgn = ws1.Cells[3, 2]; rgn.Value2 = 56;
            rgn = ws1.Cells[4, 2]; rgn.Value2 = 52;
            rgn = ws1.Cells[5, 2]; rgn.Value2 = 30;
            // 2列目(2015)データ
            rgn = ws1.Cells[2, 3]; rgn.Value2 = 15;
            rgn = ws1.Cells[3, 3]; rgn.Value2 = 73;
            rgn = ws1.Cells[4, 3]; rgn.Value2 = 61;
            rgn = ws1.Cells[5, 3]; rgn.Value2 = 32;
            // 2列目(2016)データ
            rgn = ws1.Cells[2, 4]; rgn.Value2 = 21;
            rgn = ws1.Cells[3, 4]; rgn.Value2 = 86;
            rgn = ws1.Cells[4, 4]; rgn.Value2 = 69;
            rgn = ws1.Cells[5, 4]; rgn.Value2 = 0;

            wb.SaveAs(ExcelBookFileName);
            wb.Close(false);
            ExcelApp.Quit();

            MessageBox.Show("Made Dummy Excel Data");

           
        }

        /*
         *  @brief  Btn_RdExcel_Click
         *  @note   Test1.xlsx を読み込み、折れ線グラフを作成して、保存後、
         *          Excelを起動して表示
         */
        private void Btn_RdExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Workbooks objBooks;
            Microsoft.Office.Interop.Excel.Application objApp = null;
            Microsoft.Office.Interop.Excel._Workbook objBook = null;

            try
            {
                //  読み込み
                objApp = new Microsoft.Office.Interop.Excel.Application();
                objBooks = objApp.Workbooks;
                objBook = objBooks.Open("C:\\TEMP\\Test1.xlsx");

                //  グラフを書く
                Microsoft.Office.Interop.Excel.Worksheet thisWorksheet;
                thisWorksheet = objBook.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.ChartObjects charts =
                    (Microsoft.Office.Interop.Excel.ChartObjects)thisWorksheet.ChartObjects(Type.Missing);

                // チャート作成（x = 100, y = 100, 幅500　高さ 300）
                Microsoft.Office.Interop.Excel.ChartObject chartObj = charts.Add(100, 100, 500, 300);
                Microsoft.Office.Interop.Excel.Chart chart = chartObj.Chart;
                chart.HasTitle = true;
                chart.ChartTitle.Text = "LineMarker";

                // データをセット.
                Microsoft.Office.Interop.Excel.Range chartRange = thisWorksheet.get_Range("A1", "D5");
                chart.SetSourceData(chartRange, Type.Missing);

                //	折れ線グラフのチャート指定
                //    参考 → http://home.att.ne.jp/zeta/gen/excel/c04p63.htm
                // 
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers; // 折れ線指定
                chart.PlotBy = Microsoft.Office.Interop.Excel.XlRowCol.xlColumns;   // グラフのデータ系列を列方向

                //  ファイル保存
                //objBook.SaveAs("C:\\TEMP\\Test1.xlsx");
                objBook.Save();

                //  クローズ処理
                objBook.Close();
                objBooks.Close();
                objApp.Quit();

                // 作成した Excel 起動
                System.Diagnostics.Process p =
                    System.Diagnostics.Process.Start("C:\\TEMP\\Test1.xlsx");
            }
            catch (Exception theException)
            {
                //  エラーメッセージ出力
                Console.Write(theException.ToString());

                if (objBook != null)
                    objBook.Close();

                if (objApp != null)
                    objApp.Quit();
            }
        }
    }
}
