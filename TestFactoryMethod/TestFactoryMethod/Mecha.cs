using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFactoryMethod.Framework;  // add

namespace TestFactoryMethod.Mecha
{
    /**
     * @brief   Mecha Class
     * @note    メカ名保持。メカ実行(サンプルなので Dummy動作)
     */
    public class Mecha : Product
    {
        private string name;            // mecha名

        /**
         * @brief Constructor
         */
        internal Mecha(string l_name)
        {
            this.name = l_name;
        }

        public string Name
        {
            get { return name; }
        }

        /**
         * @brief   メカ実行 (サンプルなので Deb用Consoleに文字列出力するだけ
         */
        public override void Execute()
        {
            Console.WriteLine( name+" 実行。");
        }
    }
}
