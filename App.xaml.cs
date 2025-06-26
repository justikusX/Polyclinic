using System.Windows;
using MaterialDesignThemes.Wpf;
using Polyclinic.Services;
using Polyclinic.ViewModels;
using Polyclinic.Views;

namespace Polyclinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var databaseService = new DatabaseService();
            var mainViewModel = new MainViewModel(databaseService);

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            // Инициализация Material Design
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            // Установка светлой темы
            theme.SetBaseTheme(Theme.Light);

            // Установка цветов
            theme.SetPrimaryColor(System.Windows.Media.Color.FromRgb(63, 81, 181)); // Indigo
            theme.SetSecondaryColor(System.Windows.Media.Color.FromRgb(0, 150, 136)); // Teal

            // Применение темы
            paletteHelper.SetTheme(theme);

            base.OnStartup(e);
        }

        
    }
}