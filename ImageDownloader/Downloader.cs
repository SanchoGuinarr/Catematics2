using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.PortableExecutable;

namespace ImageDownloader
{
    internal class Downloader
    {
        private const string LEXICA_API_PATH = "https://image.lexica.art/sm/78e12c85-4fdd-4644-b3b3-e20b36979c3f";
        private const string FILE_PATH = @"d:\Catematics\Images";


        

        public static void DownloadImage()
        {
            
           DownloadImageAsync(FILE_PATH, "test", new Uri(LEXICA_API_PATH)).Wait();
        }

        private static async Task DownloadImageAsync(string directoryPath, string fileName, Uri uri)
        {
            HttpClientHandler handler = new CloudflareHttpHandler();
            HttpClient httpClient = new(handler);
            
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,LEXICA_API_PATH);
            // Add our custom headers
            requestMessage.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
            requestMessage.Headers.Add("upgrade-insecure-requests", "1");
            requestMessage.Headers.Add("cache-control", "max-age=0");
            requestMessage.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            requestMessage.Headers.Add("accept-encoding", "gzip, deflate, br");
            requestMessage.Headers.Add("accept-language", "cs-CZ,cs;q=0.9");
            requestMessage.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"107\", \"Chromium\";v=\"107\", \"Not=A?Brand\";v=\"24\"");
            requestMessage.Headers.Add("sec-ch-ua-mobile", "?0");
            requestMessage.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            requestMessage.Headers.Add("sec-fetch-dest", "document");
            requestMessage.Headers.Add("sec-fetch-mode", "navigate");
            requestMessage.Headers.Add("sec-fetch-site", "none");
            requestMessage.Headers.Add("sec-fetch-user", "?1");
            requestMessage.Headers.Add("accept-language", "cs-CZ,cs;q=0.9");
            requestMessage.Headers.Add("if-none-match", "\"50c167e294803e0ab3635c565d779edb\"");

            CancellationToken cancellationToken = new();
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationToken);

            
            // Get the file extension
            // string uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            string fileExtension = ".webp"; // Path.GetExtension(uriWithoutQuery);

            // Create file path and ensure directory exists
            string path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
            Directory.CreateDirectory(directoryPath);

            // Download the image and write to the file
            byte[] imageBytes = await httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(path, imageBytes);
        }
    }

}
