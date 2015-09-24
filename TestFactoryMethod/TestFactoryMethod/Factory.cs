using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TestFactoryMethod.Framework
{
    /**
     * @brief   Factory Class
     * @note    製品生成、登録
     */
    public abstract class Factory
    {
        /**
         *  @brief  製品作成
         *  @param[in]   string  l_name  メカ名
         *  @return     product   Object
         */
        public Product Create(string l_name)						// 製品を作成して登録
        {
            Product product = CreateProduct(l_name);
            RegisterProduct(product);

            return product;
        }

        protected abstract Product CreateProduct(string l_name);	// 製品を作成
        protected abstract void RegisterProduct(Product l_product);	// 製品を登録
        
    }
}

