using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	public interface IUnitOfWork : IDisposable
	{
		public ContainerResponse Context { get; }
	}
	
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private const string ConnectionString = "AccountEndpoint=https://az-204-cosmosdb-demo-account2.documents.azure.com:443/;AccountKey=GvEg2SGQo6ooKLTUXbeV2WKIPrQMJOkY5CUG4dnH6UQ1yMiKU7cRJEKmyDa7nqmcyFAYuxMLG4EYACDbzu4pOA==;";

		private readonly ContainerResponse _container;

		public ContainerResponse Context => _container;

		private CosmosClient _client;

		public UnitOfWork(ConsistencyLevel consistencyLevel, string? region = Regions.SouthCentralUS)
		{
			var cosmosClientOptions = new CosmosClientOptions
			{
				ApplicationRegion = region,
				ConsistencyLevel = consistencyLevel,
			};

			// create a connection to azure cosmos db
			_client = new CosmosClient(ConnectionString, cosmosClientOptions);

			// create a database
			var database = _client.CreateDatabaseIfNotExistsAsync("WeatherForecast").Result;

			// create a container
			_container = database.Database.CreateContainerIfNotExistsAsync("WeatherForecast", $"/{nameof(WeatherForecast.Region)}").Result;
		}

		public void Dispose()
		{
			_client.Dispose();
			GC.SuppressFinalize(this);
		}

		public IUnitOfWork GetUnitOfWork(ConsistencyLevel consistencyLevel, string region)
		{
			return new UnitOfWork(consistencyLevel, region);
		}
	}
}