// declare a unit of work pattern
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
	// create the unit of work insterface
	public interface IUnitOfWork : IDisposable
	{
		public ContainerResponse Context { get; }
	}

	public interface IUnitOfWorkFactory
	{
		IUnitOfWork GetUnitOfWork(string region);
	}

	// implement interface
	public class UnitOfWotkFactory
	{
		public IUnitOfWork GetUnitOfWork(string region)
		{
			return new UnitOfWork(region);
		}
	}
}