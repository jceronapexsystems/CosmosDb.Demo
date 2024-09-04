namespace CosmosDb.Demo.Repo
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork GetUnitOfWork(string? region);
	}

	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork GetUnitOfWork(string? region)
		{
			return new UnitOfWork(region);
		}
	}
}