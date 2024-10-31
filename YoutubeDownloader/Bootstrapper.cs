using Caliburn.Micro;
using System.Windows;
using YoutubeDownloader.ViewModels;

namespace YoutubeDownloader
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}