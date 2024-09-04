using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CosmosDb.Demo
{
	public class WeatherForecast : BaseEntity
	{
		public DateOnly Date { get; set; }

		public int TemperatureC { get; set; }

		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

		public string? Summary { get; set; }

		public Uri SelfLink
		{
			get
			{
				return new Uri($"/api/weatherforecast?region={Region}&id={Id}", UriKind.Relative);
			}
		}
	}

    public class BaseEntity
    {
		[JsonProperty("id")]
		public string Id { get; set; } = string.Empty;

		public string Region { get; set; } = string.Empty;

		public ConsistencyLevel? ConsistencyLevel { get; set; }
    }
}
