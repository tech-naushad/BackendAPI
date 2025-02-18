using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Persistence
{
    public class ApplicationDbContextIntializer
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextIntializer(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }

        public async Task SeedDataAsync()
        {
            //Put ant seed data here
            //if (!await _context.Nationalities.AnyAsync())
            //{
            //    var nationalities = new List<Nationality>();
            //    nationalities.Add(new Nationality { Name = "United Arab Emirates" });
            //    nationalities.Add(new Nationality { Name = "United State of America" });
            //    nationalities.Add(new Nationality { Name = "India" });
            //    nationalities.Add(new Nationality { Name = "United Kingdom" });

            //    await _context.Nationalities.AddRangeAsync(nationalities);
            //    _context.SaveChanges();
            //}
        }
    }
}
