namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class GetPatients
    {
        public GetPatients(Models.Patient d)
        {
            Appointments = d.Appointments.Select(x => new Appointment.Get(x)).ToList();
        }
        public List<Appointment.Get> Appointments { get; set; } = new List<Appointment.Get>();
    }
}
