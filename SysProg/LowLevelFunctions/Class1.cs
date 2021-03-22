using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;

namespace LowLevelFunctions
{
    public class Class1
    {
        private delegate int Operation(int a, int b);
        static void Main()
        {
            Type[] OperationArgs = { typeof(int), typeof(int) };
            DynamicMethod div = new DynamicMethod("Div", typeof(int), OperationArgs);
            ILGenerator il = div.GetILGenerator(256);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Div);
            il.Emit(OpCodes.Ret);
            Operation div_op = (Operation) div.CreateDelegate(typeof(Operation));
            Console.WriteLine(div_op(23, 2));
        }
    }
}
