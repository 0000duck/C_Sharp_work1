//-----------------------------------------------------------------------
//  FactoryMethod Pattern Saple Project
//
//  作者が理解した範囲でFactoryMethod Patternのサンプルを作成しました。
//  Factoryは factoryのまま、Productは、メカ(mecha)として考えました。
//  メカの実行(Execute)はサンプルなので Dummy処理(Debugg:Consoleへの文字列出力)
//  にしていあります。
//  namespace を以下に分解
//      TestFactoryMethod.Framework に Factory と Product クラスを配置
//      TestFactoryMethod.Mecha     に MechaFactory と Mecha クラスを配置
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;           // add
using TestFactoryMethod.Framework;  // add
using TestFactoryMethod.Mecha;      // add

namespace TestFactoryMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 工場生成
            Factory factory = new MechaFactory();     

            // メカ保存用配列用意
            ArrayList products = new ArrayList();

            // 2種類のメカ生成
            products.Add( factory.Create("メカ1"));
            products.Add( factory.Create("メカ2"));

            // 生成したメカを順に実行
            foreach (Product l_p in products)
            {
                l_p.Execute();
            }

            button1.Enabled = false;
        }
    }
}
