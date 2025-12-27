using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartGrades.Model
{
    [Table("semester_subjects")]
    public class SemesterSubjects : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("semester_id")]
        public Guid SemesterId { get; set; }

        [Column("subject_id")]
        public Guid SubjectId { get; set; }
    }
}
