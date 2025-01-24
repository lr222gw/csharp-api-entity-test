using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private DatabaseContext _databaseContext;
        private DbSet<T> _table = null!;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
            _table = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetEntries()
        {
            return await _table.ToListAsync();
        }
        
        public async Task<T?> GetEntry(params object?[]? id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<T?> CreateEntry(T entry)
        {

            await _table.AddAsync(entry);
            await _databaseContext.SaveChangesAsync();
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
