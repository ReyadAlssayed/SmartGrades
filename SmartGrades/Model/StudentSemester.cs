using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartGrades.Model
{
    [Table("student_semesters")]
    public class StudentSemester : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("student_id")]
        public Guid StudentId { get; set; }

        [Column("semester_id")]
        public Guid SemesterId { get; set; }

        [Column("field")]
        public string Field { get; set; } = "";

        [Column("specialization")]
        public string Specialization { get; set; } = "";

        [Column("academic_level")]
        public string AcademicLevel { get; set; } = "";

        [Column("is_result_visible")]
        public bool IsResultVisible { get; set; }

        [Column("total_subjects")]
        public int? TotalSubjects { get; set; }

        [Column("total_score")]
        public decimal? TotalScore { get; set; }

        [Column("percentage")]
        public decimal? Percentage { get; set; }

        [Column("final_result")]
        public string? FinalResult { get; set; }

        [Column("semester_price")]
        public decimal SemesterPrice { get; set; }

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [Column("paid_amount")]
        public decimal PaidAmount { get; set; }

        [Column("remaining_amount")]
        public decimal RemainingAmount { get; set; }

        [Column("is_financially_exempt")]
        public bool IsFinanciallyExempt { get; set; }

        [Column("is_result_forced_visible")]
        public bool IsResultForcedVisible { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

}
