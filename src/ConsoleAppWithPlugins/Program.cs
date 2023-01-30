using System.Reflection;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Loader;

namespace ConsoleAppWithPlugins
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var baseDir = string.Join(
                '/',
                AppDomain.CurrentDomain.BaseDirectory.Split(new char[] { '/' }).SkipLast(5)
            );

            var fileName = "BeautifulConsolePlugin.dll";

            var plugin = PluginLoader.LoadPlugin(baseDir, fileName);

            plugin
            .LoadAssembly(
                AssemblyName.GetAssemblyName(
                    Path.GetFileNameWithoutExtension(fileName)));

            var cmd = PluginLoader.CreateCommand(plugin.Assembly);

            if (cmd == null){
                return;
            }

            cmd.Execute("Hello world!", "How you doing?", "Plugin component test");
        }
    }
}
