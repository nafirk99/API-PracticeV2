using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                // Get All Regions from API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7142/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex)
            {
                // Log the exception with error level and include the exception details.
                //logger.LogError(ex, "An error occurred while fetching regions in Index action.");
            }

            return View(response);
        }

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
        public async Task<IActionResult> Index2()
        {
            List<CryptoTickerDto> response = new List<CryptoTickerDto>();
            try
            {
                // Get All Regions from API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://api4.binance.com/api/v3/ticker/24hr");

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
