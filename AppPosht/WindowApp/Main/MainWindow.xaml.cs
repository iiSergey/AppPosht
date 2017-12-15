using System.Windows;
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
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ButtonConfig_OnClick(object sender, RoutedEventArgs e)
        {
            var config=new ConfigWindow();

            config.ShowDialog();
        }
    }
}