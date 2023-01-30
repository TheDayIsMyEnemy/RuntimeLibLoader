using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace ConsoleAppWithPlugins
{
    public class Plugin : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;

        public Plugin(string path)
            : base(true)
        {
            resolver = new AssemblyDependencyResolver(path);
        }

        public Assembly Assembly { get; private set; }

        public Assembly LoadAssembly(AssemblyName name)
        {
            string assemblyPath = resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                Assembly = LoadFromAssemblyPath(assemblyPath);
                return Assembly;
            }

            return null;
        }

        // protected override nint LoadUnmanagedDll(string unmanagedDllName)
        // {
        //     var libPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        //     if (libPath == null)
        //     {
        //         return IntPtr.Zero;
        //     }

        //     return LoadUnmanagedDllFromPath(libPath);
        // }
    }
}
