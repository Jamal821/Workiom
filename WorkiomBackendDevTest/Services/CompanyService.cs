using System.Linq.Expressions;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Classes;
using WorkiomBackendDevTest.Repositories.Interfaces;

namespace WorkiomBackendDevTest.Services
{
    public class CompanyService : BaseService<Company>               
    {
        private readonly CompanyRepository _companyRpository;

        public CompanyService(CompanyRepository companyRepository) : base(companyRepository)
        {
            _companyRpository = companyRepository;  
        }


        // Example for use generics filter in our example
        public async Task<List<Company>> FindCompaniesByLocationAsync(string location)
        {
            Expression<Func<Company, bool>> filterExpression = company =>
                company.CustomFields.ContainsKey("Location") &&
                company.CustomFields["Location"].ToString() == location;

            return await _companyRpository.FindAsync(filterExpression);
        }


        // we can use (FindAsync) function for existing field or user-extended field
        // but (Name) filed is shared between entities so we implement in (BaseEntity)
        /*
        public async Task<List<Company>> FindCompaniesByNameAsync(string nameFilter)
        {
            Expression<Func<Company, bool>> filterExpression = company => company.Name.Contains(nameFilter);
            return await _companyRpository.FindAsync(filterExpression);
        }
         */
    }
}
