using MongoDB.Driver;
using WorkiomBackendDevTest.Entities;

namespace WorkiomBackendDevTest.Repositories.Classes
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(IMongoDatabase database) : base(database, "Contacts")
        {
        }


        /*
          Note : we can here add more specific methods for Contact operations here 
                 but But in our example, the system needs are simple, there are no 
                 custom operations for Contacts, in addition to the existence of 
                 the (FindAsync) function,which solves the problem of having dynamic 
                 global filters.
          */
    }
}
