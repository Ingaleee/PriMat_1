using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex_Method
{
    public class SimplexResult    
    {
        public SimplexSolverStatus Status { get; set; }
        public double[] OptimalSolution { get; set; }
        public double OptimalValue { get; set; }
    }
}
