using System;
using System.IO;

namespace ProjectManagementModule
{
    public class ProjectViewModel : BaseViewModel
    {
        public ProjectViewModel()
        {
            Name = string.Empty;
            Description = string.Empty;
            SavePath = string.Empty;
        }

        private const string NameError = "Name must not be empty.";
        private const string DescriptionError = "Description must not be empty.";
        private const string SavePathError = "Path to project incorrect.";

        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                CheckProperty(value, nameof(Name), NameError, x => string.IsNullOrWhiteSpace(x));
                SetProperty(ref _name, value);
            }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                CheckProperty(value, nameof(Description), DescriptionError, x => string.IsNullOrWhiteSpace(x));
                SetProperty(ref _description, value);
            }
        }

        public string _savePath;
        public string SavePath
        {
            get { return _savePath; }
            set
            {
                CheckProperty(value, nameof(SavePath), SavePathError, x => string.IsNullOrWhiteSpace(x) || !Directory.Exists(value));
                SetProperty(ref _savePath, value);
            }
        }

        public Uri _gitRepository;
        public Uri GitRepository
        {
            get { return _gitRepository; }
            set { SetProperty(ref _gitRepository, value); }
        }

        public Uri _knowledgeBase;
        public Uri KnowledgeBase
        {
            get { return _knowledgeBase; }
            set { SetProperty(ref _knowledgeBase, value); }
        }

        public Uri _issueTracking;
        public Uri IssueTracking
        {
            get { return _issueTracking; }
            set { SetProperty(ref _issueTracking, value); }
        }
    }
}
