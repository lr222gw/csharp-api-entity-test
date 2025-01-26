namespace workshop.wwwapi.DTO.Response.Doctor
{
    public class Get
    {
        public Get(Models.Doctor d)
        {
            Id = d.Id;
            FullName = d.FullName;
            this.appointments = d.Appointments.Select(x => new Appointment.Get(x)).ToList();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<DTO.Response.Appointment.Get> appointments { get; set; }
    }
}
