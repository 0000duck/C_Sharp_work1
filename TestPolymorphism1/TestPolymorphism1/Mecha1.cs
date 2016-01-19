using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPolymorphism1
{
    /**
     * @brief   Mecha1 Class
     * @note    Mecha1 本体のクラス。
     */
    public class Mecha1 : AbstMecha
    {
        public  override string GetContent()
        {
            return "Mecha1 excecuted.";
        }

    }
}
