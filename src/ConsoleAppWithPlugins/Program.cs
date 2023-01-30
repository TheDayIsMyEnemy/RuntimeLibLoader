using System.Reflection;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Loader;
using System.Collections.Generic;

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

            var fileNames = new string[] { "BeautifulConsolePlugin.dll", "EmojiSpammerPlugin.dll" };

            var plugins = new List<Plugin>();

            foreach (var fileName in fileNames)
            {
                var plugin = PluginLoader.LoadPlugin(baseDir, fileName);
                plugins.Add(plugin);

                plugin.LoadMainAssembly();
                Console.WriteLine(
                    $"\n{plugin.Assembly} loaded at {DateTime.Now.ToShortTimeString()}\n"
                );

                plugin.Resolving += plugin.ResolveDependencies;

                var cmd = PluginLoader.CreateCommand(plugin.Assembly);
                if (cmd != null)
                {
                    cmd.Execute();
                }

                plugin.Unload();

                Console.WriteLine("\n\n");

                plugin.Unloading += (ctx) =>
                {
                    Console.WriteLine($"\n{ctx} unloaded at {DateTime.Now.ToShortTimeString()}\n");
                };
            }
        }
    }
}
