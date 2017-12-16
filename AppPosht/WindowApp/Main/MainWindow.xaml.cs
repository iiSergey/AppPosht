using System.Windows;
using System.Windows.Controls;
using AppPosht.WindowApp.Config;
using MahApps.Metro.Controls;

namespace AppPosht.WindowApp.Main
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindowViewModel MainWindowViewModel { get;protected set; }
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel=new MainWindowViewModel();
        }

        private void ButtonConfig_OnClick(object sender, RoutedEventArgs e)
        {
            var config=new ConfigWindow();

            config.ShowDialog();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}