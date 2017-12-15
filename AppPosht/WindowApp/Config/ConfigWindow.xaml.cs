using System;
using System.Windows;
using MahApps.Metro.Controls;

namespace AppPosht.WindowApp.Config
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    /// Логика взаимодействия для ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow 
    {
        public ConfigWindow()
        {
            InitializeComponent();
            DataContext = new ConfigViewModel();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
