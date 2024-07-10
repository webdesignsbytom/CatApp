using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatApp.Services.Utils
{
    public class HttpService
    {
        private readonly HttpClient _client;

        public HttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<byte[]> GetVideoAsync(string path)
        {
            Console.WriteLine($">>> GetVideo: {path}");

            var response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
