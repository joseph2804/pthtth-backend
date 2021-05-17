using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pthtth_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [HttpGet]
        public async Task<string> resultExrate()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.factmaven.com/xml-to-json?xml=https://portal.vietcombank.com.vn/Usercontrols/TVPortal.TyGia/pXML.aspx?b=1");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result =  await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
        public async Task<string> resultFootball([FromQuery] string id, [FromQuery] string dateFrom, [FromQuery] string dateTo )
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.football-data.org/v2/competitions/{id}/matches?dateFrom={dateFrom}&dateTo={dateTo}");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", "006716ee48814af19d909b9f1271d1ec");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}
