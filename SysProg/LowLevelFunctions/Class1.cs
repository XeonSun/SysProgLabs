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
    public class LowLevelFunctions
    {
        private delegate int Operation(int a, int b);
        static int LowLelelDiv(int a,int b)
        {
            Type[] OperationArgs = { typeof(int), typeof(int) };
            DynamicMethod div = new DynamicMethod("Div", typeof(int), OperationArgs);
            ILGenerator il = div.GetILGenerator(256);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Div);
            il.Emit(OpCodes.Ret);
            Operation div_op = (Operation) div.CreateDelegate(typeof(Operation));

            return div_op(a, b);
        }

        static int LowLelelXor(int a,int b)
        {
            Type[] OperationArgs = { typeof(int), typeof(int) };
            DynamicMethod xor = new DynamicMethod("Xor", typeof(int), OperationArgs);
            ILGenerator il = xor.GetILGenerator(256);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Xor);
            il.Emit(OpCodes.Ret);
            Operation xor_op = (Operation)xor.CreateDelegate(typeof(Operation));

            return xor_op(a, b);
        }
    }
}
