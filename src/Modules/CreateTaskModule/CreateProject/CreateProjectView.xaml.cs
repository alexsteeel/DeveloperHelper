using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Configuration;
using System.Windows.Controls;

namespace ProjectManagement
{
    public partial class CreateProjectView : UserControl
    {
        public CreateProjectView(IConfiguration configuration)
        {
            InitializeComponent();

            DataContext = new CreateProjectViewModel(DialogCoordinator.Instance, configuration);
        }
    }
}
