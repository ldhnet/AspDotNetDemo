using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Model
{
    public class EmployeeLogin
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual  Employee Employee { get; set; }

    }
}
