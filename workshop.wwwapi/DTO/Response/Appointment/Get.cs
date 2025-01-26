namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class Get
    {
        public Get(Models.Appointment a)
        {
            Booking = a.Booking;
            DoctorId = a.DoctorId;
            PatientId = a.PatientId;
            DoctorName = a.Doctor.FullName;
            PatientName= a.Patient.FullName;
        }
        
        public DateTime Booking { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
    }

}
