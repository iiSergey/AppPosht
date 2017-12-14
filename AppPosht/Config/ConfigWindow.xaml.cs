using System;
using MahApps.Metro.Controls;

namespace AppPosht.Config
{
    /// <inheritdoc cref="MetroWindow" />
    /// <summary>
    /// Логика взаимодействия для ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow 
    {
        private readonly ConfigViewModel _configViewModel;
        public ConfigWindow()
        {
            InitializeComponent();
            _configViewModel = new ConfigViewModel();
            DataContext = _configViewModel;
        }

        /// <inheritdoc />
        /// <summary>
        ///   Вызывает событие <see cref="E:System.Windows.Window.Closed" />.
        /// </summary>
        /// <param name="e">
        ///   Объект класса <see cref="T:System.EventArgs" />, содержащий данные события.
        /// </param>
        protected override void OnClosed(EventArgs e)
        {
            _configViewModel.Save();
            base.OnClosed(e);
        }
    }
}
