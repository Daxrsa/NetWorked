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
    public class ResultsService : IResultsRepo
    {
        private readonly ProfileMatchDbContext _context;
        public ResultsService(ProfileMatchDbContext context) 
        {
            _context= context;
        }
        public async Task<bool> Add(ProfileMatchingResult entity)
        {
            var results = _context.Results.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(ProfileMatchingResult entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProfileMatchingResult>> GetAll()
        {
            return await _context.Results.ToListAsync();
        }

        public Task<IEnumerable<ProfileMatchingResult>> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ProfileMatchingResult entity)
        {
            throw new NotImplementedException();
        }
    }
}
