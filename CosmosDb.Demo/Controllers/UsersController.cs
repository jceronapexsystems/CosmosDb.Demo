
using CosmosDb.Demo.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Demo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
        private readonly ILogger<UsersController> _logger;
        private readonly UserRepository _userRepository;

        public UsersController(
			ILogger<UsersController> logger,
			UserRepository userRepository
			)
		{
			_logger = logger;
			_userRepository = userRepository;
		}

		// GET: api/Users
		[HttpGet]
		public async Task<IActionResult> Get(
			[FromQuery] string? region,
			[FromQuery] string? id = null,
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10
			)
		{
			var result = await _userRepository.Get(region, id, page, pageSize);

			return Ok(result);
		}

		// POST: api/Users
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User item)
		{
			var result = await _userRepository.Create(item);

			return base.Created("", result);
		}

		// PUT: api/Users
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] User item)
		{
			var result = await _userRepository.Update(item.Region, item.Id, item);

			return Ok(result);
		}

		// DELETE: api/Users
		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] string region, [FromQuery] string id)
		{
			var result = await _userRepository.Delete(region, id);

			return Ok(result);
		}
	}
}