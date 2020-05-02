using System;
using System.Reflection;

namespace DeveloperHelper
{
    public class TypeLoader
    {
        public Type[] Load(string assemblyPath)
        {
            Assembly asm = Assembly.LoadFrom(assemblyPath);
            Type[] types = asm.GetTypes();
            return types;
        }
    }
}
