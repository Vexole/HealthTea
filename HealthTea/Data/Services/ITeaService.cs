using HealthTea.Data.Base;
using HealthTea.Data.ViewModels;
using HealthTea.Models;

namespace HealthTea.Data.Services
{
    public interface ITeaService : IEntityBaseRepository<Tea>
    {
        Task<Tea> GetTeaByIdAsync(int id);

        Task<DropDownViewModel> GetDropDownValuesAsync();

        Task AddNewTeaAsync(TeaViewModel teaViewModel);
        Task UpdateTeaAsync(TeaViewModel teaViewModel);
    }
}
