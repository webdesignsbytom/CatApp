using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatApp.Services.Utils
{
    public class VideoService
    {
        private readonly HttpService _httpService;
        private const string Host = "https://api.cat-app.app"; // Replace with your actual host

        public VideoService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> FetchVideoAsync(string videoPath)
        {
            var videoData = await _httpService.GetVideoAsync($"{Host}{videoPath}");

            // Save video to a local file
            string localPath = Path.Combine(FileSystem.AppDataDirectory, "video.mp4");
            await File.WriteAllBytesAsync(localPath, videoData);

            return localPath;
        }
    }
}
