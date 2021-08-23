using System.Windows.Controls;

namespace Reflection
{
    public partial class TypesView : UserControl
    {
        public TypesView()
        {
            InitializeComponent();

            DataContext = new TypesViewModel();
        }
    }
}
