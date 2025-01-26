using workshop.wwwapi.Models;

namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class GetDoctors
    {
        public GetDoctors(Models.Doctor d)
        {
            Appointments = d.Appointments.Select(x => new Appointment.Get(x)).ToList();
        }
        public List<Appointment.Get> Appointments { get; set; } = new List<Appointment.Get>();
    }
}
