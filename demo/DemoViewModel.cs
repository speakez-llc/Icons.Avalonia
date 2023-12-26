using System.Collections.Generic;
using System;
using System.ComponentModel;
using Avalonia.Styling;
using Projektanker.Icons.Avalonia;

namespace Demo
{
    public class DemoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isEnabled;
        private IEnumerable<IconAnimation> _iconAnimations;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        private string _themeIcon;
        public string ThemeIcon
        {
            get { return _themeIcon; }
            set
            {
                if (_themeIcon != value)
                {
                    _themeIcon = value;
                    OnPropertyChanged(nameof(ThemeIcon));
                }
            }
        }

        public IEnumerable<IconAnimation> Animations
            => _iconAnimations ??= Enum.GetValues<IconAnimation>();

        public DemoViewModel()
        {
            var app = Avalonia.Application.Current as App;
            if (app != null)
            {
                if (app.RequestedThemeVariant == ThemeVariant.Light)
                {
                    ThemeIcon = "fa-solid fa-moon"; // Set icon to moon when light theme is selected
                }
                else
                {
                    ThemeIcon = "fa-solid fa-sun"; // Set icon to sun when dark theme is selected
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}