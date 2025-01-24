
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            //appointments.MapGet("/{doctor_id}/{patient_id}", GetAppointment);
            appointments.MapGet("/appointment", GetAppointment);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAppointment(HttpContext context, IRepository<Appointment> repo, int patient_id, int doctor_id)
        {
            //var a = await repo.GetEntry(new { patient_id, doctor_id });
            var a = await repo.GetEntry(patient_id, doctor_id );
            if (a == null) return TypedResults.NotFound($"Appointment with id[{patient_id}] was not found");
            return TypedResults.Ok(new DTO.Response.Appointment.Get(a));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAppointments(HttpContext context, IRepository<Appointment> repo)
        {
            var a = await repo.GetEntries();
            if (a.Count() == 0) return TypedResults.NotFound($"No appointments was not found");
            return TypedResults.Ok(a.Select(x => new DTO.Response.Appointment.Get(x)).ToList());
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
