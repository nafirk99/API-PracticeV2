using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // CREATE Walk
        // POST: api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO addWalkDTO)
        {
            // Map DTO to Domain Model
            var walkDomainModel =  mapper.Map<Walk>(addWalkDTO);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain Model To DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);    // return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        // GET Walks
        // GET: api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDomainModel = await walkRepository.GetAllAsync();

            // Map Domain Model To DTO
            var walkDTO = mapper.Map<List<WalkDTO>>(walkDomainModel);

            return Ok(walkDTO);
        }
    }
}
