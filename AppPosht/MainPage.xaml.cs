using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppPosht.Annotations;
using AppPosht.ViewModels;
using DotNetDBF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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
            MessageBox.Show("Error");
        }

        private void ButtonDownload_OnClick(object sender, RoutedEventArgs e)
        {
        }
        
    }
}