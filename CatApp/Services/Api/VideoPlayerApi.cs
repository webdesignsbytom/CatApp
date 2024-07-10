using CatApp.Services.Utils;

namespace CatApp.Services.Api
{
    public class VideoPlayerApi
    {
        public class VideoService
        {
            private readonly HttpService _httpService;
            private const string Host = "https://api.cat-app.com"; // Replace with your actual host

            public VideoService(HttpService httpService)
            {
                _httpService = httpService;
            }

            public async Task<string> FetchVideoAsync(string videoPath)
            {
                Console.WriteLine($">>> FetchVideoAsync: {Host}{videoPath}");
                var videoData = await _httpService.GetVideoAsync($"{Host}{videoPath}");

                // Save video to a local file
                string localPath = Path.Combine(FileSystem.AppDataDirectory, "video.mp4");
                await File.WriteAllBytesAsync(localPath, videoData);

                return localPath;
            }
        }
    }
}

