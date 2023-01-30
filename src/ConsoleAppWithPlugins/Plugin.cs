using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace ConsoleAppWithPlugins
{
    public class Plugin : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;

        public Plugin(string baseDir, string fileName)
            : base(true)
        {
            BaseDir = baseDir;
            FileName = fileName;
            PluginPath = Path.Combine(BaseDir, FileName);

            resolver = new AssemblyDependencyResolver(PluginPath);
        }

        public string FileName { get; private set; }

        public string PluginPath { get; private set; }

        public string BaseDir { get; private set; }

        public Assembly Assembly { get; private set; }

        public Assembly LoadMainAssembly() =>
            LoadAssembly(AssemblyName.GetAssemblyName(PluginPath));

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

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            var libPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libPath == null)
            {
                return IntPtr.Zero;
            }

            return LoadUnmanagedDllFromPath(libPath);
        }

        public Assembly ResolveDependencies(AssemblyLoadContext ctx, AssemblyName ass)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(
                Path.Combine(BaseDir, $"{ass.Name}.dll")
            );
        }
    }
}
