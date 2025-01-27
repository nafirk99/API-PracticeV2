using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NZWalks.API.CustomActionFilters;
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
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO addWalkDTO)
        {
                // Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkDTO);

                walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

                // Map Domain Model To DTO
                var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
                return Ok(walkDTO);    // return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        // GET Walks
        // GET: api/walks
        // GET: api/walks?filterOn=Name&filterQuery=<queryname>&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                isAscending ?? true, pageNumber, pageSize); // If its nullable then change it to true

            // Map Domain Model To DTO
            var walkDTO = mapper.Map<List<WalkDTO>>(walkDomainModel);

            return Ok(walkDTO);
        }

        // GET Walk By Id
        // GET: api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkDTO updateWalkDTO)
        {
                // Map DTO To Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkDTO);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                // Map Domain Model To DTO
                var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

                return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);

            if(deletedWalkDomainModel == null)
            { 
                return NotFound(); 
            }

            // Map Domain Model To DTO
            var walkDTO = mapper.Map<WalkDTO>(deletedWalkDomainModel);

            return Ok(walkDTO);
        }
    }
}
