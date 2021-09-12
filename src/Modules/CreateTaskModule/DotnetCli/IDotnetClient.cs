using System.Collections.Generic;

namespace ProjectManagementModule
{
    /// <summary>
    /// Client for dotnet cli.
    /// </summary>
    public interface IDotnetClient
    {
        bool CreateProject(string templateShortName, string path);
        List<DotnetTemplate> GetTemplates();
    }
}