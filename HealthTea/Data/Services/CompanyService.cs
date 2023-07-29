using HealthTea.Data.Base;
using HealthTea.Models;

namespace HealthTea.Data.Services
{
    public class CompanyService : EntityBaseRepository<Company>, ICompanyService
    {
        public CompanyService(AppDbContext context) : base(context)
        {
        }
    }
}
