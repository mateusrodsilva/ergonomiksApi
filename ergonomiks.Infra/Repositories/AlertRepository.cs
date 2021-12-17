using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Infra.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly ErgonomiksContext _context;

        public AlertRepository(ErgonomiksContext context)
        {
            _context = context;
        }
        public void Create(Alerts alert)
        {
            _context.Alerts.Add(alert);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Alerts getAlert = _context.Alerts.Find(id);
            _context.Alerts.Remove(getAlert);
            _context.SaveChanges();
        }

        public List<Alerts> GetByIdEmployee(Guid idEmployee)
        {
            return _context
                .Alerts
                .AsNoTracking()
                .Where(x => x.Equipment.IdEmployee == idEmployee)
                .OrderBy(x => x.CreationDate)
                .ToList();
        }
    }
}
