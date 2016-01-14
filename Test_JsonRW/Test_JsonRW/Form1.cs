//--------------------------------------------------------------
//  JSON 読み込み、書き込み Sample Project
//
//  [内容]
//      JSON形式で保存された、ファイルを読み込み 
//      各情報をInfoClassに保存し Formに表示
//      Form上の情報を InfoClass に保存し
//      JSON形式でファイルに保存
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// [参照]→[参照の追加]→"アセンブリ"の"フレームワーク"にも
// "System.Runtime.Serialization"追加
using System.Runtime.Serialization.Json;

using System.IO;


namespace Test_JsonRW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /**
         *  @brief  Json Read Button処理
         *  @note   JSON形式ファイルから情報を取得し、シリアライズで、InfoClassに保存
         *          そのメンバデータをそれぞれ Formに表示 
         */
        private void Btn_Rd_Click(object sender, EventArgs e)
        {
            string jsonData = string.Empty;

            StreamReader sr = new StreamReader("C:\\TestJsonR.txt", Encoding.GetEncoding("SHIFT_JIS"));
            while (sr.EndOfStream == false)
            {
                //1行毎に入力
                string line = sr.ReadLine();

                //ここで読み込んだ行をjsonとして扱う
                jsonData += line + System.Environment.NewLine;
            }
            sr.Close();

            // ReadData を表示
            TxtBox_RdData.Text = jsonData;

            try {
                // シリアライザ用意
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(InfoClass));

                byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
                MemoryStream ms = new MemoryStream(bytes);
                InfoClass infoc = (InfoClass)serializer.ReadObject(ms); // 読み込んだデータを InfoClass に保存

                // InfoClass に分解して保存したものを Formに表示
                TxtBox_ID.Text = Convert.ToString(infoc.ID);
                TxtBox_NAME.Text = infoc.NAME;
                TxtBox_TYPE.Text = infoc.TYPE;
            }
            catch(System.Runtime.Serialization.SerializationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**
         *  @brief  Json Write Button処理
         *  @note   Form情報を InfoClass に保存後、シリアライズ化し、
         *          JSON形式でファイルに保存
         */
        private void Btn_Wt_Click(object sender, EventArgs e)
        {
            // シリアライザ用意
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(InfoClass));

            // Form上の情報を InfoClass にまとめる
            InfoClass infoc = new InfoClass();
            infoc.ID = Convert.ToInt32(TxtBox_ID.Text);
            infoc.NAME = TxtBox_NAME.Text;
            infoc.TYPE = TxtBox_TYPE.Text;

            // InfoClass の情報を シリアライズ化
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, infoc);

            string jsonData = Encoding.UTF8.GetString(ms.ToArray());
            //TxtBox_RdData.Text = jsonData

            // JSON形式情報をFileに保存
            StreamWriter sw = new StreamWriter("C:\\TestJsonW.txt", false, Encoding.GetEncoding("SHIFT_JIS"));
            sw.WriteLine(jsonData);
            sw.Close();
        }
    }

    /**
     *  @brief  JSON用データクラス
     *  @note   Data={"ID":x, "NAME":"xxx", "TYPE":"xxx"}
     */
    [System.Runtime.Serialization.DataContract]
    class InfoClass
    {
        [System.Runtime.Serialization.DataMember()]
        public int ID { get; set; }

        [System.Runtime.Serialization.DataMember()]
        public string NAME { get; set; }

        [System.Runtime.Serialization.DataMember()]
        public string TYPE { get; set; }

    }

}
