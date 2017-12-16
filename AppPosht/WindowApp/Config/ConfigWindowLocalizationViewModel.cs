using AppPosht.Helper;

namespace AppPosht.WindowApp.Config
{
    public class ConfigWindowLocalizationViewModel : LocalizationViewModel
    {
        public ConfigWindowLocalizationViewModel()
        {
            Property=new[]
            {
                nameof(DirectoryIn),
                nameof(DirectoryOut),
                nameof(ConfigWindow),
                nameof(Cansel),
                nameof(Ok)
            };
        }
        
        public string DirectoryIn => ConfigWindowResource.DirectoryIn;
        public string DirectoryOut => ConfigWindowResource.DirectoryOut;
        public string ConfigWindow => ConfigWindowResource.ConfigWindow;
        public string Cansel => ConfigWindowResource.Cansel;
        public string Ok => ConfigWindowResource.Ok;
    }
}