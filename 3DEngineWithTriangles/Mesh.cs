using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DEngineWithTriangles
{
    public class Mesh
    {
        public List<Triangle> Triangles;
        public Mesh()
        {
            Triangles = new List<Triangle>();
        }
    }
}
