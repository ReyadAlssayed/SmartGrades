using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGrades.Model
{
    using Supabase.Postgrest.Models;
    using Supabase.Postgrest.Attributes;

    [Table("students")]
    public class Student : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("student_code")]
        public string StudentCode { get; set; } = "";

        [Column("full_name")]
        public string FullName { get; set; } = "";

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

}
