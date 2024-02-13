using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PoolPoliceGoogleMaps.Controllers
{
    public class GoogleMapsController
    {
        private static string _mapsAPIKey = Environment.GetEnvironmentVariable("GoogleMapsAPIKey");
        private readonly WebClient _webClient;
        public GoogleMapsController() 
        { 
            _webClient = new WebClient();
        }

        public async Task<Stream> GetImageFromAddress(string address)
        {
            string encodedAddress = address.Replace(" ", "+");

            Uri uri = new Uri($"https://maps.googleapis.com/maps/api/staticmap?center={encodedAddress}"+"" +
                "&maptype=satellite"+
                "&zoom=20"+
                "&size=400x400"+
                $"&key={_mapsAPIKey}");

            byte[] imageBytes = await _webClient.DownloadDataTaskAsync(uri);

            Stream imageStream = new MemoryStream(imageBytes);

            return imageStream;
        }

    }
}
