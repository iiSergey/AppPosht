using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using AppPosht.Helper;
using Prism.Commands;
using Prism.Mvvm;

namespace AppPosht.WindowApp.Main
{
    public class MainWindowViewModel : BindableBase
    {
        private LanguageData _languageSelect;
        private ObservableCollection<LanguageData> _languages;

        public MainWindowLocalizationViewModel Localization { get; }

        public MainWindowViewModel()
        {
            Languages = new ObservableCollection<LanguageData>(LanguageData.AllLanguage);
            LanguageSelect = Languages.FirstOrDefault(p => p.Language.Contains(App.App.Language.Name));
            Localization = new MainWindowLocalizationViewModel();
            SelectedCommand = new DelegateCommand(() => { }).ObservesProperty(() => LanguageSelect);
        }

        public DelegateCommand SelectedCommand { get; }

        public ObservableCollection<LanguageData> Languages
        {
            get => _languages;
            protected set => SetProperty(ref _languages, value);
        }

        public LanguageData LanguageSelect
        {
            get => _languageSelect;
            set
            {
                App.App.Language = new CultureInfo(value.Language);
                SetProperty(ref _languageSelect, value);
            }
        }
    }
}