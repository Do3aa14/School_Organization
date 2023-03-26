using Microsoft.EntityFrameworkCore;
using SchoolOrganization.DataLayer.Contract.GenericRepository;
using SchoolOrganization.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Persistence.GenericRepository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext DBContext)
        {
            _dbContext = DBContext;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(DbContext _context)
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(DbContext _context, int id)
        {
            return _context.Find<T>(id);
        }
        public bool Insert(DbContext _context, T _obj)
        {
            _context.Add<T>(_obj);
            return SaveChanges(_context);
        }
        public bool Update(DbContext _context, T _obj)
        {
            _context.Update<T>(_obj);
            return SaveChanges(_context);
        }

        public bool Delete(DbContext _context , T _obj)
        {
            _context.Remove<T>(_obj);
            return SaveChanges(_context);
        }

        public bool SaveChanges(DbContext _context)
        {
            int result = _context.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

       
    }
}
