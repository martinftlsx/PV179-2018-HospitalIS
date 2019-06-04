using HospitalIS.Infrastructure;
using HospitalISDBContext.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalISDBContext.Entities
{
    public abstract class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(HospitalISDbContext.Users);

        [Required]
        [MaxLength(64)]
        public String Username { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        public AccessRights AccessRights { get; set; }

        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        [Required]
        [MaxLength(64)]
        public String Surname { get; set; }

        protected User() {}
    }
}
