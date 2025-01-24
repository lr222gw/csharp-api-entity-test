using workshop.wwwapi.Models;
namespace workshop.wwwapi.DTO.Response.Patient
{
    public class Get 
    {

        public Get(Models.Patient p)
        {
            Id = p.Id;
            FullName = p.FullName;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        // TODO: Add ... appointment
    }
}
