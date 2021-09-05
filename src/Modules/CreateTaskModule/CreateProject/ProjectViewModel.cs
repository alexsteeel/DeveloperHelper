using Prism.Mvvm;
using System;

namespace ProjectManagement
{
    public class ProjectViewModel : BindableBase
    {
        public string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
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
