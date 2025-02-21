using IdentityManagement.Domain.Entities;
using IdentityManagement.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> CreateAsync(ApplicationUser entity, CancellationToken cancellationToken)
        {
            _context.ApplicationUsers.Add(entity);          
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetByUserNameOrEmailAsync(string userName,string password)
        {
           return await _context.ApplicationUsers.FirstOrDefaultAsync(x=>x.UserName==userName && x.Password==password);
        }

        public Task<ApplicationUser> UpdateAsync(ApplicationUser entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
