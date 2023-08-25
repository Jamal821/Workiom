using MongoDB.Driver;
using WorkiomBackendDevTest.Entities;

namespace WorkiomBackendDevTest.Repositories.Classes
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(IMongoDatabase database) : base(database, "Companies")
        {
        }


        /*
         Note : we can here add more specific methods for Company operations here 
                but But in our example, the system needs are simple, there are no 
                custom operations for companies, in addition to the existence of 
                the (FindAsync) function,which solves the problem of having dynamic 
                global filters.
         */
    }
}
