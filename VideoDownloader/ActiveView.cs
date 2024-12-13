using Caliburn.Micro;
using VideoDownloader.ViewModels;

namespace VideoDownloader
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