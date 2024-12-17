using System.Diagnostics;
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

        public bool DownloadAudioOrVideoFromX(string url, string type)
        {
            try
            {
                string outputPath = $"{Global.DirectorySaveDownload}{(type.Equals("Video") ? "video_%(id)s.%(ext)s" : "audio_%(id)s.%(ext)s")}";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"./Utils/yt-dlp",
                        Arguments = type.Equals("Video") ? $"{url} -N 4 --concurrent-fragments 4 --no-check-certificate -o \"{outputPath}\"" : $"{url} -x --audio-format mp3 -N 4 --concurrent-fragments 4 --no-check-certificate -o \"{outputPath}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    return false;
                }

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