using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace ConsoleAppWithPlugins
{
    public class Plugin : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;
        private readonly string path;

        public Plugin(string pluginPath)
        {
            resolver = new AssemblyDependencyResolver(pluginPath);
            path = pluginPath;
        }


        public Assembly? Load()
            => LoadFromAssemblyPath(path);

        // protected override Assembly? Load(AssemblyName assemblyName)
        // {
        //     var libPath = resolver.ResolveAssemblyToPath(assemblyName);
        //     if (libPath == null)
        //     {
        //         return null;
        //     }

        //     return LoadFromAssemblyPath(libPath);
        // }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            var libPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libPath == null)
            {
                return IntPtr.Zero;
            }

            return LoadUnmanagedDllFromPath(libPath);
        }
    }
}
