using Dapplo.Confluence;
using Dapplo.Confluence.Query;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;

namespace ProjectManagement
{
    public class CreateProjectViewModel : BindableBase
    {
        private IDialogCoordinator _dialogCoordinator;
        private readonly IConfiguration _configuration;

        public CreateProjectViewModel(IDialogCoordinator instance, IConfiguration configuration)
        {
            _dialogCoordinator = instance;
            _configuration = configuration;
            Project = new ProjectViewModel();
            CreateProjectCommand = new DelegateCommand(async () => await CreateProject());
            Log += "Module initialized.\r\n";
        }

        public DelegateCommand CreateProjectCommand { get; }

        private async Task CreateProject()
        {
            if (string.IsNullOrWhiteSpace(Project.Name))
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Error", "You must specify the name of the project.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Project.Description))
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Error", "You must specify the description of the project.");
            }

            var query = Where.And(Where.Type.IsPage, Where.Text.Contains("Test Home"));
            try
            {
                var searchResult = await Confluence.Content.SearchAsync(query);
            }
            catch (Exception ex)
            {
                Log += ex.Message + "\r\n";
            }
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

        public string _log;
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }
    }
}
