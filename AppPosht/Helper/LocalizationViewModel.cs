using Prism.Mvvm;

namespace AppPosht.Helper
{
    public abstract class LocalizationViewModel : BindableBase
    {
        protected string[] Property { get; set; }

        protected LocalizationViewModel()
        {
            App.App.LanguageChanged += (sender, args) => ChangeLanguage();
        }

        public void ChangeLanguage()
        {
            if (Property != null)
                foreach (var item in Property)
                    RaisePropertyChanged(item);
        }
    }
}