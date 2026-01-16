using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartGrades.Model
{
    [Table("payments")]
    public class Payment : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("student_semester_id")]
        public Guid StudentSemesterId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; } = "";

        [Column("paid_at")]
        public DateTime PaidAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

}
