using CosmosDb.Demo.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Demo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IGenericRepository<WeatherForecast> _weatherForecastRepository;

		public WeatherForecastController(
			ILogger<WeatherForecastController> logger,
			IGenericRepository<WeatherForecast> weatherForecastRepository
			)
		{
			_logger = logger;
			_weatherForecastRepository = weatherForecastRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get(
			[FromQuery] string? region = null,
			[FromQuery] string? id = null,
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10
			)
		{
			var result = await _weatherForecastRepository.Get(region, id, page, pageSize);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] WeatherForecast item)
		{
			var result = await _weatherForecastRepository.Create(item);

			return base.Created("", result);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] WeatherForecast item)
		{
			var result = await _weatherForecastRepository.Update(item.Region, item.Id, item);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] string region, [FromQuery] string id)
		{
			var result = await _weatherForecastRepository.Delete(region, id);

			return Ok(result);
		}
	}
}
