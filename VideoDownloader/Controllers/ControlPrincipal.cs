using System.IO;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace VideoDownloader.Controllers
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

        public async Task<bool> DownloadAudioOrVideoFromX(string url, string type)
        {
            try
            {
                var ytdl = new YoutubeDL();
                ytdl.YoutubeDLPath = @"./Utils/yt-dlp";

                var options = new OptionSet();

                if (type.Equals("Video"))
                {
                    options = new OptionSet
                    {
                        Output = Path.Combine(Global.DirectorySaveDownload, "%(title)s.%(ext)s"), // Nome do arquivo de saída
                        Format = "bestvideo+bestaudio/best"
                    };
                }
                else
                {
                    options = new OptionSet
                    {
                        Output = Path.Combine(Global.DirectorySaveDownload, "%(title)s.%(ext)s"),
                        Format = "bestaudio/best",
                        ExtractAudio = true,
                        AudioFormat = AudioConversionFormat.Mp3,
                        AudioQuality = 0
                    };
                }

                var res = await ytdl.RunWithOptions(url, options);
                return true;
            }
            catch (Exception e)
            {
                clog.LogException(e.ToString(), $"DownloadAudioOrVideoFromX(string {url}, string {(type.Equals("Video") ? "Video" : "Audio")})");
                return false;
            }
        }
    }
}