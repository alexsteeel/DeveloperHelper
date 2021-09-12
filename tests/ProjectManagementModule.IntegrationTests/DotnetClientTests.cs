using NUnit.Framework;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace ProjectManagementModule.IntegrationTests
{
    public class DotnetClientTests
    {
        [Test]
        public void GetTemplates_ReturnMoreThanOneTemplate()
        {
            var dotnetClient = GetDotnetClient();
            var templates = dotnetClient.GetTemplates();

            templates[0].Name.Should().Be("Console Application");
            templates[0].ShortName.Should().Be("console");
            templates[0].Language.Should().Be("[C#],F#,VB");
            templates[0].Tags.Should().Be("Common/Console");
        }

        [Test]
        public void CreateProject_IncorrectTemplate_ReturnError()
        {
            var dotnetClient = GetDotnetClient();
            dotnetClient.CreateProject("_", @"_");
        }

        private DotnetClient GetDotnetClient()
        {
            var logger = new NullLogger<DotnetClient>();
            return new DotnetClient(logger);
        }
    }
}