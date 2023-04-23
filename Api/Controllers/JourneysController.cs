using Api.Models;
using Api.Models.Models;
using Api.Models.Models.DTOs;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        private readonly IMapper _mapper;

        public JourneysController(IJourneyService journeyService, IMapper mapper)
        {
            _journeyService = journeyService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journey>>> GetJourneys(int offset, int limit, string order, string search, bool descending, int month)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ModifiedJourney>>( await _journeyService.GetJourneys(offset, limit, order, search, descending , month)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
