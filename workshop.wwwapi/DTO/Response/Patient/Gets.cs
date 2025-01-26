using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;
namespace workshop.wwwapi.DTO.Response.Patient
{
    //public class Gets : BaseDTO<Gets, Models.Patient>
    //{
    //    public override void Instantiate(Models.Patient instance)
    //    {
    //        this.Id = instance.Id;
    //        this.FullName = instance.FullName;
    //        this.appointments = instance.Appointments.Select(x => new Appointment.Get(x)).ToList();
    //    }

    //    public int Id { get; set; }
    //    public string FullName { get; set; }
    //    public ICollection<DTO.Response.Appointment.Get> appointments { get; set; }


    //    //public override async Task<Models.Patient> DefineIncludes<W>(IRepository<Models.Patient> repo, object id)
    //        //where W : Func<>
    //    //public override async Task<Models.Patient> DefineIncludes<W>(IRepository<Models.Patient> repo, object id)
    //    public  async Task<Models.Patient> DefineIncludess(IRepository<Models.Patient> repo, object id)
    //    {
            
    //        var p = await repo.GetEntries(
    //            //x => x.Id == (int)id, 
    //            x => x.Include( x => x.Appointments).
    //                ThenInclude(x => x.Doctor));
    //        return p;
    //    }

    //    public override async Task<Models.Patient> DefineIncludes(IRepository<Models.Patient> repo, object id)
    //    {
    //        var p = await repo.GetEntries(
    //            //x => x.Id == (int)id,
    //            x => x.Include(x => x.Appointments).
    //                ThenInclude(x => x.Doctor));
    //        return p;
    //    }

    //    public override void defIncl(ref IQueryable<Models.Patient> query)
    //    {
    //        query = query.Include(x => x.Appointments).
    //                ThenInclude(x => x.Doctor);
                
    //    }




    //    // TODO: Add ... appointment
    //}
}
