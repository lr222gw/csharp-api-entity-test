using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetEntries();
        //Task<T?> GetEntry(object id);
        Task<T?> GetEntry(params object?[]? id);
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
