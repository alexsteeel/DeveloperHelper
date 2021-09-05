using Prism.Commands;
using Prism.Mvvm;

namespace CreateTask
{
    public class CreateTaskViewModel : BindableBase
    {
        public DelegateCommand LoadTypesCommand { get; }
    }
}
