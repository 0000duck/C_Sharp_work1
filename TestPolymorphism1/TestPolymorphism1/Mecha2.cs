using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPolymorphism1
{
    /**
     * @brief   Mecha2 Class
     * @note    Mecha2 本体のクラス。
     */
    public class Mecha2 : AbstMecha
    {
        public override string GetContent()
        {
            return "Mecha2 excecuted.";
        }

    }
}
