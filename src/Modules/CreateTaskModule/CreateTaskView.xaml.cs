using System.Windows.Controls;

namespace CreateTask
{
    public partial class CreateTaskView : UserControl
    {
        public CreateTaskView()
        {
            InitializeComponent();

            DataContext = new CreateTaskViewModel();
        }
    }
}
