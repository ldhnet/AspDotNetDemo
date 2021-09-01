using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace AesSample
{
    public class UserEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Encrypted] 
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
