using AppPosht.Helper;

namespace AppPosht.WindowApp.Main
{
    public class MainWindowLocalizationViewModel : LocalizationViewModel
    {
        public MainWindowLocalizationViewModel()
        {
            Property=new[]
            {
                nameof(SettingsText)
            };
        }
        

        public string SettingsText => MainWindowResource.SettingsText;
    }
}