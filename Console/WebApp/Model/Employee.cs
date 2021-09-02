using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Encrypted]
        public string BankCard { get; set; }
        public string BankCardDisplay { get; set; }

        [Encrypted]
        public string Monery { get; set; }
        [MinLength(100)]
        public string MoneryDisplay { get; set; }

    }
}
