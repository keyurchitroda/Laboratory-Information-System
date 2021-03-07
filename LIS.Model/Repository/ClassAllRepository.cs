using LIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Model.Repository
{
    public class ClassAllRepository<T> : _IAllRepository<T> where T : class
    {
        //connection string...... 
        private LissystemEntities _context;
        private DbSet<T> dbEntity = null;

        public ClassAllRepository()
        {
            _context = new LissystemEntities();
            dbEntity = _context.Set<T>();
        }
        public void Delete(int id)
        {
            T model = dbEntity.Find(id);
            dbEntity.Remove(model);
        }

        public IEnumerable<T> GetAll()
        {
            return dbEntity.ToList();
        }

        public T GetById(int id)
        {
            return dbEntity.Find(id);
        }

        public void Insert(T obj)
        {
            dbEntity.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
