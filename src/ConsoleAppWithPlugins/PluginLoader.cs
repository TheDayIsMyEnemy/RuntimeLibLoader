using System.Reflection;
using PluginBaseLib;

namespace ConsoleAppWithPlugins
{
    internal class PluginLoader
    {
        internal static Assembly? LoadPlugin(
            string projectPath,
            string pluginsFolder,
            string pluginFolder,
            string pluginFileName
        ) =>  new Plugin(Path.Combine(
                    projectPath,
                    pluginsFolder,
                    pluginFolder,
                    pluginFileName))
                      .Load();

        internal static IEnumerable<ICommand> CreatePluginCommands(
            Assembly assembly
            ) =>  from t in assembly.GetTypes()
                  where typeof(ICommand).IsAssignableFrom(t)
                  select (Activator.CreateInstance(t) as ICommand)!;
    }
}
