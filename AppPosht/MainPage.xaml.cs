using System.Windows;
using System.Windows.Controls;
using AppPosht.ViewModels;

namespace AppPosht
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); ;
        }

        

        private void ButtonDownloadAll_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ReportStatusError");
        }

        private void ButtonDownload_OnClick(object sender, RoutedEventArgs e)
        {
        }
        
    }
}