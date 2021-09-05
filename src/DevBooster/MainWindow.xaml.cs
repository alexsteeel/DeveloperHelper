using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace DevBooster
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(DialogCoordinator.Instance);
        }
    }
}
