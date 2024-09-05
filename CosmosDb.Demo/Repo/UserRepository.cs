
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Demo.Repo
{
    public class UserRepository : GenericRepository<User>
	{
		public UserRepository(IUnitOfWorkFactory unitOfWorkFactory) 
			: base(unitOfWorkFactory)
		{
		}

        protected override ConsistencyLevel _consistencyLevel => ConsistencyLevel.Session;

		protected override string ConnectionString => "AccountEndpoint=https://az-204-cosmosdb-demo-account2.documents.azure.com:443/;AccountKey=GvEg2SGQo6ooKLTUXbeV2WKIPrQMJOkY5CUG4dnH6UQ1yMiKU7cRJEKmyDa7nqmcyFAYuxMLG4EYACDbzu4pOA==;";
    }
}