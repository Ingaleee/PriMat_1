using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex_Method
{
    public class LinearProgrammingProblem
    {
        public class Constraint
        {
            public List<double> Coefficients { get; set; }
            public double RightHandSide { get; set; }
        }

        public List<double> ObjectiveFunctionCoefficients { get; set; }
        public List<Constraint> Constraints { get; set; }

        public LinearProgrammingProblem()
        {
            ObjectiveFunctionCoefficients = new List<double>();
            Constraints = new List<Constraint>();
        }
    }

}
