using System.Windows.Controls;
using AppPosht.ViewModels;

namespace AppPosht
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}