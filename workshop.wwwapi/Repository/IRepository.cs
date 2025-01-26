using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository<T> where T : class /////////////
    {

        //////////////
        DbSet<T> Table { get; }
        //////////////

        //Task<IEnumerable<T>> GetEntries();
        //Task<IEnumerable<T>> GetEntries(params Expression<Func<T, object>>[] expressions);
        Task<IEnumerable<T>> GetEntries(params Func<IQueryable<T>, IQueryable<T>>[] includes);
        //Task<T?> GetEntry(Expression<Func<T, bool>> id, params Expression<Func<T, object>>[] expressions);
        //Task<T?> GetEntry(Expression<Func<T, bool>> id, params Func<IQueryable<T>, IQueryable<T>>[] expressions);
        Task<T?> GetEntry(Func<IQueryable<T>, IQueryable<T>> id, params Func<IQueryable<T>, IQueryable<T>>[] expressions);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id);

        Task<T?> CreateEntry(T entry);
    }
    //public interface IRepository
    //{
    //    Task<IEnumerable<Patient>> GetPatients();
    //    Task<IEnumerable<Doctor>> GetDoctors();
    //    Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id);


    //}
}
