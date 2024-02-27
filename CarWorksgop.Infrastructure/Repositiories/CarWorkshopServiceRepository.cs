using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositiories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        private readonly CarWorkshopDbContext _context;

        public CarWorkshopServiceRepository(CarWorkshopDbContext context)
        {
            _context = context;
        }

        public async Task Create(CarWorkshopService service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName)
         => await _context.Services.
                Where(s => s.CarWorkshop.EncodedName == encodedName).
                ToListAsync();
        
    }
}
