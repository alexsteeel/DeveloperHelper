using System;
using System.Reflection;

namespace Reflection
{
    public class TypeLoader
    {
        public Type[] Load(string assemblyPath)
        {
            Assembly asm = Assembly.LoadFrom(assemblyPath);
            return asm.GetTypes();
        }
    }
}
