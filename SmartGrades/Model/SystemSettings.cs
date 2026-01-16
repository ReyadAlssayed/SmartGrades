using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGrades.Model
{
    [Table("system_settings")]
    public class SystemSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("default_semester_price")]
        public decimal DefaultSemesterPrice { get; set; }

        [Column("default_lecture_price")]
        public decimal DefaultLecturePrice { get; set; }

        [Column("system_name")]
        public string SystemName { get; set; } = "";

        [Column("support_phone")]
        public string? SupportPhone { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

}
