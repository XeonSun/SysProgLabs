using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;

namespace Logic.Model
{
    public static class StructureAnalysis
    {

        public static bool CheckStructWhile(string code)
        {
            int start = IsFitWhile(code);
            if (start == -1)
                throw new ArgumentException("Не правильная конструкция while");
            while (start < code.Length && (code[start] == ' ' || code[start] == '\n'))
                start++;
            if (start < code.Length)
            {
                switch (code[start])
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
                var results = provider.CompileAssemblyFromSource(parameters, $@"
                    using System;
 
                    public static class Checker 
                    {{
                        public static bool F()
                        {{
                            {code}
                            return false;
                        }}
                    }}");
                var method = results.CompiledAssembly.GetType("Checker").GetMethod("F");
                var func = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), null, method);
                return func();
            }
            throw new ArgumentException("Не правильная конструкция while");
        }

        private static int IsFitWhile(string code)
        {
            int startStruct = code.IndexOf("while");
            if (startStruct != -1)
            {
                startStruct += 5;
                while (startStruct < code.Length && (code[startStruct] == ' ' || code[startStruct] == '\n'))
                    startStruct++;
                if (startStruct < code.Length && code[startStruct] == '(')
                {
                    int needToFind = 1;
                    startStruct++;
                    while (startStruct < code.Length && needToFind > 0)
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
                else
                    startStruct = -1;
            }
            return startStruct;
        }

        public static int CheckStructFor(string code)
        {
            int start = IsFitFor(code);
            if (start == -1)
                throw new ArgumentException("Не правильная конструкция for");
            while (start < code.Length && (code[start] == ' ' || code[start] == '\n'))
                start++;
            if (start < code.Length)
            {
                switch (code[start])
                {
                    case ';':
                        code = code.Remove(start, 1);
                        code = code.Insert(start, $@"{{count++; if(count == {int.MaxValue}) return count;}}");
                        break;
                    case '{':
                        code = code.Insert(start + 1, $@"count++; if(count == {int.MaxValue}) return count;");
                        break;
                    default:
                        string inserting = $@"{{count++; if(count == {int.MaxValue}) return count;";
                        code = code.Insert(start, inserting);
                        start += inserting.Length;
                        start = code.IndexOf(';', start);
                        if (start == -1)
                            throw new ArgumentException("Не правильная конструкция for");
                        code = code.Insert(++start, "}");
                        break;
                }
                var provider = new CSharpCodeProvider();
                var parameters = new CompilerParameters { GenerateInMemory = true };
                parameters.ReferencedAssemblies.Add("System.dll");
                var results = provider.CompileAssemblyFromSource(parameters, $@"
                    using System;
 
                    public static class Checker 
                    {{
                        public static int F()
                        {{
                            int count = 0;
                            {code}
                            return count;
                        }}
                    }}");
                var method = results.CompiledAssembly.GetType("Checker").GetMethod("F");
                var func = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), null, method);
                return func();
            }
            throw new ArgumentException("Не правильная конструкция for");
        }

        private static int IsFitFor(string code)
        {
            int startStruct = code.IndexOf("for");
            if (startStruct != -1)
            {
                startStruct += 3;
                while (startStruct < code.Length && (code[startStruct] == ' ' || code[startStruct] == '\n'))
                    startStruct++;
                if (startStruct < code.Length && code[startStruct] == '(')
                {
                    int needToFind = 1;
                    startStruct++;
                    while (startStruct < code.Length && needToFind > 0)
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
                else
                    startStruct = -1;
            }
            return startStruct;
        }
    }
}
