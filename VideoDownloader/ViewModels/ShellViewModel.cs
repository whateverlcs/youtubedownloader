using Caliburn.Micro;

namespace VideoDownloader.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            ActiveView.Parent = this;
            _ = ActiveView.OpenItem(new IndexViewModel());
        }
    }
}