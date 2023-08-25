using System.Linq.Expressions;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Classes;
using WorkiomBackendDevTest.Repositories.Interfaces;

namespace WorkiomBackendDevTest.Services
{
    public class ContactService : BaseService<Contact>
    {
        private readonly ContactRepository _contactRepository;


        public ContactService(ContactRepository contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;

        }


        public async Task<List<Contact>> FindContactsByInterestAsync(string interest)
        {
            Expression<Func<Contact, bool>> filterExpression = contact =>
                contact.CustomFields.ContainsKey("Interest") &&
                contact.CustomFields["Interest"].ToString() == interest;

            return await _contactRepository.FindAsync(filterExpression);
        }


        // we can use (FindAsync) function for existing field or user-extended field
        // but (Name) filed is shared between entities so we implement in (BaseEntity)
        /*
        public async Task<List<Contact>> FindContactsByNameAsync(string nameFilter)
        {
        Expression<Func<Contact, bool>> filterExpression = contact => contact.Name.Contains(nameFilter);
        return await _contactRepository.FindAsync(filterExpression);
        }
         */

    }

}
