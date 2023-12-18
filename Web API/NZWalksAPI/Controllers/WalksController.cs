using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepo _walkRepo;
        public WalksController(IMapper mapper, IWalkRepo walkRepo) 
        {
            _mapper = mapper;
            _walkRepo = walkRepo;
        }

        //GET : Walks
        //GET : /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=100
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,

            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=10)
        {
            var walkDomain = await _walkRepo.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true, pageNumber,pageSize);

            return Ok(_mapper.Map<List<WalkDto>>(walkDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepo.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkDto obj)
        {
            //Dto to DomainModel
            var walkDomain = _mapper.Map<Walk>(obj);

            walkDomain = await _walkRepo.CreateWalkAsync(walkDomain);

            return Ok(_mapper.Map<WalkDto>(walkDomain));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto obj)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = _mapper.Map<Walk>(obj);

                walkDomain = await _walkRepo.UpdateWalkAsync(id, walkDomain);

                if (walkDomain == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<WalkDto>(walkDomain));
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain =await _walkRepo.DeleteWalkAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }
    }
}
