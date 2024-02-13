using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PoolPoliceGoogleMaps.Controllers;
using PoolPoliceGoogleMaps.Models;
using System.Web.Http;

namespace PoolPoliceGoogleMaps
{
    public static class PoolPoliceFunctions
    {
        [FunctionName("GetImageFromAddress")]
        public static async Task<IActionResult> GetImageFromAddress(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ImageRequest request = JsonConvert.DeserializeObject<ImageRequest>(requestBody);

                GoogleMapsController controller = new GoogleMapsController();

                Stream response = await controller.GetImageFromAddress(request.Address);
                return new FileStreamResult(response, "image/png");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return new ExceptionResult(ex, true);
            }

        }
    }
}
