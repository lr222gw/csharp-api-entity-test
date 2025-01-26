
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;
using workshop.wwwapi.Models.Interface;
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
            //var p = await repo.GetEntry(x => x.Id == id, x => x.Appointments);

            var p = await DTO.Response.Patient.Get.DTO(repo, id); 
            
            //var dd = new DTO.Response.Patient.Get();

            //var p = new DTO.Response.Patient.Get(repo, id);

            if (p == null) return TypedResults.NotFound($"Patient with id[{id}] was not found");

            //return TypedResults.Ok(new DTO.Response.Patient.Get(p)); //TODO: replace with DTO 
            return TypedResults.Ok(p); //TODO: replace with DTO 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPatients(HttpContext context, IRepository<Patient> repo)
        {
            //TODO: Let DTOs define what properties should be fetched with `Include`... 
            //var p = await repo.GetEntries(x => x.Include(x => x.Appointments).ThenInclude(x => x.Doctor));
            var p = await DTO.Response.Patient.Get.DTO(repo);

            if (p.Count() == 0) return TypedResults.NotFound($"No patients was found");
            //return TypedResults.Ok(p.Select(x => new DTO.Response.Patient.Get(x)).ToList());
            return TypedResults.Ok(p);
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
            //return TypedResults.Ok(new DTO.Response.Patient.Get(p)); 

            return TypedResults.Ok(await DTO.Response.Patient.Get.DTO(repo,p.Id)); 
            //return TypedResults.Ok(new DTO.Response.Patient.Get(p)); 
        }
    }
}
