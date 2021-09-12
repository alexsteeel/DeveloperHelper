using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectManagementModule
{
    /// <summary>
    /// Base class for view model with INotifyPropertyChanged and INotifyDataErrorInfo.
    /// Example for implementing INotifyDataErrorInfo is taken from
    /// https://docs.microsoft.com/en-us/previous-versions/windows/apps/ee652637(v=vs.105)
    /// </summary>
    public abstract class BaseViewModel : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errors =
            new Dictionary<string, List<string>>();

        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!errors.ContainsKey(propertyName))
            {
                errors[propertyName] = new List<string>();
            }

            if (!errors[propertyName].Contains(error))
            {
                if (isWarning)
                {
                    errors[propertyName].Add(error);
                }
                else
                {
                    errors[propertyName].Insert(0, error);
                }

                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0)
                {
                    errors.Remove(propertyName);
                }

                RaiseErrorsChanged(propertyName);
            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return string.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)
                ? null
                : errors[propertyName];
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        public bool CheckProperty<T>(T value, string propertyName, string error, Func<T, bool> func)
        {
            bool isValid = true;

            if (func(value))
            {
                AddError(propertyName, error, false);
                isValid = false;
            }
            else
            {
                RemoveError(propertyName, error);
            }

            return isValid;
        }
    }
}
