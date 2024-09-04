using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
	{
		Task<List<WeatherForecast>> Get(string? partitionKey, string? id, int page, int pageSize);
		Task<ItemResponse<T>> Create(T entity);
		Task<T> Update(string partitionKey, string id, T entity);
		Task<WeatherForecast> Delete(string partitionKey, string id);
	}
}