using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Windows.Controls;

namespace ProjectManagementModule
{
    public partial class CreateProjectView : UserControl
    {
        public CreateProjectView(IConfiguration configuration, IDotnetClient dotnetClient, ILogger logger)
        {
            InitializeComponent();

            DataContext = new CreateProjectViewModel(DialogCoordinator.Instance, configuration, dotnetClient, logger);
        }
    }
}
