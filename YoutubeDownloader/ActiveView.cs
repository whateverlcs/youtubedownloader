using Caliburn.Micro;
using YoutubeDownloader.ViewModels;

namespace YoutubeDownloader
{
    public static class ActiveView
    {
        public static ShellViewModel Parent;

        public static async Task OpenItem(IScreen t)
        {
            await Parent.ActivateItemAsync(t);
        }
    }
}