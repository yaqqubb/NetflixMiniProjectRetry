using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNetflix
{
    public abstract class BaseModel
    {
        private static int _globalId = 1;
        public int Id { get; }
        protected BaseModel()
        {
            Id = _globalId++;
        }

    }
}
