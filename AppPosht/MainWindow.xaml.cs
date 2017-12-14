using System.Windows;
using AppPosht.Config;
using MahApps.Metro.Controls;

namespace AppPosht
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