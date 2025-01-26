using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class GetDoctors : BaseDTO<GetDoctors, Models.Doctor>
    {
        //public GetDoctors(Models.Doctor d)
        //{
        //    Appointments = d.Appointments.Select(x => new Appointment.Get(x)).ToList();
        //}
        public List<Appointment.Get> Appointments { get; set; } = new List<Appointment.Get>();

        public override void defIncl(ref Func<IQueryable<Models.Doctor>, IQueryable<Models.Doctor>> queryLambda)
        {
            queryLambda = x => x.Include(x => x.Appointments).ThenInclude(x => x.Patient);
        }

        public override void def_id_Incl(ref Func<IQueryable<Models.Doctor>, IQueryable<Models.Doctor>> id_query,params object[] id)
        {
            id_query = x => x.Where(x => x.Id == (int)id[0]);
        }

        public override void Instantiate(Models.Doctor instance)
        {
            //Appointments = instance.Appointments.Select(x => new Appointment.Get(x)).ToList();
            //Appointments = instance.Appointments.Select(x =>  ToDTO<Models.Appointment,Appointment.Get>(x) ).ToList();
            Appointments = ToDTOs<Models.Appointment, Appointment.Get>(instance.Appointments).ToList();
        }
    }
}
