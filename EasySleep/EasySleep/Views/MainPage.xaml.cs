using System;
using EasySleep.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace EasySleep.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Views.Shell.HamburgerMenu.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            Views.Shell.HamburgerMenu.IsFullScreen = true;
        }
    }
}