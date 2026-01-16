using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGrades.Model
{
    [Table("teacher_payments")]
    public class TeacherPayment : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("teacher_id")]
        public Guid TeacherId { get; set; }

        [Column("semester_id")]
        public Guid SemesterId { get; set; }

        [Column("lecture_count")]
        public int LectureCount { get; set; }

        [Column("lecture_price")]
        public decimal LecturePrice { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("paid_at")]
        public DateTime PaidAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

}
