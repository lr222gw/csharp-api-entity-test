
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class AppointmentEndpoint
    {
        public static void ConfigureAppointmentEndpoint(this WebApplication app)
        {
            var appointments = app.MapGroup("/appointments");
            appointments.MapGet("/", GetAppointments);
            appointments.MapPost("/", CreateAppointment);
        }        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> GetAppointments(
            HttpContext context, 
            IRepository<Appointment> repo, 
            IRepository<Doctor> d_repo, 
            IRepository<Patient> p_repo, 
            int? patient_id, 
            int? doctor_id
            )
        {
            if (patient_id == null && doctor_id == null)      return await _GetAppointments(context, repo);
            else if (patient_id != null && doctor_id == null) return await _GetPatientAppontments(context, p_repo, patient_id.Value);
            else if (patient_id == null && doctor_id != null) return await _GetDoctorAppontments(context, d_repo, doctor_id.Value);
            else 
                return await _GetAppointment(context, repo, patient_id.Value, doctor_id.Value);
        }
        private static async Task<IResult> _GetAppointment(HttpContext context, IRepository<Appointment> repo, int patient_id, int doctor_id)
        {
            var a = await repo.GetEntry(x =>  x.Include(x => x.PatientId == patient_id && x.DoctorId == doctor_id) );
            if (a == null) return TypedResults.NotFound($"Appointment with id[{patient_id}] was not found");
            return TypedResults.Ok(new DTO.Response.Appointment.Get(a));
        }
        private static async Task<IResult> _GetAppointments(HttpContext context, IRepository<Appointment> repo)
        {
            var a = await repo.GetEntries(p => p.Include(x => x.Patient), p => p.Include(x => x.Doctor));
            if (a.Count() == 0) return TypedResults.NotFound($"No appointments was found");
            return TypedResults.Ok(a.Select(x => new DTO.Response.Appointment.Get(x)).ToList());
        }
        private static async Task<IResult> _GetDoctorAppontments(HttpContext context, IRepository<Doctor> repo, int id)
        {
            var a = await repo.GetEntry(x => x.Include(x => x.Id == id), x  => x.Include(x => x.Appointments));
            if (a == null) return TypedResults.NotFound($"Doctor with id[{id}] was not found");

            return TypedResults.Ok(new DTO.Response.Appointment.GetDoctors(a));
        }
        private static async Task<IResult> _GetPatientAppontments(HttpContext context, IRepository<Patient> repo, int id)
        {
            var p = await repo.GetEntry(x => x.Include(x => x.Id == id), x => x.Include(x => x.Appointments));
            if (p == null) return TypedResults.NotFound($"Patient with id[{id}] was not found");

            return TypedResults.Ok(new DTO.Response.Appointment.GetPatients(p));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateAppointment(HttpContext context, IRepository<Appointment> repo, DTO.Request.Appointment.Create dto)
        {
            Appointment appointments = new()
            {
                Booking = dto.Booking,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
            };

            var a = await repo.CreateEntry(appointments);
            if (a == null) return TypedResults.BadRequest($"Not a valid DTO");
            return TypedResults.Ok(new DTO.Response.Appointment.Get(a)); 
        }
    }
}
