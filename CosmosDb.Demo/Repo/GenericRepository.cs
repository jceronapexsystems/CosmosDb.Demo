using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	public class GenericRepository<T>(IUnitOfWorkFactory unitOfWorkFactory) : IGenericRepository<T> where T : BaseEntity, new()
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory = unitOfWorkFactory;

        public async Task<ItemResponse<T>> Create(T entity)
		{
			// validate entity, region and consistency level
			if (entity == null || string.IsNullOrWhiteSpace(entity.Region) || entity.ConsistencyLevel == null)
			{
				throw new ArgumentException("Entity, region and consistency level are required");
			}

			using var _unitOfWork = _unitOfWorkFactory.GetUnitOfWork(entity.Region);

			// create an item
			entity.Id = Guid.NewGuid().ToString();

			// set the consistency level
			var requestOptions = new ItemRequestOptions
			{
				ConsistencyLevel = entity.ConsistencyLevel,
			};

			// create the item
			var response = await _unitOfWork.Context.Container.CreateItemAsync(entity, partitionKey: new PartitionKey(entity.Region), requestOptions);

			return response;
		}

		public async Task<WeatherForecast> Delete(string partitionKey, string id)
		{
			using var _unitOfWork = _unitOfWorkFactory.GetUnitOfWork(partitionKey);

			// delete the item
			var partitionKey1 = new PartitionKey(partitionKey);
			var response = await _unitOfWork.Context.Container.DeleteItemAsync<WeatherForecast>(id, partitionKey1);

			return response.Resource;
		}

		public async Task<List<WeatherForecast>> Get(string? partitionKey, string? id, int page, int pageSize)
		{
			using var _unitOfWork = _unitOfWorkFactory.GetUnitOfWork(partitionKey);

			// calculate the offset based on the page and page size
			int offset = (page - 1) * pageSize;

			// build the query
			string query = BuildQuery(partitionKey, id, offset, pageSize);
			var queryDefinition = AddParameters(partitionKey, id, query);

			// execute the query
			var iterator = _unitOfWork.Context.Container.GetItemQueryIterator<WeatherForecast>(queryDefinition);

			var results = new List<WeatherForecast>();

			while (iterator.HasMoreResults)
			{
				// add each result to the results list
				var items = await iterator.ReadNextAsync();
				results.AddRange(items);
			}

			return results;
		}

		public async Task<T> Update(string partitionKey, string id, T entity)
		{
			using var _unitOfWork = _unitOfWorkFactory.GetUnitOfWork(partitionKey);

			// update the item
			var partitionKey1 = new PartitionKey(partitionKey);
			var response = await _unitOfWork.Context.Container.UpsertItemAsync(entity, partitionKey1);

			return response.Resource;
		}

		private static string BuildQuery(string? region, string? id, int offset, int pageSize)
		{
			var query = "SELECT * FROM c";
			// create the query definition

			// add region filter if present
			if (!string.IsNullOrEmpty(region))
			{
				query += " WHERE c.Region = @region";
			}

			// add id filter if present
			if (!string.IsNullOrEmpty(id))
			{
				if (!string.IsNullOrWhiteSpace(region))
				{
					query += " AND c.id = @id";
				}
				else
				{
					query += " WHERE c.id = @id";
				}
			}

			// add pagination to the query
			query += $" OFFSET {offset} LIMIT {pageSize}";

			return query;
		}

		private static QueryDefinition AddParameters(string? region, string? id, string query)
		{
			var queryDefinition = new QueryDefinition(query);
			if (!string.IsNullOrEmpty(region))
			{
				queryDefinition.WithParameter("@region", region);
			}

			if (!string.IsNullOrEmpty(id))
			{
				queryDefinition.WithParameter("@id", id);
			}
			return queryDefinition;
		}
	}
}