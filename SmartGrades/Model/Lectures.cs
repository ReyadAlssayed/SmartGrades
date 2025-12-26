using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartGrades.Model
{
    [Table("lectures")]
    public class Lectures : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("subjectid")]
        public Guid SubjectId { get; set; }

        [Column("semesterid")]
        public Guid SemesterId { get; set; }

        [Column("lecturedate")]
        public DateTime LectureDate { get; set; }

        [Column("lectureprice")]
        public decimal LecturePrice { get; set; }
    }
}
