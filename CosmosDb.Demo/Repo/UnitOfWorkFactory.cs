using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork<T> GetUnitOfWork<T>(ConsistencyLevel consistencyLevel, string connectionString, string? region)
			where T : BaseEntity;
	}

	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork<T> GetUnitOfWork<T>(ConsistencyLevel consistencyLevel, string connectionString, string? region)
			where T : BaseEntity
		{
			return new UnitOfWork<T>(consistencyLevel, connectionString, region);
		}
	}
}