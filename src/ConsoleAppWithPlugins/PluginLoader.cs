using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using PluginBaseLib;

namespace ConsoleAppWithPlugins
{
    internal class PluginLoader
    {
        internal static Plugin LoadPlugin(string baseDir, string fileName)
        {
            var dir = Directory.GetFiles(baseDir, fileName, SearchOption.AllDirectories);

            var pluginBaseDir = string.Join("/", dir[0].Split("/").SkipLast(1)) + "/";

            return new Plugin(pluginBaseDir, fileName);
        }

        internal static ICommand CreateCommand(Assembly a)
        {
            var t = a.GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t)).FirstOrDefault();

            return (ICommand)Activator.CreateInstance(t);
        }
    }
}
