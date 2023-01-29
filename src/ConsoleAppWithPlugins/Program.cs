// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System;
using System.Linq;

namespace ConsoleAppWithPlugins
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var projectDir = string.Join(
                '/',
                AppDomain.CurrentDomain.BaseDirectory.Split(new char[] { '/' }).SkipLast(4)
            );

            var plugin = PluginLoader.LoadPlugin(
                projectDir,
                "dlls",
                "BeautifulConsoleLib",
                "BeautifulConsolePlugin.dll"
            );

            if (plugin == null){
                return;
            }

            var cmd = PluginLoader
                .CreatePluginCommands(plugin)
                .FirstOrDefault();

            if (cmd == null){
                return;
            }

            cmd.Execute("Hello world!", "How you doing?", "Plugin component test");

        }
    }
}
