using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork GetUnitOfWork(ConsistencyLevel consistencyLevel, string? region);
	}

	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork GetUnitOfWork(ConsistencyLevel consistencyLevel, string? region)
		{
			return new UnitOfWork(consistencyLevel, region);
		}
	}
}