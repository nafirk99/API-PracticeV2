using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Create([FromBody] AddWalkDTO addWalkDTO)
        {

        }
    }
}
