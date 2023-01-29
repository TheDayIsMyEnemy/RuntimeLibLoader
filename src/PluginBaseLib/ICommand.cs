namespace PluginBaseLib
{
    public interface ICommand
    {
        string Name { get; }
        
        CommandResult Execute(params string[] args);
    }
}
