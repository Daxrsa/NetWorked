using Job.Configuration;
using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.EntityFrameworkCore;

namespace Job.Services
{
    public class CityService : ICity
    {
        private readonly JobDbContext _context;
        public CityService(JobDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(CityDTO entity)
        {
            City city = new()
            {
                Id= entity.Id,
                Name = entity.Name
            };
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var city = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
            _context.Remove(city);
            await _context.SaveChangesAsync();
            return true;
        }

        //Duhet me ndryshu
        public async Task<bool> Update(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            _context.Update(city);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<City>> GetAll()
        {
            List<City> cities = await _context.Cities.ToListAsync();
            return cities;
        }

        public async Task<CityDTO> GetById(int id)
        {
            CityDTO c = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
            CityDTO cityDTO = new()
            {
                Id = id,
                Name = c.Name
            };
            return cityDTO;
        }
    }
}
