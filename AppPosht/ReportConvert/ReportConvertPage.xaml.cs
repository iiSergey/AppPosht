using System.Windows.Controls;
using AppPosht.WindowApp.Main;

namespace AppPosht.ReportConvert
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Логика взаимодействия для ReportConvertPage.xaml
    /// </summary>
    public partial class ReportConvertPage
    {
        public ReportConvertPage()
        {
            InitializeComponent();
            DataContext = new ReportConvertViewModel();
        }
    }
}