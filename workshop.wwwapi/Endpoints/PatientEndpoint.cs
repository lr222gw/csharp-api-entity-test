
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class PatientEndpoint
    {
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var patients = app.MapGroup("/patients");
            patients.MapGet("/", GetPatients);
            patients.MapPost("/", CreatePatient);
            patients.MapGet("/{id}", GetPatient);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPatient(HttpContext context, IRepository<Patient> repo, int id)
        {
            var p = await repo.GetEntry(id);
            if (p == null) return TypedResults.NotFound($"Patient with id[{id}] was not found");
            return TypedResults.Ok(new DTO.Response.Patient.Get(p)); //TODO: replace with DTO 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPatients(HttpContext context, IRepository<Patient> repo)
        {
            var p = await repo.GetEntries();
            if (p.Count() == 0) return TypedResults.NotFound($"No patients was not found");
            return TypedResults.Ok(p.Select(x => new DTO.Response.Patient.Get(x)).ToList());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreatePatient(HttpContext context, IRepository<Patient> repo, DTO.Request.Patient.Create dto)
        {
            Patient patient = new()
            {
                FullName = dto.FullName
            };

            var p = await repo.CreateEntry(patient);
            if (p == null) return TypedResults.BadRequest($"Not a valid DTO");
            return TypedResults.Ok(new DTO.Response.Patient.Get(p)); 
        }
    }
}
