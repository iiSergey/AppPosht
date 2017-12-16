using AppPosht.Helper;

namespace AppPosht.ReportConvert
{
    public class ReportConvertPageLocalizationViewModel : LocalizationViewModel
    {
        public ReportConvertPageLocalizationViewModel()
        {
            Property=new[]
            {
                nameof(Convert),
                nameof(WindowTitle),
                nameof(RefreshListFiles)
            };
        }

        public string Convert => ReportConvertPageResource.Convert;
        public string WindowTitle => ReportConvertPageResource.WindowTitle;
        public string RefreshListFiles => ReportConvertPageResource.RefreshListFiles;
    }
}