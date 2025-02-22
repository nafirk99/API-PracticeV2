using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                // Get All Regions from API
                var client = httpClientFactory.CreateClient();
                var startTime = Stopwatch.GetTimestamp();
                var httpResponseMessage = await client.GetAsync("https://localhost:7142/api/regions");
                var deltaTime = Stopwatch.GetElapsedTime(startTime);
                httpResponseMessage.EnsureSuccessStatusCode();

                string Time = deltaTime.TotalSeconds.ToString();
                ViewBag.Time = Time;    

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex)
            {
                // Log the exception with error level and include the exception details.
                // logger.LogError(ex, "An error occurred while fetching regions in Index action.");
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7142/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response != null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7142/api/regions/{id}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7142/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();
            if (response != null) 
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            var client = httpClientFactory.CreateClient();
            var httpResponseMessage = await client.DeleteAsync($"https://localhost:7142/api/regions/{request.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();
            return RedirectToAction("Index", "Regions");

            // Implement try catch block to handle exceptions as we used EnsureSuccessStatusCode() method.
        }


        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            var response = new CatFactDTO();
            try
            {
                // Get All Regions from API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://catfact.ninja/fact");

                httpResponseMessage.EnsureSuccessStatusCode();

                response = (await httpResponseMessage.Content.ReadFromJsonAsync<CatFactDTO>());
            }
            catch (Exception ex)
            {
                // Log the exception with error level and include the exception details.
                //logger.LogError(ex, "An error occurred while fetching regions in Index action.");
            }

            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            List<CryptoTickerDto> response = new List<CryptoTickerDto>();
            try
            {
                // Get All Regions from API
                var client = httpClientFactory.CreateClient();
                var startTime = Stopwatch.GetTimestamp();
                var httpResponseMessage = await client.GetAsync("https://api4.binance.com/api/v3/ticker/24hr");
                var deltaTime = Stopwatch.GetElapsedTime(startTime);

                ViewBag.CryptoTime = deltaTime.TotalSeconds.ToString();

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CryptoTickerDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception with error level and include the exception details.
                //logger.LogError(ex, "An error occurred while fetching regions in Index action.");
            }

            return View(response);
        }
    }
}
