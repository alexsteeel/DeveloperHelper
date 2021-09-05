using MahApps.Metro.Controls.Dialogs;

namespace DevBooster
{
    public class MainViewModel
    {
        private IDialogCoordinator _dialogCoordinator;
        public MainViewModel(IDialogCoordinator instance)
        {
            _dialogCoordinator = instance;
        }
    }
}
