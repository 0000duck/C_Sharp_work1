using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPolymorphism1
{
    /**
     * @brief   IMecha AbstMecha
     * @note    各メカ用 abstractクラス。   
     *          各メカが直接 IMecha を継承すると、似たコードが
     *          Procsssing() の中にできるので、本クラスをはさみ
     *          Processing()内で 共通部分を実装、
     *          個々の処理は抽象Method を callする形にし、
     *          個々の継承先で、内容を実装する
     */
    public abstract class AbstMecha : IMecha
    {
        public string Processing()
        {
            string content = string.Empty;

            // 以下が一応、共通処理
            System.Windows.Forms.MessageBox.Show("Mecha Executed.");

            // 以下が一応、個々の処理 (内容は継承先で実装)
            content = GetContent();

            return content;
        }

        public abstract string GetContent();
    }
}
