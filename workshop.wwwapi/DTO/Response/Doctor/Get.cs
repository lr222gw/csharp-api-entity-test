namespace workshop.wwwapi.DTO.Response.Doctor
{
    public class Get
    {
        public Get(Models.Doctor d)
        {
            Id = d.Id;
            FullName = d.FullName;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
