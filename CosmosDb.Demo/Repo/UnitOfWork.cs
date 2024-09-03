// declare a unit of work pattern
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private const string ConnectionString = "AccountEndpoint=https://az-204-cosmosdb-demo-account2.documents.azure.com:443/;AccountKey=GvEg2SGQo6ooKLTUXbeV2WKIPrQMJOkY5CUG4dnH6UQ1yMiKU7cRJEKmyDa7nqmcyFAYuxMLG4EYACDbzu4pOA==;";

        private ContainerResponse _container;
        private CosmosClient _client;
        private DatabaseResponse _database;

		// create constructor
		public UnitOfWork(string region = Regions.SouthCentralUS)
		{
			var cosmosClientOptions = new CosmosClientOptions
			{
				ApplicationRegion = region
			};

			// create a connection to azure cosmos db
			_client = new CosmosClient(ConnectionString, cosmosClientOptions);

			// create a database with consistency level as strong
			_database = _client.CreateDatabaseIfNotExistsAsync("WeatherForecast").Result;

			// create a container
			_container = _database.Database.CreateContainerIfNotExistsAsync("WeatherForecast", $"/{nameof(WeatherForecast.Region)}").Result;
		}

        public ContainerResponse Context => _container;

        public void Dispose()
		{
			// dispose resources
			_client.Dispose();
		}

        public IUnitOfWork GetUnitOfWork(string region)
        {
            return new UnitOfWork(region);
        }
    }
}