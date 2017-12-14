using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AppPosht.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace AppPosht.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private DelegateCommand _refreshCommand;
        private ObservableCollection<Report> _reports;
        private bool _isEnabledReportsLoaded;

        public DelegateCommand RefreshComand
        {
            get => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));
            protected set => _refreshCommand = value;
        }

        public ObservableCollection<Report> Reports
        {
            get => _reports;
            protected set => SetProperty(ref _reports , value);
        }

        public bool IsEnabledReportsLoaded
        {
            get => _isEnabledReportsLoaded;
            set => SetProperty(ref _isEnabledReportsLoaded , value);
        }

        public MainViewModel()
        {
            IsEnabledReportsLoaded = true;
            Reports = new ObservableCollection<Report>();
            Refresh();
        }

        public async void Refresh()
        {
            try
            {
                IsEnabledReportsLoaded = false;
                Reports.Clear();
                if (Directory.Exists(Properties.Settings.Default.DirectoryNameOut))
                {
                    Reports.AddRange(Directory
                        .EnumerateFiles(Properties.Settings.Default.DirectoryNameOut, "*.xls")
                        .Select(p => new Report(p))
                    );
                    foreach (var report in Reports)
                    {
                        await report.LoadAsync().ConfigureAwait(true);
                    }
                }
            }
            finally
            {
                IsEnabledReportsLoaded = true;
            }
        }
    }
}