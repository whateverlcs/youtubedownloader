using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using VideoDownloader.Controllers;

namespace VideoDownloader.ViewModels
{
    public class IndexViewModel : Screen
    {
        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                NotifyOfPropertyChange(() => Loading);
            }
        }

        private string _textLoading;

        public string TextLoading
        {
            get { return _textLoading; }
            set
            {
                _textLoading = value;
                NotifyOfPropertyChange(() => TextLoading);
            }
        }

        private string _txtLinkX;

        public string TxtLinkX
        {
            get { return _txtLinkX; }
            set
            {
                _txtLinkX = value;
                NotifyOfPropertyChange(() => TxtLinkX);
            }
        }

        private bool _rbVideo = true;

        public bool RbVideo
        {
            get { return _rbVideo; }
            set
            {
                _rbVideo = value;
                NotifyOfPropertyChange(() => RbVideo);
            }
        }

        private bool _rbAudio;

        public bool RbAudio
        {
            get { return _rbAudio; }
            set
            {
                _rbAudio = value;
                NotifyOfPropertyChange(() => RbAudio);
            }
        }

        private ControlLogs clog = new ControlLogs();

        private ControlPrincipal cp = new ControlPrincipal();

        public IndexViewModel()
        {
            TextLoading = "LOADING";
        }

        public void DownloadYoutubeVideoOrAudio()
        {
            if (string.IsNullOrEmpty(TxtLinkX))
            {
                MessageBox.Show("Please enter a link from YouTube to proceed with the download.", "Please enter a link", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!TxtLinkX.Contains("youtube.com/watch"))
            {
                MessageBox.Show("Please insert a valid YouTube video link to download.", "Please enter a valid link", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectSaveDirectory())
            {
                Loading = true;
                TextLoading = "DOWNLOADING";

                Thread thread = new Thread(DownloadYoutubeVideoOrAudioThread);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        public void DownloadXVideoOrAudio()
        {
            if (string.IsNullOrEmpty(TxtLinkX))
            {
                MessageBox.Show("Please enter a link from X to proceed with the download.", "Please enter a link", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!TxtLinkX.Contains("x.com/"))
            {
                MessageBox.Show("Please insert a valid X video link to download.", "Please enter a valid link", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectSaveDirectory())
            {
                Loading = true;
                TextLoading = "DOWNLOADING";

                Thread thread = new Thread(DownloadXVideoOrAudioThread);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        public bool SelectSaveDirectory()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Global.DirectorySaveDownload = dialog.FileName + "\\";
                return true;
            }

            return false;
        }

        public async void DownloadYoutubeVideoOrAudioThread()
        {
            try
            {
                bool sucess = await cp.DownloadAudioOrVideoFromYoutube(TxtLinkX, RbVideo ? "Video" : "Audio");

                if (sucess)
                {
                    MessageBox.Show("Download Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("An error occurred while downloading the inserted video/audio. Please try again, if the error persists, contact support.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                clog.LogException(e.ToString(), "DownloadYoutubeVideoOrAudioThread()");
                MessageBox.Show("An error occurred while downloading the inserted video/audio. Please try again, if the error persists, contact support.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Loading = false;
                TextLoading = "LOADING";
                TxtLinkX = "";
                RbVideo = true;
                RbAudio = false;
            }
        }

        public void DownloadXVideoOrAudioThread()
        {
            try
            {
                bool sucess = cp.DownloadAudioOrVideoFromX(TxtLinkX, RbVideo ? "Video" : "Audio");

                if (sucess)
                {
                    MessageBox.Show("Download Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("An error occurred while downloading the inserted video/audio. Please try again, if the error persists, contact support.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                clog.LogException(e.ToString(), "DownloadYoutubeVideoOrAudioThread()");
                MessageBox.Show("An error occurred while downloading the inserted video/audio. Please try again, if the error persists, contact support.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Loading = false;
                TextLoading = "LOADING";
                TxtLinkX = "";
                RbVideo = true;
                RbAudio = false;
            }
        }
    }
}