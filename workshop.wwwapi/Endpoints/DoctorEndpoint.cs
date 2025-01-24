
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class DoctorEndpoint
    {
        public static void ConfigureDoctorEndpoint(this WebApplication app)
        {
            var doctors = app.MapGroup("/doctors");
            doctors.MapGet("/", GetDoctors);
            doctors.MapPost("/", CreateDoctor);
            doctors.MapGet("/{id}", GetDoctor);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetDoctor(HttpContext context, IRepository<Doctor> repo, int id)
        {
            var p = await repo.GetEntry(id);
            if (p == null) return TypedResults.NotFound($"Doctor with id[{id}] was not found");
            return TypedResults.Ok(new DTO.Response.Doctor.Get(p)); //TODO: replace with DTO 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetDoctors(HttpContext context, IRepository<Doctor> repo)
        {
            var p = await repo.GetEntries();
            if (p.Count() == 0) return TypedResults.NotFound($"No doctors was not found");
            return TypedResults.Ok(p.Select(x => new DTO.Response.Doctor.Get(x)).ToList());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateDoctor(HttpContext context, IRepository<Doctor> repo, DTO.Request.Doctor.Create dto)
        {
            Doctor doctors = new()
            {
                FullName = dto.FullName
            };

            var p = await repo.CreateEntry(doctors);
            if (p == null) return TypedResults.BadRequest($"Not a valid DTO");
            return TypedResults.Ok(new DTO.Response.Doctor.Get(p)); 
        }
    }
}
