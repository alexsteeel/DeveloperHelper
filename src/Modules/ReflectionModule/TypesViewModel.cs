using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Reflection
{
    public class TypesViewModel : BindableBase
    {
        public TypesViewModel()
        {
            LoadTypesCommand = new DelegateCommand(LoadTypes);
        }

        private void LoadTypes()
        {
            var typeLoader = new TypeLoader();
            Types = new ObservableCollection<Type>(typeLoader.Load(AssemblyPath));
        }

        public ObservableCollection<Type> _types;
        public ObservableCollection<Type> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }

        public string AssemblyPath { get; set; }

        public DelegateCommand LoadTypesCommand { get; }
    }
}
