﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Gof.builder;

namespace ConsoleApp.Gof.builder
{
    public class Person: IPerson
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
    }
}
