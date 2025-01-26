
using Microsoft.EntityFrameworkCore;

namespace workshop.wwwapi.DTO.Response.Appointment
{
    public class Get : BaseDTO<Get,Models.Appointment>
    {
        //public Get(Models.Appointment a)
        //{
        //    Booking = a.Booking;
        //    DoctorId = a.DoctorId;
        //    PatientId = a.PatientId;
        //    DoctorName = a.Doctor.FullName;
        //    PatientName= a.Patient.FullName;
        //}
        
        public DateTime Booking { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public override void defIncl(ref Func<IQueryable<Models.Appointment>, IQueryable<Models.Appointment>> queryLambda)
        {
            queryLambda = x => x.Include(x => x.Doctor).Include(x => x.Patient);
        }

        public override void def_id_Incl(ref Func<IQueryable<Models.Appointment>, IQueryable<Models.Appointment>> id_query, params object[] id)
        {
            //id_query = x => x.Where(x => new { x.DoctorId, x.PatientId } == id);
            //id_query = x => x.Where(x => new { DoctorId = x.DoctorId, PatientId  = x.PatientId } == id);
            //id_query = x => x.Where(x => id.Equals(new { x.DoctorId, x.PatientId }));
            //if (id is int[] dd)
            //{
                
            //    id_query = x => x.Where(x => x.DoctorId == dd[0] && x.PatientId == dd[1]);
            //}
            id_query = x => x.Where(x => x.DoctorId == (int)id[0] && x.PatientId == (int)id[1]);
            
            //id_query = x => x.Where(x => x.PatientId id.);
            //id_query = x => x.Where(x => x.DoctorId == 2) ;
        }

        public override void Instantiate(Models.Appointment instance)
        {
            Booking = instance.Booking;
            DoctorId = instance.DoctorId;
            PatientId = instance.PatientId;
            DoctorName = instance.Doctor.FullName;
            PatientName = instance.Patient.FullName;
        }
    }

}
