using System.Windows.Controls;

namespace DeveloperHelper
{
    public partial class TypesView : UserControl
    {
        public TypesView()
        {
            InitializeComponent();

            var vm = new TypesViewModel();
            DataContext = vm;
        }
    }
}
