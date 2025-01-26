using System.Linq.Expressions;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private DatabaseContext _databaseContext;
        /////////
        private DbSet<T> _table = null!;
        public DbSet<T> Table { get => _table; }
        /////////
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
            _table = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetEntries()
        {
            return await _table.ToListAsync();
        }
        
        //public async Task<IEnumerable<T>> GetEntries(params Expression<Func<T, object>>[] expressions)
        //{
        //    IQueryable<T> q = _table.AsQueryable();
            
        //    foreach (var ex in expressions)
        //        q = q.Include(ex);
        //    return await q.ToListAsync();
        //}


        public async Task<IEnumerable<T>> GetEntries(params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> q = _table.AsQueryable();
            
            foreach (var inc in includes)
                q = inc.Invoke(q);

            return await q.ToArrayAsync();
        }





        public async Task<IEnumerable<T>> GetEntries2(params Func<IIncludableQueryable<T, T>,object>[] expressions)
        {
            IIncludableQueryable<T, object> ab;            


            IQueryable<T> q = _table.AsQueryable();
            
            foreach (var ex in expressions)
            {

                //ex.inc
                //ex.Invoke(q.Include(x => x));
            }

            return await q.ToListAsync();
        }
        
        //public async Task<T?> GetEntry(params object?[]? id)
        public async Task<T?> GetEntry(object?[]? id)
        {
            return await _table.FindAsync(id);
        }

        //public async Task<T?> GetEntry(object? id, params Expression<Func<T, object>>[] expressions)
        //public async Task<T?> GetEntry(Expression<Func<T, bool>> id, params Expression<Func<T, object>>[] expressions)
        //{
        //    IQueryable<T> q  =_table.AsQueryable();
        //    //q.
        //    q = q.Where(id);
        //    foreach (var ex in expressions) {
        //        q = q.Include(ex);
        //    }

        //    return await q.FirstOrDefaultAsync();
        //        //.FindAsync(id);
        //}
        //public async Task<T?> GetEntry(Expression<Func<T, bool>> id, params Func<IQueryable<T>, IQueryable<T>>[] expressions)
        public async Task<T?> GetEntry(Func<IQueryable<T>, IQueryable<T>> id, params Func<IQueryable<T>, IQueryable<T>>[] expressions)
        {
            IQueryable<T> q = _table.AsQueryable();
            //q.
            //q = q.Where(id);
            q = id.Invoke(q);
            foreach (var ex in expressions)
            {
                q = ex.Invoke(q);
                //q = q.Include(ex);
            }

            return await q.FirstOrDefaultAsync();
            //.FindAsync(id);
        }
        public async Task<T?> CreateEntry(T entry)
        {
            var a = await _table.AddAsync(entry);
            await _databaseContext.SaveChangesAsync();
            //return await _table.FindAsync;
            return entry;

        }


        //public async Task<IEnumerable<Patient>> GetPatients()
        //{
        //    return await _databaseContext.Patients.ToListAsync();
        //}
        //public async Task<IEnumerable<Doctor>> GetDoctors()
        //{
        //    return await _databaseContext.Doctors.ToListAsync();
        //}
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId==id).ToListAsync();
        }

       
    }
}
