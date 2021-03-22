using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace Logic
{
    public abstract class Logic
    {   
        public static bool CheckStructVar7(string code)
        {
            int start = IsFitVar7(code);
            if (start == -1)
                return false;
            while (code[start] == ' ')
                start++;
            switch(code[start])
            {
                case ';':
                    code = code.Insert(start, "return true");
                    break;
                case '{':
                    code = code.Insert(start + 1, "return true;");
                    break;
                default:
                    code = code.Insert(start, "return true;");
                    break;
            }
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters { GenerateInMemory = true };
            parameters.ReferencedAssemblies.Add("System.dll");
            try
            {
                Console.WriteLine($@"
                    using System;
 
                    public static class Checker 
                    {{
                        public static bool F()
                        {{
                            {code}
                        }}
                    }}");
                var results = provider.CompileAssemblyFromSource(parameters, $@"
                    using System;
 
                    public static class Checker 
                    {{
                        public static bool F()
                        {{
                            {code}
                        }}
                    }}");
                var method = results.CompiledAssembly.GetType("Checker").GetMethod("F");
                var func = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), null, method);
                return func();
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException("Input should be valid C# expression", nameof(code));
            }
        }

        private static int IsFitVar7(string code)
        {
            int startStruct = code.IndexOf("while");
            if (startStruct != -1)
            {
                startStruct += 5;
                while (code[startStruct] == ' ')
                    startStruct++;
                int needToFind = 1;
                startStruct++;
                while(startStruct < code.Length && needToFind > 0)
                {
                    if (code[startStruct] == '(')
                        needToFind++;
                    else
                    if (code[startStruct] == ')')
                        needToFind--;
                    startStruct++;
                }
                if (startStruct >= code.Length)
                    startStruct = -1;
            }
            return startStruct;
        }
    }
}
