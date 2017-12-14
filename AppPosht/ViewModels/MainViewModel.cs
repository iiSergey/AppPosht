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

        public DelegateCommand RefreshComand
        {
            get => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));
            private set => _refreshCommand = value;
        }

        public ObservableCollection<Report> Reports { get; set; }=new ObservableCollection<Report>();
        public bool IsEnabledReportsLoaded { get; set; } = true;
        public MainViewModel()
        {
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