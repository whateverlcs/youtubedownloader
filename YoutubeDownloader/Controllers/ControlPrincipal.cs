using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace YoutubeDownloader.Controllers
{
    public class ControlPrincipal
    {
        private ControlLogs clog = new ControlLogs();

        public async Task<bool> DownloadAudioOrVideoFromYoutube(string url, string type)
        {
            try
            {
                var ytdl = new YoutubeDL();
                ytdl.YoutubeDLPath = @"./Utils/yt-dlp";
                ytdl.FFmpegPath = @"./Utils/ffmpeg";
                ytdl.OutputFolder = Global.DirectorySaveDownload;
                var res = type.Equals("Video") ? await ytdl.RunVideoDownload(url) : await ytdl.RunAudioDownload(url, AudioConversionFormat.Mp3);
                return true;
            }
            catch (Exception e)
            {
                clog.LogException(e.ToString(), $"DownloadAudioOrVideoFromYoutube(string {url}, string {(type.Equals("Video") ? "Video" : "Audio")})");
                return false;
            }
        }
    }
}