using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace workshop.wwwapi.DTO.Response.Patient
{
    public class Get : BaseDTO<Get, Models.Patient>
    {

        //public Get(Models.Patient a) { } // TODO REMOVE

        //public Get() { }

        public override void Instantiate(Models.Patient instance)
        {
            this.Id = instance.Id;
            this.FullName = instance.FullName;
            this.appointments = instance.Appointments.Select(x => new Appointment.Get(x)).ToList();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<DTO.Response.Appointment.Get> appointments { get; set; }


        //public override async Task<Models.Patient> DefineIncludes(IRepository<Models.Patient> repo, object id)
        //{
        //    var p = await repo.GetEntry
        //        (x => x.Id == (int)id, 
        //        x => x.Include( x => x.Appointments).
        //            ThenInclude(x => x.Doctor));
        //    return p;
        //}

        //public override void defIncl(ref IQueryable<Models.Patient> query, object? id = null)
        public override void defIncl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> queryLambda)
        {
            //query = query.Where(x => x.Id == (int)id).
            

            queryLambda = x => x.Include(x => x.Appointments).
                    ThenInclude(x => x.Doctor);

            //query = query.Include( x => x.Appointments).
            //        ThenInclude(x => x.Doctor);
        }
        //public override void def_id_Incl(ref Func<Models.Patient, bool> id_query, object id)
        public override void def_id_Incl(ref Func<IQueryable<Models.Patient>, IQueryable<Models.Patient>> id_query, object id)
        {
            idLambda = x => x.Where(x => x.Id == (int)id);
            //idLambda = x => x.Id == (int)id;
        }




        // TODO: Add ... appointment
    }
}
