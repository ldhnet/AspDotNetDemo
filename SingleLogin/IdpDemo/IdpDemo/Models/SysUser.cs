using System.Security.Claims;

namespace IdpDemo.Models
{
    public class SysUser
    { 
        public int Id { get;  set; }
        public string SubjectId { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; } 
         
        public string ProviderName { get; set; } 
         
        public string ProviderSubjectId { get; set; }
         
        public bool IsActive { get; set; } 
        public ICollection<Claim> Claims { get; set; }
    }
}
