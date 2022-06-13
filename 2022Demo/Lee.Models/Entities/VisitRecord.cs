using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Models.Entities
{
    public class VisitRecord
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string RequestPath { get; set; }
        public string? RequestQueryString { get; set; }
        public string RequestMethod { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
