using System;
using System.Globalization;

namespace AppPosht.App
{
    /// <inheritdoc />
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
            LanguageChanged += App_LanguageChanged;
            Language = AppPosht.Properties.Settings.Default.DefaultLanguage;
        }
        
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get => System.Threading.Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (Equals(value, System.Threading.Thread.CurrentThread.CurrentUICulture)) return;
                
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                LanguageChanged?.Invoke(Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            AppPosht.Properties.Settings.Default.DefaultLanguage = Language;
            AppPosht.Properties.Settings.Default.Save();
        }
    }
}

