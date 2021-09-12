using Dapplo.Confluence;
using Dapplo.Confluence.Query;
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
            _dotnetClient.CreateProject(SelectedDotnetTemplate.ShortName, Path.Combine(Project.SavePath, Project.Name));

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

        private bool _canCreateProject;
        private bool CanCreateProject
        {
            get { return _canCreateProject; }
            set { SetProperty(ref _canCreateProject, value); }
        }

        private IConfluenceClient CreateConfluenceClient()
        {
            var confluenceAddress = _configuration.GetSection("Confluence:Address").Value;
            var confluenceClient = ConfluenceClient.Create(new Uri(confluenceAddress));
            confluenceClient.SetBasicAuthentication(_configuration.GetSection("Confluence:Login").Value, _configuration.GetSection("Confluence:Password").Value);
            return confluenceClient;
        }

        public IConfluenceClient _confluence;
        public IConfluenceClient Confluence => _confluence ??= CreateConfluenceClient();


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
    }
}
