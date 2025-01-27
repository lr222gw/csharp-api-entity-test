using workshop.wwwapi.Models;

namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class GetPatients : BaseDTO<GetPatients, Models.Patient>
    {
        public List<Appointment.Get> Appointments { get; set; } = new List<Appointment.Get>();

        public override void defIncl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> queryLambda)
        {
            throw new NotImplementedException();
        }

        public override void def_id_Incl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> id_query, params object[] id)
        {
            throw new NotImplementedException();
        }

        public override void Instantiate(Models.Patient instance)
        {
            throw new NotImplementedException();
            //Appointments = d.Appointments.Select(x => new Appointment.Get(x)).ToList();
            //Appointments = toDtos
        }
    }
}
