using HealthTea.Data.Base;
using HealthTea.Models;

namespace HealthTea.Data.Services
{
    public class OriginService : EntityBaseRepository<Origin>, IOriginService
    {
        public OriginService(AppDbContext context) : base(context)
        {
        }
    }
}