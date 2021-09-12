using Dapplo.Confluence;
using Dapplo.Confluence.Query;
using GitLabApiClient;
using GitLabApiClient.Models.Projects.Requests;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementModule
{
    public class CreateProjectViewModel : BaseViewModel
    {
        private IDialogCoordinator _dialogCoordinator;
        private readonly IConfiguration _configuration;
        private readonly IDotnetClient _dotnetClient;
        private readonly ILogger _logger;

        public CreateProjectViewModel(
            IDialogCoordinator instance,
            IConfiguration configuration,
            IDotnetClient dotnetClient,
            ILogger logger)
        {
            _dialogCoordinator = instance;
            _configuration = configuration;
            _dotnetClient = dotnetClient;

            Project = new ProjectViewModel();
            Project.ErrorsChanged += Project_ErrorsChanged;

            DotnetTemplates = new ObservableCollection<DotnetTemplate>(_dotnetClient.GetTemplates());
            SelectedDotnetTemplate = DotnetTemplates.FirstOrDefault();

            CreateProjectCommand = new DelegateCommand(async () => await CreateProject())
                .ObservesCanExecute(() => CanCreateProject);

            _logger = logger;
            if (_logger is TextLogger)
            {
                ((TextLogger)_logger).LogAdded += CreateProjectViewModel_LogAdded;
            }
            _logger.LogInformation("Module initialized.");
        }

        private void Project_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            CanCreateProject = (!Project?.HasErrors ?? false) && !HasErrors;
        }

        private void CreateProjectViewModel_LogAdded(object sender, TextLogEventArgs e)
        {
            Log += e.Message + "\r\n";
        }

        public DelegateCommand CreateProjectCommand { get; }

        private async Task CreateProject()
        {
            IsLoaderActive = true;

            try
            {
                // create project from dotnet template
                await Task.Run(() =>
                    _dotnetClient.CreateProject(SelectedDotnetTemplate.ShortName, Path.Combine(Project.SavePath, Project.Name)));

                // create git repository and gitlab project
                await CreateGitlabProject();

                var query = Where.And(Where.Type.IsPage, Where.Text.Contains("Test Home"));
                try
                {
                    var searchResult = await Confluence.Content.SearchAsync(query);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            finally
            {
                IsLoaderActive = false;
            }
        }

        private async Task CreateGitlabProject()
        {
            try
            {
                var projects = await Gitlab.Projects.GetAsync();
                if (!projects.Any(x => x.Name == Project.Name))
                {
                    _logger.LogInformation("Start of creating a new gitlab project.");
                    var createProjectRequest = CreateProjectRequest.FromName(Project.Name);
                    await Gitlab.Projects.CreateAsync(createProjectRequest);
                    _logger.LogInformation("The gitlab project has been successfully created.");
                }
                else
                {
                    _logger.LogWarning("Repository with the same name already exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private bool _canCreateProject;
        private bool CanCreateProject
        {
            get { return _canCreateProject; }
            set { SetProperty(ref _canCreateProject, value); }
        }

        private IConfluenceClient CreateConfluenceClient()
        {
            var confluenceAddress = _configuration.GetSection("Confluence:Address").Value;
            var login = _configuration.GetSection("Confluence:Login").Value;
            var password = _configuration.GetSection("Confluence:Password").Value;

            var confluenceClient = ConfluenceClient.Create(new Uri(confluenceAddress));
            confluenceClient.SetBasicAuthentication(login, password);
            return confluenceClient;
        }

        private IGitLabClient CreateGitLabClient()
        {
            var gitlabAddress = _configuration.GetSection("Gitlab:Address").Value;
            var gitlabAccessToken = _configuration.GetSection("Gitlab:AccessToken").Value;
            return new GitLabClient(gitlabAddress, gitlabAccessToken);
        }

        public IConfluenceClient _confluence;
        public IConfluenceClient Confluence => _confluence ??= CreateConfluenceClient();

        public IGitLabClient _gitlab;
        public IGitLabClient Gitlab => _gitlab ??= CreateGitLabClient();


        public ProjectViewModel _project;
        public ProjectViewModel Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        public ObservableCollection<DotnetTemplate> _dotnetTemplates;
        public ObservableCollection<DotnetTemplate> DotnetTemplates
        {
            get { return _dotnetTemplates; }
            set { SetProperty(ref _dotnetTemplates, value); }
        }

        public DotnetTemplate _selectedDotnetTemplate;
        public DotnetTemplate SelectedDotnetTemplate
        {
            get { return _selectedDotnetTemplate; }
            set { SetProperty(ref _selectedDotnetTemplate, value); }
        }

        public string _log;
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        public bool _isLoaderActive;
        public bool IsLoaderActive
        {
            get { return _isLoaderActive; }
            set { SetProperty(ref _isLoaderActive, value); }
        }
    }
}
