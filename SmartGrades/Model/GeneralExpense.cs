using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGrades.Model
{
    [Table("general_expenses")]
    public class GeneralExpense : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("semester_id")]
        public Guid SemesterId { get; set; }

        [Column("title")]
        public string Title { get; set; } = "";

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("expense_date")]
        public DateTime ExpenseDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

}
