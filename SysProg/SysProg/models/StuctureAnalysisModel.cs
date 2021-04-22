using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    public class StuctureAnalysisModel : IStructureAnalysisModel
    {
        public string AnalysisWhile(string structure)
        {
            try
            {
                bool analysis = StructureAnalysis.CheckStructWhile(structure);
                return analysis ? "Конструкция while выполнится хотя бы 1 раз." : "Конструкция while не выполнится ни разу.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception)
            {
                return "Строка должна быть конструкцией языка C#.";
            }
        }

        public string AnalysisFor(string structure)
        {
            try
            {
                int analysis = StructureAnalysis.CheckStructFor(structure);
                return "Конструкция for выполниться " + analysis + " раз.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (Exception)
            {
                return "Строка должна быть конструкцией языка C#.";
            }
        }
    }
}
