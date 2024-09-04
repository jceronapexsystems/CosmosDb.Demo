
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
    public class UserRepository : GenericRepository<User>
	{
		public UserRepository(IUnitOfWorkFactory unitOfWorkFactory) 
			: base(unitOfWorkFactory)
		{
		}

        protected override ConsistencyLevel _consistencyLevel => ConsistencyLevel.Strong;

        protected override string ConnectionString => throw new NotImplementedException();
    }
}