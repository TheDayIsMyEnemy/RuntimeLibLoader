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


### Assembly resolving func needs to be implemented in order to load the main assembly dependencies

- It happens when an assembly fails to load into the load context.
- Assembly resolving method signature:

    `Func<AssemblyLoadContext, AssemblyName, Assembly?>? AssemblyLoadContext.Resolving`



### Build and run

- Building the plugin projects first, and considering you are in the folder with the sln file
   
    ` dotnet build `
   
- Running the console app

    ` dotnet run `



### References

- https://learn.microsoft.com/en-us/dotnet/core/dependency-loading/understanding-assemblyloadcontext
- https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability
