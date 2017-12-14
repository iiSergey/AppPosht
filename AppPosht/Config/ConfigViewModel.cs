using Prism.Mvvm;

namespace AppPosht.Config
{
    public class ConfigViewModel: BindableBase
    {
        private string _directoryIn;
        private string _directoryOut;

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