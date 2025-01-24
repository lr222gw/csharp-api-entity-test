﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using workshop.wwwapi.Models.Interface;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly    
    [Table("doctors")]
    public class Doctor : ISeedable
    {
        [Key]
        public int Id { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("appointments")]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        
    }
}
