using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    class LowLevelModel : ILowLevelModel
    {
        public string Div(string a, string b)
        {
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                return "Не корректные входные данные.";
            }
            else
            {
                try
                {
                    int result = LowLevelFunctions.LowLevelFunctions.LowLelelDiv(x, y);
                    return result.ToString();
                }
                catch (Exception ex)
                {
                    return "Произошла ошибка при вычислении: " + ex.Message;
                }
            }
        }

        public string Xor(string a, string b)
        {
            int x = 0, y = 0;
            if (!int.TryParse(a, out x) || !int.TryParse(b, out y))
            {
                return "Не корректные входные данные.";
            }
            else
                return LowLevelFunctions.LowLevelFunctions.LowLelelXor(x, y).ToString();
        }
    }
}
