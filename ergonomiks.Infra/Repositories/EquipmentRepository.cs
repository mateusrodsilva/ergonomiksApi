using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ergonomiks.Infra.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ErgonomiksContext _context;

        public EquipmentRepository(ErgonomiksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a Equipment
        /// </summary>
        /// <param name="equipment">Equipment object with equipment data</param>
        public void Create(Equipment equipment)
        {
            _context.Equipments.Add(equipment);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a equipment
        /// </summary>
        /// <param name="id">Equipment's id that will be deleted</param>
        public void Delete(Guid id)
        {
            Equipment getEquipment = _context.Equipments.Find(id);
            _context.Equipments.Remove(getEquipment);
            _context.SaveChanges();
        }

        /// <summary>
        /// Equipments list
        /// </summary>
        /// <returns>Equipments list</returns>
        public List<Equipment> GetAll()
        {
            return _context
                .Equipments
                .AsNoTracking()
                .Select(x =>
                    new Equipment
                    {
                        Id = x.Id,
                        Temperature = x.Temperature,
                        LightLevel = x.LightLevel,
                        //Noise = x.Noise,
                        Moisture = x.Moisture,
                        IdEmployee = x.IdEmployee
                    })
                .OrderBy(x => x.CreationDate)
                .ToList();
        }

        /// <summary>
        /// Search a equipment by id
        /// </summary>
        /// <param name="id">Equipment's id</param>
        /// <returns>Equipment Object</returns>
        public Equipment GetById(Guid id)
        {
            return _context
                .Equipments
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Search a equipment by idEmployee
        /// </summary>
        /// <param name="id">Employee's id</param>
        /// <returns>Equipment object</returns>
        public Equipment GetByIdEmployee(Guid id)
        {
            return _context
                .Equipments
                .FirstOrDefault(x => x.IdEmployee == id);
        }
    }
}
