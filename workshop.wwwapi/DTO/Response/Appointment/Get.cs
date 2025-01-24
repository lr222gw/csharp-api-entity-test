namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class Get
    {
        public Get(Models.Appointment a)
        {
            Booking = a.Booking;
            DoctorId = a.DoctorId;
            PatientId = a.PatientId;
        }
        
        public DateTime Booking { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }

}
