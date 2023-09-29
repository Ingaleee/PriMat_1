using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex_Method
{
    public class SimplexSolver
    {
        public SimplexResult Solve(LinearProgrammingProblem problem)
        {
            SimplexTableau tableau = new SimplexTableau(problem);

            while (!tableau.IsOptimal())
            {
                int enteringColumn = tableau.SelectEnteringColumn();

                if (enteringColumn == -1)
                {
                    return new SimplexResult { Status = SimplexSolverStatus.Infeasible };
                }

                int departingRow = tableau.SelectDepartingRow(enteringColumn);

                if (departingRow == -1)
                {
                    return new SimplexResult { Status = SimplexSolverStatus.Unbounded };
                }

                tableau.UpdateTableau(enteringColumn, departingRow);
            }

            double[] optimalSolution = tableau.ExtractOptimalSolution();
            double optimalValue = tableau[0, tableau.Columns - 1];

            return new SimplexResult
            {
                Status = SimplexSolverStatus.Optimal,
                OptimalSolution = optimalSolution,
                OptimalValue = optimalValue
            };
        }
    }
}

