using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace workshop.wwwapi.DTO.Response.Patient
{
    public class Get : BaseDTO<Get, Models.Patient>
    {
        public override void Instantiate(Models.Patient instance)
        {
            this.Id = instance.Id;
            this.FullName = instance.FullName;
            this.appointments = ToDTOs<Models.Appointment, Appointment.Get>(instance.Appointments).ToList();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<DTO.Response.Appointment.Get> appointments { get; set; }

        public override void defIncl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> queryLambda)
        {
            queryLambda = x => x.Include(x => x.Appointments).
                    ThenInclude(x => x.Doctor);

        }
        public override void def_id_Incl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> id_query, params object[] id)
        {
            idLambda = x => x.Where(x => x.Id == (int)id[0]);
        }

    }
}
