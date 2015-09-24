using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFactoryMethod.Framework;  // add
using System.Collections;           // add for ArrayList

namespace TestFactoryMethod.Mecha
{
    /**
     * @brief   MechaFactory Class
     * @note    メカを作成し、内部Listに登録
     */
    public class MechaFactory : Factory
    {
        // 作成したメカ登録用List作成
        private ArrayList mechaes = new ArrayList();

        public ArrayList Mechaes
        {
            get { return mechaes; }
        }

        /**
         *  @brief  製品作成
         *  @param[in]   string  l_name  メカ名
         *  @return     Mecha   Object
         */
        protected override Product CreateProduct(string l_name)
        {
            return new Mecha(l_name);
        }

        /**
         *  @brief 製品登録
         *  @param[in]  Product l_product   製品Object 
         */
        protected override void RegisterProduct(Product l_product)
        {
            mechaes.Add( ((Mecha)l_product).Name );
        }

    }
}
