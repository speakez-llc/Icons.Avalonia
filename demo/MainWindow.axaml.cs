using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Demo
{
    public class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new DemoViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public void ToggleTheme(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var app = Avalonia.Application.Current as Demo.App;
            if (app != null)
            {
                if (app.RequestedThemeVariant == ThemeVariant.Light)
                {
                    app.RequestedThemeVariant = ThemeVariant.Dark;
                    ((DemoViewModel)DataContext).ThemeIcon = "fa-solid fa-sun"; // Set icon to sun when dark theme is selected
                }
                else
                {
                    app.RequestedThemeVariant = ThemeVariant.Light;
                    ((DemoViewModel)DataContext).ThemeIcon = "fa-solid fa-moon"; // Set icon to moon when light theme is selected
                }
            }
        }
    }
}