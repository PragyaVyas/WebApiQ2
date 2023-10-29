using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Intercom.Data;

namespace WebApiTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private const string ApiKey = "fe923f99dc9706382322810f1740c060\r\n";
        private const string IpStackApiUrl = "http://api.ipstack.com/";
        [HttpGet]
        public async Task<IActionResult> GetLocationByIP(string ipAddress)
        {
            using var client = new HttpClient(); client.BaseAddress = new Uri(IpStackApiUrl);
            try
            {   
                // Build the URL with the IP address and API key
                 string url = $"{ipAddress}?access_key={ApiKey}";

                // Send a GET request to the IP/Location API 
                var response = await client.GetFromJsonAsync<LocationData>(url);

                if (response != null) { return Ok(response); } 
                else { return BadRequest("Unable to retrieve location information."); }
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
