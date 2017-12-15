using Prism.Commands;
using Prism.Mvvm;

namespace AppPosht.WindowApp.Config
{
    public class ConfigViewModel: BindableBase
    {
        private string _directoryIn;
        private string _directoryOut;
        private DelegateCommand _saveCommand;

        public ConfigViewModel()
        {
            DirectoryIn = Properties.Settings.Default.DirectoryNameIn;
            DirectoryOut = Properties.Settings.Default.DirectoryNameOut;
        }

        public void Save()
        {
            Properties.Settings.Default.DirectoryNameIn = DirectoryIn;
            Properties.Settings.Default.DirectoryNameOut = DirectoryOut;
            Properties.Settings.Default.Save();
        }

        public DelegateCommand SaveComand
        {
            get => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
            protected set => _saveCommand = value;
        }
        public string DirectoryIn
        {
            get => _directoryIn;
            set => SetProperty(ref _directoryIn, value);
        }
        public string DirectoryOut
        {
            get => _directoryOut;
            set => SetProperty(ref _directoryOut, value);
        }
    }
}