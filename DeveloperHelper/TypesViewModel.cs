using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DeveloperHelper
{
    public class TypesViewModel : INotifyPropertyChanged
    {
        public TypesViewModel()
        {
            LoadTypesCommand = new RelayCommand(o => LoadTypes());
        }

        private void LoadTypes()
        {
            var typeLoader = new TypeLoader();
            Types = new ObservableCollection<Type>(typeLoader.Load(AssemblyPath));
        }

        public ObservableCollection<Type> Types { get; set; }

        public string AssemblyPath { get; set; }

        public RelayCommand LoadTypesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
