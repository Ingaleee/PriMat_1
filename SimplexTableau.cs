using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex_Method
{
    public class SimplexTableau
    {
        private double[,] Table { get; set; }
        private int Rows { get; set; }
        public int Columns { get; set; }

        public SimplexTableau(LinearProgrammingProblem problem)
        {
            int numConstraints = problem.Constraints.Count;
            int numVariables = problem.ObjectiveFunctionCoefficients.Count;

            Table = new double[numConstraints + 1, numVariables + numConstraints + 1];
            Rows = numConstraints + 1;
            Columns = numVariables + numConstraints + 1;

            for (int j = 0; j < numVariables; j++)
            {
                Table[0, j] = -problem.ObjectiveFunctionCoefficients[j];
            }

            for (int i = 1; i <= numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    Table[i, j] = problem.Constraints[i - 1].Coefficients[j];
                }

                Table[i, numVariables + i] = 1; 
                Table[i, Columns - 1] = problem.Constraints[i - 1].RightHandSide;
            }
        }

        public double this[int row, int col]
        {
            get { return Table[row, col]; }
            set { Table[row, col] = value; }
        }

        public bool IsOptimal()
        {
            for (int j = 0; j < Columns - 1; j++)
            {
                if (Table[0, j] < 0)
                {
                    return false; 
                }
            }
            return true;
        }

        public int SelectEnteringColumn()
        {
            double minCoefficient = double.MaxValue;
            int enteringColumn = -1;

            for (int j = 0; j < Columns - 1; j++)
            {
                if (Table[0, j] < minCoefficient)
                {
                    minCoefficient = Table[0, j];
                    enteringColumn = j;
                }
            }

            if (minCoefficient >= 0)
            {
                return -1; 
            }

            return enteringColumn;
        }

        public int SelectDepartingRow(int enteringColumn)
        {
            double minRatio = double.MaxValue;
            int departingRow = -1;

            for (int i = 1; i < Rows; i++)
            {
                if (Table[i, enteringColumn] <= 0)
                {
                    continue; 
                }

                double ratio = Table[i, Columns - 1] / Table[i, enteringColumn];
                if (ratio < minRatio)
                {
                    minRatio = ratio;
                    departingRow = i;
                }
            }

            return departingRow;
        }

        public void UpdateTableau(int enteringColumn, int departingRow)
        {
            double pivotElement = Table[departingRow, enteringColumn];

            for (int j = 0; j < Columns; j++)
            {
                Table[departingRow, j] /= pivotElement;
            }

            for (int i = 0; i < Rows; i++)
            {
                if (i == departingRow)
                {
                    continue;
                }

                double rowMultiplier = Table[i, enteringColumn];
                for (int j = 0; j < Columns; j++)
                {
                    Table[i, j] -= rowMultiplier * Table[departingRow, j];
                }
            }

        }
        public double[] ExtractOptimalSolution()
        {
            double[] optimalSolution = new double[Columns - 1];

            for (int j = 0; j < Columns - 1; j++)
            {
                int nonZeroEntries = 0;
                int nonZeroIndex = -1;

                for (int i = 1; i < Rows; i++)
                {
                    if (Table[i, j] != 0)
                    {
                        nonZeroEntries++;
                        nonZeroIndex = i;
                    }
                }

                if (nonZeroEntries == 1 && Table[nonZeroIndex, Columns - 1] >= 0)
                {
                    optimalSolution[j] = Table[nonZeroIndex, Columns - 1];
                }
                else
                {
                    optimalSolution[j] = 0;
                }
            }

            return optimalSolution;
        }
    }
}
