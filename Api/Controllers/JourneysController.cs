using Api.Models;
using Api.Models.DTOs;
using Api.Models.Models;
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
        public async Task<ActionResult<IEnumerable<JourneyAbstract>>> GetJourneys(int offset, int limit, string order, string? search, bool descending, int month, string? departure, string? returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax)
        {
            Console.WriteLine($"{search}, {departure}, {returnTime}, {distanceMin}, {distanceMax}, {durationMin}, {durationMax}");
            if (search == null)
            {
                search = "";
            }
            if (departure == null)
            {
                departure = "2021-01-01T00:00:00";
            }
            if (returnTime == null)
            {
                returnTime = "2021-12-31T23:59:59";
            }
            if (distanceMax == 0)
            {
                distanceMax = 214748.3647;
            }
            if (durationMax == 0)
            {
                durationMax = 2147483647;
            }
            Console.WriteLine($"{search}, {departure}, {returnTime}, {distanceMin}, {distanceMax}, {durationMin}, {durationMax}");
            try
            {
                return Ok(_mapper.Map<IEnumerable<ModifiedJourneyDto>>( await _journeyService.GetJourneys(offset, limit, order, search, descending, month, departure, returnTime, distanceMin, distanceMax, durationMin, durationMax)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<JourneyAbstract>> PostJourney(Journey journey)
        {
            try
            {
                var answer = await _journeyService.CreateJourney(journey);
                return CreatedAtAction(nameof(GetJourneys), answer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetJourneysCount(string? search, int month, string? departure, string? returnTime, double distanceMin, double distanceMax, int durationMin, int durationMax)
        {
            if (search == null)
            {
                search = "";
            }
            if (departure == null)
            {
                departure = "2021-01-01T00:00:00";
            }
            if (returnTime == null)
            {
                returnTime = "2021-12-31T23:59:59";
            }
            if (distanceMax == 0)
            {
                distanceMax = 214748.3647;
            }
            if (durationMax == 0)
            {
                durationMax = 2147483647;
            }
            try
            {
                return Ok(await _journeyService.GetJourneysCount(search, month, departure, returnTime, distanceMin, distanceMax, durationMin, durationMax));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
