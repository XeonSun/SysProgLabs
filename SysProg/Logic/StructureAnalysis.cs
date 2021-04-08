﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace Logic
{
    public static class StructureAnalysis
    {

        public static bool CheckStructVar7(string code)
        {
            int start = IsFitVar7(code);
            if (start == -1)
                return false;
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
                try
                {
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
                catch (FileNotFoundException)
                {
                    throw new ArgumentException("Input should be valid C# expression", nameof(code));
                }
            }
            return false;
        }

        private static int IsFitVar7(string code)
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

        public static int CheckStructVar11(string code)
        {
            int start = IsFitVar11(code);
            if (start == -1)
                return 0;
            while (start < code.Length && (code[start] == ' ' || code[start] == '\n'))
                start++;
            if (start < code.Length)
            {
                switch (code[start])
                {
                    case ';':
                        code = code.Insert(start, "count++");
                        break;
                    case '{':
                        code = code.Insert(start + 1, "count++;");
                        break;
                    default:
                        code = code.Insert(start, "{count++;");
                        start += 9;
                        start = code.IndexOf(';', start);
                        if (start == -1)
                            return 0;
                        code = code.Insert(++start, "}");
                        break;
                }
                var provider = new CSharpCodeProvider();
                var parameters = new CompilerParameters { GenerateInMemory = true };
                parameters.ReferencedAssemblies.Add("System.dll");
                try
                {
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
                catch (FileNotFoundException)
                {
                    throw new ArgumentException("Input should be valid C# expression", nameof(code));
                }
            }
            return 0;
        }

        private static int IsFitVar11(string code)
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
