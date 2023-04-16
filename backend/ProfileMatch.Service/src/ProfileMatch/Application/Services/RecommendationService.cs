using Application.Core.InterfaceRepos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RecommendationService : IRecommendationsRepo
    {
        private readonly ProfileMatchDbContext _context;
        public RecommendationService(ProfileMatchDbContext context) 
        { 
            _context= context;
        }

        public async Task<bool> Add(Reccomendation entity)
        {
            var recommendations = _context.Reccomendations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public Task<bool> Delete(Reccomendation entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reccomendation>> GetAll()
        {
            return await _context.Reccomendations.ToListAsync();
        }

        public async Task<IEnumerable<Reccomendation>> GetById()
        {
            //return await _context.Reccomendations.FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public Task<bool> Update(Reccomendation entity)
        {
            throw new NotImplementedException();
        }
    }
}
