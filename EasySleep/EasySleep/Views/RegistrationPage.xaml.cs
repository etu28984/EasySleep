using EasySleep.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace EasySleep.Views
{
    public sealed partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            Views.Shell.HamburgerMenu.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            Views.Shell.HamburgerMenu.IsFullScreen = true;
        }
    }
}
