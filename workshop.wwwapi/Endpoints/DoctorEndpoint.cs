
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //var p = await repo.GetEntry(x => x.Id == id, x => (x.Include(x => x.Appointments)) );
            var p = await repo.GetEntry(x => x.Include(x => x.Id == id), x => (x.Include(x => x.Appointments)) );
            //var p = await repo.GetEntry(x => x.Id == id, x => x.Appointments );
            //var p = await repo.GetEntries2(x => x.ThenInclude(a => a.Appointments).ThenInclude(x => x.Patient));
            //var p = await repo.GetEntries2(x => x.ThenInclude(a => a.Appointments).ThenInclude(x => x.Patient));
            if (p == null) return TypedResults.NotFound($"Doctor with id[{id}] was not found");
            return TypedResults.Ok(new DTO.Response.Doctor.Get(p)); //TODO: replace with DTO 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetDoctors(HttpContext context, IRepository<Doctor> repo)
        {
            //var p = await repo.GetEntries(x => x.Appointments);
            //var p = await repo.GetEntries2(x => x.Include(x => x.Appointments));

            //////////////////////////////
            //var d = repo.Table.AsQueryable();
            //d = d.Include(x => x.Appointments).ThenInclude(x => x.Doctor);
            //var dd = d.Include(x => x.Appointments);

            //var p = await repo.GetEntries3(d, y => y.Appointments);
            //var p = await repo.GetEntries3(y => y.Appointments);
            //var p = await repo.GetEntries3(y => y.Include(x => x.Appointments));
            var p = await repo.GetEntries(y => y.Include(x => x.Appointments).ThenInclude(x => x.Patient));
            //////////////////////////////
            
            if (p.Count() == 0) return TypedResults.NotFound($"No doctors was found");
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
