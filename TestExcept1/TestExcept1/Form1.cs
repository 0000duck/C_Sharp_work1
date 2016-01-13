//-------------------------------------------------------
//  例外処理 Sample
//
//  例外を throw して、伝搬するのを確認したサンプル Project
//
//  division()メソッド内で例外を throw し、メソッド呼び出しもとで
//  catch している。
//-------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestExcept1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /**
         * @brief   division
         * @note    v1 を v2 で割る
         * @param[in]   int v1  割られる数
         * @param[in]   int v2  割る数
         * @return      int result
         */
        private int division(int v1, int v2)
        {
            try {
                if (v1 == 0)
                {
                    // 0割なので、例外を発生。
                    throw (new Exception("Error. 0 divided."));
                }
                else
                {
                    return v1 / v2;
                }
            }
            catch (Exception ex)
            {
                // 例外を呼び出し元へ通知
                throw (new Exception(ex.Message));
            }
        }

        /**
         * @brief   button1処理
         * @note    division() お試し処理
         */
        private void button1_Click(object sender, EventArgs e)
        {
            int data1 = 0;
            int data2 = 2;
            int iret = 0;

            try {
                iret = division(data1, data2);
            }
            catch(Exception ex)
            {
                // 呼び出し先の例外を拾う
                Console.WriteLine(ex.Message);
            }
        }
    }
}
