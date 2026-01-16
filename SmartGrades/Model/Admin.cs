using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace SmartGrades.Model
{
    [Table("admins")]
    public class Admin : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; } = "";

        [Column("password_hash")]
        public string PasswordHash { get; set; } = "";

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // تم تغيير اسم الخاصية هنا لتناسب واجهة المستخدم الجديدة
        [Column("phone")]
        public string PhoneNumber { get; set; } = "";
    }
}