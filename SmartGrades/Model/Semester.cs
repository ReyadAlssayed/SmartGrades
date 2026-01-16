using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartGrades.Model
{
    [Table("semesters")]
    public class Semester : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }


}
