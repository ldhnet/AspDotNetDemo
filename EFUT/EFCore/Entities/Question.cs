using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
    }
}
