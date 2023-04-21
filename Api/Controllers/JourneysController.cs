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
        public async Task<ActionResult<IEnumerable<Journey>>> GetJourneys(int offset, int limit, string order, string search, bool descending)
        {
            try
            {
                if (search == "null") { search = ""; }
                var answ = await _journeyService.GetJourneys(offset, limit, order, search, descending);
                Console.WriteLine(answ);

                return Ok(_mapper.Map<IEnumerable<ModifiedJourney>>( await _journeyService.GetJourneys(offset, limit, order, search, descending)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
