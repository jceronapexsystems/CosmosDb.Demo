using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CosmosDb.Demo
{
    public class BaseEntity
    {
		[JsonProperty("id")]
		public string Id { get; set; } = string.Empty;

		public string Region { get; set; } = string.Empty;

		public ConsistencyLevel? ConsistencyLevel { get; set; }

		public Uri SelfLink
		{
			get
			{
				return new Uri($"/api/weatherforecast?region={Region}&id={Id}", UriKind.Relative);
			}
		}
    }
}
