﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNetflix
{
    public class Genre : BaseModel
    {
        public Genre()
        {
        }
        public Genre( string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
