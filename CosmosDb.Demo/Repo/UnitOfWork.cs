using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
	{
		public ContainerResponse Context { get; }
	}
	
	public class UnitOfWork<T> : IUnitOfWork<T>, IDisposable where T : BaseEntity
	{

		private readonly ContainerResponse _container;

		public ContainerResponse Context => _container;

		private CosmosClient _client;

		public UnitOfWork(ConsistencyLevel consistencyLevel, string connectionString, string? region)
		{
			var cosmosClientOptions = new CosmosClientOptions
			{
				ApplicationRegion = region,
				ConsistencyLevel = consistencyLevel,
			};

			// create a connection to azure cosmos db
			_client = new CosmosClient(connectionString, cosmosClientOptions);

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

		public IUnitOfWork<T> GetUnitOfWork(ConsistencyLevel consistencyLevel, string region, string connectionString)
		{
			return new UnitOfWork<T>(consistencyLevel, connectionString, region);
		}
	}
}