using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    class Brand : CarsMapEntity
    {
        public List<Model> Models { get; set; }
    }
}
