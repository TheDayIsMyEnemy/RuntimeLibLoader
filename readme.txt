## Loading and unloading a DLL at runtime


### Project configuration for each concrete plugin (a child of the base dll)

 - EnableDynamicLoading must be added in the .csproj file
    in order to copy the dependencies in the output dir

    ` <EnableDynamicLoading>true</EnableDynamicLoading> `


- Set the project reference 'Private' element to false
    for the PluginBaseLib.csproj. This tells MSBuild to not copy
    the PluginBaseLib.dll to the output.

    ` <Private>false</Private> `


- Setting ExcluseAssets works the same way, but for package references

    ` <ExcludeAssets>runtime</ExcludeAssets> `


### Passing the relative path to the plugin

    ` dotnet run "BeautifulConsolePlugin.dll" `

### Reference

- https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability
