using Job.DTOs;
using Job.Models;

namespace Job.Interfaces
{
    public interface ICity
    {
        Task<List<City>> GetAll();
        Task<CityDTO> GetById(int id);
        Task<bool> Add(CityDTO entity);
        Task<bool> Delete(int id);
        Task<bool> Update(int id);
    }
}
