using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperHelper
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            TypesViewModel = new TypesViewModel();
        }

        public TypesViewModel TypesViewModel { get; set; }
    }
}
