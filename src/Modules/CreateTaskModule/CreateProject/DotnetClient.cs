using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectManagementModule.CreateProject
{
    public class DotnetClient
    {
        private readonly ILogger<DotnetClient> _logger;
        public DotnetClient(ILogger<DotnetClient> logger)
        {
            _logger = logger;
        }

        public List<DotnetTemplate> GetTemplates()
        {
            var templates = new List<DotnetTemplate>();

            var process = CreateDotnetProcess("new", "--list");
            process.Start();

            var nameMaxLength = 0;
            var shortNameMaxLength = 0;
            var languageMaxLength = 0;
            var tagsMaxLength = 0;

            var count = 0;
            while (!process.StandardOutput.EndOfStream)
            {
                var str = process.StandardOutput.ReadLine();

                if (count == 1)
                {
                    var strs = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    nameMaxLength = strs[0].Length;
                    shortNameMaxLength = strs[1].Length;
                    languageMaxLength = strs[2].Length;
                    tagsMaxLength = strs[3].Length;
                }

                if (count > 1 && !string.IsNullOrWhiteSpace(str))
                {
                    var dotnetTemplate = new DotnetTemplate();

                    dotnetTemplate.Name = str.Substring(0, nameMaxLength).Trim();
                    str = str.Remove(0, nameMaxLength + 2);

                    dotnetTemplate.ShortName = str.Substring(0, shortNameMaxLength).Trim();
                    str = str.Remove(0, shortNameMaxLength + 2);

                    dotnetTemplate.Language = str.Substring(0, languageMaxLength).Trim();
                    str = str.Remove(0, languageMaxLength + 2);

                    dotnetTemplate.Tags = str.Substring(0, tagsMaxLength).Trim();

                    templates.Add(dotnetTemplate);
                }

                count++;
            }

            return templates;
        }

        /// <summary>
        /// Create project from dotnet template.
        /// </summary>
        /// <param name="templateShortName">Short name of dotnet template, for example, console.</param>
        /// <param name="path">Output path for project.</param>
        /// <returns><c>true</c> if success, else <c>false</c>.</returns>
        public bool CreateProject(string templateShortName, string path)
        {
            var process = CreateDotnetProcess("new", templateShortName, $"--output {path}");
            process.Start();

            while (!process.StandardError.EndOfStream)
            {
                _logger.LogError(process.StandardError.ReadLine());
            }

            if (process.HasExited)
            {
                return false;
            }

            while (!process.StandardOutput.EndOfStream)
            {
                _logger.LogInformation(process.StandardOutput.ReadLine());
            }
            return true;
        }

        private static Process CreateDotnetProcess(params string[] args)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet.exe",
                    Arguments = string.Join(" ", args),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };
        }
    }
}
