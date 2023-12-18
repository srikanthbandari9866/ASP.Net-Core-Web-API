using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepo _regioRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepo region , IMapper mapper, ILogger<RegionsController> logger) 
        {
            _regioRepo = region;
            _mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles ="Admin,User")]
        
        public async Task<IActionResult> GetAll()
        {
            
            // Get Data from Database - DomainModels
            var regionsDomain = await _regioRepo.GetAllAsync();

            
            // converting Domain to Dto and returning
            return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
        }

        [HttpGet]
        [Authorize(Roles ="Admin,User")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regioRepo.GetByIdAsync(id);
            if(regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
        
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] AddRegionDto obj)
        {
           
                var regionDomain = _mapper.Map<Region>(obj);
                regionDomain = await _regioRepo.CreateRegionAsync(regionDomain);

                var regionDto = _mapper.Map<RegionDto>(regionDomain);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto obj)
        {
            if (ModelState.IsValid)
            {
                var regionDomain = _mapper.Map<Region>(obj);

                regionDomain = await _regioRepo.UpdateRegionAsync(id, regionDomain);
                if (regionDomain == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RegionDto>(regionDomain));
            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await _regioRepo.DeleteRegionAsync(id);

            if(regionModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionModel));
        }
    }
}
