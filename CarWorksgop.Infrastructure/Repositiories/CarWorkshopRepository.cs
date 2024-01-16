using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositiories
{
    public class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly CarWorkshopDbContext _context;

        public CarWorkshopRepository(CarWorkshopDbContext context)
        {
            _context = context;
        }

        public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            _context.Add(carWorkshop);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.CarWorkshop?> GetByName(string name)
            => _context.CarWorkshops.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()
            => await _context.CarWorkshops.ToListAsync();

        public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encodedName)
            => await _context.CarWorkshops.FirstAsync(c => c.EncodedName == encodedName);

        public Task Commit()
            => _context.SaveChangesAsync();
    }
}
