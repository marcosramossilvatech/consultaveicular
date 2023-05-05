using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ConsultaSerpro.Models
{
    [Table("auth_user")]
    public class Usuario
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("is_superuser")]
        public bool IsSuperuser { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("is_staff")]
        public bool IsStaff { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("date_joined")]
        public DateTime? DateJoined { get; set; }

        [NotMapped]
        public int Empresa { get; set; }
    }
}
